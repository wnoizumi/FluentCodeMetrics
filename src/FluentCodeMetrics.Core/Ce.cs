using System;
using System.Linq;

using FluentCodeMetrics.Core.TypeConstraints;
using FluentCodeMetrics.Core.TypeSets;
using System.Collections.Generic;

namespace FluentCodeMetrics.Core
{
    public abstract class Ce : CodeMetric
    {
        public override int Value
        {
            get { return References.Count(); }
        }

        public abstract ReferencedTypesTypeSet References { get; }

        public static implicit operator int(Ce source)
        {
            return source.Value;
        }

        public abstract Ce FilterBy(TypeConstraint filter);

        public Ce Ignoring(TypeConstraint toIgnore)
        {
            return FilterBy(toIgnore.Not());
        }

        public Ce Ignoring<T>()
        {
            return Ignoring(typeof(T));
        }

        public static TypeSetCe For(TypeSet typeSet)
        {
            var source = from type in typeSet
                         select For(type);

            return new TypeSetCe(source);
        }

        public static TypeCe For(Type type)
        {
            var references = ReferencesInspector.For(type)
                .Where(type.NestedTypes().Not());
            return new TypeCe(references, type);
        }

        public static TypeCe For<T>()
        {
            return For(typeof (T));
        }
    }

   
    public class TypeCe : Ce
    {
        private readonly ReferencedTypesTypeSet references;
        private readonly Type type;
        public Type Type
        {
            get { return type;  }
        }

        internal TypeCe(ReferencedTypesTypeSet refs, Type type)
        {   
            references = refs;
            this.type = type;
        }

        public override ReferencedTypesTypeSet References
        {
            get { return references; }
        }

        public override Ce FilterBy(TypeConstraint filter)
        {
            return filter == null ?
                this :
                new TypeCe(references.FilterBy(filter), Type);
        }
    }

    public class TypeSetCe : Ce
    {
        private readonly IEnumerable<Ce> source;
        private readonly ReferencedTypesTypeSet references;

        public IEnumerable<Ce> Source
        {
            get { return source; }
        }

        public override ReferencedTypesTypeSet References
        {
            get { return this.references; }
        }

        internal TypeSetCe(IEnumerable<Ce> source)
        {
            this.source = source;

            var allReferences = from member in source
                                from reference in member.References
                                select reference;
            this.references = new ReferencedTypesTypeSet(allReferences.Distinct(), null);
        }

        public override Ce FilterBy(TypeConstraint filter)
        {
            var newSource = source.Select(ceResult => ceResult.FilterBy(filter)).ToList();
            return new TypeSetCe(newSource);
        }
    }
}
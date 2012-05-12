using System;
using System.Linq;

using FluentCodeMetrics.Core.TypeFilters;

namespace FluentCodeMetrics.Core
{
    public class Ce
    {
        public readonly ReferencedTypes references;
        public ReferencedTypes References
        { get { return references; } }

        public int Value
        { get { return References.Count(); } }

        private Ce(Type type)
        {
            references = ReferencesInspector.For(type)
                .Where(type.NestedTypes().Not());
        }

        private Ce(ReferencedTypes refs)
        {
            references = refs;
        }

        public Ce Ignoring(TypeFilter toIgnore)
        {
            return FilterBy(toIgnore.Not());
        }

        public Ce Ignoring<T>()
        {
            return Ignoring(typeof (T));
        }

        public Ce FilterBy(TypeFilter filter)
        {
            return filter == null ? 
                this : 
                new Ce(references.FilterBy(filter));
        }

        public static implicit operator int(Ce source)
        {
            return source.Value;
        }

        public static Ce For(Type type)
        {
            return new Ce(type);
        }

        public static Ce For<T>()
        {
            return For(typeof (T));
        }
    }
}
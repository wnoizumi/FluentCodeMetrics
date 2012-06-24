using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FluentCodeMetrics.Core.Cecil;
using FluentCodeMetrics.Core.TypeConstraints;
using ThrowHelper;

namespace FluentCodeMetrics.Core
{
    public class Ca : CodeMetric
    {
        private readonly Type target;
        private readonly IEnumerable<Type> references;

        public override int Value
        {
            get { return this.references.Count(); }
        }

        public IEnumerable<Type> References
        {
            get { return this.references; }
        }

        public Ca(Type target, IEnumerable<Type> references)
        {
            this.target = target;
            this.references = references;
        }

        public Ca Including(Assembly externalAssembly)
        {
            Throw.IfArgumentNull(externalAssembly, "externalAssembly");

            if (externalAssembly.Equals(target.Assembly)) return this;

            var allReferences = GetTypesThatReferencesFromAssembly(target, externalAssembly);
            allReferences = allReferences.Concat(references);

            return new Ca(target, allReferences);
        }

        public static Ca For<T>()
        {
            return For(typeof(T));
        }

        public static Ca For(Type type)
        {
            Throw.IfArgumentNull(type, "type");

            var types = GetTypesThatReferences(type);
            return new Ca(type, types);
        }

        private static IEnumerable<Type> GetTypesThatReferences(Type target)
        {
            return GetTypesThatReferencesFromAssembly(target, target.Assembly);
        }

        private static IEnumerable<Type> GetTypesThatReferencesFromAssembly(Type target, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (ReferencesInspector.For(type).Contains(target))
                    yield return type;
            }
        }
    }
}

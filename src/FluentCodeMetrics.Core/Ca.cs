using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using FluentCodeMetrics.Core.Cecil;
using FluentCodeMetrics.Core.TypeConstraints;

namespace FluentCodeMetrics.Core
{
    public class Ca : CodeMetric
    {
        private readonly IEnumerable<Type> references;

        public override int Value
        {
            get { return this.references.Count(); }
        }

        public Ca(IEnumerable<Type> references)
        {
            this.references = references;
        }

        public static Ca For(Type type)
        {
            var types = GetTypesThatReferences(type);
            return new Ca(types);
        }

        private static IEnumerable<Type> GetTypesThatReferences(Type target)
        { 
            //TODO: Need to inspect more assemblies
            var allTypes = target.Assembly.GetTypes();

            foreach (Type type in allTypes)
            {
                if (ReferencesInspector.For(type).Contains(target))
                    yield return type;
            }
        }
    }
}

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

        private static IEnumerable<Type> GetTypesThatReferences(Type type)
        { 
            var allTypes = type.Assembly.GetTypes();

            foreach (Type t in allTypes)
            {
                if (ReferencesInspector.For(t).All().Contains(type))
                    yield return t;
            }
        }
    }
}

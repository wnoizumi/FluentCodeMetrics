using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class CollectionTypeConstraint : TypeConstraint
    {
        private readonly IEnumerable<Type> typesField;

        internal CollectionTypeConstraint(IEnumerable<Type> types)
        {
            typesField = types;
        }

        public override bool Check(Type type)
        {
            return typesField.Contains(type);
        }
    }
}

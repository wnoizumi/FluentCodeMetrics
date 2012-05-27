using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrowHelper;

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
            Throw.IfArgumentNull(type, "type");
            return typesField.Contains(type);
        }
    }
}

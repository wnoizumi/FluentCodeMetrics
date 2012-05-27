using System;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class NotTypeConstraint : TypeConstraint
    {
        private readonly TypeConstraint filterField;
        internal NotTypeConstraint(TypeConstraint filter)
        {
            filterField = filter;
        }

        public override bool Check(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return !filterField.Check(type);
        }
    }
}
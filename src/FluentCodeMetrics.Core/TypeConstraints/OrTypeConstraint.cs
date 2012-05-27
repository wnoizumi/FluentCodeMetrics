using System;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class OrTypeConstraint : TypeConstraint
    {
        private readonly TypeConstraint leftField;
        private readonly TypeConstraint rightField;

        internal OrTypeConstraint(TypeConstraint left, TypeConstraint right)
        {
            leftField = left;
            rightField = right;
        }

        public override bool Check(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return leftField.Check(type) || rightField.Check(type);
        }
    }
}
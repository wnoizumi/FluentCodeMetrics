using System;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class AndTypeConstraint : TypeConstraint
    {
        private readonly TypeConstraint leftField;
        private readonly TypeConstraint rightField;

        internal AndTypeConstraint(TypeConstraint left, TypeConstraint right)
        {
            Throw.IfArgumentNull(left, "left");
            Throw.IfArgumentNull(right, "right");

            leftField = left;
            rightField = right;
        }

        public override bool Check(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return leftField.Check(type) && rightField.Check(type);
        }
    }
}
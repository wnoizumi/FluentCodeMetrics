using System;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public sealed class EqualsToTypeConstraint : TypeConstraint
    {
        private readonly Type typeField;

        internal EqualsToTypeConstraint(Type type)
        {
            typeField = type;
        }

        public override bool Check(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return typeField == type;
        }
    }
}
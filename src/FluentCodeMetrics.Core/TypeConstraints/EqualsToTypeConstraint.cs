using System;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class EqualsToTypeConstraint : TypeConstraint
    {
        private readonly Type typeField;

        internal EqualsToTypeConstraint(Type type)
        {
            typeField = type;
        }

        public override bool Check(Type type)
        {
            return typeField == type;
        }
    }
}
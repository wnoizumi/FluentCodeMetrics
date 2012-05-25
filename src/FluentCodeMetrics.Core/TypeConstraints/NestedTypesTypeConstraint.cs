using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public class NestedTypesTypeConstraint : TypeConstraint
    {
        private readonly Type declaringType;

        internal NestedTypesTypeConstraint(Type declaringType)
        {
            this.declaringType = declaringType;
        }

        public override bool Check(Type type)
        {
           return type.DeclaringType == declaringType;
        }
    }
}

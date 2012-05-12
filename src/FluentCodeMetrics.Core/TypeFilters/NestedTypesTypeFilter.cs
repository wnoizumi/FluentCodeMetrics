using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public class NestedTypesTypeFilter : TypeFilter
    {
        private readonly Type declaringType;

        internal NestedTypesTypeFilter(Type declaringType)
        {
            this.declaringType = declaringType;
        }

        public override bool Check(Type type)
        {
           return type.DeclaringType == declaringType;
        }
    }
}

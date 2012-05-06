using System;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public class EqualsToTypeFilter : TypeFilter
    {
        private readonly Type typeField;

        internal EqualsToTypeFilter(Type type)
        {
            typeField = type;
        }

        public override bool Check(Type type)
        {
            return typeField == type;
        }
    }
}
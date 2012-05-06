using System;
namespace FluentCodeMetrics.Core.TypeFilters
{
    public class NotTypeFilter : TypeFilter
    {
        private readonly TypeFilter filterField;
        internal NotTypeFilter(TypeFilter filter)
        {
            filterField = filter;
        }

        public override bool Check(Type type)
        {
            return !filterField.Check(type);
        }
    }
}
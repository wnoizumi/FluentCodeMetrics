using System;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public class AndTypeFilter : TypeFilter
    {
        private readonly TypeFilter leftField;
        private readonly TypeFilter rightField;

        internal AndTypeFilter(TypeFilter left, TypeFilter right)
        {
            leftField = left;
            rightField = right;
        }

        public override bool Check(Type type)
        {
            return leftField.Check(type) && rightField.Check(type);
        }
    }
}
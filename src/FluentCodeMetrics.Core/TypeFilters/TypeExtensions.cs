using System;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public static class TypeExtensions
    {
        public static TypeFilter Or(this Type left, TypeFilter right)
        {
            return TypeFilter.EqualsTo(left)
                .Or(right);
        }

        public static TypeFilter Not(this Type that)
        {
            return TypeFilter.Not(that);
        }
    }
}
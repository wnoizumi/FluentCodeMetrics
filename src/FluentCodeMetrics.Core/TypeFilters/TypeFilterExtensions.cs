using System;
using System.Reflection;
using TypeFilter = FluentCodeMetrics.Core.TypeFilters.TypeFilter;

namespace FluentCodeMetrics.Core
{
    public static class TypeFilterExtensions
    {
        public static TypeFilter Or(this Type left, TypeFilter right)
        {
            return TypeFilter.EqualsTo(left)
                .Or(right);
        }

        public static TypeFilter And(this Type left, TypeFilter right)
        {
            return TypeFilter.EqualsTo(left)
                .And(right);
        }

        public static TypeFilter Not(this Type that)
        {
            return TypeFilter.Not(that);
        }

        public static TypeFilter Not(this Assembly that)
        {
            return TypeFilter.Not(that);
        }

        public static TypeFilter NestedTypes(this Type that)
        {
            return TypeFilter.NestedTypes(that);
        }
    }
}
using System;
using System.Reflection;
using FluentCodeMetrics.Core.TypeConstraints;

namespace FluentCodeMetrics.Core
{
    public static class TypeConstraintExtensions
    {
        public static TypeConstraint Or(this Type left, TypeConstraint right)
        {
            return TypeConstraint.EqualsTo(left)
                .Or(right);
        }

        public static TypeConstraint And(this Type left, TypeConstraint right)
        {
            return TypeConstraint.EqualsTo(left)
                .And(right);
        }

        public static TypeConstraint Not(this Type that)
        {
            return TypeConstraint.Not(that);
        }

        public static TypeConstraint Not(this Assembly that)
        {
            return TypeConstraint.Not(that);
        }

        public static TypeConstraint NestedTypes(this Type that)
        {
            return TypeConstraint.NestedTypes(that);
        }
    }
}
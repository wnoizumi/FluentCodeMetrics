using System;
using System.Reflection;
using FluentCodeMetrics.Core.TypeSets;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public abstract class TypeConstraint
    {
        public abstract bool Check(Type type);

        public TypeConstraint Or(TypeConstraint filter)
        {
            return Or(this, filter);
        }

        public TypeConstraint And(TypeConstraint filter)
        {
            return And(this, filter);
        }

        public TypeConstraint Not()
        {
            return Not(this);
        }

        public static implicit operator TypeConstraint(Type type)
        {
            return EqualsTo(type);
        }

        public static implicit operator TypeConstraint(Assembly assembly)
        {
            return FromAssembly(assembly);
        }

        public static implicit operator TypeConstraint(TypeSet set)
        {
            return new CollectionTypeConstraint(set);
        }

        public static TypeConstraint FromAssembly(Assembly assembly)
        {
            return new FromAssemblyTypeConstraint(assembly);
        }

        public static TypeConstraint Not(TypeConstraint filter)
        {
            return new NotTypeConstraint(filter);
        }

        public static TypeConstraint And(TypeConstraint left, TypeConstraint right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            return new AndTypeConstraint(left, right);
        }

        public static TypeConstraint Or(TypeConstraint left, TypeConstraint right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            return new OrTypeConstraint(left, right);
        }

        public static TypeConstraint EqualsTo(Type type)
        {
            return new EqualsToTypeConstraint(type);
        }

        internal static TypeConstraint NestedTypes(Type declaringType)
        {
            return new NestedTypesTypeConstraint(declaringType);
        }
    }
}
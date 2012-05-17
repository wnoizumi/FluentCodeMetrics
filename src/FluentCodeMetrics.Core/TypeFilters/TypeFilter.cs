using System;
using System.Reflection;
using FluentCodeMetrics.Core.TypeSets;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public abstract class TypeFilter
    {
        public abstract bool Check(Type type);

        public TypeFilter Or(TypeFilter filter)
        {
            return Or(this, filter);
        }

        public TypeFilter And(TypeFilter filter)
        {
            return And(this, filter);
        }

        public TypeFilter Not()
        {
            return Not(this);
        }

        public static implicit operator TypeFilter(Type type)
        {
            return EqualsTo(type);
        }

        public static implicit operator TypeFilter(Assembly assembly)
        {
            return FromAssembly(assembly);
        }

        public static implicit operator TypeFilter(TypeSet set)
        {
            return new CollectionTypeFilter(set);
        }

        public static TypeFilter FromAssembly(Assembly assembly)
        {
            return new FromAssemblyTypeFilter(assembly);
        }

        public static TypeFilter Not(TypeFilter filter)
        {
            return new NotTypeFilter(filter);
        }

        public static TypeFilter And(TypeFilter left, TypeFilter right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            return new AndTypeFilter(left, right);
        }

        public static TypeFilter Or(TypeFilter left, TypeFilter right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            return new OrTypeFilter(left, right);
        }

        public static TypeFilter EqualsTo(Type type)
        {
            return new EqualsToTypeFilter(type);
        }

        internal static TypeFilter NestedTypes(Type declaringType)
        {
            return new NestedTypesTypeFilter(declaringType);
        }
    }
}
using System;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public abstract class TypeFilter
    {
        public abstract bool Check(Type type);

        public TypeFilter Or(TypeFilter filter)
        {
            return new OrTypeFilter(this, filter);
        }

        public TypeFilter And(TypeFilter filter)
        {
            return new AndTypeFilter(this, filter);
        }

        public TypeFilter Not()
        {
            return TypeFilter.Not(this);
        }

        public static implicit operator TypeFilter(Type type)
        {
            return TypeFilter.EqualsTo(type);
        }

        public static TypeFilter Not(TypeFilter filter)
        {
            return new NotTypeFilter(filter);
        }

        public static TypeFilter And(TypeFilter left, TypeFilter right)
        {
            return new AndTypeFilter(left, right);
        }

        public static TypeFilter Or(TypeFilter left, TypeFilter right)
        {
            return new OrTypeFilter(left, right);
        }

        public static TypeFilter EqualsTo(Type type)
        {
            return new EqualsToTypeFilter(type);
        }
    }
}
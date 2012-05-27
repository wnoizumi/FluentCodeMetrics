using System;
using System.Reflection;
using FluentCodeMetrics.Core.TypeSets;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeConstraints
{
    public abstract class TypeConstraint
    {
        public abstract bool Check(Type type);

        public TypeConstraint Or(TypeConstraint filter)
        {
            Throw.IfArgumentNull(filter, "filter");
            return Or(this, filter);
        }

        public TypeConstraint And(TypeConstraint filter)
        {
            Throw.IfArgumentNull(filter, "filter");
            return And(this, filter);
        }

        public TypeConstraint Not()
        {
            return Not(this);
        }

        public static implicit operator TypeConstraint(Type type)
        {
            Throw.IfArgumentNull(type, "type");
            return EqualsTo(type);
        }

        public static implicit operator TypeConstraint(Assembly assembly)
        {
            Throw.IfArgumentNull(assembly, "assembly");
            return FromAssembly(assembly);
        }

        public static implicit operator TypeConstraint(TypeSet set)
        {
            return new CollectionTypeConstraint(set);
        }

        public static TypeConstraint FromAssembly(Assembly assembly)
        {
            Throw.IfArgumentNull(assembly, "assembly");
            return new FromAssemblyTypeConstraint(assembly);
        }

        public static TypeConstraint Not(TypeConstraint filter)
        {
            Throw.IfArgumentNull(filter, "filter");
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
            Throw.IfArgumentNull(type, "type");
            return new EqualsToTypeConstraint(type);
        }

        internal static TypeConstraint NestedTypes(Type declaringType)
        {
            return new NestedTypesTypeConstraint(declaringType);
        }
    }
}
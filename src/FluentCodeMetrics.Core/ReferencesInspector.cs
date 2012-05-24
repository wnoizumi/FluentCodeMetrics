using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil;
using Mono.Cecil.Cil;

using TypeFilter = FluentCodeMetrics.Core.TypeFilters.TypeFilter;
using FluentCodeMetrics.Core.TypeSets;

namespace FluentCodeMetrics.Core
{
    public class ReferencesInspector
    {
        private readonly Type workingType;
        private readonly IEnumerable<Type> other;

        internal ReferencesInspector(Type type, IEnumerable<Type> other)
        {
            workingType = type;
            this.other = other;
        }

        private ReferencesInspector(Type type)
        {
            workingType = type;
            other = new Type[] { };
        }

        public static ReferencesInspector For<T>()
        {
            return For(typeof(T));
        }

        public static ReferencesInspector For(Type type)
        {
            return new ReferencesInspector(type);
        }

        private const BindingFlags Flags = BindingFlags.Static |
                                           BindingFlags.Instance |
                                           BindingFlags.NonPublic |
                                           BindingFlags.Public;

        public ReferencedTypesTypeSet
            FromBaseType()
        {
            return new ReferencedTypesTypeSet(
                new[] { workingType.BaseType },
                workingType
                );
        }

        public ReferencedTypesTypeSet
            FromNewobjInstructions()
        {
            return FromMethodReferences(OpCodes.Newobj);
        }

        public ReferencedTypesTypeSet
            FromStaticMethodCalls()
        {
            return FromMethodReferences(OpCodes.Call);
        }

        ReferencedTypesTypeSet
            FromMethodReferences(OpCode opCode)
        {
            var typeDef = workingType.ToDefiniton();

            if (typeDef == null)
                return new ReferencedTypesTypeSet(other, workingType);

            var source =
                from method in typeDef.Methods
                from instruction in method.Body.Instructions
                where instruction.OpCode == opCode
                let operand = instruction.Operand as MethodReference
#if DEBUG
                where !operand.DeclaringType.Resolve().FullName.StartsWith("nCrunch")
#endif
                let type = operand.DeclaringType.ToType()
                select type;

            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromExceptionHandlers()
        {
            var typeDef = workingType.ToDefiniton();

            if (typeDef == null)
                return new ReferencedTypesTypeSet(other, workingType);

            var source = from method in typeDef.Methods
                         from exceptionHandler in method.Body.ExceptionHandlers
                         where exceptionHandler.CatchType != null
                         let type = exceptionHandler.CatchType.ToType()
                         where type != null
                         select type;

            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromCtorParameters()
        {
            var source =
                from ctor in workingType.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromMethodsParameters()
        {
            var source =
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;

            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromMethodsReturnTypes()
        {
            var source =
                from method in workingType.GetMethods(Flags)
                where method.ReturnType != typeof(void)
                select method.ReturnType;

            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromProperties()
        {
            var source =
                from property in workingType.GetProperties(Flags)
                select property.PropertyType;
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromFields()
        {
            var source =
                from field in workingType.GetFields(Flags)
                select field.FieldType;
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromParametersMetaAttributes()
        {
            var source =
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromMethodsMetaAttributes()
        {
            var source =
                from method in workingType.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromFieldsMetaAttributes()
        {
            var source =
                from field in workingType.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }

        public ReferencedTypesTypeSet
            FromMetaAttributes()
        {
            var source =
                from attribute in workingType.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypesTypeSet(other.Union(source), workingType);
        }



        public ReferencedTypesTypeSet
            All()
        {
            return new ReferencedTypesTypeSet(
                FromBaseType()
                .And.FromNewobjInstructions()
                .And.FromMetaAttributes()
                .And.FromFieldsMetaAttributes()
                .And.FromMethodsMetaAttributes()
                .And.FromParametersMetaAttributes()
                .And.FromFields()
                .And.FromProperties()
                .And.FromMethodsReturnTypes()
                .And.FromMethodsParameters()
                .And.FromCtorParameters()
                .And.FromStaticMethodCalls()
                .And.FromExceptionHandlers()
                .Distinct()
                ,
                workingType
                );
        }

        public ReferencedTypesTypeSet
            Where(TypeFilter filter)
        {
            return All().FilterBy(filter);
        }


    }
}

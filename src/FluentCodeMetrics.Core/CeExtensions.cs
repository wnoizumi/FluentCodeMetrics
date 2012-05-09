using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil;
using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil.Cil;
using TypeFilter = FluentCodeMetrics.Core.TypeFilters.TypeFilter;

namespace FluentCodeMetrics.Core
{
    public static class CeExtensions
    {
        public static int ComputeCe(this Type that)
        {
            return that
                .GetReferencedTypes()
                .Count();
        }

        const BindingFlags Flags = BindingFlags.Static |
                                       BindingFlags.Instance |
                                       BindingFlags.NonPublic |
                                       BindingFlags.Public;

        public static IEnumerable<Type>
            GetReferencedTypes(this Type that)
        {
            var typeMetaAttributeTypes = GetTypeMetaAttributeTypes(that);
            var fieldMetaAttributeTypes = GetFieldMetaAttributeTypes(that);
            var methodMetaAttributeTypes = GetMethodMetaAttributeTypes(that);
            var methodParameterMetaAttributeTypes = GetMethodParameterMetaAttributeTypes(that);
            var fieldTypes = GetFieldTypes(that);
            var propertyTypes = GetPropertyTypes(that);
            var methodReturnTypes = GetMethodReturnTypes(that);
            var methodParameterTypes = GetMethodParameterTypes(that);
            var ctorParameterTypes = GetCtorParameterTypes(that);

            return new[] { that.BaseType }
                .Union(that.GetReferencedTypesByNewobjInstruction())
                .Union(typeMetaAttributeTypes)
                .Union(fieldMetaAttributeTypes)
                .Union(methodMetaAttributeTypes)
                .Union(methodParameterMetaAttributeTypes)
                .Union(ctorParameterTypes)
                .Union(methodParameterTypes)
                .Union(fieldTypes)
                .Union(propertyTypes)
                .Union(methodReturnTypes)
                .Distinct();
        }

        public static IEnumerable<Type>
            GetReferencedTypesByNewobjInstruction(this Type that)
        {
            var assembly = AssemblyCache.Load(that.Assembly.GetName().Name);
            var typeDef = assembly.MainModule.Types
                .First(type => type.FullName == that.FullName);

            return
                from method in typeDef.Methods
                from instruction in method.Body.Instructions
                where instruction.OpCode == OpCodes.Newobj
                let operand = instruction.Operand as MethodReference
                let typeFullName = operand.DeclaringType.FullName
                let type = Type.GetType(typeFullName)
                where type != null
                select type;
        }

        private static IEnumerable<Type> GetCtorParameterTypes(Type that)
        {
            var ctorParameterTypes =
                from ctor in that.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
            return ctorParameterTypes;
        }

        private static IEnumerable<Type> GetMethodParameterTypes(Type that)
        {
            var methodParameterTypes =
                from method in that.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;
            return methodParameterTypes;
        }

        private static IEnumerable<Type> GetMethodReturnTypes(Type that)
        {
            var methodReturnTypes =
                from method in that.GetMethods(Flags)
                where method.ReturnType != typeof (void)
                select method.ReturnType;
            return methodReturnTypes;
        }

        private static IEnumerable<Type> GetPropertyTypes(Type that)
        {
            var propertyTypes =
                from property in that.GetProperties(Flags)
                select property.PropertyType;
            return propertyTypes;
        }

        private static IEnumerable<Type> GetFieldTypes(Type that)
        {
            var fieldTypes =
                from field in that.GetFields(Flags)
                select field.FieldType;
            return fieldTypes;
        }

        private static IEnumerable<Type> GetMethodParameterMetaAttributeTypes(Type that)
        {
            var methodParameterMetaAttributeTypes =
                from method in that.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
            return methodParameterMetaAttributeTypes;
        }

        private static IEnumerable<Type> GetMethodMetaAttributeTypes(Type that)
        {
            var methodMetaAttributeTypes =
                from method in that.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
            return methodMetaAttributeTypes;
        }

        private static IEnumerable<Type> GetFieldMetaAttributeTypes(Type that)
        {
            var fieldMetaAttributeTypes =
                from field in that.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
            return fieldMetaAttributeTypes;
        }

        private static IEnumerable<Type> GetTypeMetaAttributeTypes(Type that)
        {
            return
                from attribute in that.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return that
                .GetReferencedTypes()
                .Count(filter.Check);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace FluentCodeMetrics.Core
{
    public static class ReferencesInspector
    {
        private const BindingFlags Flags = BindingFlags.Static |
                                           BindingFlags.Instance |
                                           BindingFlags.NonPublic |
                                           BindingFlags.Public;

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

        public static IEnumerable<Type> GetCtorParameterTypes(Type that)
        {
            return    
                from ctor in that.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
        }

        public static IEnumerable<Type> GetMethodParameterTypes(Type that)
        {
            return    
                from method in that.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;
        }

        public static IEnumerable<Type> GetMethodReturnTypes(Type that)
        {
            return    
                from method in that.GetMethods(Flags)
                where method.ReturnType != typeof(void)
                select method.ReturnType;
        }

        public static IEnumerable<Type> GetPropertyTypes(Type that)
        {
            return    
                from property in that.GetProperties(Flags)
                select property.PropertyType;
        }

        public static IEnumerable<Type> GetFieldTypes(Type that)
        {
            return    
                from field in that.GetFields(Flags)
                select field.FieldType;
        }

        public static IEnumerable<Type> GetMethodParameterMetaAttributeTypes(Type that)
        {
            return    
                from method in that.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public static IEnumerable<Type> GetMethodMetaAttributeTypes(Type that)
        {
            return    
                from method in that.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public static IEnumerable<Type> GetFieldMetaAttributeTypes(Type that)
        {
            return
                from field in that.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public static IEnumerable<Type> GetTypeMetaAttributeTypes(Type that)
        {
            return
                from attribute in that.GetCustomAttributes(true)
                select attribute.GetType();
        }
    }
}

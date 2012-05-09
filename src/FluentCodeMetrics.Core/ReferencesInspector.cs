using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace FluentCodeMetrics.Core
{
    public class ReferencesInspector
    {
        // ReSharper disable InconsistentNaming
        private readonly Type workingType;
        // ReSharper restore InconsistentNaming
        
        public ReferencesInspector(Type type)
        {
            workingType = type;
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

        public IEnumerable<Type>
            ByNewobjInstruction()
        {
            var assembly = AssemblyCache.Load(workingType.Assembly.GetName().Name);
            var typeDef = assembly.MainModule.Types
                .First(type => type.FullName == workingType.FullName);

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

        public IEnumerable<Type> 
            ByCtorParametersTypes()
        {
            return    
                from ctor in workingType.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
        }

        public IEnumerable<Type> 
            ByMethodsParametersTypes()
        {
            return    
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;
        }

        public IEnumerable<Type> 
            ByMethodsReturnTypes()
        {
            return    
                from method in workingType.GetMethods(Flags)
                where method.ReturnType != typeof(void)
                select method.ReturnType;
        }

        public IEnumerable<Type> 
            ByPropertiesTypes()
        {
            return    
                from property in workingType.GetProperties(Flags)
                select property.PropertyType;
        }

        public IEnumerable<Type> 
            ByFieldsTypes()
        {
            return    
                from field in workingType.GetFields(Flags)
                select field.FieldType;
        }

        public IEnumerable<Type> 
            ByParametersMetaAttributesTypes()
        {
            return    
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public IEnumerable<Type> 
            OfMethodsMetaAttributesTypes()
        {
            return    
                from method in workingType.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public IEnumerable<Type> 
            OfFieldsMetaAttributesTypes()
        {
            return
                from field in workingType.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
        }

        public IEnumerable<Type> OfMetaAttributesTypes()
        {
            return
                from attribute in workingType.GetCustomAttributes(true)
                select attribute.GetType();
        }
    }
}

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

        public ReferencedTypes
            OfNewobjInstruction()
        {
            var assembly = AssemblyCache.Load(workingType.Assembly.GetName().Name);
            var typeDef = assembly.MainModule.Types
                .First(type => type.FullName == workingType.FullName);

            var source = 
                from method in typeDef.Methods
                from instruction in method.Body.Instructions
                where instruction.OpCode == OpCodes.Newobj
                let operand = instruction.Operand as MethodReference
                let typeFullName = operand.DeclaringType.FullName
                let type = Type.GetType(typeFullName)
                where type != null
                select type;

            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfCtorParametersTypes()
        {
            var source =    
                from ctor in workingType.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfMethodsParametersTypes()
        {
            var source =    
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;

            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfMethodsReturnTypes()
        {
            var source =     
                from method in workingType.GetMethods(Flags)
                where method.ReturnType != typeof(void)
                select method.ReturnType;

            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfPropertiesTypes()
        {
            var source =     
                from property in workingType.GetProperties(Flags)
                select property.PropertyType;
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfFieldsTypes()
        {
            var source =    
                from field in workingType.GetFields(Flags)
                select field.FieldType;
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfParametersMetaAttributesTypes()
        {
            var source =   
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfMethodsMetaAttributesTypes()
        {
            var source =   
                from method in workingType.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfFieldsMetaAttributesTypes()
        {
            var source = 
                from field in workingType.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(source);
        }

        public ReferencedTypes 
            OfMetaAttributesTypes()
        {
            var source = 
                from attribute in workingType.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(source);
        }
    }
}

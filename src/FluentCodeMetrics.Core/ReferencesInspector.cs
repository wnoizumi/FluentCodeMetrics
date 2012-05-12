using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil;
using Mono.Cecil.Cil;

using TypeFilter = FluentCodeMetrics.Core.TypeFilters.TypeFilter;

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
            other = new Type[] {};
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
            FromBaseType()
        {
            return new ReferencedTypes(
                new[] { workingType.BaseType },
                workingType
                );
        }

        public ReferencedTypes
            FromNewobjInstructions()
        {
            return FromMethodReferences(OpCodes.Newobj);
        }

        public ReferencedTypes
            FromStaticMethodCalls()
        {
            return FromMethodReferences(OpCodes.Call);
        }

        ReferencedTypes 
            FromMethodReferences (OpCode opCode)
        {
            var assembly = AssemblyCache.Load(workingType.Assembly.GetName().Name);
            var typeDef = assembly.MainModule.Types
                .First(type => type.FullName == workingType.FullName);

            var source =
                from method in typeDef.Methods
                from instruction in method.Body.Instructions
                where instruction.OpCode == opCode
                let operand = instruction.Operand as MethodReference
                let typeFullName = operand.DeclaringType.FullName
                let type = Type.GetType(typeFullName)
                where type != null
                select type;

            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromCtorParameters()
        {
            var source =    
                from ctor in workingType.GetConstructors(Flags)
                from parameter in ctor.GetParameters()
                select parameter.ParameterType;
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromMethodsParameters()
        {
            var source =    
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                select parameter.ParameterType;

            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromMethodsReturnTypes()
        {
            var source =     
                from method in workingType.GetMethods(Flags)
                where method.ReturnType != typeof(void)
                select method.ReturnType;

            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromProperties()
        {
            var source =     
                from property in workingType.GetProperties(Flags)
                select property.PropertyType;
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromFields()
        {
            var source =    
                from field in workingType.GetFields(Flags)
                select field.FieldType;
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromParametersMetaAttributes()
        {
            var source =   
                from method in workingType.GetMethods(Flags)
                from parameter in method.GetParameters()
                from attribute in parameter.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromMethodsMetaAttributes()
        {
            var source =   
                from method in workingType.GetMethods(Flags)
                from attribute in method.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromFieldsMetaAttributes()
        {
            var source = 
                from field in workingType.GetFields(Flags)
                from attribute in field.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(other.Union(source), workingType);
        }

        public ReferencedTypes 
            FromMetaAttributes()
        {
            var source = 
                from attribute in workingType.GetCustomAttributes(true)
                select attribute.GetType();
            return new ReferencedTypes(other.Union(source), workingType);
        }

        

        public ReferencedTypes
            All()
        {
            return new ReferencedTypes(
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
                .Distinct()
                ,
                workingType
                );
        }

        public ReferencedTypes
            Where(TypeFilter filter)
        {
            return All().FilterBy(filter);
        }

        
    }
}

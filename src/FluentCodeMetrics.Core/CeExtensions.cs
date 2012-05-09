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

        
        public static IEnumerable<Type>
            GetReferencedTypes(this Type that)
        {
            var inspector = new ReferencesInspector();
            var referencedTypesByNewobjInstruction = inspector.GetReferencedTypesByNewobjInstruction(that);
            var typeMetaAttributeTypes = inspector.GetTypeMetaAttributeTypes(that);
            var fieldMetaAttributeTypes = inspector.GetFieldMetaAttributeTypes(that);
            var methodMetaAttributeTypes = inspector.GetMethodMetaAttributeTypes(that);
            var methodParameterMetaAttributeTypes = inspector.GetMethodParameterMetaAttributeTypes(that);
            var fieldTypes = inspector.GetFieldTypes(that);
            var propertyTypes = inspector.GetPropertyTypes(that);
            var methodReturnTypes = inspector.GetMethodReturnTypes(that);
            var methodParameterTypes = inspector.GetMethodParameterTypes(that);
            var ctorParameterTypes = inspector.GetCtorParameterTypes(that);

            return new[] { that.BaseType }
                .Union(referencedTypesByNewobjInstruction)
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


        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return that
                .GetReferencedTypes()
                .Count(filter.Check);
        }
    }
}

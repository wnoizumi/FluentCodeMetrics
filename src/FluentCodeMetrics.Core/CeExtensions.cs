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
            var typeMetaAttributeTypes = ReferencesInspector.GetTypeMetaAttributeTypes(that);
            var fieldMetaAttributeTypes = ReferencesInspector.GetFieldMetaAttributeTypes(that);
            var methodMetaAttributeTypes = ReferencesInspector.GetMethodMetaAttributeTypes(that);
            var methodParameterMetaAttributeTypes = ReferencesInspector.GetMethodParameterMetaAttributeTypes(that);
            var fieldTypes = ReferencesInspector.GetFieldTypes(that);
            var propertyTypes = ReferencesInspector.GetPropertyTypes(that);
            var methodReturnTypes = ReferencesInspector.GetMethodReturnTypes(that);
            var methodParameterTypes = ReferencesInspector.GetMethodParameterTypes(that);
            var ctorParameterTypes = ReferencesInspector.GetCtorParameterTypes(that);

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


        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return that
                .GetReferencedTypes()
                .Count(filter.Check);
        }
    }
}

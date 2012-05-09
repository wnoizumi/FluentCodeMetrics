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
            var inspector = ReferencesInspector.For(that);
            var referencedTypesByNewobjInstruction = inspector.OfNewobjInstruction();
            var typeMetaAttributeTypes = inspector.OfMetaAttributesTypes();
            var fieldMetaAttributeTypes = inspector.OfFieldsMetaAttributesTypes();
            var methodMetaAttributeTypes = inspector.OfMethodsMetaAttributesTypes();
            var methodParameterMetaAttributeTypes = inspector.OfParametersMetaAttributesTypes();
            var fieldTypes = inspector.OfFieldsTypes();
            var propertyTypes = inspector.OfPropertiesTypes();
            var methodReturnTypes = inspector.OfMethodsReturnTypes();
            var methodParameterTypes = inspector.OfMethodsParametersTypes();
            var ctorParameterTypes = inspector.OfCtorParametersTypes();

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

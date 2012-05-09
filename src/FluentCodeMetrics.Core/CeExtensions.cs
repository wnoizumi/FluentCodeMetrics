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

        
        public static ReferencedTypes
            GetReferencedTypes(this Type that)
        {
            return ReferencesInspector.For(that)
                .FromBaseType()
                .And.FromNewobjInstructions()
                .And.FromMetaAttributes()
                .And.FromFieldsMetaAttributes()
                .And.FromMethodsMetaAttributes()
                .And.FromParametersMetaAttributes()
                .And.FromFields()
                .And.FromProperties()
                .And.FromMethodsReturnTypes()
                .And.FromMethodsParameters()
                .And.FromCtorParameters();
        }


        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return that
                .GetReferencedTypes()
                .Count(filter.Check);
        }
    }
}

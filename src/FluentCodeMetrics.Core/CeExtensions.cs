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
            return ReferencesInspector.For(that)
                .All()
                .Where(t=> t.DeclaringType != that)
                .Count();
        }

        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return ReferencesInspector.For(that)
                .All()                
                .FilterBy(filter)
                .Where(t => t.DeclaringType != that)
                .Count();
        }
    }
}

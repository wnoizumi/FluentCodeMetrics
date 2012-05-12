using System;
using System.Linq;
using FluentCodeMetrics.Core.TypeFilters;

namespace FluentCodeMetrics.Core
{
    public static class CeExtensions
    {
        
        public static int ComputeCe(this Type that, TypeFilter filter = null)
        {
            return ReferencesInspector.For(that)
                .Where(
                    that.NestedTypes().Not()
                    .And(filter)
                    )
                .Count();
        }
    }
}

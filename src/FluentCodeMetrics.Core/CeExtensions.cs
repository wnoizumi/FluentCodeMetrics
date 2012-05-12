using System;
using System.Linq;
using FluentCodeMetrics.Core.TypeFilters;

namespace FluentCodeMetrics.Core
{
    public static class CeExtensions
    {
        public static int ComputeCe(this Type that)
        {
            return ReferencesInspector.For(that)
                .All()
                .FilterBy(that.NestedTypes().Not())
                .Count();
        }

        public static int ComputeCe(this Type that, TypeFilter filter)
        {
            return ReferencesInspector.For(that)
                .All()                
                .FilterBy(
                    that.NestedTypes().Not()
                    .And(filter)
                    )
                .Count();
        }
    }
}

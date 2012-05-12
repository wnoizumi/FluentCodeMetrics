using System;
using System.Linq;
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

using System;
using FluentCodeMetrics.Core.TypeConstraints;

namespace FluentCodeMetrics.Core
{
    public static class CeExtensions
    {
        public static int ComputeCe(this Type that, TypeConstraint filter = null)
        {
            return Ce
                .For(that)
                .FilterBy(filter);
        }
    }
}

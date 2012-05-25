using System;
using FluentCodeMetrics.Core.TypeConstraints;
using System.Reflection;

namespace FluentCodeMetrics.Core
{
    public static class MetricsExtensions
    {
        public static int ComputeCe(this Type that, TypeConstraint filter = null)
        {
            return Ce
                .For(that)
                .FilterBy(filter);
        }

        public static int ComputeCc(this MethodInfo that)
        {
            return Cc.For(that);
        }
    }
}

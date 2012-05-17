using System.Reflection;

namespace FluentCodeMetrics.Core.TypeSets
{
    public static class TypeSetExtensions
    {
        public static TypeSet FromAssembly(this Assembly that)
        {
            return TypeSet.FromAssembly(that);
        }
    }
}

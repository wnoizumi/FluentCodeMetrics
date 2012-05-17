using System;
using System.Reflection;

namespace FluentCodeMetrics.Core.TypeSets
{
    public abstract class TypeSet
    {
        public static TypeSet FromAssembly(Assembly assembly)
        {
            throw new NotImplementedException();
        }
    }
}

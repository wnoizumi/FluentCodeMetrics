using System;
using System.Linq;
using System.Reflection;

using Mono.Cecil;

namespace FluentCodeMetrics.Core.Cecil
{
    public static class CecilExtensions
    {
        public static Type ToType(this TypeReference reference)
        {
            TypeDefinition definition = reference.Resolve();
            return Type.GetType(string.Format("{0}, {1}", definition.FullName, definition.Module.Assembly.FullName));
        }

        public static TypeDefinition ToDefiniton(this Type type)
        {
            var assembly = AssemblyCache.Load(type.Assembly);
            return assembly.MainModule.Types.FirstOrDefault(t => t.FullName == type.FullName);
        }

        public static MethodDefinition ToDefinition(this MethodInfo method)
        {
            return method.DeclaringType.ToDefiniton().Methods
                .First(m => m.Name == method.Name);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using System;
using System.Reflection;

namespace FluentCodeMetrics.Core.Cecil
{
    public static class AssemblyCache
    {
        private static readonly Dictionary<Assembly, AssemblyDefinition>
            Dictionary = new Dictionary<Assembly, AssemblyDefinition>();

        public static AssemblyDefinition Load(Assembly assembly)
        {
            if (!Dictionary.ContainsKey(assembly))
                Dictionary.Add(
                    assembly,
                    AssemblyDefinition.ReadAssembly(assembly.ManifestModule.FullyQualifiedName)
                    );

            return Dictionary[assembly];
        }
    }
}
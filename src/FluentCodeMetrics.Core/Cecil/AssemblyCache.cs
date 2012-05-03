using System.Collections.Generic;
using System.IO;
using Mono.Cecil;

namespace FluentCodeMetrics.Core.Cecil
{
    public static class AssemblyCache
    {
        private static readonly Dictionary<string, AssemblyDefinition>
            Dictionary = new Dictionary<string, AssemblyDefinition>();

        public static AssemblyDefinition Load(string assemblyName)
        {
            if (!File.Exists(assemblyName))
                assemblyName = assemblyName + ".dll";

            if (!Dictionary.ContainsKey(assemblyName))
                Dictionary.Add(
                    assemblyName,
                    AssemblyDefinition.ReadAssembly(assemblyName)
                    );

            return Dictionary[assemblyName];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public class FromAssemblyTypeFilter : TypeFilter
    {
        private readonly Assembly assemblyField;

        internal FromAssemblyTypeFilter(Assembly assembly)
        {
            assemblyField = assembly;
        }

        public override bool Check(Type type)
        {
            return type.Assembly == assemblyField;
        }
    }
}

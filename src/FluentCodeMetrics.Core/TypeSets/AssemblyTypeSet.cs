using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FluentCodeMetrics.Core.TypeSets
{
    public class AssemblyTypeSet : TypeSet
    {
        private readonly Assembly source;

        public AssemblyTypeSet(Assembly source)
        {
            this.source = source;
        }

        public override IEnumerable<Type> GetAllTypes()
        {
            return source.GetTypes();
        }
    }
}

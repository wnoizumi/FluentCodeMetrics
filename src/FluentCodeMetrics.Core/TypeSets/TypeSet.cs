using System;
using System.Reflection;
using System.Collections.Generic;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeSets
{
    public abstract class TypeSet : IEnumerable<Type>
    {
        public abstract IEnumerable<Type> GetAllTypes();

        public IEnumerator<Type> GetEnumerator()
        {
            return GetAllTypes().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetAllTypes().GetEnumerator();
        }

        public static TypeSet FromAssembly(Assembly assembly)
        {
            Throw.IfArgumentNull(assembly, "assembly");
            return new CollectionTypeSet(assembly.GetTypes());
                // AssemblyTypeSet(assembly);
        }

        public static TypeSet With(params Type[] types)
        {
            return new CollectionTypeSet(types);
        }

        public static implicit operator TypeSet(Assembly source)
        {
            return FromAssembly(source);
        }
    }
}

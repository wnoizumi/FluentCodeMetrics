using System;
using System.Collections.Generic;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeSets
{
    public sealed class CollectionTypeSet : TypeSet
    {
        private readonly IEnumerable<Type> source; 
        public CollectionTypeSet(IEnumerable<Type> source)
        {
            Throw.IfArgumentNull(source, "source");
            this.source = source;
        }

        public override IEnumerable<Type> GetAllTypes()
        {
            return source;
        }
    }
}
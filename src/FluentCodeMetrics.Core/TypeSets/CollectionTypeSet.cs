using System;
using System.Collections.Generic;
using ThrowHelper;

namespace FluentCodeMetrics.Core.TypeSets
{
    public class CollectionTypeSet : TypeSet
    {
        private IEnumerable<Type> source; 
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
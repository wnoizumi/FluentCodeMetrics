using System;
using System.Collections.Generic;

namespace FluentCodeMetrics.Core.TypeSets
{
    public class CollectionTypeSet : TypeSet
    {
        private IEnumerable<Type> source; 
        public CollectionTypeSet(IEnumerable<Type> source)
        {
            this.source = source;
        }

        public override IEnumerable<Type> GetAllTypes()
        {
            return source;
        }
    }
}
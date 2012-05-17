using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core.TypeFilters
{
    public class CollectionTypeFilter : TypeFilter
    {
        private readonly IEnumerable<Type> typesField;

        internal CollectionTypeFilter(IEnumerable<Type> types)
        {
            typesField = types;
        }

        public override bool Check(Type type)
        {
            return typesField.Contains(type);
        }
    }
}

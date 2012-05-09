using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core
{
    public class ReferencedTypes : 
        IEnumerable<Type>
    {
        private readonly IEnumerable<Type> sourceField;  
        internal ReferencedTypes(IEnumerable<Type> source)
        {
            sourceField = source;
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return sourceField.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return sourceField.GetEnumerator();
        }
    }
}

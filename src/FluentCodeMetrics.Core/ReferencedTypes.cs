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
        private readonly Type originalField;

        internal ReferencedTypes(IEnumerable<Type> source, Type original)
        {
            sourceField = source;
            originalField = original;
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return sourceField.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return sourceField.GetEnumerator();
        }

        public ReferencesInspector And
        {
            get { return new ReferencesInspector(originalField, sourceField); }
        }


    }
}

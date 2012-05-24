using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCodeMetrics.Core
{
    public abstract class CodeMetric
    {
        public abstract int Value { get; }
    }
}

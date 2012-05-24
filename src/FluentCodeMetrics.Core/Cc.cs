using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FluentCodeMetrics.Core
{
    public class Cc : CodeMetric
    {
        private Cc(int value)
        {
            this.value = value;
        }

        private int value = 0;
        public override int Value
        {
            get { return 1; }
        }

        public static Cc For(MethodInfo method)
        {
            return new Cc(1);
        }
    }
}

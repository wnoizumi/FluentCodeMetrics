using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.Ca
{
    public class Foo
    {

    }

    public class FooCalculator
    {
        public void Add(Foo left, Foo right)
        {
            throw new FooException();
        }
    }

    public class FooException : Exception
    {

    }

    public class TryToCatchFooException
    {
        public void DoSomething()
        {
            try
            { 
            
            }
            catch (FooException)
            { 
            
            }
        }
    }
}

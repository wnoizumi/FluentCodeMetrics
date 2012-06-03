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

    [BarAttribute]
    public class FooException : Exception
    {
        [BarAttribute]
        public void Add([BarAttribute] string param)
        { 
        
        }
    }

    public class BarAttribute : Attribute
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

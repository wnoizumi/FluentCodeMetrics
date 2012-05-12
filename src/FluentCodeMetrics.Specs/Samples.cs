
using System;

// ReSharper disable CheckNamespace
namespace Samples
// ReSharper restore CheckNamespace
{
    public class Fee
    {
    }

    public class EmptyClass
    {
    }

    public class SingleArgCtor
    {
// ReSharper disable UnusedParameter.Local
        public SingleArgCtor(Fee arg)
// ReSharper restore UnusedParameter.Local
        {}
    }

    public class SingleArgVoidMethod
    {
         public void Foo(Fee arg)
         {}
    }

    public class FeeMethod
    {
        public Fee Foo()
        {
            return new Fee();
        }
    }

    public class DateTimeArgDateTimeMethod
    {
         public DateTime Foo(DateTime arg)
         {
             return DateTime.Now;
         }
    }

    public class SingleProperty
    {
        public DateTime Foo { get; set; }
    }

    public class SingleNonAutoProperty
    {
        public DateTime DummyProperty
        {
            get { return DateTime.Now; }
        }
    }

   
    public class SingleField
    {
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
        private DateTime foo;
// ReSharper restore UnusedMember.Local
// ReSharper restore InconsistentNaming
    }

    public class ExceptionRaiser
    {
         public void Foo()
         {
             throw new Exception();
         }
    }

    public class SingleEvent
    {
        public EventHandler Event;
    }


    [Serializable]
    public class Attributes
    {
        [NonSerialized] public int FooField;

        [FooAttribute]
        public void FooMethod([FooAttribute2] int foo) {}
    }

    

    public class FooAttribute : Attribute
    {
    }

    public class FooAttribute2 : Attribute
    {}


    public static class StaticMember
    {
        public static DateTime Foo { get; set; }
    }

    public class ClassDependsOnASubClass
    {
        private SubClass foo;

        public class SubClass
        {}
    }

    public class StaticPropertyAndMethodCall
    {
        public void DoNothing()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}

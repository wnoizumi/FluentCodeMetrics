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

    public class TryCatch
    {
        public void DoNothing()
        {
            try
            { }
            catch (DivideByZeroException)
            { }
        }
    }

    public class TryCatchCustomException
    {
        public void DoNothing()
        {
            try
            { }
            catch (MyException)
            { }
        }
    }

    public class TryCatchWithUndefinedType
    {
        public void DoNothing()
        {
            try
            { }
            catch
            { }
        }
    }

    public class VoidMethodUsingFee
    {
        public void Foo()
        {
            var f = new Fee();
        }
    }

    public class MyException : Exception
    { 
    
    }

    // ---------------------------------------------

    public class MyClass
    {
        public MyClass()
        {
            if (DateTime.Today.Equals(new DateTime(2012, 12, 21)))
                throw new Exception("Goodbye cruel world, I'm leaving you today, Goodbye, Goodbye, Goodbye");
        }

        public void EmptyVoidMethod()
        {
            
        }

        public string Name 
        {
            get { return "My name is Bond..."; }
        }

        public DateTime DayOfBirth
        {
            get; set;
        }

        public int Age 
        {
            get { 
                if (DateTime.Now.Year <= 1950)
                    return 0;

                return DateTime.Now.Year - 1950;
            }
        }

        public string GetGreetingMessage()
        {
            return DateTime.Now.Hour < 12 
                ? "Bom dia!" 
                : "Boa tarde!";
        }

        public void PrintHelloTenTimesUsingFor()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello");
            }
        }

        public void PrintHelloTenTimesUsingWhile()
        {
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine("Hello");
                i++;
            }
        }

        public string GetGenreGreeting(Genre genre)
        {
            string result = null;
            switch (genre)
            {
                case Genre.Male:
                    result = "Hello, Sir";
                    break;
                case Genre.Female:
                    result = "Hello, Lady";
                    break;
            }
            return result;
        }

        public void TryCatchMethod()
        {
            try
            {
                PrintHelloTenTimesUsingFor();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("--> "+ e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Console.WriteLine("done!");
            }
        }

        public void IHaveABadSmell()
        {
            int hour = DateTime.Now.Year;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            
            if (hour <= 12)
            {
                if (minute >= 0 && minute < 30)
                {
                    if (second >= 0 && second < 30)
                    {
                        try
                        {

                        }
                        catch { }
                        finally { }
                    }
                }
            }
            else
            {
                if (minute >= 0 && minute < 30)
                {
                    if (second >= 0 && second < 30)
                    {
                        do
                        {
                            second--;
                        } while (second <= 30);
                    }
                }
            }
        }
    }

    public enum Genre
    {
        Male,
        Female
    }
}

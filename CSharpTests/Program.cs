using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CSharpTests
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Q1
            Console.WriteLine("Q1");
            //B obj1 = (B)new A();//wrong
            //obj1.Foo();           
            B obj2 = new B();
            obj2.Foo();
            
            A obj3 = new B();
            obj3.Foo();
            Console.WriteLine(string.Empty);
            #endregion
            
            #region Q2
            Console.WriteLine("Q2");
            var s = new S();
            using (s)
            {
                Console.WriteLine(s.GetDispose());
            }
            Console.WriteLine(s.GetDispose());
            Console.WriteLine(string.Empty);
            #endregion

            #region Q3
            Console.WriteLine("Q3");
            List<Action> actions = new List<Action>();
            for (var ind = 0; ind < 10; ind++)
            {
                actions.Add(() => Console.WriteLine(ind));
            }
            foreach (var action in actions)
            {
                action();
            }
            Console.WriteLine(string.Empty);
            #endregion

            #region Q4   
            Console.WriteLine("Q4");
            int i = 1;
            object obj = i;
            ++i;
            Console.WriteLine(i);
            Console.WriteLine(obj);
            //Console.WriteLine((short)obj);wrong - cast is not valid
            Console.WriteLine(string.Empty);
            #endregion

            #region Q5
            Console.WriteLine("Q5");
            var s1 = string.Format("{0}{1}", "abc", "cba");
            var s2 = "abc" + "cba";
            var s3 = "abccba";

            Console.WriteLine(s1 == s2);
            Console.WriteLine((object)s1 == (object)s2);
            Console.WriteLine(s2 == s3);
            Console.WriteLine((object)s2 == (object)s3);
            Console.WriteLine(string.Empty);
            #endregion

            #region Q6
            Console.WriteLine("Q6");
            LockTest.Test();
            Console.WriteLine(string.Empty);
            #endregion

            #region Q7
            Console.WriteLine("Q7");
            var c = new CC();
            AA a = c;
            a.Print2();
            a.Print1();
            c.Print2();
            Console.WriteLine(string.Empty);
            #endregion

            #region Q8
            Console.WriteLine("Q8");
            var w = new TestYeld.Wrap();
            var wraps = new TestYeld.Wrap[3];
            for (int index = 0; index < wraps.Length; index++)
            {
                wraps[index] = w;
            }

            var values = wraps.Select(x => x.Value);
            var results = TestYeld.Square(values);
            int sum = 0;
            int count = 0;
            foreach (var r in results)
            {
                count++;
                sum += r;
            }
            Console.WriteLine("Count {0}", count);
            Console.WriteLine("Sum {0}", sum);

            Console.WriteLine("Count {0}", results.Count());
            Console.WriteLine("Sum {0}", results.Sum());
            Console.WriteLine(string.Empty);
            #endregion

            #region Q9
            Console.WriteLine("Q9");
            try
            {
                Calc();
            }
            catch (MyCustomException e)
            {
                Console.WriteLine("Catch MyCustomException");
                //throw;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Catch Exception");
            }
            Console.WriteLine(string.Empty);
            #endregion

            #region Q10
            Console.WriteLine("Q10");
            Console.WriteLine((int)En.Second);
            Console.Read();
            #endregion

            #region Q11
            //int cc = 3;
            //Console.Write(Sum(5, 3, out cc) + " ");
            //Console.Write(c);
            #endregion

            #region Q12 // compile error
            //var a = null;
            //a = 10;
            //Console.WriteLine(a);
            #endregion
            Console.ReadLine();

            #region Q13
            Console.WriteLine("Q9");
            object sync = new object();
            var thread = new Thread(() =>
            {
                try
                {
                    Work();
                }
                finally
                {
                    lock (sync)
                    {
                        Monitor.PulseAll(sync);
                    }
                }
            });
            thread.Start();
            lock (sync)
            {
                Monitor.Wait(sync);
            }
            Console.WriteLine("test");//never prints -  dead lock
            #endregion
        }

        private static void Work()
        {
            Thread.Sleep(1000);
        }

        //static int Sum(int a, int b, out int c)
        //{
        //    return a + b; //compile error 
        //}

        private static void Calc()
        {
            int result = 0;
            var x = 5;
            int y = 0;
            try
            {
                result = x / y;
            }
            catch (MyCustomException e)
            {
                Console.WriteLine("Catch DivideByZeroException");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch Exception");
            }
            finally
            {
                throw new MyCustomException();
            }
        }

        private enum En
        {
            First = 15,
            Second,
            Third = 54
        }
    }

    
    class MyCustomException : DivideByZeroException
    {

    }
    class TestYeld
    {
        public static IEnumerable<int> Square(IEnumerable<int> a)
        {
            foreach (var r in a)
            {
                Console.WriteLine(r * r);
                yield return r * r;
            }
        }
        public class Wrap
        {
            private static int init = 0;
            public int Value
            {
                get { return ++init; }
            }
        }
    }
    public class AA
    {
        public virtual void Print1()
        {
            Console.Write("AA");
        }
        public void Print2()
        {
            Console.Write("AA");
        }
    }
    public class BB : AA
    {
        public override void Print1()
        {
            Console.Write("BB");
        }
    }
    public class CC : BB
    {
        new public void Print2()
        {
            Console.Write("CC");
        }
    }

    class A
    {
        public virtual void Foo()
        {
            Console.WriteLine("Class A");
        }
    }
    class B : A
    {
        public override void Foo()
        {
            Console.WriteLine("Class B");
        }
    }

    public struct S : IDisposable
    {
        private bool dispose;
        public void Dispose()
        {
            dispose = true;
        }
        public bool GetDispose()
        {
            return dispose;
        }
    }

    internal class LockTest
    {
        private static Object syncObject = new Object();
        private static void Write()
        {
            lock (syncObject)
            {
                Console.WriteLine("test");
            }
        }
        public static void Test()
        {
            lock (syncObject)
            {
                Write();
            }
        }
    }
}

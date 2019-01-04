using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EventsDelegates.Counter;

namespace EventsDelegates
{
    // https://www.codeproject.com/Articles/4773/Events-and-Delegates-simplified

    // Event Declaration
    public delegate void NumberReachedEventHandler(object sender, NumberReachedEventArgs e);

    public class Program 
    {
        public static void Main(string[] args)
        {
            Counter counter = new Counter();

            //Syntax for initiating an event handler for some event
            counter.NumberReached += new NumberReachedEventHandler(oCounter_NumberReached);

            counter.CountTo(10, 9);
        }

        private static void oCounter_NumberReached(object sender, NumberReachedEventArgs e)
        {
            Console.WriteLine("Reached: " + e.ReachedNumber.ToString());
        }
    }

    public class Counter
    {
        // NumberReached event for Usage
        public event NumberReachedEventHandler NumberReached;

        public Counter()
        {

        }
        
        public void CountTo(int countTo, int reachableNum)
        {
            if (countTo < reachableNum)
            {
                throw new ArgumentException("reachableNum should be less than countTo");
            }

            for (int ctr = 0; ctr <= countTo; ctr++)
            {
                if (ctr == reachableNum)
                {
                    NumberReachedEventArgs e = new NumberReachedEventArgs(reachableNum);
                    OnNumberReached(e);
                    return;//don't count any more
                }
            }
        }

        public class NumberReachedEventArgs : EventArgs
        {
            private int _reached;
            public NumberReachedEventArgs(int num)
            {
                this._reached = num;
            }
            public int ReachedNumber
            {
                get
                {
                    return _reached;
                }
            }
        }

        // Protected : it means it's available for classes which are derived from this class (inheriting classes). 
        // virtual : his means that it could be overridden in a derived class. 
        protected virtual void OnNumberReached(NumberReachedEventArgs e)
        {
            if (NumberReached != null)
            {
                // Event Raised
                NumberReached(this, e);//Raise the event
            }
        }
    }

    /*
    // Delegates
    
      // Declaration
      public delegate int SomeDelegate(string s, bool b);

      public static void Main(string[] args)
         {
             SomeDelegate someDelegate = new SomeDelegate(SomeFunction);
             someDelegate("Somestring", true);
         }

         public static int SomeFunction(string str, bool bln)
         {
             Console.WriteLine("hello" + str);
             Console.ReadKey();
             return 0;
         }
      */

}


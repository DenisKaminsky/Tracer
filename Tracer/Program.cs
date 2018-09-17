using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerProgram;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace Tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            test t = new test();
            t.methodtest();
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        void hi()
        {
           
        }
    }

    class test
    {
        public void methodtest()
        {
            Console.WriteLine(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
        }
    }
}

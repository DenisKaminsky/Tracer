﻿using System;
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
            /*test t = new test();
            t.methodtest();
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread.Sleep(100);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString()+"ms");*/

            test t = new test();
            myclass n = new myclass();
            Console.WriteLine(n.a);
            t.methodtest(n);
            Console.WriteLine(n.a);



        }
    }

    class test
    {
        public void methodtest(myclass m)
        {
            //Console.WriteLine(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            m.a = 5;
        }
    }

    class myclass
    {
        public int a;
        public myclass()
        {
            a = 0;
        }
    }
}

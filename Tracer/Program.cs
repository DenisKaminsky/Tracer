using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using TracerProgram;

namespace Tracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();

        }
    }

    class test
    {
        public test()
        {

        }

        public void method1()
        {

        }

        public void method2()
        {

        }
        public void method3()
        {

        }
        public void method4()
        {

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using TracerProgram;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
namespace Tracer
{
    class Program
    {
        static TracerProgram.Tracer tracer = new TracerProgram.Tracer();
        static void Main(string[] args)
        {
            tracer.StartTrace();
            method1();
            method2();
            tracer.StopTrace();
            TracerProgram.TraceResult result = new TraceResult();
            result = tracer.GetTraceResult();

            ITraceConverter converter = new JsonConverter();
            converter.Convert(result);                        
        }

        static public void method1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            method3();
            tracer.StopTrace();
        }

        static public void method2()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            method4();
            method5();
            tracer.StopTrace();
        }
        static public void method3()
        {
            tracer.StartTrace();
            Thread.Sleep(300);
            tracer.StopTrace();
        }
        static public void method4()
        {
            tracer.StartTrace();
            Thread.Sleep(400);
            tracer.StopTrace();
        }
        static public void method5()
        {
            tracer.StartTrace();
            Thread.Sleep(500);
            method6();
            tracer.StopTrace();
        }
        static public void method6()
        {
            tracer.StartTrace();
            Thread.Sleep(600);
            tracer.StopTrace();
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

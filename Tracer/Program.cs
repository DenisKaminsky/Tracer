using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using TracerProgram;

namespace Tracer
{
    class Program
    {
        static TracerProgram.Tracer tracer = new TracerProgram.Tracer();

        static void Main(string[] args)
        {
            TraceResult result;
            UsingExamples example = new UsingExamples(tracer);
            example.StartTest();
            result = tracer.GetTraceResult();

            ConvertResult(new JsonConverter(), result);
            ConvertResult(new XMLConverter(), result);
            ConvertResult(new ConsoleWriter(), result);      
        }

        static void ConvertResult(ITraceConverter converter,TraceResult result)
        {            
            converter.Convert(result);
        }        
    }    

    public class UsingExamples
    {
        private ITracer tracer;

        public UsingExamples(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void StartTest()
        {
            var threads = new List<Thread>();
            
            for (int i = 0; i < 5; i++)
            {
                Thread thread;
                if (i < 2)
                    thread = new Thread(method1);
                else if (i >= 2 && i < 4)
                    thread = new Thread(method2);
                else
                    thread = new Thread(method8);
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        public void method1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            method3();
            tracer.StopTrace();
        }

        public void method2()
        {
            tracer.StartTrace();
            var threads = new List<Thread>();

            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(method5);                
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            Thread.Sleep(200);
            method4();
            method5();
            tracer.StopTrace();
        }

        public void method3()
        {
            tracer.StartTrace();
            Thread.Sleep(300);
            tracer.StopTrace();
        }

        public void method4()
        {
            tracer.StartTrace();
            Thread.Sleep(400);
            tracer.StopTrace();
        }

        public void method5()
        {
            tracer.StartTrace();
            var threads = new List<Thread>();

            for (int i = 0; i < 2; i++)
            {
                Thread thread = new Thread(method6);
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            Thread.Sleep(500);
            method6();
            tracer.StopTrace();
        }

        public void method6()
        {
            tracer.StartTrace(); 
            Thread.Sleep(600);
            method7();
            tracer.StopTrace();
        }

        public void method7()
        {
            tracer.StartTrace();
            Thread.Sleep(700);
            tracer.StopTrace();
        }

        public void method8()
        {
            tracer.StartTrace();
            Thread.Sleep(800);
            tracer.StopTrace();
        }

    }
}

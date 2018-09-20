using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerProgram;
using System.Threading;

namespace TracerUnitTest
{
    [TestClass]
    public class TracerTest
    {
        private Tracer tracer;
        private int sleeptime;
        private TraceResult traceresult;
        private const int threadscount = 3;
        private const int threadsnestedcount = 2;

        public void TestMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleeptime);
            tracer.StopTrace();
        }

        public void TestMethod2()
        {
            tracer.StartTrace();
            Thread.Sleep(sleeptime*2);
            tracer.StopTrace();
        }

        public void Innermethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleeptime);
            TestMethod();
            tracer.StopTrace();
        }

        public void NestedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(sleeptime);
            Innermethod();
            tracer.StopTrace();
        }

        public void MultipleThreadMethod()
        {
            tracer.StartTrace();            
            var threads = new List<Thread>();
            for (int i = 0; i < threadsnestedcount; i++)
            {
                Thread thread = new Thread(TestMethod);
                threads.Add(thread);
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            TestMethod();
            Thread.Sleep(sleeptime);
            tracer.StopTrace();
        }

        //время одного потока
        [TestMethod]
        public void TimeTestSingleThread()
        {
            tracer = new Tracer();
            sleeptime = 222;
            TestMethod();
            traceresult = tracer.GetTraceResult();
            Assert.IsTrue(traceresult.threads[0].TimeInt >= sleeptime);
        }
        
        //время нескольких потоков
        [TestMethod]
        public void TimeTestMultiThread()
        {
            tracer = new Tracer();
            sleeptime = 111;

            var threads = new List<Thread>();
            for (int i = 0; i < threadscount; i++)
            {
                Thread thread = new Thread(TestMethod);
                threads.Add(thread);
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            traceresult = tracer.GetTraceResult();
            long actualtime = 0;
            for (int i = 0; i < traceresult.threads.Count; i++)
            {
                actualtime += traceresult.threads[i].TimeInt;
            }
            Assert.IsTrue(actualtime >= sleeptime * threadscount);
        }

        //время вложенных методов
        [TestMethod]
        public void TimeTestNestedMethods()
        {
            tracer = new Tracer();
            sleeptime = 50;

            NestedMethod();
            traceresult = tracer.GetTraceResult();

            Assert.IsTrue(traceresult.threads[0].TimeInt>=sleeptime*3);
        }

        //вложенные потоки
        [TestMethod]
        public void TestNestedThreads()
        {
            tracer = new Tracer();
            sleeptime = 80;
            int singlemethods = 0, nestedmethods = 0;

            var threads = new List<Thread>();
            for (int i = 0; i < threadscount; i++)
            {
                Thread thread = new Thread(MultipleThreadMethod);
                threads.Add(thread);
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            traceresult = tracer.GetTraceResult();
            Assert.AreEqual(threadscount * threadsnestedcount+ threadscount, traceresult.threads.Count);
            for (int i=0; i< traceresult.threads.Count;i++)
            {
                Assert.AreEqual(1,traceresult.threads[i].Methods.Count);
                Assert.AreEqual(nameof(TracerTest), traceresult.threads[i].Methods[0].Class_name);
                if (traceresult.threads[i].Methods[0].Methodlist.Count != 0)
                {
                    nestedmethods++;         
                    Assert.AreEqual(nameof(MultipleThreadMethod), traceresult.threads[i].Methods[0].Method_name);
                    Assert.AreEqual(nameof(TestMethod), traceresult.threads[i].Methods[0].Methodlist[0].Method_name);
                }
                else
                    singlemethods++;
            }
            Assert.AreEqual(threadscount, nestedmethods);
            Assert.AreEqual(threadsnestedcount*threadscount, singlemethods);
        }

        //несколько методов в одном потоке(не вложенных)
        [TestMethod]
        public void TestMultipleMethodsInSingleThread()
        {
            tracer = new Tracer();
            sleeptime = 400;

            TestMethod();
            TestMethod2();
            traceresult = tracer.GetTraceResult();

            Assert.AreEqual(1, traceresult.threads.Count);
            Assert.AreEqual(2, traceresult.threads[0].Methods.Count);
            Assert.IsTrue(traceresult.threads[0].TimeInt >= sleeptime*3);
            Assert.AreEqual(nameof(TestMethod), traceresult.threads[0].Methods[0].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[0].Class_name);
            Assert.AreEqual(nameof(TestMethod2), traceresult.threads[0].Methods[1].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[1].Class_name);
        }

        //один метод в одном потоке
        [TestMethod]
        public void TestSingleNestedMethod()
        {
            tracer = new Tracer();
            sleeptime = 666;

            TestMethod();
            traceresult = tracer.GetTraceResult();

            Assert.AreEqual(1, traceresult.threads.Count);//количесво потоков
            Assert.AreEqual(1, traceresult.threads[0].Methods.Count);//количесво методов в потоке
            Assert.IsTrue(traceresult.threads[0].TimeInt >= sleeptime);//время потока
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, traceresult.threads[0].Id);
            Assert.AreEqual(0, traceresult.threads[0].Methods[0].Methodlist.Count);//количесво методов в Testmethod
            Assert.IsTrue(traceresult.threads[0].Methods[0].TimeInt >= sleeptime);//время NestedMethod
            Assert.AreEqual(nameof(TestMethod), traceresult.threads[0].Methods[0].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[0].Class_name);
        }

        //нескольоко вложенных методов в одном потоке
        [TestMethod]
        public void TestMultipleNestedMethods()
        {
            tracer = new Tracer();
            sleeptime = 312;

            NestedMethod();
            traceresult = tracer.GetTraceResult();

            Assert.AreEqual(1,traceresult.threads.Count);//количесво потоков
            Assert.AreEqual(1,traceresult.threads[0].Methods.Count);//количесво методов в потоке
            Assert.IsTrue(traceresult.threads[0].TimeInt >= sleeptime * 3);//время потока
            Assert.AreEqual(1,traceresult.threads[0].Methods[0].Methodlist.Count);//количесво методов в NestedMethod
            Assert.IsTrue(traceresult.threads[0].Methods[0].TimeInt >= sleeptime * 3);//время NestedMethod
            Assert.AreEqual(nameof(NestedMethod), traceresult.threads[0].Methods[0].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[0].Class_name);
            Assert.AreEqual(1,traceresult.threads[0].Methods[0].Methodlist[0].Methodlist.Count);//количесво методов в InnerMethod
            Assert.IsTrue(traceresult.threads[0].Methods[0].Methodlist[0].TimeInt>=sleeptime*2);//время InnerMethod
            Assert.AreEqual(nameof(Innermethod), traceresult.threads[0].Methods[0].Methodlist[0].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[0].Methodlist[0].Class_name);
            Assert.AreEqual(0,traceresult.threads[0].Methods[0].Methodlist[0].Methodlist[0].Methodlist.Count);//количесво методов в TestMethod
            Assert.IsTrue(traceresult.threads[0].Methods[0].Methodlist[0].Methodlist[0].TimeInt >= sleeptime);//время TestMethod
            Assert.AreEqual(nameof(TestMethod), traceresult.threads[0].Methods[0].Methodlist[0].Methodlist[0].Method_name);
            Assert.AreEqual(nameof(TracerTest), traceresult.threads[0].Methods[0].Methodlist[0].Methodlist[0].Class_name);
        }
    }
}

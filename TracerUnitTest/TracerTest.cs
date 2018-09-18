using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerProgram;
using System.Threading;

namespace TracerUnitTest
{
    [TestClass]
    public class TracerTest
    {
        private Tracer tracer;
        private int time;
        private int sleeptime;
        private TraceResult traceresult;

        [TestMethod]
        public void TimeTestSingleThread()
        {
            tracer = new Tracer();
            sleeptime = 222;

            tracer.StartTrace();
            Thread.Sleep(sleeptime);
            tracer.StopTrace();
            traceresult = tracer.GetTraceResult();

            Assert.IsTrue(traceresult.threads[0].TimeInt>=sleeptime);
        }
    }
}

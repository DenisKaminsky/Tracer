using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace TracerProgram
{
    public class Tracer : ITracer
    {
        private TraceResult traceresult;

        public Tracer()
        {
            traceresult = new TraceResult();        
        }

        public void StartTrace()
        {
            MethodBase currentMethod = new StackTrace().GetFrame(1).GetMethod();
            traceresult.StartTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);       
        }

        public void StopTrace()
        {
            
        }

        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
}

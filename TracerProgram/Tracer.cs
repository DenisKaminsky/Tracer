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
            traceresult.StopTrace(Thread.CurrentThread.ManagedThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return traceresult;
        }
    }
}

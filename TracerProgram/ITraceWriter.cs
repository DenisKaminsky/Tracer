namespace TracerProgram
{
    public interface ITraceWriter
    {        
        void Write(TraceResult traceresult,ITraceConverter converter);        
    }
}

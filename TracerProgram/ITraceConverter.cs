using System.IO;

namespace TracerProgram
{
    public interface ITraceConverter
    {
        void Convert(TraceResult traceresult,Stream stream);
    }
}

using System;
using System.IO;

namespace TracerProgram
{
    public class ConsoleWriter: ITraceWriter
    {
        public void Write(TraceResult traceresult, ITraceConverter converter)
        {
            using (Stream consolestream = Console.OpenStandardOutput())
            {
                converter.Convert(traceresult, consolestream);
            }
        }
    }
}

using System.IO;

namespace TracerProgram
{
    public class FileWriter:ITraceWriter
    {
        public void Write(TraceResult result,ITraceConverter converter)
        {
            using (FileStream fs = new FileStream("json.dat", FileMode.Create))
            {
                converter.Convert(result, fs);
            }
        }
    }
}

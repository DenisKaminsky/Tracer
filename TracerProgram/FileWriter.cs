using System.IO;

namespace TracerProgram
{
    public class FileWriter:ITraceWriter
    {
        private string filename;

        public FileWriter(string filename)
        {
            this.filename = filename;
        }

        public void Write(TraceResult result,ITraceConverter converter)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                converter.Convert(result, fs);
            }
        }
    }
}

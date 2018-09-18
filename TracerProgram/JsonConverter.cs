using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;

namespace TracerProgram
{
    public class JsonConverter:ITraceConverter
    {
        private DataContractJsonSerializer jsonFormatter;

        public JsonConverter()
        {
            jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
        }

        public void Convert(TraceResult traceresult)
        {
            using (FileStream fs = new FileStream("json.dat", FileMode.Create))
            {
                using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, ownsStream: true, indent: true,indentChars: "     "))
                {
                    jsonFormatter.WriteObject(jsonWriter, traceresult);
                }
            }
        }
    }
}

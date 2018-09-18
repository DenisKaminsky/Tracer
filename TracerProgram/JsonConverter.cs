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

        public void Convert(TraceResult traceresult,Stream stream)
        {
            using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: true, indent: true,indentChars: "     "))
            {
                jsonFormatter.WriteObject(jsonWriter, traceresult);
            }
            
        }
    }
}

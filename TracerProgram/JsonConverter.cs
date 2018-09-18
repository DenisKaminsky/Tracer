using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;

namespace TracerProgram
{
    public class JsonConverter:ITraceConverter
    {
        public void Convert(TraceResult traceresult)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
            using (FileStream fs = new FileStream("json.dat", FileMode.Create))
            {
                using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, ownsStream: true, indent: true))
                {

                    jsonFormatter.WriteObject(jsonWriter, traceresult);
                }
            }
        }
    }
}

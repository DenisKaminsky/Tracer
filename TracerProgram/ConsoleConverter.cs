using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;

namespace TracerProgram
{
    public class ConsoleConverter: ITraceConverter
    {
        private DataContractJsonSerializer jsonFormatter;

        public ConsoleConverter()
        {
            jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
        }

        public void Convert(TraceResult traceresult)
        {
            using (Stream consolestream = Console.OpenStandardOutput())
            {
                using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(consolestream, Encoding.UTF8, ownsStream: true, indent: true, indentChars: "     "))
                {
                    jsonFormatter.WriteObject(jsonWriter, traceresult);
                }
            }
        }
    }
}

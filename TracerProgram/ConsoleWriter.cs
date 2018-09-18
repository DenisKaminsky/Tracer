using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;

namespace TracerProgram
{
    public class ConsoleWriter: ITraceWriter
    {/*
        private DataContractJsonSerializer jsonFormatter;

        public ConsoleWriter()
        {
            jsonFormatter = new DataContractJsonSerializer(typeof(TraceResult));
        }*/

        public void Write(TraceResult traceresult, ITraceConverter converter)
        {
            using (Stream consolestream = Console.OpenStandardOutput())
            {
                converter.Convert(traceresult, consolestream);
                /*using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(consolestream, Encoding.UTF8, ownsStream: true, indent: true, indentChars: "     "))
                {
                    jsonFormatter.WriteObject(jsonWriter, traceresult);
                }*/
            }
        }
    }
}

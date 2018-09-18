using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace TracerProgram
{
    public class XMLConverter:ITraceConverter
    {
        private DataContractJsonSerializer xmlconverter;
        private XmlWriterSettings xmlWriterSettings; 

        public XMLConverter()
        {
            xmlconverter = new DataContractJsonSerializer(typeof(TraceResult));
            xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            };
        }

        public void Convert(TraceResult traceresult)
        {
            using (FileStream fs = new FileStream("xml.dat", FileMode.Create))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(fs, xmlWriterSettings))
                {
                    xmlconverter.WriteObject(xmlWriter, traceresult);
                }
            }
        }

    }
}

using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace TracerProgram
{
    public class XMLConverter:ITraceConverter
    {
        private DataContractSerializer xmlconverter;
        private XmlWriterSettings xmlWriterSettings; 

        public XMLConverter()
        {            
            xmlconverter = new DataContractSerializer(typeof(TraceResult));
            xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            };
        }

        public void Convert(TraceResult traceresult,Stream stream)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
            {
                xmlconverter.WriteObject(xmlWriter, traceresult);
            }
        }

    }
}

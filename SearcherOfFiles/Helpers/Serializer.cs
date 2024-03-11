using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SearcherOfFiles.Helpers
{
    internal class Serializer
    {

        public Serializer() 
        {
            
        }

        public static void Serialize(List<Element> list, string file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Element>));
            string xml;
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, list);
                xml = stringWriter.ToString();
            }
            File.WriteAllText(file, xml, Encoding.Default);
        }

        public static List<Element> Deserialize(string file)
        {
            if(file == null) 
            {
                return null;
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Element>));
            List<Element> list;
            using (StreamReader sr = new StreamReader(file))
            {
                list = (List<Element>)xmlSerializer.Deserialize(sr);
            }
            return list;
        }

    }


}

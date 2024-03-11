using System.Xml.Serialization;

namespace SearcherOfFiles.Helpers
{
    internal static class ObjectHelper
    {
        public static void Serialize(object obj, string file)
        {
            if (obj == null)
            {
                return;
            }

            string dir = Path.GetDirectoryName(file);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter stringWriter = new StreamWriter(file))
            {
                new XmlSerializer(obj.GetType()).Serialize(stringWriter, obj);
            }            
        }

        public static T? Deserialize<T>(string? file) where T : class
        {
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file)) 
            {
                return null;
            }

            using (StreamReader sr = new StreamReader(file))
            {
                return new XmlSerializer(typeof(T)).Deserialize(sr) as T;
            }
        }
    }
}

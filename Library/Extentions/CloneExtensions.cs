using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Extentions
{
    public static class CloneExtensions
    {
        public static T Clone<T>(this T objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objSource);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}

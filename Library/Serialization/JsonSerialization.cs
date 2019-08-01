using Newtonsoft.Json;
using System.IO;

namespace Library.Serialization
{
    public static class JsonSerialization
    {
        public static string ToJson(this object model)
        {
            string result = "";
            var serializer = JsonSerializer.CreateDefault();
            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, model);
                result = sw.ToString();
            }
            return result;
        }

        public static T ToObject<T>(string json)
        {
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(T));
        }
    }
}

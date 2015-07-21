using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Meta.Net
{
    public static class SerializationExtensions
    {
        public static T DeepClone<T>(this T obj) where T : class
        {
            var serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null);
            using (var ms = new System.IO.MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                ms.Position = 0;
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string ToJson<T>(this T obj, Formatting formatting = Formatting.Indented) where T : class
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }
        
        public static T FromJson<T>(this string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Json;
using  System.Runtime.Serialization;
using System.Text;

namespace GBI.Metrix.Utility
{
    public static class JsonHelper
    {
        /// <summary>
        /// Json的序列化和反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string ToJson<T>(T instance)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var tempStream = new MemoryStream())
            {
                serializer.WriteObject(tempStream, instance);
                return Encoding.UTF8.GetString(tempStream.ToArray());
            }
        }
        public static T FromJson<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var tempStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)serializer.ReadObject(tempStream);
            }
        }
    }
}
using System;
using Newtonsoft.Json;

namespace SGAM.Elfec.Helpers.Utils
{
    /// <summary>
    /// Se encarga de copiar objetos en instancias diferentes
    /// </summary>
    public class ObjectCloner
    {
        /// <summary>
        /// Copia el objeto en uno nuevo es un DeepClone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source, JsonUtils.Settings);
            Console.WriteLine(serialized);
            return JsonConvert.DeserializeObject<T>(serialized, JsonUtils.Settings);
        }
    }
}
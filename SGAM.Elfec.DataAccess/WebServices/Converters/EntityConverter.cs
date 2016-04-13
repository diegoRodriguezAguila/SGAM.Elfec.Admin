using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using System;

namespace SGAM.Elfec.DataAccess.WebServices.Converters
{
    /// <summary>
    /// Conversor que permite convertir entidades <see cref="IEntity"/>
    /// a objetos de entidad como: <see cref="User"/> y <see cref="UserGroup"/>
    /// </summary>
    public class EntityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IEntity));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["entity_type"].Value<string>() == "User")
                return jo.ToObject<User>(serializer);
            if (jo["entity_type"].Value<string>() == "UserGroup")
                return jo.ToObject<UserGroup>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //throw new NotImplementedException();
        }
    }
}

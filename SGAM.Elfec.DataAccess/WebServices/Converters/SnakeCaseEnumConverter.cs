using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SGAM.Elfec.Helpers.Text;
using System;

namespace SGAM.Elfec.DataAccess.WebServices.Converters
{
    /// <summary>
    /// Converts an <see cref="Enum"/> to and from its name string value.
    /// </summary>
    public sealed class SnakeCaseEnumConverter : StringEnumConverter
    {

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Enum e = (Enum)value;

            string enumName = e.ToString("G");

            if (char.IsNumber(enumName[0]) || enumName[0] == '-')
            {
                writer.WriteValue(value);
            }
            else
            {
                string finalName = enumName.FromCamelToSnakeCase();
                writer.WriteValue(finalName);
            }
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (Nullable.GetUnderlyingType(objectType) != null)
                {
                    throw new JsonSerializationException(string
                        .Format("Cannot convert null value to {0}.", objectType));
                }

                return null;
            }

            bool isNullable = Nullable.GetUnderlyingType(objectType) != null;
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            try
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string enumText = reader.Value.ToString();
                    return Enum.Parse(t, enumText.FromSnakeToCamelCase(), true);
                }

                if (reader.TokenType == JsonToken.Integer)
                {
                    return base.ReadJson(reader, objectType, existingValue, serializer);
                }
            }
            catch (Exception ex)
            {
                new JsonSerializationException(string.Format("Error converting value {0} to type '{1}'.", reader.Value, objectType), ex);
            }

            // we don't actually expect to get here.
            throw new JsonSerializationException(string.Format("Unexpected token {0} when parsing enum.", reader.TokenType));

        }
    }
}

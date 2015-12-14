using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Helpers.Text
{
    /// <summary>
    /// Formatea las Listas de objetos en un mensaje en Lista 
    /// </summary>
    public class MessageListFormatter
    {

        /// <summary>
        /// Funcion que permite elegir la cadena mensaje a partir de un objeto cualquiera
        /// </summary>
        /// <typeparam name="T">tipo cualquiera del objeto</typeparam>
        /// <param name="obj">el objeto</param>
        /// <returns>cadena para agregar a los mensajes</returns>
        public delegate string AttributePickerDelegate<T>(T obj);

        /// <summary>
        /// Formatea una Lista de errores una Lista de errores en un mensaje listado
        /// </summary>
        /// <param name="errors">lista de errores</param>
        /// <returns>Lista de errores en mensaje</returns>
        public static String FormatFromErrorList(IList<Exception> errors)
        {
            return FormatFromObjectList(errors, (e)=> { return e.Message; });
        }

        /// <summary>
        /// Formatea una Lista de objetos una Lista (en cadena) en un mensaje listado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">Lista de objetos</param>
        /// <param name="attributePicker">Mensaje de Lista de objetos</param>
        /// <returns></returns>
        public static string FormatFromObjectList<T>(IList<T> objects,
                AttributePickerDelegate<T> attributePicker)
        {
            StringBuilder str = new StringBuilder();
            int size = objects.Count;
            if (size == 1)
                return str.Append(attributePicker(objects[0])).ToString();
            for (int i = 0; i < size; i++)
            {
                str.Append("\u25CF ").Append(
                        attributePicker(objects[i]));
                str.Append((i < size - 1 ? "\r\n" : ""));
            }
            return str.ToString();
        }

        /// <summary>
        /// Formatea una Lista de mensajes una Lista (en cadena) en un mensaje listado
        /// </summary>
        /// <param name="messages">Lista de mensajes</param>
        /// <returns>Mensaje de Lista de mensajes</returns>
        public static string FormatFromStringList(IList<string> messages)
        {
            return FormatFromObjectList(messages, (m) => { return m;});
        }
    }
}

using SGAM.Elfec.Helpers.Text;
using System.Collections.Generic;
using System.Text;

namespace SGAM.Elfec.Helpers.Utils
{
    /// <summary>
    /// Helper for collection operations
    /// </summary>
    public static class CollectionHelper
    {
        /// <summary>
        /// Adds all items from the collection to the other
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this IList<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }
        /// <summary>
        /// Removes all items from the collection to the other
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="items"></param>
        public static void RemoveRange<T>(this IList<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Remove(item);
            }
        }

        /// <summary>
        /// Gets a string list in the form of item1,item2,item3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static string ToStringRepresentation<T>(this IList<T> list,
            MessageListFormatter.AttributePickerDelegate<T> attributePicker)
        {
            if (list == null || list.Count == 0)
                return "";
            StringBuilder str = new StringBuilder();
            foreach (var item in list)
            {
                str.Append(attributePicker(item)).Append(",");
            }
            str.Append("$%&");
            str.Replace(",$%&", "");
            return str.ToString();
        }
    }
}

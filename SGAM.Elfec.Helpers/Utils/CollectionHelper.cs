using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        /// Adds all items from the collection to the other
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <param name="items"></param>
        public static void AddRange(this IList coll, IEnumerable items)
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
        public static string ToString<T>(this IList<T> list,
            Func<T, string> attributePicker)
        {
            return string.Join(",", list.Select(attributePicker));
        }

        /// <summary>
        /// Returns true if the list is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IList<T> list)
        {
            return list.Count == 0;
        }
    }
}

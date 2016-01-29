using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SGAM.Elfec.Helpers.Utils
{
    public static class CollectionHelper
    {
        public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }

        public static void RemoveRange<T>(this ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Remove(item);
            }
        }
    }
}

using SGAM.Elfec.Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace SGAM.Elfec.Presenters.Presentation.Collections
{
    /// <summary>
    /// Provides of extensions for ObservableCollections
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// The syncContext
        /// </summary>
        public static SynchronizationContext SyncContext { get; set; }
        /// <summary>
        /// Frecuencia con la que se cargan los datos (de cuantos en cuantos)
        /// </summary>
        public static int LoadFrequency { get; set; } = 25;
        /// <summary>
        /// Intervalo en milisegundos con el que se cargan los datos
        /// </summary>
        public static int LoadInterval { get; set; } = 84;

        /// <summary>
        /// Inicializa el SynContext para poder utilizar ciertos metodos de las extensiones
        /// </summary>
        public static void Init()
        {
            SyncContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Ordena y agrega el item en orden a la colección actual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <param name="orderby"></param>
        public static void AddInOrder<T, T2>(this IList<T> collection, T item, Func<T, T2> orderby)
        {
            collection.Add(item);
            var tempCol = collection.OrderBy(orderby).ToList();
            collection.Clear();
            collection.AddRange(tempCol);
        }

        /// <summary>
        /// Convierte asincronamente los elementos a una colección observable, 
        /// Primero crea una nueva <see cref="ObservableCollection{T}"/> con los primeros
        /// <see cref="LoadFrequency"/> elementos agregandolos de forma síncrona, luego en
        /// otro hilo agrega el resto de elementos en intervalos 
        /// de forma progresiva para no bloquear el UI thread, si la collección
        /// esta bindeada a alguna vista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obsCollection"></param>
        /// <param name="collection"></param>
        public static ObservableCollection<T> ToObservableCollectionAsync<T>(this IEnumerable<T> collection)
        {
            if (SyncContext == null)
                throw new InvalidOperationException("SyncContext was not initialized, please call Init in the Main thread in the Main Window");
            var obsCollection = new ObservableCollection<T>(collection.Take(LoadFrequency));
            int count = collection.Count(), freq = LoadFrequency;
            if (count > freq)
                new Thread(() =>
                {
                    for (int i = 1; i * freq < count; i++)
                    {
                        Thread.Sleep(LoadInterval);
                        SyncContext.Post((o) =>
                        {
                            obsCollection.AddRange(collection.Skip(i * freq).Take(freq));
                        }, null);
                    }
                }).Start();
            return obsCollection;
        }
    }
}

using System.Collections.ObjectModel;

namespace IconViewer.Logic.Extensions
{
    internal static class ObservableCollectionExtension
    {
        /// <summary>
        /// Checks if the <paramref name="collection"/> is Empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        internal static bool Empty<T>(this ObservableCollection<T> collection)
        {
            return collection.Count == 0 ? true : false;
        }

        /// <summary>
        /// Updates the existing Collection with <paramref name="newCollection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldCollection"></param>
        /// <param name="newCollection"></param>
        internal static void Update<T>(this ObservableCollection<T> oldCollection, ObservableCollection<T> newCollection)
        {
            if (oldCollection == null)
                oldCollection = new ObservableCollection<T>();

            oldCollection.Clear();
            oldCollection.AddRange(newCollection);
        }

        internal static void AddRange<T>(this ObservableCollection<T> list, ObservableCollection<T> argument)
        {
            foreach(var item in argument)
                list.Add(item);
        }
    }
}

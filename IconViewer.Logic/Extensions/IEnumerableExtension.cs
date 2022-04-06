using System.Collections.ObjectModel;

namespace IconViewer.Logic.Extensions
{
    internal static class IEnumerableExtension
    {
        internal static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> list)
        {
            return new ObservableCollection<T>(list);
        }
    }
}

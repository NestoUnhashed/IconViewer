using System.Collections.Specialized;

namespace IconViewer.Logic.Extensions
{
    internal static class StringExtensions
    {
        internal static bool Matches(this string value, string compareTo)
        {
            return value.ToLower().Contains(compareTo.ToLower());
        }
    }

    internal static class StringCollectionExtensions
    {
        internal static StringCollection SetFile(this StringCollection collection, string path)
        {
            collection.Clear();
            collection.Add(path);

            return collection;
        }
    }
}

using System.Collections.Specialized;

namespace IconViewer.Logic.Extensions
{
    internal static class StringExtensions
    {
        internal static bool Matches(this string value, string args)
        {
            return value.ToLower().Contains(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        internal static bool Contains(this string value, IEnumerable<string> args)
        {
            return args.Any(arg => value.Contains(arg));
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

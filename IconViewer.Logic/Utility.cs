using System.ComponentModel;
using System.IO;

namespace IconViewer.Logic
{
    internal static class Utility
    {
        internal static void OpenInExplorer(string path, FileType args)
        {
            string argument = GetExplorerArgsByFileType(path, args);
            System.Diagnostics.Process.Start(Apps.explorer, argument);
        }

        private static string GetExplorerArgsByFileType(string path, FileType type) => type switch
        {
            FileType.File => $"/select, {Path.GetFullPath(path)}",
            FileType.Folder => path,
            _ => throw new InvalidEnumArgumentException($"{nameof(FileType)}. The value {type} was not expected."),
        };
    }

    internal static class Apps
    {
        internal static string explorer = "explorer.exe";
    }

    internal enum FileType
    {
        Folder,
        File,
    }
}

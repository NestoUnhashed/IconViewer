using IconViewer.Logic.Extensions;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace IconViewer.Logic
{
    public class Icon : INotifyPropertyChanged
    {
        public string Name { get; private set; }
        public string FilePath { get; private set; }
        public string Data { get; set; }
        public string PathColor { get; set; }
        private readonly string? parentFolder;
        public bool IsValid = true;
        internal static StringCollection CopiedFile = new StringCollection();
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly DataContractSerializer serializer = new DataContractSerializer(typeof(XElement));

        private readonly ICommand copyFileCommand;
        public ICommand CopyFileCommand
        {
            get
            {
                return copyFileCommand ?? new RelayCommand(
                        x => true,
                        x => Clipboard.SetFileDropList(CopiedFile.SetFile(FilePath))
                    );
            }
        }

        private readonly ICommand copyDataCommand;
        public ICommand CopyDataCommand
        {
            get
            {
                return copyDataCommand ?? new RelayCommand(
                        x => true,
                        x => Clipboard.SetText(Data)
                    );
            }
        }

        private readonly ICommand openInExplorerCommand;
        public ICommand OpenInExplorerCommand
        {
            get
            {
                return openInExplorerCommand ?? new RelayCommand(
                        x => Directory.Exists(parentFolder),
                        x => Utility.OpenInExplorer(FilePath, FileType.File)
                    );
            }
        }

        public Icon(string path)
        {
            FilePath = path ?? throw new ArgumentNullException(nameof(path));
            Name = Path.GetFileNameWithoutExtension(FilePath).ToLower();
            Data = GetPathData();

            parentFolder = Path.GetDirectoryName(FilePath);
        }

        public Icon(string path, string color) : this(path)
        {
            FilePath = path ?? throw new ArgumentNullException(nameof(path));
            Name = Path.GetFileNameWithoutExtension(FilePath).ToLower();
            Data = GetPathData();

            parentFolder = Path.GetDirectoryName(FilePath);

            PathColor = color;
        }

        public string GetPathData()
        {
            using (XmlReader reader = XmlReader.Create(FilePath))
            {
                string data = String.Empty;

                try
                {
                    XElement svgData = serializer.ReadObject(reader) as XElement ?? throw new ArgumentNullException($"The File {FilePath} is not valid, make sure it contains a Path Tag.");
                    data = svgData.Element(XName.Get("path", @"http://www.w3.org/2000/svg")).Attribute(XName.Get("d")).Value;
                }
                catch (Exception)
                {
                    IsValid = false;
                }

                return data;
            }
        }

        public void RisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}

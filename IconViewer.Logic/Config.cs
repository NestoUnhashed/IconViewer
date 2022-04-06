using IconViewer.Logic.HelperClasses;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml;
using Xamarin.Forms.Dynamic;

namespace IconViewer.Logic
{
    [DataContract]
    public class Config
    {
        [DataMember]
        public ObservableDictionary<string, BooleanHelper> IconPaths { get; set; } = new ObservableDictionary<string, BooleanHelper>();
        [DataMember]
        public string DefaultColor { get; set; } = "#4d5959";

        private readonly string FileName;
        private readonly string CreatorFolder;
        private readonly string ProjectFolder;
        private string AppDataFolderPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private event EventHandler<BooleanHelper> DeletePath;

        private readonly DataContractSerializer serializer = new DataContractSerializer(typeof(Config));
        private readonly XmlWriterSettings settings = new XmlWriterSettings()
        {
            Indent = true,
            NewLineOnAttributes = true,
        };


        public Config()
        {
            CreatorFolder = Path.Combine(AppDataFolderPath, "ScrewsTools");
            ProjectFolder = Path.Combine(CreatorFolder, "IconViewer");
            FileName = Path.Combine(ProjectFolder, "IconViewer.config");

            RetrieveExistingConfig();
            DeletePath += RemoveRecord;
        }

        private void RemoveRecord(object? sender, BooleanHelper e)
        {
            if (e is null)
                throw new ArgumentNullException(nameof(e));

            IconPaths.Remove(IconPaths.First(kvp => kvp.Value == e).Key);
        }

        internal void AddPath(string fileName)
        {
            if(!IconPaths.ContainsKey(fileName))
            {
                IconPaths.Add(fileName, new BooleanHelper(true, IconPaths));
                IconPaths[fileName].SetCollection(IconPaths);
                IconPaths[fileName].SetConfig(this);
            }
        }

        private bool RetrieveExistingConfig()
        {
            if (!IsValid())
                return false;

            using (XmlReader reader = XmlReader.Create(FileName))
            {
                var config = (Config)serializer.ReadObject(reader);
                IconPaths = config.IconPaths;
                DefaultColor = config.DefaultColor;

                IconPaths.ToList().ForEach(path =>
                {
                    path.Value.SetCollection(IconPaths);
                    path.Value.SetConfig(this);
                });
            }

            return true;
        }

        public void UpdateConfig()
        {
            using (XmlWriter writer = XmlWriter.Create(FileName, settings))
            {
                serializer.WriteObject(writer, this);
            }
        }

        public bool IsValid()
        {
            return IsFolderValid(CreatorFolder) && IsFolderValid(ProjectFolder) && IsFileValid();
        }

        private bool IsFileValid()
        {
            try
            {
                if (!File.Exists(FileName))
                    UpdateConfig();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        private bool IsFolderValid(string folder)
        {
            DirectoryInfo directoryInfo;
            DirectorySecurity directorySecurity;
            AccessRule rule;
            SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            if (!Directory.Exists(folder))
            {
                directoryInfo = Directory.CreateDirectory(folder);
                bool modified;
                directorySecurity = directoryInfo.GetAccessControl();
                rule = new FileSystemAccessRule(
                        securityIdentifier,
                        FileSystemRights.Write |
                        FileSystemRights.ReadAndExecute |
                        FileSystemRights.Modify,
                        AccessControlType.Allow);

                directorySecurity.ModifyAccessRule(AccessControlModification.Add, rule, out modified);
                directoryInfo.SetAccessControl(directorySecurity);
            }

            return true;
        }

        internal void RiseDeletionEvent(BooleanHelper booleanHelper)
        {
            DeletePath?.Invoke(this, booleanHelper);
        }
    }

    internal class SerializerHelper
    {
        public ObservableDictionary<string, BooleanHelper> IconsPaths { get; set; }
        public string Color { get; set; }
    }
}

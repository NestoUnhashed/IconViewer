using IconViewer.Logic.Extensions;
using IconViewer.Logic.HelperClasses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Windows.Input;

namespace IconViewer.Logic
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //#4d5959 Default

        public ObservableCollection<Icon> Icons { get; private set; }
        public ColorManager ColorManager { get; private set; }
        public Settings Settings { get; private set; }

        private readonly Config config = new Config();
        public string DefaultColor { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;


        private int iconsCount;
        public int IconsCount
        {
            get { return iconsCount; }
            private set
            {
                iconsCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconsCount)));
            }
        }

        private string searchTerm = String.Empty;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                if (searchTerm == value)
                    return;

                searchTerm = value.ToLower();
                iconView.Refresh();
            }
        }

        private readonly ICommand updateIconsCommand;
        public ICommand UpdateIconsCommand
        {
            get
            {
                return updateIconsCommand ?? new RelayCommand(
                    x => true,
                    x => UpdateIcons(this, EventArgs.Empty)
                    );
            }
        }

        private readonly ICollectionView iconView;
        public ICollectionView IconView
        {
            get { return iconView; }
        }


        public MainWindowViewModel()
        {
            DefaultColor = config.DefaultColor;
            Settings = new Settings(config, DefaultColor);
            ColorManager = new ColorManager(DefaultColor);

            Icons = SearchForIconsIn(config.IconPaths);
            IconsCount = Icons.Count();
            iconView = CollectionViewSource.GetDefaultView(Icons);
            iconView.Filter = new Predicate<object>(x => string.IsNullOrWhiteSpace(SearchTerm) || ((Icon)x).Name.Matches(SearchTerm));

            config.IconPaths.CollectionChanged += UpdateIcons;

            if (Icons.Empty())
                return;

            ColorManager.SetIcons(Icons);
        }

        private void UpdateIcons(object? sender, EventArgs e)
        {
            Icons.Update(SearchForIconsIn(config.IconPaths));
            IconsCount = Icons.Count();

            if (Icons.Empty())
                return;

            ColorManager.SetIcons(Icons);
        }

        private ObservableCollection<Icon> SearchForIconsIn(ObservableDictionary<string, BooleanHelper> paths)
        {
            List<FileInfo>? files = new List<FileInfo>();

            paths.Where(path => path.Value.IsOn).ToList().ForEach(path =>
            {
                if (Directory.Exists(path.Key))
                {
                    files.AddRange(Directory.GetFiles(path.Key)
                     .Where(file => Path.GetExtension(file).ToLower() == ".svg")
                     .Select(vector => new FileInfo(vector)));

                    return;
                }

                path.Value.DeletePathCommand.Execute(path.Key);
                
                if(Icons == null)
                    Icons = new ObservableCollection<Icon>();

                UpdateIcons(this, null);
            });

            return files.Select(file => new Icon(file.FullName, ColorManager.Color)).Where(icon => icon.IsValid).ToObservable();
        }
    }

}

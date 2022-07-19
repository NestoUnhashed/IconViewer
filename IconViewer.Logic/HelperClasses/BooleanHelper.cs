using System.Windows.Input;

namespace IconViewer.Logic.HelperClasses
{
    public class BooleanHelper
    {
        private Config config;
        private ObservableDictionary<string, BooleanHelper> collection;


        private readonly ICommand openPathCommand;
        public ICommand OpenPathCommand => openPathCommand ?? new RelayCommand(
                    x => true,
                    x => Utility.OpenInExplorer(collection.First(kvp => kvp.Value == this).Key, FileType.Folder)
                    );

        private readonly ICommand deletePathCommand;
        public ICommand DeletePathCommand => deletePathCommand ?? new RelayCommand(
                    x => true,
                    x => config.RiseDeletionEvent(this)
                    );

        private bool isOn;
        public bool IsOn
        {
            get => isOn;
            set
            {
                if (isOn == value)
                {
                    return;
                }

                isOn = value;

                if (collection == null)
                {
                    return;
                }

                collection.RiseCollectionChanged();
            }
        }

        [Obsolete]
        public BooleanHelper()
        {

        }

        public void SetCollection(ObservableDictionary<string, BooleanHelper> collection)
        {
            this.collection = collection;
        }

        public void SetConfig(Config config)
        {
            this.config = config;
        }

        public BooleanHelper(bool value, ObservableDictionary<string, BooleanHelper> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            IsOn = value;
        }
    }
}

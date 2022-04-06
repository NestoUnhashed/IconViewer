using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;

namespace IconViewer.Logic
{
    public class Settings
    {
        public Config Config { get; private set; }

        private string color;
        public string Color
        {
            get { return color; }
            set 
            {
                if (color == value)
                    return;

                color = value;
            }
        }

        private readonly ICommand addPathCommand;
        public ICommand AddPathCommand
        {
            get
            {
                return addPathCommand ?? new RelayCommand(
                    x => true,
                    x => OpenFolderPicker()
                    );
            }
        }

        private readonly ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? new RelayCommand(
                    x => true,
                    x => SaveChanges()
                    );
            }
        }

        private void SaveChanges()
        {
            Config.DefaultColor = color;
            Config.UpdateConfig();
        }

        public Settings(Config config, string color)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }

        private void OpenFolderPicker()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                Title = "Select a Folder containing Icons",
                IsFolderPicker = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                EnsurePathExists = true,
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                return;

            Config.AddPath(dialog.FileName);
        }
    }
}

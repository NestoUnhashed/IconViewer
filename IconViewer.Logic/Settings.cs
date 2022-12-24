using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;

namespace IconViewer.Logic
{
    public class Settings
    {
        public Config Config { get; private set; }

        private readonly ICommand addPathCommand;
        public ICommand AddPathCommand => addPathCommand ?? new RelayCommand(
                    x => true,
                    x => OpenFolderPicker()
                    );

        private readonly ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ?? new RelayCommand(
                    x => true,
                    x => SaveChanges()
                    );

        private void SaveChanges()
        {
            Config.UpdateConfig();
        }

        public Settings(Config config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private void OpenFolderPicker()
        {
            CommonOpenFileDialog dialog = new()
            {
                Title = "Select a Folder containing Icons",
                IsFolderPicker = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                EnsurePathExists = true,
            };

            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return;
            }

            Config.AddPath(dialog.FileName);
        }
    }
}

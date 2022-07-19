using FullControls.SystemComponents;
using IconViewer.Logic;
using System.Windows;
using System.Windows.Input;

namespace IconViewer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AvalonWindow
    {
        private readonly Settings settings;
        private readonly MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            settings = new IconViewer.View.Settings(mainWindowViewModel.Settings);

            Settings_Frame.Content = settings;
        }

        private void mainPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = mainPanel.Focus();
        }

        private void ToggleButtonPlus_Checked(object sender, RoutedEventArgs e)
        {
            Settings_Frame.Visibility = Visibility.Visible;
        }

        private void ToggleButtonPlus_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings_Frame.Visibility = Visibility.Collapsed;
        }
    }
}

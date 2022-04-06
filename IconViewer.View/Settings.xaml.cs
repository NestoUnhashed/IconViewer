using System;
using System.Windows.Controls;

namespace IconViewer.View
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private readonly Logic.Settings settings;

        public Settings(Logic.Settings settings)
        {
            InitializeComponent();
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

            DataContext = this.settings;
        }
    }
}

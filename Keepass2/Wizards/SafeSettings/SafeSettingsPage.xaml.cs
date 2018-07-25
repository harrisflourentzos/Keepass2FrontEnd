using System.Windows.Controls;
using System.Windows.Input;

namespace Keepass2.Wizards.SafeSettings
{
    /// <summary>
    /// Interaction logic for SafeSettingsPage.xaml
    /// </summary>
    public partial class SafeSettingsPage : Page
    {
        public SafeSettingsPage()
        {
            InitializeComponent();
        }

        private void OnEditMP(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ChangeMPPage{DataContext = DataContext});
        }

        private void OnSecuritySettings(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new SecuritySettingsPage());
        }
    }
}

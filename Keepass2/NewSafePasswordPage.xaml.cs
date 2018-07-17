using Keepass2.Model;
using System.Windows;
using System.Windows.Controls;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for NewSafePasswordPage.xaml
    /// </summary>
    public partial class NewSafePasswordPage : Page
    {
        public NewSafePasswordPage()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((NewSafeState) DataContext).MasterPassword = ((PasswordBox)sender).SecurePassword;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newSafe = new Safe((NewSafeState)DataContext);

            NavigationService.Navigate(new SafeBrowserPage(newSafe));
        }
    }
}

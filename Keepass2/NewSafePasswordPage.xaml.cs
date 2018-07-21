using Keepass2.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Keepass2.Storage;
using Keepass2.Utilities;

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
            if (sender is PasswordBox)
            {
                ((NewSafeState)DataContext).MasterPassword = ((PasswordBox)sender).SecurePassword;
            }
            else
            {
                ((NewSafeState) DataContext).MasterPassword = Extensions.StringToSecureString(((TextBox)sender).Text);
            }

        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            var newSafe = new Safe((NewSafeState)DataContext);

            Repository.Instance = new JsonRepository();
            Repository.Instance.Save(newSafe);

            NavigationService.Navigate(new SafeBrowserPage(newSafe));
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new NewSafeLocationPage() { DataContext = DataContext });
        }

        private void OnRevealPassword(object sender, MouseButtonEventArgs e)
        {
            if (FakePasswordBox.Visibility == Visibility.Hidden)
            {
                FakePasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
                FakePasswordBox.Text = PasswordBox.Password;

                RepeatFakePasswordBox.Visibility = Visibility.Visible;
                RepeatPasswordBox.Visibility = Visibility.Hidden;
                RepeatFakePasswordBox.Text = RepeatPasswordBox.Password;
            }
            else
            {
                FakePasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Password = FakePasswordBox.Text;

                RepeatFakePasswordBox.Visibility = Visibility.Hidden;
                RepeatPasswordBox.Visibility = Visibility.Visible;
                RepeatPasswordBox.Password = RepeatFakePasswordBox.Text;
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Keepass2.Model;
using Keepass2.Utilities;

namespace Keepass2.Wizards.OpenSafe
{
    /// <summary>
    /// Interaction logic for OpenExistingSafePage.xaml
    /// </summary>
    public partial class OpenExistingSafePage : Page
    {
        public OpenExistingSafePage()
        {
            InitializeComponent();
        }

        private void OnDone(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == ((Safe)DataContext).Password.SecureStringToString() || FakePasswordBox.Text == ((Safe)DataContext).Password.SecureStringToString())
            {
                NavigationService.Navigate(new SafeBrowserPage((Safe)DataContext));
            }
            else
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new NewOrExistingPage());
        }

        private void OnTextInput(object sender, RoutedEventArgs e)
        {
            WrongPassTextBlock.Visibility = Visibility.Hidden;
        }

        private void OnRevealPassword(object sender, MouseButtonEventArgs e)
        {
            if (FakePasswordBox.Visibility == Visibility.Hidden)
            {
                FakePasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
                FakePasswordBox.Text = PasswordBox.Password;
            }
            else
            {
                FakePasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Password = FakePasswordBox.Text;
            }
        }
    }
}

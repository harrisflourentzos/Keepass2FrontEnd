using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Keepass2.Model;
using Keepass2.Storage;
using Keepass2.Utilities;

namespace Keepass2.Wizards.NewSafe
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
                PasswordBox.Password = ((TextBox)sender).Text;
                ((NewSafeState)DataContext).MasterPassword = ((TextBox)sender).Text.StringToSecureString();
            }

            ShowPasswordStrength(sender);
            WrongPassTextBlock.Visibility = Visibility.Hidden;

        }

        private void OnRepeatPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
            }
            else
            {
                RepeatPasswordBox.Password = ((TextBox)sender).Text;
                WrongPassTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            if (PasswordBox.Password == RepeatPasswordBox.Password)
            {
                var newSafe = new Safe((NewSafeState)DataContext);

                Repository.Instance = new JsonRepository();
                Repository.Instance.Save(newSafe);

                ((NewSafeState)DataContext).OnCompletion(newSafe);

                NavigationService.Navigate(new SafeBrowserPage(newSafe));
            }
            else
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
           
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new NewSafeLocationPage { DataContext = DataContext });
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

                RepeatFakePasswordBox.Visibility = Visibility.Hidden;
                RepeatPasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void ShowPasswordStrength(object sender)
        {
            var characters = new int();

            characters = sender is PasswordBox ? PasswordBox.Password.Length : FakePasswordBox.Text.Length;

            PasswordRectangle.Width = 8 * characters;
            PasswordRectangle.Opacity = 0.7;
            CharacterTextBlock.Text = "Characters: " + characters;

            if (characters <= 10)
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.Red);

            }
            else if (characters > 10 && characters <= 20)
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.Green);
            }
        }
    }
}

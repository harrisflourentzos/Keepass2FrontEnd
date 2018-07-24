using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Keepass2.Wizards.NewCredential
{
    /// <summary>
    /// Interaction logic for NewCredentialPage.xaml
    /// </summary>
    public partial class NewCredentialPage : Page
    {
        private bool _navigating;

        public NewCredentialPage()
        {
            InitializeComponent();
        }

        private void OnNext(object sender, MouseButtonEventArgs e)
        {
            if (PasswordBox.Password == RepeatPasswordBox.Password)
            {
                _navigating = true;
                NavigationService.Navigate(new NewCredentialPage2 { DataContext = DataContext });
            }
            else
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_navigating) return;

            if (sender is PasswordBox)
                ((NewCredentialState)DataContext).Credential.Password = ((PasswordBox)sender).Password;
            else
            {
                PasswordBox.Password = ((TextBox)sender).Text;
                ((NewCredentialState)DataContext).Credential.Password = ((TextBox)sender).Text;
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
            }
            WrongPassTextBlock.Visibility = Visibility.Hidden;
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

        private void OnCopyPassword(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(PasswordBox.Visibility == Visibility.Hidden
                ? PasswordBox.Password
                : FakePasswordBox.Text);
        }

        private void OnGeneratePassword(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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

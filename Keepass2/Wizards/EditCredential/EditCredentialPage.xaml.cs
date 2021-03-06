using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Keepass2.Wizards.EditCredential
{
    /// <summary>
    /// Interaction logic for EditCredentialPage.xaml
    /// </summary>
    public partial class EditCredentialPage : Page
    {
        public EditCredentialPage()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            if (PasswordBox.Password == RepeatPasswordBox.Password)
            {
                ((EditCredentialState)DataContext).NewCredential.UserName = UsernameTextBox.Text;
                ((EditCredentialState)DataContext).NewCredential.Title = TitleTextBox.Text;
                ((EditCredentialState)DataContext).NewCredential.Url = UrlTextBox.Text;
                ((EditCredentialState)DataContext).NewCredential.Notes = NotesTextBox.Text;

                ((EditCredentialState)DataContext).OnConfirm();
            }
            else
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var password = ((EditCredentialState) DataContext).OldCredential.Password;

            PasswordBox.Password = password;
            RepeatPasswordBox.Password = password;

            DataContextChanged -= OnDataContextChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
                ((EditCredentialState) DataContext).NewCredential.Password = ((PasswordBox)sender).Password;
            else
            {
                PasswordBox.Password = ((TextBox)sender).Text;
                ((EditCredentialState) DataContext).NewCredential.Password = ((TextBox) sender).Text;
            }

            ShowPasswordStrength(sender);
            WrongPassTextBlock.Visibility = Visibility.Hidden;
        }

        private void OnRepeatPasswordChange(object sender, RoutedEventArgs e)
        {
            if (!(sender is PasswordBox))
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
            Flyout.IsOpen = true;
        }

        private void ShowPasswordStrength(object sender)
        {
            var characters = new int();

            characters = sender is PasswordBox ? PasswordBox.Password.Length : FakePasswordBox.Text.Length;

            PasswordRectangle.Width = 8 * characters;
            PasswordRectangle.Opacity = 1;
            CharacterTextBlock.Text = "Characters: " + characters;

            if (characters == 0)
            {
                PasswordStrengthTextBlock.Visibility = Visibility.Hidden;
            }
            else if (characters <= 10)
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.Red);
                PasswordStrengthTextBlock.Text = "Weak";
                PasswordStrengthTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                PasswordStrengthTextBlock.Visibility = Visibility.Visible;

            }
            else if (characters > 10 && characters <= 20)
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.Orange);
                PasswordStrengthTextBlock.Text = "Medium";
                PasswordStrengthTextBlock.Foreground = new SolidColorBrush(Colors.Orange);
                PasswordStrengthTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordRectangle.Fill = new SolidColorBrush(Colors.LawnGreen);
                PasswordStrengthTextBlock.Text = "Strong";
                PasswordStrengthTextBlock.Foreground = new SolidColorBrush(Colors.LawnGreen);
                PasswordStrengthTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}

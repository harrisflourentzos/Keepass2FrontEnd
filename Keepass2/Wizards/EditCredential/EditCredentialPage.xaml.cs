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
            ((EditCredentialState) DataContext).NewCredential.UserName = UsernameTextBox.Text;
            ((EditCredentialState) DataContext).NewCredential.Title = TitleTextBox.Text;
            ((EditCredentialState) DataContext).NewCredential.Url = UrlTextBox.Text;
            ((EditCredentialState) DataContext).NewCredential.Notes = NotesTextBox.Text;

            ((EditCredentialState)DataContext).OnConfirm();
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
                ((EditCredentialState) DataContext).NewCredential.Password = ((TextBox) sender).Text;

            ShowPasswordStrength(sender);
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

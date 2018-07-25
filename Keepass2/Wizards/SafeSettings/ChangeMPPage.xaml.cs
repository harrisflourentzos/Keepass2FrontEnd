using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keepass2.Wizards.SafeSettings
{
    /// <summary>
    /// Interaction logic for ChangeMPPage.xaml
    /// </summary>
    public partial class ChangeMPPage : Page
    {
        public ChangeMPPage()
        {
            InitializeComponent();
        }

        private void OnOldPasswordChange(object sender, RoutedEventArgs routedEventArgs)
        {
            if (sender is PasswordBox)
            {
            }
            else
            {
                OldPasswordBox.Password = ((TextBox)sender).Text;
                WrongOldPassTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            if (PasswordBox.Password != RepeatPasswordBox.Password && OldPasswordBox.Password == ((ChangeMPState)DataContext).OldMp)
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
            else if (OldPasswordBox.Password != ((ChangeMPState)DataContext).OldMp && PasswordBox.Password == RepeatPasswordBox.Password)
            {
                WrongOldPassTextBlock.Visibility = Visibility.Visible;
            }
            else if (PasswordBox.Password != RepeatPasswordBox.Password && OldPasswordBox.Password != ((ChangeMPState)DataContext).OldMp)
            {
                WrongOldPassTextBlock.Visibility = Visibility.Visible;
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ((ChangeMPState)DataContext).NewMp = PasswordBox.Password;

                ((ChangeMPState)DataContext).OnConfirm();
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                ((ChangeMPState)DataContext).NewMp = ((PasswordBox)sender).Password;
            }
            else
            {
                PasswordBox.Password = ((TextBox)sender).Text;
                ((ChangeMPState)DataContext).NewMp = ((TextBox)sender).Text;
            }

            ShowPasswordStrength(sender);
            WrongPassTextBlock.Visibility = Visibility.Hidden;

        }

        private void OnRepeatPasswordChange(object sender, RoutedEventArgs e)
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

                FakeOldPasswordBox.Visibility = Visibility.Visible;
                OldPasswordBox.Visibility = Visibility.Hidden;
                FakeOldPasswordBox.Text = OldPasswordBox.Password;
            }
            else
            {
                FakePasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;

                RepeatFakePasswordBox.Visibility = Visibility.Hidden;
                RepeatPasswordBox.Visibility = Visibility.Visible;

                FakeOldPasswordBox.Visibility = Visibility.Hidden;
                OldPasswordBox.Visibility = Visibility.Visible;
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

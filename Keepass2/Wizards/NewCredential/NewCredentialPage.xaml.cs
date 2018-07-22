﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Keepass2.Wizards.NewCredential
{
    /// <summary>
    /// Interaction logic for NewCredentialPage.xaml
    /// </summary>
    public partial class NewCredentialPage : Page
    {
        public NewCredentialPage()
        {
            InitializeComponent();
        }

        private void OnNext(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new NewCredentialPage2 { DataContext = DataContext });
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((NewCredentialState)DataContext).Credential.Password = 
                sender is PasswordBox box ? box.Password : ((TextBox)sender).Text;
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
    }
}

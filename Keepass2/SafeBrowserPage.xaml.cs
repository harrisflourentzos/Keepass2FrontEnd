using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Keepass2.Model;
using Keepass2.Utilities;
using Keepass2.Wizards.EditCredential;
using Keepass2.Wizards.NewCredential;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for SafeBrowserPage.xaml
    /// </summary>
    public partial class SafeBrowserPage : Page
    {
        private readonly Safe _safe;

        public SafeBrowserPage(Safe safe)
        {
            _safe = safe;

            InitializeComponent();

            CategoriesListBox.ItemsSource = _safe.Groups;

            DataContext = safe;

            DisableNewCredentialButton();
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string)((ListBox)sender).SelectedItem;

            CredentialsListView.ItemsSource = category == null ? null : _safe[category];

            if (category == null) DisableNewCredentialButton();
            else EnableNewCredentialButton();
        }

        private void OnCreateNewCategory(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnDeleteCategory(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;

            if (category == null) return;

            _safe.RemoveGroup(category);
        }

        private void OnEditCategory(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnCreateNewCredential(object sender, MouseButtonEventArgs e)
        {
            var credential = new Credential();

            var frame = new Frame
            {
                Content = new NewCredentialPage
                {
                    DataContext = new NewCredentialState
                    {
                        Credential = credential,
                        OnConfirm = () => CreateNewCredential(credential)
                    }
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void CreateNewCredential(Credential credential)
        {
            var category = (string)CategoriesListBox.SelectedItem;

            _safe[category].Add(credential);

            Flyout.IsOpen = false;
        }

        private void OnEditCredential(object sender, RoutedEventArgs e)
        {
            var oldCredential = (Credential)((FrameworkElement)sender).DataContext;
            var newCredential = new Credential();

            var frame = new Frame
            {
                Content = new EditCredentialPage
                {
                    DataContext = new EditCredentialState
                    {
                        NewCredential = newCredential,
                        OldCredential = oldCredential,
                        OnConfirm = () => EditCredential(oldCredential, newCredential)
                    }
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void EditCredential(Credential oldCredential, Credential newCredential)
        {
            var category = (string)CategoriesListBox.SelectedItem;
            var credentials = _safe[category];
            var index = credentials.IndexOf(oldCredential);

            credentials.RemoveAt(index);
            credentials.Insert(index, newCredential);

            Flyout.IsOpen = false;
        }

        private void OnDeleteCredential(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;
            var credential = (Credential)((FrameworkElement)sender).DataContext;

            if (credential == null) return;

            _safe[category].Remove(credential);
        }

        private void OnCopyUserName(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;
            credential.UserName.CopyToClipboard();
        }

        private void OnCopyPassword(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;
            credential.Password.CopyToClipboard();
        }

        private void OnOpenUrl(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;

            try
            {
                System.Diagnostics.Process.Start(credential.Url);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        private void DisableNewCredentialButton()
        {
            NewCredentialButton.Style = FindResource("DisabledImage") as Style;
        }

        private void EnableNewCredentialButton()
        {
            NewCredentialButton.Style = FindResource("ClickableImage") as Style;
        }
    }
}

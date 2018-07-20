using System;
using System.Windows;
using System.Windows.Controls;
using Keepass2.Model;
using Keepass2.Utilities;

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
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string)((ListBox)sender).SelectedItem;

            CredentialsDataGrid.ItemsSource = category == null ? null : _safe[category];
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

        private void OnDeleteCredential(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;
            var credential = (Credential)CredentialsDataGrid.SelectedItem;

            if (credential == null) return;

            _safe[category].Remove(credential);
        }

        private void OnEditCredential(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnCopyUserName(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)CredentialsDataGrid.SelectedItem;
            credential.UserName.CopyToClipboard();
        }

        private void OnCopyPassword(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)CredentialsDataGrid.SelectedItem;
            credential.Password.CopyToClipboard();
        }

        private void OnOpenUrl(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)CredentialsDataGrid.SelectedItem;

            try
            {
                System.Diagnostics.Process.Start(credential.Url);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
            }
        }
    }
}

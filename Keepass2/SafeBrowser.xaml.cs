using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Keepass2.Model;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for SafeBrowser.xaml
    /// </summary>
    public partial class SafeBrowser : Window
    {
        private readonly Safe _safe;

        public SafeBrowser()
        {
            #region safe object
            _safe = new Safe();

            _safe.AddGroup("Internet");
            _safe.AddGroup("Bank");
            _safe.AddGroup("Windows");

            new List<Credential>
            {
                new Credential("Evernote", "keepass@gmail.com", "password1234", "evernote.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),

            }.ForEach(_safe["Internet"].Add);

            new List<Credential>
            {
                new Credential("BankofScottland", "keepass@gmail.com", "password1234", "evernote.com", "notes"),
                new Credential("BankofCyprus", "keepass@gmail.com", "password1234", "facebook.com", "notes")

            }.ForEach(_safe["Bank"].Add);
            #endregion

            InitializeComponent();

            CategoriesListBox.ItemsSource = _safe.Groups;
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string) ((ListBox) sender).SelectedItem;

            CredentialsDataGrid.ItemsSource = category == null ? null : _safe[category];
        }

        private void OnDeleteCategory(object sender, RoutedEventArgs e)
        {
            var category = (string) CategoriesListBox.SelectedItem;
            
            if (category == null) return;

            _safe.RemoveGroup(category);
        }

        private void OnEditCategory(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnDeleteCredential(object sender, RoutedEventArgs e)
        {
            var category = (string) CategoriesListBox.SelectedItem;
            var credential = (Credential)CredentialsDataGrid.SelectedItem;

            if (credential == null) return;

            _safe[category].Remove(credential);
        }

        private void OnEditCredential(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void OnCopyCredential(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}

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

            _safe["Internet"].AddRange(new List<Credential>
            {
                new Credential("Evernote", "keepass@gmail.com", "password1234", "evernote.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),
                new Credential("Facebook", "keepass@gmail.com", "password1234", "facebook.com", "notes"),

            });

            _safe["Bank"].AddRange(new List<Credential>
            {
                new Credential("BankofScottland", "keepass@gmail.com", "password1234", "evernote.com", "notes"),
                new Credential("BankofCyprus", "keepass@gmail.com", "password1234", "facebook.com", "notes")

            });
            #endregion

            InitializeComponent();

            CategoriesListBox.ItemsSource = _safe.Groups;
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string) ((ListBox) sender).SelectedItem;

            CredentialsDataGrid.ItemsSource = _safe[category];
        }
    }
}

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

        public SafeBrowser(Safe safe)
        {
            _safe = safe;
            InitializeComponent();

            CategoriesListBox.ItemsSource = safe.Groups;
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string)((ListBoxItem) ((ListBox) sender).SelectedItem).Content;

            CredentialsDataGrid.ItemsSource = _safe[category];
        }
    }
}

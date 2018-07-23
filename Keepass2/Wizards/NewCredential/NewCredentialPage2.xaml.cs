using System.Windows.Controls;
using System.Windows.Input;

namespace Keepass2.Wizards.NewCredential
{
    /// <summary>
    /// Interaction logic for NewCredentialPage2.xaml
    /// </summary>
    public partial class NewCredentialPage2 : Page
    {
        public NewCredentialPage2()
        {
            InitializeComponent();
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            ((NewCredentialState) DataContext).Credential.Title = TitleTextBox.Text;
            ((NewCredentialState) DataContext).Credential.Url = UrlTextBox.Text;
            ((NewCredentialState) DataContext).Credential.Notes = NotesTextBox.Text;

            ((NewCredentialState) DataContext).OnConfirm();
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new NewCredentialPage {DataContext = DataContext});
        }
    }
}

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
            ((NewCredentialState) DataContext).OnConfirm();
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

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

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for NewOrExistingPage.xaml
    /// </summary>
    public partial class NewOrExistingPage : Page
    {
        public NewOrExistingPage()
        {
            InitializeComponent();
        }

        private void OnCreateNew(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewSafeLocationPage(){DataContext = DataContext});
        }

        private void OnOpenExisting(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

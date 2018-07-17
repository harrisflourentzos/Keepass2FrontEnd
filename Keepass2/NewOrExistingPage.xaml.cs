using System;
using System.Windows;
using System.Windows.Controls;

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
            NavigationService.Navigate(new NewSafeLocationPage(){DataContext = new NewSafeState()});
        }

        private void OnOpenExisting(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

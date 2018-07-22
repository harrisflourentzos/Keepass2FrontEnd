using System;
using System.Windows;
using System.Windows.Controls;
using Keepass2.Storage;
using Keepass2.Wizards.NewSafe;
using Keepass2.Wizards.OpenSafe;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Keepass2.Wizards
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
            NavigationService.Navigate(new NewSafeLocationPage() { DataContext = new NewSafeState() });
        }

        private void OnOpenExisting(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Repository.Instance = new JsonRepository();
                NavigationService.Navigate(new OpenExistingSafePage() { DataContext = Repository.Instance.Load(dialog.FileName) });
            }
        }

    }
}

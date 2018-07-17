using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for NewSafeLocationPage.xaml
    /// </summary>
    public partial class NewSafeLocationPage : Page
    {
        public NewSafeLocationPage()
        {
            InitializeComponent();
        }

        private void OnLocationBrowse(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ((NewSafeState) DataContext).Location = dialog.FileName;
                LocationTextBlock.Text = dialog.FileName;
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewSafePasswordPage() { DataContext = DataContext });
        }
    }
}

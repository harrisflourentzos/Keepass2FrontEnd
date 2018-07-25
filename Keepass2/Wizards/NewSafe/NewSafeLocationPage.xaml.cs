using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Keepass2.Utilities;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Keepass2.Wizards.NewSafe
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
                ((NewSafeState)DataContext).Location = dialog.FileName;
                LocationTextBlock.Text = dialog.FileName;
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            var state = (NewSafeState) DataContext;
            var error = "";
            if (state.Name.IsNullOrEmpty()) error += "Please provide a name for your safe.\n";
            if (state.Location.IsNullOrEmpty()) error += "Please provide a suitable location to store your safe.";

            if (error == "")
            {
                NavigationService.Navigate(new NewSafePasswordPage { DataContext = DataContext });
            }
            else
            {
                MessageBox.Show(error, "Provide required information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnBack(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

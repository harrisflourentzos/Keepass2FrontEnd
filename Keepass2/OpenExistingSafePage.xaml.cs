using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using Keepass2.Model;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for OpenExistingSafePage.xaml
    /// </summary>
    public partial class OpenExistingSafePage : Page
    {
        public OpenExistingSafePage()
        {
            InitializeComponent();
        }

        private void OnDone(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == SecureStringToString(((Safe)DataContext).Password))
            {
                NavigationService.Navigate(new SafeBrowserPage((Safe)DataContext));
            }
            else
            {
                WrongPassTextBlock.Visibility = Visibility.Visible;
            }
        }

        public String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}

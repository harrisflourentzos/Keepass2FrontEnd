using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Keepass2.Wizards.NewCategory
{
    /// <summary>
    /// Interaction logic for NewCategoryPage.xaml
    /// </summary>
    public partial class NewCategoryPage : Page
    {
        public NewCategoryPage()
        {
            InitializeComponent();
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            var state = (NewCategoryState) DataContext;

            state.Category = CategoryName.Text;
            state.OnConfirm();
        }
    }
}

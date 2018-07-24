using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Keepass2.Wizards.EditCategory
{
    /// <summary>
    /// Interaction logic for EditCategoryPage.xaml
    /// </summary>
    public partial class EditCategoryPage : Page
    {
        public EditCategoryPage()
        {
            InitializeComponent();
        }

        private void OnDone(object sender, MouseButtonEventArgs e)
        {
            var state = (EditCategoryState)DataContext;

            state.NewCategory = CategoryName.Text;
            state.OnConfirm();
        }
    }
}

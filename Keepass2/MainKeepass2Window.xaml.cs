using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Keepass2.Model;
using Keepass2.Wizards;
using MahApps.Metro.Controls;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for MainKeepass2Window.xaml
    /// </summary>
    public partial class MainKeepass2Window : MetroWindow
    {
        private readonly List<TabItem> _tabItems = new List<TabItem>();
        private readonly TabItem _tabAdd = new TabItem { Header = "+" };

        public MainKeepass2Window()
        {
            InitializeComponent();

            _tabItems.Add(_tabAdd);

            // add first tab
            AddEmptyTab();

            // bind tab control
            SafeBrowser.DataContext = _tabItems;

            SafeBrowser.SelectedIndex = 0;
        }

        private void AddEmptyTab()
        {
            SafeBrowser.DataContext = null;
            
            // create new tab item
            var tab = new TabItem
            {
                Header = "Welcome",
                HeaderTemplate = SafeBrowser.FindResource("TabHeader") as DataTemplate
            };

            void OnSafeLoad(Safe safe)
            {
                tab.Header = safe.Name;
                tab.DataContext = safe;
            }

            // add controls to tab item
            var frame = new Frame { Content = new NewOrExistingPage{ DataContext = (Action<Safe>) OnSafeLoad}, NavigationUIVisibility = NavigationUIVisibility.Hidden };
            tab.Content = frame;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(_tabItems.Count - 1, tab);

            SafeBrowser.SelectedItem = tab;
            SafeBrowser.DataContext = _tabItems;
        }

        private void OnTabSelected(object sender, SelectionChangedEventArgs e)
        {
            if (!(SafeBrowser.SelectedItem is TabItem tab)) return;

            if (!tab.Equals(_tabAdd)) return;

            AddEmptyTab();
        }

        private void OnTabDeleted(object sender, RoutedEventArgs e)
        {
            var tab = (TabItem)((Button) sender).CommandParameter;

            if (tab == null) return;

            var pendingChanges = (tab.DataContext as Safe)?.PendingChanges ?? false;

            if (!pendingChanges || MessageBox.Show("There are unsaved changes to your safe. Would you like to discard your changes and close this tab?", "Unsaved Changes - Close Tab?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // get selected tab
                var selectedTab = SafeBrowser.SelectedItem as TabItem;

                // clear tab control binding
                SafeBrowser.DataContext = null;

                _tabItems.Remove(tab);

                // bind tab control
                SafeBrowser.DataContext = _tabItems;

                // select previously selected tab. if that is removed then select first tab
                if (selectedTab == null || selectedTab.Equals(tab))
                {
                    selectedTab = _tabItems[0];
                }

                SafeBrowser.SelectedItem = selectedTab;
            }

            if (_tabItems.Count <= 1)
            {
                AddEmptyTab();
            }
        }
    }
}

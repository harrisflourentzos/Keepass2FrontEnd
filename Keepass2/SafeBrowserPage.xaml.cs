using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Keepass2.Model;
using Keepass2.Storage;
using Keepass2.Utilities;
using Keepass2.Wizards.EditCategory;
using Keepass2.Wizards.EditCredential;
using Keepass2.Wizards.NewCategory;
using Keepass2.Wizards.NewCredential;
using Keepass2.Wizards.SafeSettings;

namespace Keepass2
{
    /// <summary>
    /// Interaction logic for SafeBrowserPage.xaml
    /// </summary>
    public partial class SafeBrowserPage : Page
    {
        private readonly Safe _safe;

        public SafeBrowserPage(Safe safe)
        {
            _safe = safe;

            InitializeComponent();

            CategoriesListBox.ItemsSource = _safe.Groups;

            DataContext = safe;

            DisableNewCredentialButton();
            DisableSaveButton();
        }

        private void OnCategorySelection(object sender, SelectionChangedEventArgs e)
        {
            var category = (string)((ListBox)sender).SelectedItem;

            CredentialsListView.ItemsSource = category == null ? null : _safe[category];

            if (category == null) DisableNewCredentialButton();
            else EnableNewCredentialButton();
        }

        private void OnCreateNewCategory(object sender, MouseButtonEventArgs e)
        {
            var state = new NewCategoryState();

            state.OnConfirm = () => CreateNewCategory(state);

            var frame = new Frame
            {
                Content = new NewCategoryPage
                {
                    DataContext = state
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void CreateNewCategory(NewCategoryState state)
        {
            _safe.AddGroup(state.Category);

            Flyout.IsOpen = false;

            EnableSaveButton();
        }

        private void OnDeleteCategory(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;

            if (category == null) return;

            _safe.RemoveGroup(category);

            EnableSaveButton();
        }

        private void OnEditCategory(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;

            if (category == null) return;

            var state = new EditCategoryState
            {
                OldCategory = category
            };

            state.OnConfirm = () => EditCategory(state);

            var frame = new Frame
            {
                Content = new EditCategoryPage
                {
                    DataContext = state
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void EditCategory(EditCategoryState state)
        {
            _safe.RenameGroup(state.OldCategory, state.NewCategory);

            Flyout.IsOpen = false;

            EnableSaveButton();
        }

        private void OnCreateNewCredential(object sender, MouseButtonEventArgs e)
        {
            var credential = new Credential();

            var frame = new Frame
            {
                Content = new NewCredentialPage
                {
                    DataContext = new NewCredentialState
                    {
                        Credential = credential,
                        OnConfirm = () => CreateNewCredential(credential)
                    }
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void CreateNewCredential(Credential credential)
        {
            var category = (string)CategoriesListBox.SelectedItem;

            _safe[category].Add(credential);

            Flyout.IsOpen = false;

            EnableSaveButton();
        }

        private void OnEditCredential(object sender, RoutedEventArgs e)
        {
            var oldCredential = (Credential)((FrameworkElement)sender).DataContext;
            var newCredential = new Credential();

            var frame = new Frame
            {
                Content = new EditCredentialPage
                {
                    DataContext = new EditCredentialState
                    {
                        NewCredential = newCredential,
                        OldCredential = oldCredential,
                        OnConfirm = () => EditCredential(oldCredential, newCredential)
                    }
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void EditCredential(Credential oldCredential, Credential newCredential)
        {
            var category = (string)CategoriesListBox.SelectedItem;
            var credentials = _safe[category];
            var index = credentials.IndexOf(oldCredential);

            credentials.RemoveAt(index);
            credentials.Insert(index, newCredential);

            Flyout.IsOpen = false;

            EnableSaveButton();
        }

        private void OnDeleteCredential(object sender, RoutedEventArgs e)
        {
            var category = (string)CategoriesListBox.SelectedItem;
            var credential = (Credential)((FrameworkElement)sender).DataContext;

            if (credential == null) return;

            _safe[category].Remove(credential);

            EnableSaveButton();
        }

        private void OnCopyUserName(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;
            credential.UserName.CopyToClipboard();
        }

        private void OnCopyPassword(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;
            credential.Password.CopyToClipboard();
        }

        private void OnOpenUrl(object sender, RoutedEventArgs e)
        {
            var credential = (Credential)((FrameworkElement)sender).DataContext;

            try
            {
                System.Diagnostics.Process.Start(credential.Url);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
            }
        }

        private void DisableNewCredentialButton()
        {
            NewCredentialButton.Style = FindResource("DisabledImage") as Style;
        }

        private void EnableNewCredentialButton()
        {
            NewCredentialButton.Style = FindResource("ClickableImage") as Style;
        }

        private void DisableSaveButton()
        {
            SaveButton.Style = FindResource("DisabledImage") as Style;
            _safe.PendingChanges = false;
        }

        private void EnableSaveButton()
        {
            SaveButton.Style = FindResource("ClickableImage") as Style;
            _safe.PendingChanges = true;
        }

        private void OnSave(object sender, MouseButtonEventArgs e)
        {
            Repository.Instance.Save(_safe);
            DisableSaveButton();
        }

        private void OnSafeSettings(object sender, MouseButtonEventArgs e)
        {
            var oldMp = ((Safe) DataContext).Password.SecureStringToString();
            var state = new ChangeMPState{ OldMp = oldMp};
            state.OnConfirm = () => ChangeMp(state);

            var frame = new Frame
            {
                Content = new SafeSettingsPage(){
                    DataContext = state
                },
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MaxWidth = 400,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Flyout.IsOpen = true;
            Flyout.Content = frame;
        }

        private void ChangeMp(ChangeMPState state)
        {
            _safe.Password = ((ChangeMPState)state).NewMp.StringToSecureString();

            Flyout.IsOpen = false;

            EnableSaveButton();
        }
    }
}

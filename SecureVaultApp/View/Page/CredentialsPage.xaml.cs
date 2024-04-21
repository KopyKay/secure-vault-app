using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;
using SecureVaultApp.Controls;
using System;

namespace SecureVaultApp.View.Page
{
    public sealed partial class CredentialsPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;

        public CredentialsPage()
        {
            this.InitializeComponent();

            _listViewButton.IsChecked = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton checkedToggleButton)
                return;

            var toggleButtons = _toggleButtons.Children.OfType<ToggleButton>();

            foreach (var toggleButton in toggleButtons)
            {
                toggleButton.IsChecked = toggleButton == checkedToggleButton;
                toggleButton.IsHitTestVisible = toggleButton != checkedToggleButton;
            }

            var gridView = (ItemsPanelTemplate)Resources["gridViewPanelTemplate"];
            var listView = (ItemsPanelTemplate)Resources["listViewPanelTemplate"];

            switch (checkedToggleButton.Name)
            {
                case "_gridViewButton":
                    _vaultFilesCollection.ItemsPanel = gridView;
                    break;
                case "_listViewButton":
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
                default:
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
            }
        }

        private async void AddCredentialButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Add new credential",
                Style = (Style)Application.Current.Resources["AddCredentialContentDialog"]
            };

            var result = await dialog.ShowAsync();

            dialog.PrimaryButtonClick += async (sender, args) =>
            {
                var domainApplicationNameTextBlock = dialog.FindName("domainApplicationName") as TextBlock;
                var emailTextBlock = dialog.FindName("email") as TextBlock;
                var loginTextBlock = dialog.FindName("login") as TextBlock;
                var passwordPasswordBox = dialog.FindName("password") as PasswordBox;

                var credential = new VaultCredential(domainApplicationNameTextBlock.Text);
            };
        }
    }
}

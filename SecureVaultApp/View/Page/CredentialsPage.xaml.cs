using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;
using SecureVaultApp.Controls;
using System;
using vault.Models;
using System.Collections.Generic;

namespace SecureVaultApp.View.Page
{
    public sealed partial class CredentialsPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;
        private List<Credential> _userCredentials;

        public CredentialsPage()
        {
            this.InitializeComponent();

            _listViewButton.IsChecked = true;
        }

        #region Helper methods
        private void CreateVaultCredentials()
        {
            foreach (var credential in _userCredentials)
            {
                var vaultCredential = new VaultCredential(credential, _appController);
                vaultCredential.DeleteClicked += VaultCredential_DeleteClicked;
                _vaultCredentialsCollection.Items.Add(vaultCredential);
            }
        }

        private void RemoveVaultCredential(VaultCredential vaultCredential)
        {
            _userCredentials.Remove(vaultCredential.credential);
            _vaultCredentialsCollection.Items.Remove(vaultCredential);
        }

        private async void OpenCredentialsContentDialog()
        {
            var dialog = new VaultCredentialContentDialog(_appController)
            {
                XamlRoot = this.XamlRoot,
                Title = "Add new credential",
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };
            dialog.AddButtonClicked += Dialog_AddButtonClicked;
            var result = await dialog.ShowAsync();
        }

        private void UploadCredential(VaultCredential vaultCredential)
        {
            vaultCredential.DeleteClicked += VaultCredential_DeleteClicked;
            _userCredentials.Add(vaultCredential.credential);
            _vaultCredentialsCollection.Items.Add(vaultCredential);
        }

        private void DisplayErrorDialog(string message)
        {
            _appController.DisplayErrorDialog(this.XamlRoot, message);
        }
        #endregion

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
                _userCredentials = await _appController.GetUserCredentialsAsync();

                CreateVaultCredentials();
            }
        }

        private async void VaultCredential_DeleteClicked(object sender, EventArgs e)
        {
            var vaultCredential = sender as VaultCredential;

            var isCredentialDeleted = await _appController.TryDeleteCredentialAsync(vaultCredential.id);

            if (isCredentialDeleted == false)
            {
                DisplayErrorDialog("Cannot delete this credential.");
                return;
            }
            else
            {
                RemoveVaultCredential(vaultCredential);
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
                    _vaultCredentialsCollection.ItemsPanel = gridView;
                    break;
                case "_listViewButton":
                    _vaultCredentialsCollection.ItemsPanel = listView;
                    break;
                default:
                    _vaultCredentialsCollection.ItemsPanel = listView;
                    break;
            }
        }

        private void AddCredentialButton_Click(object sender, RoutedEventArgs e)
        {
            OpenCredentialsContentDialog();
        }

        private void Dialog_AddButtonClicked(VaultCredential vaultCredential)
        {
            UploadCredential(vaultCredential);
        }
    }
}

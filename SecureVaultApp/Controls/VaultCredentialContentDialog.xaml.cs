using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using System;
using vault.Models;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredentialContentDialog : ContentDialog
    {
        private AppController _appController;

        public event AddButtonHandler AddButtonClicked;
        public delegate void AddButtonHandler(VaultCredential vaultCredential);

        public VaultCredentialContentDialog(XamlRoot xamlRoot, AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();

            XamlRoot = xamlRoot;
            Title = "Add new credential";
            PrimaryButtonText = "Add";
            CloseButtonText = "Cancel";
            DefaultButton = ContentDialogButton.Primary;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var applicationName = _appName.Text;
            var link = _link.Text;
            var login = _login.Text;
            var password = _password.Password;

            var credential = new Credential
            {
                App = applicationName,
                Login = login,
                Password = password,
                Link = link
            };

            var addedCredential = await _appController.UploadCredentialAsync(credential);

            if (addedCredential == null)
            {
                _appController.DisplayErrorDialog(XamlRoot, "Credential cannot be uploaded.");
            }

            var vaultCredential = new VaultCredential(addedCredential, _appController);

            AddButtonClicked?.Invoke(vaultCredential);
        }
    }
}

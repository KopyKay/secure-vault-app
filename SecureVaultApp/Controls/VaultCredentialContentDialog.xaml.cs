using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using vault.Models;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredentialContentDialog : ContentDialog
    {
        private AppController _appController;

        public event AddButtonHandler AddButtonClicked;
        public delegate void AddButtonHandler(VaultCredential vaultCredential);

        public VaultCredentialContentDialog(AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();

            _appName.TextChanged += TextBox_TextChanged;
            _login.TextChanged += TextBox_TextChanged;
            _password.PasswordChanged += PasswordBox_PasswordChanged;

            IsPrimaryButtonEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePrimaryButtonState();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePrimaryButtonState();
        }

        private void UpdatePrimaryButtonState()
        {
            var applicationName = !string.IsNullOrWhiteSpace(_appName.Text);
            var login = !string.IsNullOrWhiteSpace(_login.Text);
            var password = !string.IsNullOrWhiteSpace(_password.Password);

            IsPrimaryButtonEnabled = applicationName && login && password;
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

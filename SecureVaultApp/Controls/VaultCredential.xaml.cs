using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredential : UserControl
    {
        // private Credential credential;

        public VaultCredential(string name) // get data from Credential model
        {
            this.InitializeComponent();

            _credentialName.Text = name;
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Edit credential",
                Style = (Style)Application.Current.Resources["AddCredentialContentDialog"]
            };

            var result = await dialog.ShowAsync();

            dialog.Loaded += (sender, e) =>
            {
                var domainApplicationNameTextBlock = dialog.FindName("domainApplicationName") as TextBlock;
                var emailTextBlock = dialog.FindName("email") as TextBlock;
                var loginTextBlock = dialog.FindName("login") as TextBlock;
                var passwordPasswordBox = dialog.FindName("password") as PasswordBox;
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

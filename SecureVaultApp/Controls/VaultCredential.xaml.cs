using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredential : UserControl
    {
        public VaultCredential(string name)
        {
            this.InitializeComponent();

            _credentialName.Text = name;
        }

        private async void Edit_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Style = (Style)Application.Current.Resources["AddCredentialContentDialog"]
            };

            var result = await dialog.ShowAsync();

            dialog.Loaded += (sender, e) =>
            {
                var domainTextBlock = dialog.FindName("domainName") as TextBlock;
                var applicationTextBlock = dialog.FindName("applicationName") as TextBox;
                var emailTextBlock = dialog.FindName("email") as TextBlock;
                var loginTextBlock = dialog.FindName("login") as TextBlock;
                var passwordPasswordBox = dialog.FindName("password") as PasswordBox;
            };
        }

        private void Delete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

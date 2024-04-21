using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultAddCredential : UserControl
    {
        public VaultAddCredential()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Style = (Style)Application.Current.Resources["AddCredentialContentDialog"]
            };

            var result = await dialog.ShowAsync();

            dialog.PrimaryButtonClick += async (sender, args) =>
            {
                var domainTextBlock = dialog.FindName("_domainName") as TextBlock;
                var applicationTextBlock = dialog.FindName("_applicationName") as TextBox;
                var emailTextBlock = dialog.FindName("_email") as TextBlock;
                var loginTextBlock = dialog.FindName("_login") as TextBlock;
                var passwordPasswordBox = dialog.FindName("_password") as PasswordBox;
            };
        }
    }
}

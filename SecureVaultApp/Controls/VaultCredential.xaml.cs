using Microsoft.UI.Xaml.Controls;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredential : UserControl
    {
        public VaultCredential(string name)
        {
            this.InitializeComponent();

            _credentialName.Text = name;
        }

        private void Edit_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

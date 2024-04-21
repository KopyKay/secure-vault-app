using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using SecureVaultApp.Controller;

namespace SecureVaultApp.View.Page
{
    public sealed partial class UserAccountPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;

        public UserAccountPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

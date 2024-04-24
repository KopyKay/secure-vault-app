using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using SecureVaultApp.Controller;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;
using System.Diagnostics;
using vault.Models;
using SecureVaultApp.View.Window;
using System;

namespace SecureVaultApp.View.Page
{
    public sealed partial class UserAccountPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;
        private MainWindow _mainWindow;

        public UserAccountPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;

                var userAccount = await _appController.GetUserAccountAsync();
                if (userAccount != null)
                {
                    _accountName.Text = userAccount.Email;
                }
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            var newAuthWindow = new AuthorizationWindow(_appController);
            newAuthWindow.ExtendsContentIntoTitleBar = true;
            newAuthWindow.Activate();

            var mainWindow = AuthorizationWindow._mainWindow;
            mainWindow.Close();

            _appController.ResetToken();
        }
    }
}

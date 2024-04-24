using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using System;
using WinUIEx;

namespace SecureVaultApp.View.Window
{
    public sealed partial class AuthorizationWindow : WinUIEx.WindowEx
    {
        public static WindowEx _mainWindow { get; private set; } // Remove get; set; and try!
        private AppController _appController;

        public AuthorizationWindow(AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();
            this.CenterOnScreen();
            this.SetTitleBar(_customTitleBar);
        }

        private async void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            var xamlRoot = ((Button)sender).XamlRoot;

            var userEmail = signInEmailTextBox.Text;
            var userPassword = signInPasswordPassowrdBox.Password;

            var isUserSingedIn = await _appController.TryUserSignInAsync(userEmail, userPassword);

            if (isUserSingedIn == false)
            {
                _appController.DisplayErrorDialog(xamlRoot, "Cannot sign in, try again.");
                return;
            }

            ActivateMainWindow();

            this.Close();
        }

        private void ButtonDisplayCreateAccountContent_Click(object sender, RoutedEventArgs e)
        {
            ShowCreateAccountContent();
        }

        private async void ButtonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var xamlRoot = ((Button)sender).XamlRoot;

            var userEmail = createAccountEmailTextBox.Text;
            var userPassword1 = createAccountPasswordPasswordBox1.Password;
            var userPassword2 = createAccountPasswordPasswordBox2.Password;

            string[] inputs = { userEmail, userPassword1, userPassword2 };

            foreach (var input in inputs)
            {
                if (input.Length == 0)
                {
                    _appController.DisplayErrorDialog(xamlRoot, "Input cannot be blank!");
                    return;
                }
            }

            if (!userPassword1.Equals(userPassword2))
            {
                _appController.DisplayErrorDialog(xamlRoot, "Varied passwords, correct them again.");
                return;
            }

            var isAccountCreated = await _appController.TryCreateNewUserAsync(userEmail, userPassword1, userPassword2);

            if (isAccountCreated == false)
            {
                _appController.DisplayErrorDialog(xamlRoot, "Cannot create account, try again.");
                return;
            }

            ClearInputs();

            ShowSignInContent();
        }

        private void ButtonDisplaySignInContent_Click(object sender, RoutedEventArgs e)
        {
            ShowSignInContent();
        }

        private void ShowSignInContent()
        {
            _contentSwapAnimation.Begin();
            _signInContent.Visibility = Visibility.Visible;
            _createAccountContent.Visibility = Visibility.Collapsed;
        }

        private void ShowCreateAccountContent()
        {
            _contentSwapAnimation.Begin();
            _signInContent.Visibility = Visibility.Collapsed;
            _createAccountContent.Visibility = Visibility.Visible;
        }

        private void ClearInputs()
        {
            createAccountEmailTextBox.Text = "";
            createAccountPasswordPasswordBox1.Password = "";
            createAccountPasswordPasswordBox2.Password = "";
        }

        private void ActivateMainWindow()
        {
            _mainWindow = new MainWindow(_appController);
            _mainWindow.ExtendsContentIntoTitleBar = true;
            _mainWindow.Activate();
        }
    }
}

using Microsoft.UI.Xaml;
using SecureVaultApp.Controller;
using WinUIEx;

namespace SecureVaultApp.View.Window
{
    public sealed partial class AuthorizationWindow : WinUIEx.WindowEx
    {
        public static WindowEx _mainWindow { get; private set; }
        private AppController _appController = new();

        public AuthorizationWindow()
        {
            this.InitializeComponent();

            SetTitleBar(_customTitleBar);
            this.CenterOnScreen();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: display main window after successful sign in, and close this window
            //TODO: if signing in fails, display an error message

            _mainWindow = new MainWindow(this._appController);
            _mainWindow.ExtendsContentIntoTitleBar = true;
            _mainWindow.Activate();

            this.Close();
        }

        private void ButtonDisplayCreateAccountContent_Click(object sender, RoutedEventArgs e)
        {
            _contentSwapAnimation.Begin();
            _signInContent.Visibility = Visibility.Collapsed;
            _createAccountContent.Visibility = Visibility.Visible;
        }

        private void ButtonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            //TODO: move to sign-in content after successful registration
            //TODO: if registration fails, display an error message
        }

        private void ButtonDisplaySignInContent_Click(object sender, RoutedEventArgs e)
        {
            _contentSwapAnimation.Begin();
            _signInContent.Visibility = Visibility.Visible;
            _createAccountContent.Visibility = Visibility.Collapsed;
        }
    }
}

using Microsoft.UI.Xaml;
using WinUIEx;

namespace SecureVaultApp.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthorizationWindow : WinUIEx.WindowEx
    {
        private Window _mainWindow;

        public AuthorizationWindow()
        {
            this.InitializeComponent();

            SetTitleBar(gridAuthorizationWindowTitleBar);
            this.CenterOnScreen();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: display main window after successful sign in, and close this window
            //TODO: if signing in fails, display an error message

            _mainWindow = new MainWindow();
            _mainWindow.ExtendsContentIntoTitleBar = true;
            _mainWindow.Activate();

            this.Close();
        }

        private void ButtonDisplayCreateAccountContent_Click(object sender, RoutedEventArgs e)
        {
            contentSwapAnimation.Begin();
            gridSignInContent.Visibility = Visibility.Collapsed;
            gridCreateAccountContent.Visibility = Visibility.Visible;
        }

        private void ButtonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            //TODO: move to sign-in content after successful registration
            //TODO: if registration fails, display an error message
        }

        private void ButtonDisplaySignInContent_Click(object sender, RoutedEventArgs e)
        {
            contentSwapAnimation.Begin();
            gridSignInContent.Visibility = Visibility.Visible;
            gridCreateAccountContent.Visibility = Visibility.Collapsed;
        }
    }
}

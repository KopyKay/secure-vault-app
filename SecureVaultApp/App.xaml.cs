using Microsoft.UI.Xaml;
using SecureVaultApp.Controller;
using SecureVaultApp.View.Window;

namespace SecureVaultApp
{
    public partial class App : Application
    {
        private Window _authorizationWindow;
        private AppController _appController;

        public App()
        {
            _appController = new AppController();

            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _authorizationWindow = new AuthorizationWindow(_appController);
            _authorizationWindow.ExtendsContentIntoTitleBar = true;
            _authorizationWindow.Activate();
        }
    }
}

using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using SecureVaultApp.View.Page;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WinUIEx;

namespace SecureVaultApp.View.Window
{
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private AppController _appController;
        private string _timerColon = string.Empty;
        private CultureInfo _engCultureInfo = new("en-US");

        public MainWindow(AppController appController)
        {
            this._appController = appController;

            this.InitializeComponent();
            this.CenterOnScreen();
            this.SetTitleBar(_customTitleBar);
            this.StartClock();

            _frameWindowContent.Navigate(typeof(MyVaultPage));
        }

        private async void StartClock()
        {
            while (true)
            {
                RefreshTimeAndDate();
                await Task.Delay(1000);
            }
        }

        private void RefreshTimeAndDate()
        {
            _timerColon = (this._timerColon != " ") ? " " : ":"; // change the colon view every time you call RunTimeAndDate
            _titleBarClock.Text = DateTime.Now.ToString($"HH{this._timerColon}mm");
            _titleBarDate.Text = DateTime.Now.ToString("dddd dd.MM.yyyy", this._engCultureInfo);
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            _frameWindowContent.Navigate((string)item.Tag switch
            {
                "nviMyVault" => typeof(MyVaultPage),
                "nviUserAccount" => typeof(UserAccountPage),
                _ => typeof(MyVaultPage)
            });
        }
    }
}
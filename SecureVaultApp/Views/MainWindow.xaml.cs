using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Views;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using WinUIEx;

namespace SecureVaultApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private string _timerColon;
        private CultureInfo _engCultureInfo;

        public MainWindow()
        {
            this._timerColon = string.Empty;
            this._engCultureInfo = new CultureInfo("en-US");
            this.InitializeComponent();
            SetTitleBar(gridMainWindowTitleBar);
            StartClock();
            this.CenterOnScreen();
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
            this._timerColon = (this._timerColon != " ") ? " " : ":"; // change the colon view every time you call RunTimeAndDate
            textBlockTitleBarClock.Text = DateTime.Now.ToString($"HH{this._timerColon}mm");
            textBlockTitleBarDate.Text = DateTime.Now.ToString("dddd dd.MM.yyyy", this._engCultureInfo);
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            frameWindowContent.Navigate((string)item.Tag switch
            {
                "nviMyVault" => typeof(MyVaultPage),
                "nviUserAccount" => typeof(UserAccountPage),
                _ => typeof(UserAccountPage)
            });
        }
    }
}

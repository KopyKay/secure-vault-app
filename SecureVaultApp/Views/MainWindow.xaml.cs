using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace SecureVaultApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private string _timerColon;
        private CultureInfo _engCultureInfo;

        public MainWindow()
        {
            this._timerColon = string.Empty;
            this._engCultureInfo = new CultureInfo("en-US");

            this.InitializeComponent();
            SetTitleBar(windowTitleBar);
            StartClock();
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
            titleBarClock.Text = DateTime.Now.ToString($"HH{this._timerColon}mm");
            titleBarDate.Text = DateTime.Now.ToString("dddd dd.MM.yyyy", this._engCultureInfo);
        }
    }
}

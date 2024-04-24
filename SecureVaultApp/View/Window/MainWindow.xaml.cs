using api_access;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using SecureVaultApp.View.Page;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WinUIEx;

namespace SecureVaultApp.View.Window
{
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        public AppController _appController { get; private set; } // Remove get; set; and try!
        private static DateTime _now = DateTime.Now;
        private static CultureInfo _engCultureInfo = new("en-US");
        private string _timeNow = _now.ToString("HH:mm");
        private string _dateNow = _now.ToString("dddd dd.MM.yyyy", _engCultureInfo);

        public MainWindow(AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();
            this.CenterOnScreen();
            this.SetTitleBar(_customTitleBar);
            this.StartClock();

            _frameWindowContent.Navigate(typeof(FilesPage), _appController);
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
            _titleBarClock.Text = (DateTime.Now.Second % 2 == 0) ? _timeNow.Replace(":", " ") : _timeNow;
            _titleBarDate.Text = _dateNow;
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;
            Type currentPage = _frameWindowContent.SourcePageType;
            Type targetPage = (string)item.Tag switch
            {
                "nviVaultFiles" => typeof(FilesPage),
                "nviVaultCredentials" => typeof(CredentialsPage),
                "nviUserAccount" => typeof(UserAccountPage),
                _ => typeof(FilesPage)
            };

            if (targetPage == currentPage)
                return;

            _frameWindowContent.Navigate(targetPage, _appController);
        }
    }
}
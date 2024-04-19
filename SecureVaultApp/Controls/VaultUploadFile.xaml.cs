using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Converter;
using System;
using System.Diagnostics;
using Windows.Storage.Pickers;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultUploadFile : UserControl
    {
        private FileBlobConverter _converter;

        public VaultUploadFile()
        {
            _converter = new FileBlobConverter();
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            var window = View.Window.AuthorizationWindow._mainWindow;
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add("*");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                try
                {
                    var blob = _converter.SaveFileAsBlob(file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}

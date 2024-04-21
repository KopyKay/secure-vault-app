using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;
using SecureVaultApp.Converter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Storage.Pickers;

namespace SecureVaultApp.View.Page
{
    public sealed partial class FilesPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;
        private FileBlobConverter _converter;
        private List<string> _sortByOptions;

        public FilesPage()
        {
            _converter = new FileBlobConverter();

            this.InitializeComponent();

            _listViewButton.IsChecked = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
                _sortByOptions = _appController.sortByOptions;
            }
        }

        private void VaultComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton checkedToggleButton)
                return;

            var toggleButtons = _toggleButtons.Children.OfType<ToggleButton>();

            foreach (var toggleButton in toggleButtons)
            {
                toggleButton.IsChecked = toggleButton == checkedToggleButton;
                toggleButton.IsHitTestVisible = toggleButton != checkedToggleButton;
            }

            var gridView = (ItemsPanelTemplate)Resources["gridViewPanelTemplate"];
            var listView = (ItemsPanelTemplate)Resources["listViewPanelTemplate"];

            switch (checkedToggleButton.Name)
            {
                case "_gridViewButton":
                    _vaultFilesCollection.ItemsPanel = gridView;
                    break;
                case "_listViewButton":
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
                default:
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
            }
        }

        private async void UploadFileButton_Click(object sender, RoutedEventArgs e)
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

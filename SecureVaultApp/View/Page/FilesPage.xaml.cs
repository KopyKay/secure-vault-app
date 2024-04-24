using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;
using SecureVaultApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using vault.Models;

namespace SecureVaultApp.View.Page
{
    public sealed partial class FilesPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;
        private List<File> _userFiles;

        public FilesPage()
        {
            this.InitializeComponent();

            _listViewButton.IsChecked = true;
        }

        #region Helper methods
        private void CreateVaultFiles()
        {
            foreach (var file in _userFiles)
            {
                var vaultFile = new VaultFile(file, _appController);
                vaultFile.DeleteClicked += VaultFile_DeleteClicked;
                _vaultFilesCollection.Items.Add(vaultFile);
            }
        }

        private void RemoveVaultFile(VaultFile vaultFile)
        {
            _userFiles.Remove(vaultFile.file);
            _vaultFilesCollection.Items.Remove(vaultFile);
        }

        private void UploadFile(File file)
        {
            var vaultFile = new VaultFile(file, _appController);
            vaultFile.DeleteClicked += VaultFile_DeleteClicked;
            _userFiles.Add(file);
            _vaultFilesCollection.Items.Add(vaultFile);
        }

        private void DisplayErrorDialog(string message)
        {
            _appController.DisplayErrorDialog(this.XamlRoot, message);
        }
        #endregion

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
                _userFiles = await _appController.GetUserFilesAsync();

                CreateVaultFiles();
            }
        }

        private async void VaultFile_DeleteClicked(object sender, EventArgs e)
        {
            var vaultFile = sender as VaultFile;

            var isFileDeleted = await _appController.TryDeleteFileAsync(vaultFile.id);

            if (isFileDeleted == false)
            {
                DisplayErrorDialog("Cannot delete this file.");
                return;
            }
            else
            {
                RemoveVaultFile(vaultFile);
            }
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
           var pickedFile = await _appController.OpenFilePickerAsync();

            if (pickedFile != null)
            {
                try
                {
                    var blob = _appController.ConvertFileToBlob(pickedFile);

                    var file = new File
                    {
                        Name = pickedFile.Name,
                        Payload = blob
                    };

                    var uploadedFile = await _appController.UploadFileAsync(file);

                    if (uploadedFile == null)
                    {
                        DisplayErrorDialog("File cannot be uploaded.");
                    }
                    else
                    {
                        UploadFile(uploadedFile);
                    }
                }
                catch (Exception ex)
                {
                    DisplayErrorDialog($"Message: {ex.Message}");
                }
            }
        }
    }
}

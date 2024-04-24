using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using System;
using vault.Models;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFile : UserControl
    {
        private AppController _appController;
        public File file { get; private set; }
        public int id { get => this.file.Id; }

        public event EventHandler DeleteClicked;

        public VaultFile(File file, AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();

            this.file = file;
            _fileName.Text = $"{file.Name}";
            _fileSize.Text = GetFormattedFileSize((ulong)file.Payload.Length);
        }

        private string GetFormattedFileSize(ulong fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double sizeInBytes = fileSize;

            while (sizeInBytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                sizeInBytes /= 1024;
            }

            return $"{sizeInBytes:0.##} {sizes[order]}";
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            var folderPath = await _appController.GetFolderPathAsync();

            if (folderPath.Length == 0)
                return;

            _appController.ConvertBlobToFile(this.file.Payload, @$"{folderPath}\{this.file.Name}");
        }

        private void OnDeleteClicked()
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            OnDeleteClicked();
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFile : UserControl
    {
        // private File file;

        public VaultFile(StorageFile file) // get data from File model
        {
            this.InitializeComponent();

            _fileName.Text = $"{file.Name}.{file.FileType}";
            _fileSize.Text = GetFileSizeAsync(file).ToString();
            _fileLastModification.Text = file.DateCreated.ToString("dd.MM.yyyy HH:mm");
        }

        #region File model methods - pass it later
        private async Task<string> GetFileSizeAsync(StorageFile file)
        {
            var fileProperties = await file.GetBasicPropertiesAsync();
            var fileSize = fileProperties.Size;

            return GetFormattedFileSize(fileSize);
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
        #endregion

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

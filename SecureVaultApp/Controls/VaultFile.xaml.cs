using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFile : UserControl
    {
        public VaultFile(string fileName, uint fileSize, DateTime fileLastModification)
        {
            this.InitializeComponent();

            _fileName.Text = fileName;
            _fileSize.Text = fileSize.ToString();
            _fileLastModification.Text = fileLastModification.ToString("dd.MM.yyyy HH:mm");
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

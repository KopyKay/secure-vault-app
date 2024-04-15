using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFile : UserControl
    {
        public VaultFile(string fileName, uint fileSize, DateTime fileLastModification)
        {
            this.fileName.Text = fileName;
            this.fileSize.Text = fileSize.ToString();
            this.fileLastModification.Text = fileLastModification.ToString("dd.MM.yyyy HH:mm");

            this.InitializeComponent();
        }
    }
}

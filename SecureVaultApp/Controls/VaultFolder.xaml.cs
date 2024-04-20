using Microsoft.UI.Xaml.Controls;
using System;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFolder : UserControl
    {
        public VaultFolder(string folderName, string folderDetails, DateTime folderLastModification)
        {
            this.InitializeComponent();

            _folderName.Text = folderName;
            _folderDetails.Text = folderDetails;
            _folderLastModification.Text = folderLastModification.ToString("dd.MM.yyyy HH:mm");
        }
    }
}

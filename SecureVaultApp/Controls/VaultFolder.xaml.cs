using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.StartScreen;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultFolder : UserControl
    {
        public VaultFolder(string folderName, string folderDetails, DateTime folderLastModification)
        {
            this.folderName.Text = folderName;
            this.folderDetails.Text = folderDetails;
            this.folderLastModification.Text = folderLastModification.ToString("dd.MM.yyyy HH:mm");

            this.InitializeComponent();
        }
    }
}

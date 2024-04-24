using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SecureVaultApp.Controller;
using System;
using vault.Models;

namespace SecureVaultApp.Controls
{
    public sealed partial class VaultCredential : UserControl
    {
        private AppController _appController;
        public Credential credential { get; private set; }
        public int id { get => this.credential.Id; }

        public EventHandler DeleteClicked;

        public VaultCredential(Credential credential, AppController appController)
        {
            _appController = appController;

            this.InitializeComponent();

            this.credential = credential;
            _credentialName.Text = credential.App;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
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

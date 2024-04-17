using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace SecureVaultApp.View.Page
{
    public sealed partial class MyVaultPage : Microsoft.UI.Xaml.Controls.Page
    {
        public List<string> sortByOptions { get; } = new List<string>()
        {
            "Date", "Size", "Type", "Name"
        };

        public MyVaultPage()
        {
            this.InitializeComponent();
            this._listViewButton.IsChecked = true;
        }

        private void VaultComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewOptionToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (clickedButton == null)
                return;

            if (clickedButton == _folderViewButton)
            {
                if (_gridViewButton.IsChecked == true || _listViewButton.IsChecked == true)
                {
                    _gridViewButton.IsChecked = false;
                    _listViewButton.IsChecked = false;
                }
            }
            else if (clickedButton == _gridViewButton)
            {
                if (_gridViewButton.IsChecked == true || _listViewButton.IsChecked == true)
                {
                    _folderViewButton.IsChecked = false;
                    _listViewButton.IsChecked = false;
                }

                _vaultFilesCollection.ItemsPanel = (ItemsPanelTemplate)Resources["gridViewPanelTemplate"];
            }
            else if (clickedButton == _listViewButton)
            {
                if (_gridViewButton.IsChecked == true || _listViewButton.IsChecked == true)
                {
                    _folderViewButton.IsChecked = false;
                    _gridViewButton.IsChecked = false;
                }

                _vaultFilesCollection.ItemsPanel = (ItemsPanelTemplate)Resources["listViewPanelTemplate"];
            }
        }
    }
}

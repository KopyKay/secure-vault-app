using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls.Primitives;
using SecureVaultApp.Controls;

namespace SecureVaultApp.View.Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyVaultPage : Microsoft.UI.Xaml.Controls.Page
    {
        public List<string> sortByOptions { get; } = new List<string>()
        {
            "Date", "Size", "Type", "Name"
        };

        public MyVaultPage()
        {
            this.InitializeComponent();
            this.listViewButton.IsChecked = true;
        }

        private void MyVaultPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewOptionToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (clickedButton == null)
                return;

            if (clickedButton == folderViewButton)
            {
                if (gridViewButton.IsChecked == true || listViewButton.IsChecked == true)
                {
                    gridViewButton.IsChecked = false;
                    listViewButton.IsChecked = false;
                }
            }
            else if (clickedButton == gridViewButton)
            {
                if (folderViewButton.IsChecked == true || listViewButton.IsChecked == true)
                {
                    folderViewButton.IsChecked = false;
                    listViewButton.IsChecked = false;
                }

                myVaultPageFiles.ItemsPanel = (ItemsPanelTemplate)Resources["gridViewPanelTemplate"];
            }
            else if (clickedButton == listViewButton)
            {
                if (folderViewButton.IsChecked == true || gridViewButton.IsChecked == true)
                {
                    folderViewButton.IsChecked = false;
                    gridViewButton.IsChecked = false;
                }

                myVaultPageFiles.ItemsPanel = (ItemsPanelTemplate)Resources["listViewPanelTemplate"];
            }
        }
    }
}

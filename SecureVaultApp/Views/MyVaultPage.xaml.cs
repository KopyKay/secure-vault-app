using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace SecureVaultApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyVaultPage : Page
    {
        public List<string> sortByOptions { get; } = new List<string>()
        {
            "Date", "Size", "Type", "Name"
        };

        public MyVaultPage()
        {
            this.InitializeComponent();
        }

        private void myVaultPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void folderViewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gridViewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

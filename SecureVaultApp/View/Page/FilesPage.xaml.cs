using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;
using System.Collections.Generic;
using System.Linq;

namespace SecureVaultApp.View.Page
{
    public sealed partial class FilesPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;
        private List<string> _sortByOptions;

        public FilesPage()
        {
            this.InitializeComponent();

            _listViewButton.IsChecked = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppController appController)
            {
                _appController = appController;
                _sortByOptions = _appController.sortByOptions;
            }
        }

        private void VaultComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleButton checkedToggleButton)
                return;

            var toggleButtons = _toggleButtons.Children.OfType<ToggleButton>();

            foreach (var toggleButton in toggleButtons)
            {
                toggleButton.IsChecked = toggleButton == checkedToggleButton;
                toggleButton.IsHitTestVisible = toggleButton != checkedToggleButton;
            }

            var gridView = (ItemsPanelTemplate)Resources["gridViewPanelTemplate"];
            var listView = (ItemsPanelTemplate)Resources["listViewPanelTemplate"];

            switch (checkedToggleButton.Name)
            {
                case "_gridViewButton":
                    _vaultFilesCollection.ItemsPanel = gridView;
                    break;
                case "_listViewButton":
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
                default:
                    _vaultFilesCollection.ItemsPanel = listView;
                    break;
            }
        }
    }
}

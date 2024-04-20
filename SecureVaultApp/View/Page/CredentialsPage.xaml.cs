using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Microsoft.UI.Xaml.Navigation;
using SecureVaultApp.Controller;

namespace SecureVaultApp.View.Page
{
    public sealed partial class CredentialsPage : Microsoft.UI.Xaml.Controls.Page
    {
        private AppController _appController;

        public CredentialsPage()
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
            }
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

using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace SecureVaultApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserAccountPage : Page
    {
        private SolidColorBrush lightGrayBrush = new SolidColorBrush(Colors.LightGray);
        private SolidColorBrush transparentBrush = new SolidColorBrush(Colors.Transparent);

        public UserAccountPage()
        {
            this.InitializeComponent();
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var button = sender as Button;
            button.Background = lightGrayBrush;
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var button = sender as Button;
            button.Background = transparentBrush;
        }
    }
}

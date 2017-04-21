using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow; // If not a static method, this.MainWindow would work
            main.ToggleFlyout(1);
        }
    }
}

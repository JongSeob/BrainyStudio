using System.Windows;
using System.Windows.Controls;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start Recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Set up and start Recording
            ((MainWindow)Application.Current.MainWindow).StartNewRecording(RecordingName.Text, RecordingDescription.Text);

            //Disable controls during the recording
            RecordButton.IsEnabled = false;
            SubjectProfile.IsEnabled = false;
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private System.Windows.Threading.DispatcherTimer HeartBeat = new System.Windows.Threading.DispatcherTimer();

        public HomeView()
        {
            InitializeComponent();

            //Set up update frequency
            HeartBeat.Tick += new EventHandler(HeartBeat_Tick);
            HeartBeat.Interval = new TimeSpan(0, 0, 0, 0, 2000);
            HeartBeat.Start();
        }

        private void HeartBeat_Tick(object sender, EventArgs e)
        {
            // Get Battery, Signal info
            try
            {

                if (((MainWindow)Application.Current.MainWindow)._engine.HardwareGetVersion(0) < 9999 
                    && ((MainWindow)Application.Current.MainWindow)._engine.HardwareGetVersion(0) > 0)
                    HeadsetVersion.Content = "Dongle Found, No Signal to headset" ;
                else if (((MainWindow)Application.Current.MainWindow)._engine.HardwareGetVersion(0) > 10000)
                    HeadsetVersion.Content = "Connected to headset" + " (S/N: " + ((MainWindow)Application.Current.MainWindow)._engine.HardwareGetVersion(0).ToString() + ")";

                ((MainWindow)Application.Current.MainWindow)._es.GetBatteryChargeLevel(out int current, out int max);
                BatteryLevel.Value = current;
                BatteryLevel.Maximum = max;

                //Update signal display
                var signal = ((MainWindow)Application.Current.MainWindow)._es.GetWirelessSignalStatus();
                if (signal == Emotiv.EdkDll.EE_SignalStrength_t.GOOD_SIGNAL) SignalLevel.Value = 100;
                if (signal == Emotiv.EdkDll.EE_SignalStrength_t.BAD_SIGNAL) SignalLevel.Value = 50;
                if (signal == Emotiv.EdkDll.EE_SignalStrength_t.BAD_SIGNAL) SignalLevel.Value = 0;
            }
            catch (Exception)
            { }
        }
    }
}
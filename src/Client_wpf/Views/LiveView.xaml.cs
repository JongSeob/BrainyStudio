using Emotiv;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFapp;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;

        // Chart Models - RAW
        public ChartValues<MeasureModel> ChartAF3 { get; set; }

        public ChartValues<MeasureModel> ChartF7 { get; set; }
        public ChartValues<MeasureModel> ChartF3 { get; set; }
        public ChartValues<MeasureModel> ChartFC5 { get; set; }
        public ChartValues<MeasureModel> ChartT7 { get; set; }
        public ChartValues<MeasureModel> ChartP7 { get; set; }
        public ChartValues<MeasureModel> ChartO1 { get; set; }
        public ChartValues<MeasureModel> ChartO2 { get; set; }
        public ChartValues<MeasureModel> ChartP8 { get; set; }
        public ChartValues<MeasureModel> ChartT8 { get; set; }
        public ChartValues<MeasureModel> ChartFC6 { get; set; }
        public ChartValues<MeasureModel> ChartF4 { get; set; }
        public ChartValues<MeasureModel> ChartF8 { get; set; }
        public ChartValues<MeasureModel> ChartAF4 { get; set; }

        // Axis
        public Func<double, string> DateTimeFormatter { get; set; }

        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        private System.Windows.Threading.DispatcherTimer HeartBeat = new System.Windows.Threading.DispatcherTimer();

        public AboutView()
        {
            InitializeComponent();

            //Set up update frequency
            HeartBeat.Tick += new EventHandler(HeartBeat_Tick);
            HeartBeat.Interval = new TimeSpan(0, 0, 0, 0, 500);
            HeartBeat.Start();

            CartChart.AnimationsSpeed = TimeSpan.FromMilliseconds(500);

            //To handle live data easily, in this case we built a specialized type
            //the MeasureModel class, it only contains 2 properties
            //DateTime and Value
            //We need to configure LiveCharts to handle MeasureModel class
            //The next code configures MeasureModel  globally, this means
            //that LiveCharts learns to plot MeasureModel and will use this config every time
            //a IChartValues instance uses this type.
            //this code ideally should only run once
            //you can configure series in many ways, learn more at
            //http://lvcharts.net/App/examples/v1/wpf/Types%20and%20Configuration

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            ChartAF3 = new ChartValues<MeasureModel>();
            ChartAF4 = new ChartValues<MeasureModel>();
            ChartF3 = new ChartValues<MeasureModel>();
            ChartF4 = new ChartValues<MeasureModel>();
            ChartF7 = new ChartValues<MeasureModel>();
            ChartF8 = new ChartValues<MeasureModel>();
            ChartFC5 = new ChartValues<MeasureModel>();
            ChartFC6 = new ChartValues<MeasureModel>();
            ChartO1 = new ChartValues<MeasureModel>();
            ChartO2 = new ChartValues<MeasureModel>();
            ChartP7 = new ChartValues<MeasureModel>();
            ChartP8 = new ChartValues<MeasureModel>();
            ChartT7 = new ChartValues<MeasureModel>();
            ChartT8 = new ChartValues<MeasureModel>();

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString("ss");

            //AxisStep forces the distance between each separator in the X axis
            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);

            //The next code simulates data changes every 300 ms

            IsReading = false;
            DataContext = this;
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }

        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        public bool IsReading { get; set; }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks;
            AxisMin = now.Ticks - TimeSpan.FromSeconds(3).Ticks;
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged implementation

        private void HeartBeat_Tick(object sender, EventArgs e)
        {
            try
            {
                while (true)
                {
                    Dictionary<EdkDll.EE_DataChannel_t, double[]> data =
                       ((MainWindow)Application.Current.MainWindow)._engine.GetData((uint)0);

                    var now = DateTime.Now;

                    //Append RAW values
                    ChartF3.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.F3][0] });
                    ChartF4.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.F4][0] });
                    ChartF7.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.F7][0] });
                    ChartF8.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.F8][0] });
                    ChartAF3.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.AF3][0] });
                    ChartAF4.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.AF4][0] });
                    ChartP7.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.P7][0] });
                    ChartP8.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.P8][0] });
                    ChartO1.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.O1][0] });
                    ChartO2.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.O2][0] });
                    ChartT7.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.T7][0] });
                    ChartT8.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.T8][0] });
                    ChartFC5.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.FC5][0] });
                    ChartFC6.Add(new MeasureModel { DateTime = now, Value = data[EdkDll.EE_DataChannel_t.FC6][0] });

                    SetAxisLimits(now);

                    //lets only use the last 150 values
                    if (ChartP7.Count > 20)
                    {
                        ChartF3.RemoveAt(0);
                        ChartF4.RemoveAt(0);
                        ChartF7.RemoveAt(0);
                        ChartF8.RemoveAt(0);
                        ChartAF3.RemoveAt(0);
                        ChartAF4.RemoveAt(0);
                        ChartP7.RemoveAt(0);
                        ChartP8.RemoveAt(0);
                        ChartO1.RemoveAt(0);
                        ChartO2.RemoveAt(0);
                        ChartT7.RemoveAt(0);
                        ChartT8.RemoveAt(0);
                        ChartFC5.RemoveAt(0);
                        ChartFC6.RemoveAt(0);
                    }
                }
            }
            catch
            { }
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void RawData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
    }
}
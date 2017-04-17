using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using System.Windows;
using Emotiv;
using System.Collections.Generic;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        System.Windows.Threading.DispatcherTimer HeartBeat = new System.Windows.Threading.DispatcherTimer();
        
        public AboutView()
        {
            InitializeComponent();

            //Set up update frequency
            HeartBeat.Tick += new EventHandler(HeartBeat_Tick);
            HeartBeat.Interval = new TimeSpan(0, 0, 1);
            HeartBeat.Start();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");
            DataContext = this;

        }

        private void HeartBeat_Tick(object sender, EventArgs e)
        {
            try
            {
                //Fetch data from the EMOTIV Engine
                Dictionary<EdkDll.EE_DataChannel_t, double[]> data = 
                    ((MainWindow)Application.Current.MainWindow)._engine.GetData((uint)0);

                //Add to graph
                SeriesCollection[2].Values.Add(data[EdkDll.EE_DataChannel_t.F8][0]);
            }
            catch
            { }

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}

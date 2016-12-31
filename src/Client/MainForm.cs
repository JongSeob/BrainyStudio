using Emotiv;
using Newtonsoft.Json;
using rtChart;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class RecorderForm : Form
    {
        /// <summary>
        /// Emoengine global values
        /// </summary>
        private EmoEngine _engine = EmoEngine.Instance;
        EmoState _es;

        // Recorder stopwatch
        Stopwatch recorder_stopwatch = new Stopwatch();

        private int _userID;

        #region Raw sensor data charts

        private kayChart _kChart_af3;
        private kayChart _kChart_f7;
        private kayChart _kChart_f3;
        private kayChart _kChart_fc5;
        private kayChart _kChart_t7;
        private kayChart _kChart_p7;
        private kayChart _kChart_o1;
        private kayChart _kChart_o2;
        private kayChart _kChart_p8;
        private kayChart _kChart_t8;
        private kayChart _kChart_fc6;
        private kayChart _kChart_f4;
        private kayChart _kChart_f8;
        private kayChart _kChart_af42;

        #endregion Raw sensor data charts

        public EEGRecording _ondra = new EEGRecording(0, "Test Recordidng");

        public RecorderForm()
        {
            InitializeComponent();
            statusBar.Text = "Ready";

            prepareGraphSeries_Sensors();

            //Events
            _engine.EmoStateUpdated +=
             new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);

            _engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);

            _engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);

            _engine.UserRemoved +=
                 new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);


           //Test Events
           // _engine.AffectivEmoStateUpdated +=
           //     new EmoEngine.AffectivEmoStateUpdatedEventHandler(engine_AffectivEmoStateUpdated);

            //Connect to headset
            statusBar.Text = "EMOTIV Dongle not found.";
            headsetSetupBox.Enabled = false;
            statusBox.Enabled = false;
            statusStrip.BackColor = System.Drawing.Color.DarkRed;
            _engine.Connect();
        }


        /// <summary>
        /// Record latest raw sensor data from headset
        /// </summary>
        /// <param name="buffer">How many from buffer shold be recorded</param>
        private void Record(int buffer)
        {
            //Get latest status from emo engine
            EmoState es = _es;

            //Fetch latest sensor data from engine
            Dictionary<EdkDll.EE_DataChannel_t, double[]> data = _engine.GetData((uint)_userID);

            //Write into EEG Recording structure
            if (data != null)
            {
                for (int i = 0; i < buffer; i++)
                {
                    //Append time
                    _ondra.AppendTimestamp(recorder_stopwatch.Elapsed);

                    //Append raw sensor data
                    _ondra.AppendRaw(data[EdkDll.EE_DataChannel_t.AF3][i],
                    data[EdkDll.EE_DataChannel_t.F7][i],
                    data[EdkDll.EE_DataChannel_t.F3][i],
                    data[EdkDll.EE_DataChannel_t.FC5][i],
                    data[EdkDll.EE_DataChannel_t.T7][i],
                    data[EdkDll.EE_DataChannel_t.P7][i],
                    data[EdkDll.EE_DataChannel_t.O1][i],
                    data[EdkDll.EE_DataChannel_t.O2][i],
                    data[EdkDll.EE_DataChannel_t.P8][i],
                    data[EdkDll.EE_DataChannel_t.T8][i],
                    data[EdkDll.EE_DataChannel_t.FC6][i],
                    data[EdkDll.EE_DataChannel_t.F4][i],
                    data[EdkDll.EE_DataChannel_t.F8][i],
                    data[EdkDll.EE_DataChannel_t.AF4][i]);

                    //Append affectiv values
                    _ondra.AppendAffectiv(es.AffectivGetExcitementShortTermScore(),
                        es.AffectivGetEngagementBoredomScore(),
                        es.AffectivGetMeditationScore(),
                        es.AffectivGetFrustrationScore());
                }
            }
        }


        /// <summary>
        /// Main update event
        /// </summary>
        private void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            //EmoState
            _es = e.emoState;

            //Fetch latest sensor data from engine
            Dictionary<EdkDll.EE_DataChannel_t, double[]> data = _engine.GetData((uint)_userID);

            //Update statusbar
            statusBar.Text = "EPOC Headset Connected";
            statusStrip.BackColor = System.Drawing.Color.ForestGreen;

            try
            {
                if (data != null)
                {
                    //Update raw sensor graph
                    updateGraphSeries_Sensors(data);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Affection state updated event 
        /// </summary>
        private void engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.EE_AffectivAlgo_t[] affAlgoList = { 
                                                      EdkDll.EE_AffectivAlgo_t.AFF_ENGAGEMENT_BOREDOM,
                                                      EdkDll.EE_AffectivAlgo_t.AFF_EXCITEMENT,
                                                      EdkDll.EE_AffectivAlgo_t.AFF_FRUSTRATION,
                                                      EdkDll.EE_AffectivAlgo_t.AFF_MEDITATION,
                                                      };

            Boolean[] isAffActiveList = new Boolean[affAlgoList.Length];

            Single longTermExcitementScore = es.AffectivGetExcitementLongTermScore();
            Single shortTermExcitementScore = es.AffectivGetExcitementShortTermScore();
            for (int i = 0; i < affAlgoList.Length; ++i)
            {
                isAffActiveList[i] = es.AffectivIsActive(affAlgoList[i]);
            }

            Single meditationScore = es.AffectivGetMeditationScore();
            Single frustrationScore = es.AffectivGetFrustrationScore();
            Single boredomScore = es.AffectivGetEngagementBoredomScore();

            double rawScoreEc=0, rawScoreMd=0, rawScoreFt=0, rawScoreEg=0;
            double minScaleEc=0, minScaleMd=0, minScaleFt=0, minScaleEg=0;
            double maxScaleEc=0, maxScaleMd=0, maxScaleFt=0, maxScaleEg=0;
            double scaledScoreEc = 0, scaledScoreMd = 0, scaledScoreFt = 0, scaledScoreEg = 0;

            es.AffectivGetExcitementShortTermModelParams(out rawScoreEc, out minScaleEc, out maxScaleEc);
            if (minScaleEc != maxScaleEc)
            {
                if (rawScoreEc < minScaleEc)
                {
                    scaledScoreEc = 0;
                }
                else if (rawScoreEc > maxScaleEc)
                {
                    scaledScoreEc = 1;
                }
                else
                {
                    scaledScoreEc = (rawScoreEc - minScaleEc) / (maxScaleEc - minScaleEc);
                }

            }

            es.AffectivGetEngagementBoredomModelParams(out rawScoreEg, out minScaleEg, out maxScaleEg);
            if (minScaleEg != maxScaleEg)
            {
                if (rawScoreEg < minScaleEg)
                {
                    scaledScoreEg = 0;
                }
                else if (rawScoreEg > maxScaleEg)
                {
                    scaledScoreEg = 1;
                }
                else
                {
                    scaledScoreEg = (rawScoreEg - minScaleEg) / (maxScaleEg - minScaleEg);
                }
                Console.WriteLine("Affectiv Engagement : Raw Score {0:f5}  Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreEg, minScaleEg, maxScaleEg, scaledScoreEg);
            }
            es.AffectivGetMeditationModelParams(out rawScoreMd, out minScaleMd, out maxScaleMd);
            if (minScaleMd != maxScaleMd)
            {
                if (rawScoreMd < minScaleMd)
                {
                    scaledScoreMd = 0;
                }
                else if (rawScoreMd > maxScaleMd)
                {
                    scaledScoreMd = 1;
                }
                else
                {
                    scaledScoreMd = (rawScoreMd - minScaleMd) / (maxScaleMd - minScaleMd);
                }
                Console.WriteLine("Affectiv Meditation : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreMd, minScaleMd, maxScaleMd, scaledScoreMd);
            }
            es.AffectivGetFrustrationModelParams(out rawScoreFt, out minScaleFt, out maxScaleFt);
            if (maxScaleFt != minScaleFt)
            {
                if (rawScoreFt < minScaleFt)
                {
                    scaledScoreFt = 0;
                }
                else if (rawScoreFt > maxScaleFt)
                {
                    scaledScoreFt = 1;
                }
                else
                {
                    scaledScoreFt = (rawScoreFt - minScaleFt) / (maxScaleFt - minScaleFt);
                }
                Console.WriteLine("Affectiv Frustration : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreFt, minScaleFt, maxScaleFt, scaledScoreFt);
            }

            //affLog.Write(
            //    "{0},{1},{2},{3},{4},{5},",
            //    timeFromStart,
            //    longTermExcitementScore, shortTermExcitementScore, meditationScore, frustrationScore, boredomScore);
          
            //for (int i = 0; i < affAlgoList.Length; ++i)
            //{
            //    affLog.Write("{0},", isAffActiveList[i]);
            //}
            //affLog.WriteLine("");
            //affLog.Flush();
        }

        private void refreshdata_Tick(object sender, EventArgs e)
        {
            _engine.ProcessEvents();


        }

        #region Minor UI code

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            poolerTimer.Interval = poolingSpeedSlider.Value;
            labelPooling.Text = poolingSpeedSlider.Value.ToString() + " ms";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Double percentage = (recordBufferSlider.Value * 100 / 100) * 10;
            bufferLabel.Text = percentage.ToString() + " %";
        }

        #endregion Minor UI code

        #region Graphing

        /// <summary>
        /// Prepares graph for raw sensor data
        /// </summary>
        private void prepareGraphSeries_Sensors()
        {
            _kChart_af3 = new kayChart(channel_graph, 120);
            _kChart_af3.serieName = "AF3";

            _kChart_af42 = new kayChart(channel_graph, 120);
            _kChart_af42.serieName = "AF42";

            _kChart_f7 = new kayChart(channel_graph, 120);
            _kChart_f7.serieName = "F7";

            _kChart_f3 = new kayChart(channel_graph, 120);
            _kChart_f3.serieName = "F3";

            _kChart_fc5 = new kayChart(channel_graph, 120);
            _kChart_fc5.serieName = "FC5";

            _kChart_t7 = new kayChart(channel_graph, 120);
            _kChart_t7.serieName = "T7";

            _kChart_p7 = new kayChart(channel_graph, 120);
            _kChart_p7.serieName = "P7";

            _kChart_o1 = new kayChart(channel_graph, 120);
            _kChart_o1.serieName = "O1";

            _kChart_o2 = new kayChart(channel_graph, 120);
            _kChart_o2.serieName = "O2";

            _kChart_p8 = new kayChart(channel_graph, 120);
            _kChart_p8.serieName = "P8";

            _kChart_t8 = new kayChart(channel_graph, 120);
            _kChart_t8.serieName = "T8";

            _kChart_fc6 = new kayChart(channel_graph, 120);
            _kChart_fc6.serieName = "FC6";

            _kChart_f4 = new kayChart(channel_graph, 120);
            _kChart_f4.serieName = "F4";

            _kChart_f8 = new kayChart(channel_graph, 120);
            _kChart_f8.serieName = "F8";

            _kChart_af42 = new kayChart(channel_graph, 120);
            _kChart_af42.serieName = "AF42";
        }

        /// <summary>
        /// Updates graph with latest data from emoengine
        /// </summary>
        /// <param name="data">Input data in DataChannel form</param>
        private void updateGraphSeries_Sensors(Dictionary<EdkDll.EE_DataChannel_t, double[]> data)
        {
            new Thread(() =>
                       {
                           if (af3_check.Checked)
                               _kChart_af3.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.AF3][1]);
                           if (f7_check.Checked)
                               _kChart_f7.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.F7][1]);
                           if (f3_check.Checked)
                               _kChart_f3.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.F3][1]);
                           if (fc5_check.Checked)
                               _kChart_fc5.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.FC5][1]);
                           if (t7_check.Checked)
                               _kChart_t7.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.T7][1]);
                           if (p7_check.Checked)
                               _kChart_p7.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.P7][1]);
                           if (o1_check.Checked)
                               _kChart_o1.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.O1][1]);
                           if (o2_check.Checked)
                               _kChart_o2.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.O2][1]);
                           if (p8_check.Checked)
                               _kChart_p8.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.P8][1]);
                           if (t8_check.Checked)
                               _kChart_t8.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.T8][1]);
                           if (fc6_check.Checked)
                               _kChart_fc6.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.FC6][1]);
                           if (f4_check.Checked)
                               _kChart_f4.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.F4][1]);
                           if (f8_check.Checked)
                               _kChart_t8.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.F8][1]);
                           if (f4_check.Checked)
                               _kChart_f4.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.F4][1]);
                           if (af4_check.Checked)
                               _kChart_af42.TriggeredUpdate(data[EdkDll.EE_DataChannel_t.AF4][1]);
                       }).Start();
        }

        #endregion Graphing

        #region Basic Events

        /// <summary>
        /// Event called on Dongle remove
        /// </summary>
        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = "EMOTIV Dongle removed, User " + _userID + " unregistered";
            statusStrip.BackColor = System.Drawing.Color.DarkRed;
            headsetSetupBox.Enabled = false;
            statusBox.Enabled = false;
        }

        /// <summary>
        /// Event called on headset disconnection
        /// </summary>
        private void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = "EPOC Headset Disconnected";
            statusStrip.BackColor = System.Drawing.Color.DarkRed;
            headsetSetupBox.Enabled = false;
            statusBox.Enabled = false;
        }

        /// <summary>
        /// Event called on Dongle added
        /// </summary>
        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = "EPOC USB Dongle Detected";
            statusStrip.BackColor = System.Drawing.Color.DeepSkyBlue;
            headsetSetupBox.Enabled = true;
            statusBox.Enabled = true;

            // record the user
            _userID = (int)e.userId;

            // enable data aquisition for this user.
            _engine.DataAcquisitionEnable((uint)_userID, true);

            // ask for up to 1 second of buffered data
            _engine.EE_DataSetBufferSizeInSec(1);
        }

        #endregion Basic Events

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(_ondra);
            System.IO.File.WriteAllText("path.txt", json);
        }

        private void RecorderTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = recorder_stopwatch.Elapsed.ToString();
            Record(1);
        }

 

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Disable Controls
            poolingSpeedSlider.Enabled = false;
            rec.Enabled = false;
            pause.Enabled = true;
            stop.Enabled = true;
            rec.Text = "Record";

            //Set timer interval
            recorderTimer.Interval = poolingSpeedSlider.Value;

            // Begin timing.
            recorder_stopwatch.Start();
            // Enable recording timer.
            recorderTimer.Enabled = true;
        }

        private void pause_Click(object sender, EventArgs e)
        {
            //Switch Controls
            rec.Enabled = true;
            pause.Enabled = false;
            rec.Text = "Continue";

            // Stop timing.
            recorder_stopwatch.Stop();
            // Stop recording timer
            recorderTimer.Enabled = false;
        }
    }
}
using Emotiv;
using Newtonsoft.Json;
using rtChart;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Sdk.Models;
using EmoEngineClientLibrary;
using System.Drawing;

namespace Client
{
    public partial class RecorderForm : Form
    {
        /// Main EmoEngine and EmoState instances
        private EmoEngine _engine = EmoEngine.Instance;
        EmoState _es;
        private int _userID;

        // Recorder stopwatch
        Stopwatch _stopwatch = new Stopwatch();
        public bool _recording;

        

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

        // Sample recording
        public Recording _ondra = new Recording("Test", DateTime.Now);

        /// <summary>
        /// Main tick for fetching data from emoengine (1000ms)
        /// </summary>
        private void Heartbeat_tick(object sender, EventArgs e)
        {
            _engine.ProcessEvents();

            if (this._recording)
                timeLabel.Text = _stopwatch.Elapsed.ToString();

        }


        /// <summary>
        /// Initilzation function
        /// </summary>
        public RecorderForm()
        {

            InitializeComponent();
            statusBar.Text = "Ready";

            prepareGraphSeries_Sensors();

            //Events
            _engine.EmoStateUpdated +=
             new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);

            _engine.UserAdded += 
                new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);

            _engine.EmoEngineDisconnected +=
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);

            _engine.UserRemoved +=
                 new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);

            _engine.AffectivEmoStateUpdated +=
                new EmoEngine.AffectivEmoStateUpdatedEventHandler(engine_AffectivEmoStateUpdated);

            _engine.ExpressivEmoStateUpdated +=
                new EmoEngine.ExpressivEmoStateUpdatedEventHandler(engine_ExpressivEmoStateUpdated);

            _engine.ExpressivTrainingStarted+=
               new EmoEngine.ExpressivTrainingStartedEventEventHandler(engine_ExpressivTrainingStarted);

            _engine.ExpressivTrainingSucceeded +=
               new EmoEngine.ExpressivTrainingSucceededEventHandler(engine_ExpressivTrainingSucceeded);

            _engine.ExpressivTrainingFailed +=
                 new EmoEngine.ExpressivTrainingFailedEventHandler(engine_ExpressivTrainingFailed);


            //Connect to headset
            statusBar.Text = "EMOTIV Dongle not found.";
            statusBox.Enabled = false;
            statusStrip.BackColor = System.Drawing.Color.DarkRed;
            _engine.Connect();

        }

        private void ExpressionTraining(EdkDll.EE_ExpressivAlgo_t action)
        {
            _engine.ExpressivSetTrainingAction(0, action);
            _engine.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_START);
        }

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
            statusBox.Enabled = false;
        }

        /// <summary>
        /// Event called on headset disconnection
        /// </summary>
        private void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = "EPOC Headset Disconnected";
            statusStrip.BackColor = System.Drawing.Color.DarkRed;
            statusBox.Enabled = false;
        }

        /// <summary>
        /// Event called on Dongle added
        /// </summary>
        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = "EPOC USB Dongle Detected";
            statusStrip.BackColor = System.Drawing.Color.DeepSkyBlue;
            statusBox.Enabled = true;

            // record the user
            _userID = (int)e.userId;

            // enable data aquisition for this user.
            _engine.DataAcquisitionEnable((uint)_userID, true);

            // ask for up to 1 second of buffered data
            _engine.EE_DataSetBufferSizeInSec(1);
        }

        /// <summary>
        /// Main update event for RAW data
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

                    //If recording is enabled...
                    if (this._recording)
                    {
                        for (int i = 0; i < data[EdkDll.EE_DataChannel_t.F3].Length; i++)
                        {
                            //Append raw sensor data (The whole buffer)
                            _ondra.AppendRawData(data[EdkDll.EE_DataChannel_t.AF3][i],
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
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Event raised when the expression training has failed
        /// </summary>
        private void engine_ExpressivTrainingFailed(object sender, EmoEngineEventArgs e)
        {
            _engine.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_REJECT);
            statusBar.Text = _engine.CognitivGetTrainingAction(0).ToString() + " training failed and rejected !.";
        }

        /// <summary>
        /// Event raised when the expression training has suceeded
        /// </summary>
        private void engine_ExpressivTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            MessageBox.Show(_engine.ExpressivGetTrainingAction(0) + " training accepted and saved.");
            _engine.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_ACCEPT);

        }

        /// <summary>
        /// Event raised on begining of expression training
        /// </summary>
        private void engine_ExpressivTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            statusBar.Text = _engine.ExpressivGetTrainingAction(0) + " training started";
        }


        /// <summary>
        /// Main update event for AFFECTION data
        /// </summary>
        private void engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {

            EmoState es = e.emoState;
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

            double rawScoreEc = 0, rawScoreMd = 0, rawScoreFt = 0, rawScoreEg = 0;
            double minScaleEc = 0, minScaleMd = 0, minScaleFt = 0, minScaleEg = 0;
            double maxScaleEc = 0, maxScaleMd = 0, maxScaleFt = 0, maxScaleEg = 0;
            double scaledScoreEc = 0, scaledScoreMd = 0, scaledScoreFt = 0, scaledScoreEg = 0;

            // Short Excitement
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

            // Short Engagaement
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
                //Console.WriteLine("Affectiv Engagement : Raw Score {0:f5}  Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreEg, minScaleEg, maxScaleEg, scaledScoreEg);
            }

            // Meditation
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
                //Console.WriteLine("Affectiv Meditation : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreMd, minScaleMd, maxScaleMd, scaledScoreMd);
            }

            // Frustration
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
                //Console.WriteLine("Affectiv Frustration : Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreFt, minScaleFt, maxScaleFt, scaledScoreFt);
            }

            // If recording is enabled...
            if (this._recording)
            {
                //Append SCALED affectiv values
                _ondra.AppendAffectivData(_stopwatch.Elapsed.TotalSeconds, scaledScoreEc,
                scaledScoreEg, scaledScoreMd, scaledScoreFt);
            }
        }

        /// <summary>
        /// Main update event for EXPRESSION data
        /// </summary>
        private void engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            label4.Text = _es.ExpressivGetLowerFaceAction().ToString();


        }


        #endregion Basic Events

        #region Recording

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Disable Controls
            this._recording = true;
            rec.Enabled = false;
            pause.Enabled = true;
            stop.Enabled = true;
            rec.Text = "Record";

            // Begin timing.
            _stopwatch.Start();
            
        }

        private void pause_Click(object sender, EventArgs e)
        {
            //Switch Controls
            rec.Enabled = true;
            pause.Enabled = false;
            rec.Text = "Continue";

            // Stop timing.
            _stopwatch.Stop();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _ondra._length = _stopwatch.Elapsed.TotalMilliseconds;
            _ondra.AppendConfig(Int32.Parse(_engine.DataGetSamplingRate(0).ToString()), 4, _engine.HardwareGetVersion(0).ToString(), "1.0");
            _ondra._subject = new Subject("Petra", "Řeháčková", 11, "Female", "Toto je tesovací subjekt");

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_ondra);

            using (StreamWriter writer =
                new StreamWriter("json.txt"))
            {
                writer.Write(jsonString);
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            this._recording = false;
        }



        #endregion Recording

        private void button1_Click(object sender, EventArgs e)
        {
            ExpressionTraining(EdkDll.EE_ExpressivAlgo_t.EXP_SMILE);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExpressionTraining(EdkDll.EE_ExpressivAlgo_t.EXP_BLINK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ExpressionTraining(EdkDll.EE_ExpressivAlgo_t.EXP_EYEBROW);
            _engine.EE_SaveUserProfile(0, "lol.txt");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
using System.Windows;
using MahApps.Metro.Controls;
using Emotiv;
using System.Diagnostics;
using Sdk.Models;
using System;
using System.Collections.Generic;

namespace HamburgerMenuApp.V3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// Main EmoEngine and EmoState instances
        public EmoEngine _engine = EmoEngine.Instance;
        public EmoState _es;
        public Dictionary<EdkDll.EE_DataChannel_t, double[]> _data;
        private int _userID;

        // Recorder stopwatch
        Stopwatch _stopwatch = new Stopwatch();
        public bool _recording;

        // Sample recording
        public Recording _ondra = new Recording("Test", DateTime.Now);

        //Heartbeat dispatchtimer
        System.Windows.Threading.DispatcherTimer HeartBeat = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// Main tick for fetching data from emoengine (1000ms)
        /// </summary>
        private void HeartBeat_Tick(object sender, EventArgs e)
        {
            _engine.ProcessEvents();
        }

        /// <summary>
        /// Initilzation
        /// </summary>
        public MainWindow()
        {
            //Initialize
            InitializeComponent();

            //Set up update frequency
            HeartBeat.Tick += new EventHandler(HeartBeat_Tick);
            HeartBeat.Interval = new TimeSpan(0, 0, 1);
            HeartBeat.Start();

            //Attach events
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

            _engine.ExpressivTrainingStarted +=
               new EmoEngine.ExpressivTrainingStartedEventEventHandler(engine_ExpressivTrainingStarted);

            _engine.ExpressivTrainingSucceeded +=
               new EmoEngine.ExpressivTrainingSucceededEventHandler(engine_ExpressivTrainingSucceeded);

            _engine.ExpressivTrainingFailed +=
                 new EmoEngine.ExpressivTrainingFailedEventHandler(engine_ExpressivTrainingFailed);

            _engine.Connect();
        }

        /// <summary>
        /// Initilzation of specific training of selected expression
        /// </summary>
        private void ExpressionTraining(EdkDll.EE_ExpressivAlgo_t action)
        {
            _engine.ExpressivSetTrainingAction(0, action);
            _engine.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_START);
        }


        #region Basic Events

        /// <summary>
        /// Event called on Dongle remove
        /// </summary>
        private void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {

        }

        /// <summary>
        /// Event called on headset disconnection
        /// </summary>
        private void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {

        }

        /// <summary>
        /// Event called on Dongle added
        /// </summary>
        private void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
        {
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
            _data = _engine.GetData((uint)_userID);

            try
            {
                if (this._data != null)
                {
                    //If recording is enabled...
                    if (this._recording)
                    {
                        for (int i = 0; i < _data[EdkDll.EE_DataChannel_t.F3].Length; i++)
                        {
                            //Append raw sensor data (The whole buffer)
                            _ondra.AppendRawData(_data[EdkDll.EE_DataChannel_t.AF3][i],
                            _data[EdkDll.EE_DataChannel_t.F7][i],
                            _data[EdkDll.EE_DataChannel_t.F3][i],
                            _data[EdkDll.EE_DataChannel_t.FC5][i],
                            _data[EdkDll.EE_DataChannel_t.T7][i],
                            _data[EdkDll.EE_DataChannel_t.P7][i],
                            _data[EdkDll.EE_DataChannel_t.O1][i],
                            _data[EdkDll.EE_DataChannel_t.O2][i],
                            _data[EdkDll.EE_DataChannel_t.P8][i],
                            _data[EdkDll.EE_DataChannel_t.T8][i],
                            _data[EdkDll.EE_DataChannel_t.FC6][i],
                            _data[EdkDll.EE_DataChannel_t.F4][i],
                            _data[EdkDll.EE_DataChannel_t.F8][i],
                            _data[EdkDll.EE_DataChannel_t.AF4][i]);
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
        }

        /// <summary>
        /// Event raised when the expression training has suceeded
        /// </summary>
        private void engine_ExpressivTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            _engine.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_ACCEPT);

        }

        /// <summary>
        /// Event raised on begining of expression training
        /// </summary>
        private void engine_ExpressivTrainingStarted(object sender, EmoEngineEventArgs e)
        {

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


        }


        #endregion Basic Events
    }
}

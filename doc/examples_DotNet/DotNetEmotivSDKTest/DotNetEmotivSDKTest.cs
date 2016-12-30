using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Emotiv;

namespace DotNetEmotivSDKTest
{
    class Program
    {
        static System.IO.StreamWriter engineLog = new System.IO.StreamWriter("engineLog.log");
        static System.IO.StreamWriter expLog = new System.IO.StreamWriter("expLog.log");
        static System.IO.StreamWriter cogLog = new System.IO.StreamWriter("cogLog.log");
        static System.IO.StreamWriter affLog = new System.IO.StreamWriter("affLog.log");
        static Profile profile;

        static void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("connected");
        }

        static void engine_EmoEngineDisconnected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("disconnected");
        }
        static void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user added ({0})", e.userId);
            Profile profile = EmoEngine.Instance.GetUserProfile(0);
            profile.GetBytes();
        }
        static void engine_UserRemoved(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("user removed");
        }

        static void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();
            // Console.WriteLine("new emostate {0}", timeFromStart);
        }

        static void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();
            
            Int32 headsetOn = es.GetHeadsetOn();
            Int32 numCqChan = es.GetNumContactQualityChannels();            
            EdkDll.EE_EEG_ContactQuality_t[] cq = es.GetContactQualityFromAllChannels();
            for (Int32 i = 0; i < numCqChan; ++i)
            {
                if (cq[i] != es.GetContactQuality(i))
                {
                    throw new Exception();
                }
            }
            EdkDll.EE_SignalStrength_t signalStrength = es.GetWirelessSignalStatus();
            Int32 chargeLevel = 0;
            Int32 maxChargeLevel = 0;
            es.GetBatteryChargeLevel(out chargeLevel, out maxChargeLevel);

            engineLog.Write(
                "{0},{1},{2},{3},{4},",
                timeFromStart,
                headsetOn, signalStrength, chargeLevel, maxChargeLevel);

            for (int i = 0; i < cq.Length; ++i)
            {
                engineLog.Write("{0},", cq[i]);
            }
            engineLog.WriteLine("");
            engineLog.Flush();
        }      

        static void engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
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
                Console.WriteLine("Affectiv Short Excitement: Raw Score {0:f5} Min Scale {1:f5} max Scale {2:f5} Scaled Score {3:f5}\n", rawScoreEc, minScaleEc, maxScaleEc, scaledScoreEc);
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

            affLog.Write(
                "{0},{1},{2},{3},{4},{5},",
                timeFromStart,
                longTermExcitementScore, shortTermExcitementScore, meditationScore, frustrationScore, boredomScore);
          
            for (int i = 0; i < affAlgoList.Length; ++i)
            {
                affLog.Write("{0},", isAffActiveList[i]);
            }
            affLog.WriteLine("");
            affLog.Flush();
        }

        static void engine_CognitivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.EE_CognitivAction_t cogAction = es.CognitivGetCurrentAction();
            Single power = es.CognitivGetCurrentActionPower();
            Boolean isActive = es.CognitivIsActive();            

            cogLog.WriteLine(
                "{0},{1},{2},{3}",
                timeFromStart,
                cogAction, power, isActive);
            cogLog.Flush();
        }

        static void engine_ExpressivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            EmoState es = e.emoState;

            Single timeFromStart = es.GetTimeFromStart();

            EdkDll.EE_ExpressivAlgo_t[] expAlgoList = { 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_BLINK, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_CLENCH, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_EYEBROW, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_FURROW, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_HORIEYE, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_LAUGH, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_NEUTRAL, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_SMILE, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_SMIRK_LEFT, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_SMIRK_RIGHT, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_WINK_LEFT, 
                                                      EdkDll.EE_ExpressivAlgo_t.EXP_WINK_RIGHT
                                                      };
            Boolean[] isExpActiveList = new Boolean[expAlgoList.Length];

            Boolean isBlink = es.ExpressivIsBlink();
            Boolean isLeftWink = es.ExpressivIsLeftWink();
            Boolean isRightWink = es.ExpressivIsRightWink();
            Boolean isEyesOpen = es.ExpressivIsEyesOpen();
            Boolean isLookingUp = es.ExpressivIsLookingUp();
            Boolean isLookingDown = es.ExpressivIsLookingDown();
            Boolean isLookingLeft = es.ExpressivIsLookingLeft();
            Boolean isLookingRight = es.ExpressivIsLookingRight();
            Single leftEye = 0.0F;
            Single rightEye = 0.0F;
            Single x = 0.0F;
            Single y = 0.0F;
            es.ExpressivGetEyelidState(out leftEye, out rightEye);
            es.ExpressivGetEyeLocation(out x, out y);
            Single eyebrowExtent = es.ExpressivGetEyebrowExtent();
            Single smileExtent = es.ExpressivGetSmileExtent();
            Single clenchExtent = es.ExpressivGetClenchExtent();
            EdkDll.EE_ExpressivAlgo_t upperFaceAction = es.ExpressivGetUpperFaceAction();
            Single upperFacePower = es.ExpressivGetUpperFaceActionPower();
            EdkDll.EE_ExpressivAlgo_t lowerFaceAction = es.ExpressivGetLowerFaceAction();
            Single lowerFacePower = es.ExpressivGetLowerFaceActionPower();
            for (int i = 0; i < expAlgoList.Length; ++i)
            {
                isExpActiveList[i] = es.ExpressivIsActive(expAlgoList[i]);
            }

            expLog.Write(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},",
                timeFromStart,
                isBlink, isLeftWink, isRightWink, isEyesOpen, isLookingUp,
                isLookingDown, isLookingLeft, isLookingRight, leftEye, rightEye,
                x, y, eyebrowExtent, smileExtent, upperFaceAction,
                upperFacePower, lowerFaceAction, lowerFacePower);
            for (int i = 0; i < expAlgoList.Length; ++i)
            {
                expLog.Write("{0},", isExpActiveList[i]);
            }
            expLog.WriteLine("");
            expLog.Flush();   
        }

        static void engine_CognitivTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Start Cognitiv Training");
        }

        static void engine_CognitivTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Cognitiv Training Success. (A)ccept/Reject?");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                Console.WriteLine("Accept!!!");
                EmoEngine.Instance.CognitivSetTrainingControl(0, EdkDll.EE_CognitivTrainingControl_t.COG_ACCEPT);
            }
            else
            {
                EmoEngine.Instance.CognitivSetTrainingControl(0, EdkDll.EE_CognitivTrainingControl_t.COG_REJECT);
            }
        }

        static void engine_CognitivTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Cognitiv Training Completed.");
        }

        static void engine_CognitivTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Cognitiv Training Rejected.");
        }

        static void engine_ExpressivTrainingStarted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Start Expressiv Training");
        }

        static void engine_ExpressivTrainingSucceeded(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Expressiv Training Success. (A)ccept/Reject?");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.A)
            {
                Console.WriteLine("Accept!!!");
                EmoEngine.Instance.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_ACCEPT);
            }
            else
            {               
                EmoEngine.Instance.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_REJECT);
            }
        }

        static void engine_ExpressivTrainingCompleted(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Expressive Training Completed.");
        }

        static void engine_ExpressivTrainingRejected(object sender, EmoEngineEventArgs e)
        {
            Console.WriteLine("Expressiv Training Rejected.");
        }

        static void keyHandler(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:                    
                    EmoEngine.Instance.CognitivSetTrainingAction(0, EdkDll.EE_CognitivAction_t.COG_PUSH);                    
                    EmoEngine.Instance.CognitivSetTrainingControl(0, EdkDll.EE_CognitivTrainingControl_t.COG_START);
                    break;
                case ConsoleKey.F2:
                    EmoEngine.Instance.ExpressivSetTrainingAction(0, EdkDll.EE_ExpressivAlgo_t.EXP_CLENCH);
                    EmoEngine.Instance.ExpressivSetTrainingControl(0, EdkDll.EE_ExpressivTrainingControl_t.EXP_START);
                    break;
                case ConsoleKey.F3:
                    profile = EmoEngine.Instance.GetUserProfile(0);
                    Console.WriteLine("Get profile");
                    break;
                case ConsoleKey.F4:
                    EmoEngine.Instance.SetUserProfile(0, profile);
                    Console.WriteLine("Set profile");
                    break;
                case ConsoleKey.F5:
                    EmoEngine.Instance.CognitivSetActivationLevel(0, 2);
                    Console.WriteLine("Cog Activateion level set to {0}", EmoEngine.Instance.CognitivGetActivationLevel(0));
                    break;
                case ConsoleKey.F6:
                    Console.WriteLine("Cog Activateion level is {0}", EmoEngine.Instance.CognitivGetActivationLevel(0));
                    break;
                case ConsoleKey.F7:
                    OptimizationParam oParam = new OptimizationParam();
                    oParam.SetVitalAlgorithm(EdkDll.EE_EmotivSuite_t.EE_AFFECTIV, 0);
                    oParam.SetVitalAlgorithm(EdkDll.EE_EmotivSuite_t.EE_COGNITIV, 0);
                    oParam.SetVitalAlgorithm(EdkDll.EE_EmotivSuite_t.EE_EXPRESSIV, 0);
                    EmoEngine.Instance.OptimizationEnable(oParam);
                    Console.WriteLine("Optimization is On");
                    break;
                case ConsoleKey.F8:
                    EmoEngine.Instance.OptimizationDisable();
                    Console.WriteLine("Optimization is Off");
                    break;
                case ConsoleKey.F9:
                    String version;
                    UInt32 buildNum;
                    EmoEngine.Instance.SoftwareGetVersion(out version, out buildNum);
                    Console.WriteLine("Software Version: {0}, {1}", version, buildNum);
                    break;
            }
        }

        static void Main(string[] args)
        {
            //Console.Write(System.IO.File.Exists("edk.dll"));
            //Console.ReadKey();
            //return;
            EmoEngine engine = EmoEngine.Instance;
            
            engine.EmoEngineConnected += 
                new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
            engine.EmoEngineDisconnected += 
                new EmoEngine.EmoEngineDisconnectedEventHandler(engine_EmoEngineDisconnected);
            engine.UserAdded += 
                new EmoEngine.UserAddedEventHandler(engine_UserAdded);
            engine.UserRemoved += 
                new EmoEngine.UserRemovedEventHandler(engine_UserRemoved);
            engine.EmoStateUpdated += 
                new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
            engine.ExpressivEmoStateUpdated += 
                new EmoEngine.ExpressivEmoStateUpdatedEventHandler(engine_ExpressivEmoStateUpdated);
            engine.CognitivEmoStateUpdated +=
                new EmoEngine.CognitivEmoStateUpdatedEventHandler(engine_CognitivEmoStateUpdated);
            engine.AffectivEmoStateUpdated +=
                new EmoEngine.AffectivEmoStateUpdatedEventHandler(engine_AffectivEmoStateUpdated);
            engine.EmoEngineEmoStateUpdated += 
               new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
            engine.CognitivTrainingStarted +=
                new EmoEngine.CognitivTrainingStartedEventEventHandler(engine_CognitivTrainingStarted);
            engine.CognitivTrainingSucceeded +=
                new EmoEngine.CognitivTrainingSucceededEventHandler(engine_CognitivTrainingSucceeded);
            engine.CognitivTrainingCompleted +=
                new EmoEngine.CognitivTrainingCompletedEventHandler(engine_CognitivTrainingCompleted);
            engine.CognitivTrainingRejected +=
                new EmoEngine.CognitivTrainingRejectedEventHandler(engine_CognitivTrainingRejected);
            engine.ExpressivTrainingStarted +=
                new EmoEngine.ExpressivTrainingStartedEventEventHandler(engine_ExpressivTrainingStarted);
            engine.ExpressivTrainingSucceeded +=
                new EmoEngine.ExpressivTrainingSucceededEventHandler(engine_ExpressivTrainingSucceeded);
            engine.ExpressivTrainingCompleted += 
                new EmoEngine.ExpressivTrainingCompletedEventHandler(engine_ExpressivTrainingCompleted);
            engine.ExpressivTrainingRejected += 
                new EmoEngine.ExpressivTrainingRejectedEventHandler(engine_ExpressivTrainingRejected);

            engine.Connect();

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            // int x, y;

            while (true)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        cki = Console.ReadKey(true);
                        keyHandler(cki.Key);

                        if (cki.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }
                    engine.ProcessEvents(1000);

                    int x, y;
                    engine.HeadsetGetGyroDelta(0, out x, out y);
                    if (x != 0 || y != 0)
                        Console.WriteLine("{0}, {1}", x, y);
                }
                catch (EmoEngineException e)
                {
                    Console.WriteLine("{0}", e.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.ToString());
                }
            }
            engine.Disconnect();
        }
    }
}

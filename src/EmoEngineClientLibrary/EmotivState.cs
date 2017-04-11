// Copyright © 2009 James Galasyn 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emotiv;
using System.Diagnostics;

namespace EmoEngineClientLibrary
{   
    /// <summary>
    /// Provides property wrappers around the <see cref="EmoState"/> methods.
    /// </summary>
    /// <remarks>
    /// The <see cref="EmotivState"/> class wraps <see cref="EmoState"/> methods 
    /// with CLR properties, which enables data binding in the visualization layer.
    /// </remarks>
    public class EmotivState
    {
        EmoState _emoState;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmotivState"/> class.
        /// </summary>
        /// <param name="emoState">An <see cref="EmoState"/> to back the <see cref="EmotivState"/> properties.</param>
        public EmotivState( EmoState emoState )
        {
            if( emoState != null )
            {
                // Cache a copy.
                this._emoState = emoState.Clone() as EmoState;
            }
            else
            {
                throw new ArgumentNullException( "emoState", "must be assigned" );
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.AffectivGetEngagementBoredomScore"/> method.
        /// </summary>
        public float AffectivEngagementBoredomScore
        {
            get
            {
                return this._emoState.AffectivGetEngagementBoredomScore();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.AffectivGetExcitementLongTermScore"/> method.
        /// </summary>
        public float AffectivExcitementLongTermScore
        {
            get
            {
                return this._emoState.AffectivGetExcitementLongTermScore();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.AffectivGetExcitementShortTermScore"/> method.
        /// </summary>
        public float AffectivExcitementShortTermScore
        {
            get
            {
                return this._emoState.AffectivGetExcitementShortTermScore();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.AffectivGetFrustrationScore"/> method.
        /// </summary>
        public float AffectivFrustrationScore
        {
            get
            {
                return this._emoState.AffectivGetFrustrationScore();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.AffectivGetMeditationScore"/> method.
        /// </summary>
        public float AffectivMeditationScore
        {
            get
            {
                return this._emoState.AffectivGetMeditationScore();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.CognitivGetCurrentAction"/> method.
        /// </summary>
        public EdkDll.EE_CognitivAction_t CognitivCurrentAction
        {
            get
            {
                return this._emoState.CognitivGetCurrentAction();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.CognitivGetCurrentActionPower"/> method.
        /// </summary>
        public float CognitivCurrentActionPower
        {
            get
            {
                return this._emoState.CognitivGetCurrentActionPower();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.CognitivIsActive"/> method.
        /// </summary>
        public bool CognitivIsActive
        {
            get
            {
                return this._emoState.CognitivIsActive();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetClenchExtent"/> method.
        /// </summary>
        public float ExpressivClenchExtent
        {
            get
            {
                return this._emoState.ExpressivGetClenchExtent();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetEyebrowExtent"/> method.
        /// </summary>
        public float ExpressivEyebrowExtent
        {
            get
            {
                return this._emoState.ExpressivGetEyebrowExtent();
            }
        }

        /// <summary>
        /// Gets the left-eye output param from the <see cref="EmoState.ExpressivGetEyelidState"/> method.
        /// </summary>
        public float ExpressivLeftEyelidState
        {
            get
            {
                float leftEye;
                float rightEye;

                this._emoState.ExpressivGetEyelidState( out leftEye, out rightEye );

                return leftEye;
            }
        }

        /// <summary>
        /// Gets the right-eye output param from the <see cref="EmoState.ExpressivGetEyelidState"/> method.
        /// </summary>
        public float ExpressivRightEyelidState
        {
            get
            {
                float leftEye;
                float rightEye;

                this._emoState.ExpressivGetEyelidState( out leftEye, out rightEye );

                return rightEye;
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetLowerFaceAction"/> method.
        /// </summary>
        public EdkDll.EE_ExpressivAlgo_t ExpressivLowerFaceAction
        {
            get
            {
                return this._emoState.ExpressivGetLowerFaceAction();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetLowerFaceActionPower"/> method.
        /// </summary>
        public float ExpressivLowerFaceActionPower
        {
            get
            {
                return this._emoState.ExpressivGetLowerFaceActionPower();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetSmileExtent"/> method.
        /// </summary>
        public float ExpressivSmileExtent
        {
            get
            {
                return this._emoState.ExpressivGetSmileExtent();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetUpperFaceAction"/> method.
        /// </summary>
        public EdkDll.EE_ExpressivAlgo_t ExpressivUpperFaceAction
        {
            get
            {
                return this._emoState.ExpressivGetUpperFaceAction();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivGetUpperFaceActionPower"/> method.
        /// </summary>
        public float ExpressivUpperFaceActionPower
        {
            get
            {
                return this._emoState.ExpressivGetUpperFaceActionPower();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsBlink"/> method.
        /// </summary>
        public bool ExpressivIsBlink
        {
            get
            {
                return this._emoState.ExpressivIsBlink();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsEyesOpen"/> method.
        /// </summary>
        public bool ExpressivIsEyesOpen
        {
            get
            {
                return this._emoState.ExpressivIsEyesOpen();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsLeftWink"/> method.
        /// </summary>
        public bool ExpressivIsLeftWink
        {
            get
            {
                return this._emoState.ExpressivIsLeftWink();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsLookingDown"/> method.
        /// </summary>
        public bool ExpressivIsLookingDown
        {
            get
            {
                return this._emoState.ExpressivIsLookingDown();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsLookingLeft"/> method.
        /// </summary>
        public bool ExpressivIsLookingLeft
        {
            get
            {
                return this._emoState.ExpressivIsLookingLeft();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsLookingRight"/> method.
        /// </summary>
        public bool ExpressivIsLookingRight
        {
            get
            {
                return this._emoState.ExpressivIsLookingRight();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsLookingUp"/> method.
        /// </summary>
        public bool ExpressivIsLookingUp
        {
            get
            {
                return this._emoState.ExpressivIsLookingUp();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.ExpressivIsRightWink"/> method.
        /// </summary>
        public bool ExpressivIsRightWink
        {
            get
            {
                return this._emoState.ExpressivIsRightWink();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.GetHeadsetOn"/> method.
        /// </summary>
        public int HeadsetOn
        {
            get
            {
                return this._emoState.GetHeadsetOn();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.GetNumContactQualityChannels"/> method.
        /// </summary>
        public int NumContactQualityChannels
        {
            get
            {
                return this._emoState.GetNumContactQualityChannels();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.GetTimeFromStart"/> method.
        /// </summary>
        public float TimeFromStart
        {
            get
            {
                return this._emoState.GetTimeFromStart();
            }
        }

        /// <summary>
        /// Gets the value returned by the <see cref="EmoState.GetWirelessSignalStatus"/> method.
        /// </summary>
        public EdkDll.EE_SignalStrength_t WirelessSignalStatus
        {
            get
            {
                return this._emoState.GetWirelessSignalStatus();
            }
        }

        /// <summary>
        /// Gets the Battery Charge Level output param from the <see cref="EmoState.GetBatteryChargeLevel"/> method.
        /// </summary>
        public int BatteryChargeLevel
        {
            get
            {
                int batteryChargeLevel;
                int maxChargeLevel;
                this._emoState.GetBatteryChargeLevel( out batteryChargeLevel, out maxChargeLevel );

                return batteryChargeLevel;
            }
        }

        /// <summary>
        /// Gets the Max Charge Level output param from the <see cref="EmoState.GetBatteryChargeLevel"/> method.
        /// </summary>
        public int MaxBatteryChargeLevel
        {
            get
            {
                int batteryChargeLevel;
                int maxChargeLevel;
                this._emoState.GetBatteryChargeLevel( out batteryChargeLevel, out maxChargeLevel );

                return maxChargeLevel;
            }
        }

        public static Dictionary<int, EdkDll.EE_DataChannel_t> InputChannelIndexToDataChannelMap
        {
            get
            {
                // TBD: A bit hacky.
                Dictionary<EdkDll.EE_InputChannels_t, EdkDll.EE_DataChannel_t> d = InputChannelToDataChannelMap;

                return _inputChannelIndexToDataChannelMap;
            }
        }

        public static Dictionary<EdkDll.EE_InputChannels_t, EdkDll.EE_DataChannel_t> InputChannelToDataChannelMap
        {
            get
            {
                if( _inputChannelToDataChannelMap == null )
                {
                    _inputChannelToDataChannelMap = new Dictionary<EdkDll.EE_InputChannels_t, EdkDll.EE_DataChannel_t>();
                    _inputChannelIndexToDataChannelMap = new Dictionary<int, EdkDll.EE_DataChannel_t>();

                    Array inputChannels = Enum.GetValues( typeof( EdkDll.EE_InputChannels_t ) );

                    for( int i = 0; i < inputChannels.Length; i++ )
                    {
                        EdkDll.EE_InputChannels_t inputChannel = (EdkDll.EE_InputChannels_t)inputChannels.GetValue( i );

                        switch( inputChannel )
                        {
                            case EdkDll.EE_InputChannels_t.EE_CHAN_AF3:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.AF3;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.AF3;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_AF4:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.AF4;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.AF4;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_F3:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.F3;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.F3;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_F4:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.F4;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.F3;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_F7:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.F7;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.F7;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_F8:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.F8;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.F8;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_FC5:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.FC5;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.FC5;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_FC6:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.FC6;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.FC6;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_O1:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.O1;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.O1;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_O2:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.O2;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.O2;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_P7:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.P7;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.P7;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_P8:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.P8;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.P8;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_T7:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.T7;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.T7;
                                break;
                            }
                            case EdkDll.EE_InputChannels_t.EE_CHAN_T8:
                            {
                                _inputChannelToDataChannelMap[inputChannel] = EdkDll.EE_DataChannel_t.T8;
                                _inputChannelIndexToDataChannelMap[i] = EdkDll.EE_DataChannel_t.T8;
                                break;
                            }

                            default:
                            {
                                break;
                            }
                        }
                    }
                }

                return _inputChannelToDataChannelMap;
            }
        }

        public Dictionary<EdkDll.EE_DataChannel_t, EdkDll.EE_EEG_ContactQuality_t> ContactQualityFromAllChannels
        {
            get
            {
                Dictionary<EdkDll.EE_DataChannel_t, EdkDll.EE_EEG_ContactQuality_t> contactQualityDictionary =
                    new Dictionary<EdkDll.EE_DataChannel_t, EdkDll.EE_EEG_ContactQuality_t>();

                //Array enums = Enum.GetValues( typeof( EdkDll.EE_InputChannels_t ) );
                //string[] enumNames = Enum.GetNames( typeof( EdkDll.EE_InputChannels_t ) );
                //List<EdkDll.EE_InputChannels_t> inputChannelEnums = new List<EdkDll.EE_InputChannels_t>();

                EdkDll.EE_EEG_ContactQuality_t[] contactQualityArray = this._emoState.GetContactQualityFromAllChannels();

                for( int i = 0; i < contactQualityArray.Length; i++ )
                {
                    if( InputChannelIndexToDataChannelMap.ContainsKey( i ) )
                    {
                        EdkDll.EE_DataChannel_t channel = InputChannelIndexToDataChannelMap[i];

                        contactQualityDictionary[channel] = contactQualityArray[i];
                    }
                }

                return contactQualityDictionary;
            }
        }


        public List<EdkDll.EE_EEG_ContactQuality_t> ContactQualityFromElectrodeChannels
        {
            get
            {
                //Array inputChannelEnums = Enum.GetValues( typeof( EdkDll.EE_InputChannels_t ) );
                
                //EdkDll.EE_EEG_ContactQuality_t[] contactQualityArray = this._emoState.GetContactQualityFromAllChannels();

                //// TBD: There really has to be a more elegant way of doing this with LINQ.
                //List<EdkDll.EE_EEG_ContactQuality_t> electrodeContactQuality = contactQualityArray.ToList();

                //electrodeContactQuality.RemoveAt( 0 );  // EdkDll.EE_InputChannels_t.EE_CHAN_CMS = 0
                //electrodeContactQuality.RemoveAt( 1 );  // EdkDll.EE_InputChannels_t.EE_CHAN_DRL = 1
                //electrodeContactQuality.RemoveAt( 2 );  // EdkDll.EE_InputChannels_t.EE_CHAN_FP1 = 2
                //electrodeContactQuality.RemoveAt( 17 ); // EdkDll.EE_InputChannels_t.EE_CHAN_FP2 = 17

                //return electrodeContactQuality;
                return null;
            }
        }


        //public List<EdkDll.EE_EEG_ContactQuality_t> ContactQualityFromAllChannels
        //{
        //    get
        //    {
        //        EdkDll.EE_EEG_ContactQuality_t[] contactQualityArray = this._emoState.GetContactQualityFromAllChannels();

        //        //return this._emoState.GetContactQualityFromAllChannels();
        //        return contactQualityArray.ToList();
        //    }
        //}

        #region Private Fields

        private static Dictionary<EdkDll.EE_InputChannels_t, EdkDll.EE_DataChannel_t> _inputChannelToDataChannelMap;
        private static Dictionary<int, EdkDll.EE_DataChannel_t> _inputChannelIndexToDataChannelMap;

        #endregion

    }
}

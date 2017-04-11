// Copyright © 2010 James Galasyn 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emotiv;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace EmoEngineClientLibrary
{
    /// <summary>
    /// Provides a synchronized collection of electrode data from the Emotiv neuroheadset.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use the <see cref="SampleBufferDictionary"/> class to buffer realtime electrode data 
    /// from the Emotiv neuroheadset. The <see cref="SampleBufferDictionary"/> class is a 
    /// dictionary that maps the <see cref="EmoEngine"/> data channels enum to electrode data 
    /// from the neuroheadset.  
    /// </para>
    /// <para>
    /// Data channels are denoted throughout the <see cref="EmoEngineClientLibrary"/> by 
    /// using the <see cref="EdkDll.EE_DataChannel_t"/> enumeration. Realtime data samples from the 
    /// neuroheadset arrive in arrays of type <c>double</c>. A data frame comprises
    /// all of the channels defined in the <see cref="EdkDll.EE_DataChannel_t"/> enumeration mapped 
    /// to corresponding arrays of sample data.
    /// </para>
    /// <para>
    /// The <see cref="SampleBufferDictionary"/> class implements a circular buffer that holds 
    /// data frames. You specify the size of the buffer in the <see cref="SampleBufferDictionary"/>
    /// constructor. Access the size of the buffer, in data frames, by using the 
    /// <see cref="FrameCapacity"/> property. Access the size of the buffer, in data samples, 
    /// by using the <see cref="BufferSize"/> property. 
    /// </para>
    /// Synchronize access to the <see cref="SampleBufferDictionary"/> class by using
    /// the <c>lock</c> statement and the <see cref="SyncRoot"/> property. 
    /// <para>
    /// Call the <see cref="Add"/> method to insert a data frame into the dictionary. 
    /// Synchronization is automatic, so you do not need to synchronize calls to the 
    /// <see cref="Add"/> method explicitly.  
    /// </para>
    /// <para>
    /// Simple signal processing is supported, such as removing DC bias and computing
    /// Fast Fourier Transform (FFT) for a channel. 
    /// </para>
    /// <para>
    /// If the <see cref="ChannelContext"/> for a data channel has "RemoveDCBias" 
    /// set to <c>true</c>, the corresponding value in the <see cref="ChannelAverages"/>
    /// dictionary is subtracted from each of the channel's new samples.
    /// </para>
    /// <para>
    /// If the <see cref="ChannelContext"/> for a data channel has "ComputeFFT" 
    /// set to <c>true</c>, the corresponding value in the <see cref="FastFourierTransforms"/>
    /// dictionary is calculated for the channel's buffer, including the channel's new samples.
    /// </para>
    /// </remarks>
    public class SampleBufferDictionary : Dictionary<EdkDll.EE_DataChannel_t, double[]>
    {
        ///////////////////////////////////////////////////////////////////////
        #region Construction and Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBufferDictionary"/> class that
        /// contains the specified number of data frames.
        /// </summary>
        /// <param name="frameCapacity">The number of data frames that 
        /// the <see cref="SampleBufferDictionary"/> contains.</param>
        public SampleBufferDictionary( int frameCapacity )
        {
            if( frameCapacity > 0 )
            {
                this.FrameCapacity = frameCapacity;
            }
            else
            {
                throw new ArgumentException( "must be greater than 0", "frameCapacity" );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBufferDictionary"/> class that's a copy
        /// of the specified <see cref="SampleBufferDictionary"/> instance.
        /// </summary>
        /// <param name="dictionary">The <see cref="SampleBufferDictionary"/> to copy.</param>
        public SampleBufferDictionary( SampleBufferDictionary dictionary ) : base( dictionary )
        {   
            this.FrameCapacity = dictionary.FrameCapacity;
            this.TotalFrames = dictionary.TotalFrames;
            this.BufferSize = dictionary.BufferSize;
            //this.TotalSamples = dictionary.TotalSamples;

        }

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Public Properties

        /// <summary>
        /// Gets the size of the buffer, in data frames. 
        /// </summary>
        public int FrameCapacity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the size of the buffer, in samples.
        /// </summary>
        public int BufferSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the index of the current frame. 
        /// </summary>
        public int CurrentFrameIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// /// Gets the index of the current sample. 
        /// </summary>
        /// <remarks>
        /// The <see cref="Add"/> method inserts a new data frame at this index.
        /// </remarks>
        public int CurrentSampleIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the count of frames that have been added since the first
        /// call to the <see cref="Add"/> method.
        /// </summary>
        /// <remarks>
        /// The <see cref="TotalFrames"/> property is used to compute the 
        /// running average for each data channel.
        /// </remarks>
        public long TotalFrames
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the count of samples that have been added since the first
        /// call to the <see cref="Add"/> method.
        /// </summary>
        public long TotalSamples
        {
            get
            {
                // TBD: Hack -- fails if AF3 isn't there.
                return this.TotalFrames * this[EdkDll.EE_DataChannel_t.AF3].Length;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to 
        /// the <see cref="SampleBufferDictionary"/>.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return _lockThis;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="SampleBufferDictionary"/> is synchronized (thread safe).
        /// </summary>
        /// <remarks>
        /// Always returns <c>true</c>.
        /// </remarks>
        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a dictionary that contains the running averages for each data channel.
        /// </summary>
        public Dictionary<EdkDll.EE_DataChannel_t, double> ChannelAverages
        {
            get
            {
                if( this._channelAverages == null )
                {
                    this._channelAverages = new Dictionary<EdkDll.EE_DataChannel_t, double>();
                }

                return this._channelAverages;
            }
        }

        /// <summary>
        /// Gets a dictionary that contains the Fast Fourier Transform (FFT) for each data channel.
        /// </summary>
        //public Dictionary<EdkDll.EE_DataChannel_t, double[]> FastFourierTransforms
        //{
        //    get
        //    {
        //        if( this._FFTs == null )
        //        {
        //            this._FFTs = new Dictionary<EdkDll.EE_DataChannel_t, double[]>();
        //        }

        //        return this._FFTs;
        //    }
        //}

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Adds a new data frame to the <see cref="SampleBufferDictionary"/>. 
        /// </summary>
        /// <param name="dataFrame">A dictionary that contains the new sample data.</param>
        /// <remarks>
        /// <para>
        /// Access to the <see cref="SampleBufferDictionary"/> is synchronized.
        /// </para>
        /// <para>
        /// The <paramref name="dataFrame"/> parameter is assumed to originate from the 
        /// <see cref="EmoEngine"/>, which means that all data channels from the neuroheadset
        /// are present. 
        /// </para>
        /// <para>
        /// Only data channels with the "AddToBuffer" <see cref="ChannelContext"/> set 
        /// to <c>true</c> are added. Usually, signals that are not electrode data 
        /// are excluded, but some signals, such as <see cref="EdkDll.EE_DataChannel_t.COUNTER"/>, 
        /// are usually included for diagnostic purposes.
        /// </para>
        /// </remarks>
        public void Add( Dictionary<EdkDll.EE_DataChannel_t, double[]> dataFrame )
        {
            // Return if there is no data frame.
            if( dataFrame != null )
            {
                // Get the number of samples in the new data frame. This number is assumed
                // to be the same for all channels.  
                // TBD: picked this channel at random (first in Intellisense list).
                int sampleCount = dataFrame[EdkDll.EE_DataChannel_t.AF3].Length;

                // Track whether the samples in the new data frame will
                // wrap from the end to the beginning of the buffer.
                //bool wrapped = false;
                
                // Synchronize access to the dictionary.
                lock( this._lockThis )
                {
                    // If this is the first frame, set the buffer size.
                    if( this.Count == 0 )
                    {
                        this.BufferSize = sampleCount * this.FrameCapacity;
                    }


                    // Compute the number of samples between the current sample and the end of the buffer. 
                    int samplesToBufferEnd = this.BufferSize - this.CurrentSampleIndex;

                    bool wrapped = samplesToBufferEnd < sampleCount ? true : false;

                    // Not enough space, so compute how many samples to wrap to the 
                    // start of the buffer.
                    int samplesToWrap = sampleCount - samplesToBufferEnd;

                    var wrapDictionary = new Dictionary<EdkDll.EE_DataChannel_t, double[]>();



                    // Iterate through all of the channels in the data frame and copy each array of new
                    // samples to the corresponding buffer.
                    foreach( KeyValuePair<EdkDll.EE_DataChannel_t, double[]> kvp in dataFrame )
                    {
                        bool addToBuffer = (bool)EmoEngineClient.ChannelContexts[kvp.Key]["AddToBuffer"];
                        if( addToBuffer )
                        {
                            Emotiv.EdkDll.EE_DataChannel_t channel = kvp.Key;

                            // If the dictionary entry for the channel doesn't exist, create it. 
                            if( !this.ContainsKey( channel ) )
                            {
                                // Create a dictionary entry for the channel. 
                                this[channel] = null;
                            }

                            // If the dictionary entry for the channel average value doesn't exist, create it. 
                            if( !this.ChannelAverages.ContainsKey( channel ) )
                            {
                                // Create a dictionary entry for the channel average. 
                                this.ChannelAverages[channel] = 0;
                            }

                            // Get the new samples in the data frame.
                            double[] newSamples = kvp.Value;

                            // Compute the average of the new samples.
                            double newSampleAverage = newSamples.Sum() / newSamples.Length;

                            // Determine whether to remove DC bias from this channel. 
                            bool removeDCBias = (bool)EmoEngineClient.ChannelContexts[channel]["RemoveDCBias"];

                            // Determine whether to compute the FFT for the channel. 
                            bool computeFFT = (bool)EmoEngineClient.ChannelContexts[channel]["ComputeFFT"];

                            // Get or create the buffer for the current channel. 
                            double[] currentBuffer = this[channel];
                            if( currentBuffer == null )
                            {
                                currentBuffer = new double[this.BufferSize];
                                this[channel] = currentBuffer;
                            }

                            ///////////////////////////////////////
                            #region Remove DC bias from new samples

                            // Compute the running average for the current channel.
                            this.ChannelAverages[kvp.Key] = ( this.TotalFrames * this.ChannelAverages[kvp.Key] + newSampleAverage ) / ( this.TotalFrames + 1 );

                            // Remove the DC bias, if the channel context specifies it.
                            if( removeDCBias )
                            {
                                // Subtract the average value for the channel. 
                                var newSamplesMinusDCBias = newSamples.Select( d => d - this.ChannelAverages[kvp.Key] );
                                newSamples = newSamplesMinusDCBias.ToArray();
                            }

                            // Special case for the COUNTER channel.
                            if( kvp.Key == EdkDll.EE_DataChannel_t.COUNTER )
                            {
                                // Subtract the median value for the channel. 
                                var newSamplesMinusDCBias = newSamples.Select( d => d - 64.0d );
                                newSamples = newSamplesMinusDCBias.ToArray();
                            }

                            #endregion

                            ////////////////////////////////////////
                            #region Commit new samples to the buffer

                            // Compute the number of samples between the current sample and the end of the buffer. 
                           // int samplesToBufferEnd = this.BufferSize - this.CurrentSampleIndex;

                            // If there is enough space, copy the new samples into the buffer.
                            //if( samplesToBufferEnd >= newSamples.Length )
                            if( !wrapped )
                            {
                                newSamples.CopyTo( currentBuffer, this.CurrentSampleIndex );
                            }
                            else
                            {
                                // Not enough space, so compute how many samples to wrap to the 
                                // start of the buffer.
                                //int samplesToWrap = newSamples.Length - samplesToBufferEnd;

                                double[] wrap = new double[newSamples.Length]; 
                                Array.Copy(
                                    newSamples,
                                    samplesToBufferEnd,
                                    wrap,
                                    0,
                                    samplesToWrap );

                                wrapDictionary.Add( channel, wrap );

                                // Copy new samples to the end of the buffer.
                                //Array.Copy(
                                //    newSamples,
                                //    0,
                                //    currentBuffer,
                                //    this.CurrentSampleIndex,
                                //    samplesToBufferEnd );

                                //var buffer = currentBuffer.Select( d => d );
                                //double[] bufferD = buffer.ToArray<double>();
                                //BufferFilledEventArgs bfea = new BufferFilledEventArgs( bufferD, channel );
                                //this.OnBufferFilled( bfea );

                                // Copy the wrapped new samples to the beginning of the buffer.
                                //Array.Copy(
                                //    newSamples,
                                //    samplesToBufferEnd,
                                //    currentBuffer,
                                //    0,
                                //    samplesToWrap );

                                // Indicate that the buffer wrapped.
                                //wrapped = true;
                            }

                            #endregion 

                            ////////////////////////////////////////////
                            #region Compute Fast Fourier Transform (FFT)

                            // Optionally compute the FFT for the channel.
                            // TBD: lock?
                            //if( computeFFT )
                            //{
                            //    this.FastFourierTransforms[kvp.Key] = ComputeRealFourierTransform( kvp.Key );
                            //}

                            #endregion
                        }
                    }

                    if( wrapped )
                    {
                        BufferFilledEventArgs bfea = new BufferFilledEventArgs( this );
                        this.OnBufferFilled( bfea );

                        foreach( KeyValuePair<EdkDll.EE_DataChannel_t, double[]> kvp in wrapDictionary )
                        {
                            // Copy the wrapped new samples to the beginning of the buffer.
                            Array.Copy(
                                kvp.Value,
                                0,
                                this[kvp.Key],
                                this.CurrentSampleIndex,
                                samplesToBufferEnd );
                        }
                    }

                    // Increment the count of total frames.
                    this.TotalFrames++;

                    // Increment the current frame index, modulo the buffer capacity (in frames).
                    this.CurrentFrameIndex = ( this.CurrentFrameIndex + 1 ) % this.FrameCapacity;

                    // Increment the current sample index, modulo the buffer capacity (in samples).
                    this.CurrentSampleIndex = ( this.CurrentSampleIndex + sampleCount ) % this.BufferSize;
                }
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////
        #region Implementation

        public event BufferFilledEventHandler BufferFilled;

        protected virtual void OnBufferFilled( BufferFilledEventArgs bfea )
        {
            if( BufferFilled != null )
                BufferFilled( this, bfea );
        }


        /// <summary>
        /// Calculates the real discrete Fourier transform for the specified data channel.
        /// </summary>
        /// <param name="channel">The data channel to calculate the Fourier transform for.</param>
        /// <returns>An array that contains the magnitudes of samples in the frequency domain (spectrum)
        /// of the real time-domain signal in <paramref name="channel"/>.</returns>
        /// <remarks>
        /// The <see cref="ComputeRealFourierTransform"/> method calls the <see cref="ComputeFourierTransform"/> method
        /// and extracts the real part of the transformed signal from the returned <see cref="Complex"/> array. The real 
        /// part is computed by using the <see cref="Complex.Magnitude"/> property. The <see cref="Complex.Phase"/> is 
        /// ignored. The first N / 2 samples are returned, which comprise the positive frequencies.   
        /// </remarks>
        public double[] ComputeRealFourierTransform( EdkDll.EE_DataChannel_t channel )
        {
            Complex[] complexDataArray = this.ComputeFourierTransform( channel );

            var magnitudes = complexDataArray.Select( c => c.Magnitude ).Take( complexDataArray.Length / 2 );

            return magnitudes.ToArray<double>();
        }

        private static Complex[] ComputeFourierTransform( EdkDll.EE_DataChannel_t channel, double[] data )
        {
            var complexData = data.Select( d => new Complex( d, 0 ) );
            Complex[] complexDataArray = complexData.ToArray<Complex>();

            Transform.FourierForward( complexDataArray );

            return complexDataArray;
        }

        public Complex[] ComputeFourierTransform( EdkDll.EE_DataChannel_t channel )
        {
            return ( ComputeFourierTransform( channel, this[channel] ) );
        }

        /// <summary>
        /// Calculates the real discrete inverse Fourier transform for the specified array of frequencies.
        /// </summary>
        /// <param name="complexDataArray">The frequencies to transform.</param>
        /// <returns>An array that contains the real discrete inverse Fourier transform that corresponds 
        /// with <paramref name="complexDataArray"/>. </returns>
        /// <remarks>
        /// The <paramref name="complexDataArray"/> is usually returned by a call to 
        /// the <see cref="ComputeFourierTransform"/> method.
        /// </remarks>
        public static double[] ComputeRealInverseFourierTransform( Complex[] complexDataArray )
        {
            Complex[] inverseFourierArray = ComputeInverseFourierTransform( complexDataArray );

            var doubleData = complexDataArray.Select( c => c.Real ).Take( inverseFourierArray.Length / 2 );

            var doubleData2 = doubleData.Reverse();

            var invFFT = doubleData.Concat( doubleData2 );

            return invFFT.ToArray<double>();
        }

        private static Complex[] ComputeInverseFourierTransform( Complex[] complexDataArray )
        {
            Complex[] inverseFourierArray = null;
            complexDataArray.CopyTo( inverseFourierArray, 0 );

            Transform.FourierInverse( inverseFourierArray );

            return inverseFourierArray;
        }

        #endregion


        ///////////////////////////////////////////////////////////////////////
        #region Private Fields
        
        /// <summary>
        /// The synchronization object that backs the <see cref="SyncRoot"/> property.
        /// </summary>
        private Object _lockThis = new Object();

        /// <summary>
        /// The dictionary of contains the running averages for each channel.
        /// </summary>
        private Dictionary<EdkDll.EE_DataChannel_t, double> _channelAverages;

        /// <summary>
        /// The dictionary of contains the Fast Fourier Transform (FFT) for each channel.
        /// </summary>
        //private Dictionary<EdkDll.EE_DataChannel_t, double[]> _FFTs;

        #endregion
    }
}
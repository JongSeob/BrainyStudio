// Copyright © 2010 James Galasyn
using Emotiv;
using System;
using System.Collections.Generic;

namespace EmoEngineClientLibrary
{
    /// <summary>
    /// Contains samples of electrode data from the Emotiv neuroheadset.
    /// </summary>
    /// <remarks>
    /// <para>The <see cref="SampleBuffer"/> class collects electrode data from the
    /// neuroheadset and saves them in a <see cref="SampleBufferDictionary"/>.</para>
    /// <para>
    /// The neuroheadset periodically sends data frames of electrode measurements and
    /// reference signals. The engine's <see cref="EmoEngine.DataGetSamplingRate"/> property
    /// determines how fast data frames arrive. This value is typically 128Hz.
    /// </para>
    /// <para>
    /// Each frame contains data for the channels specified in the <see cref="EdkDll.EE_DataChannel_t"/>
    /// enumeration. Currently, each data frame comprises 14 channels of electrode data
    /// and 11 channels of reference and other signals.
    /// </para>
    /// <para>
    /// Use the <see cref="AddFrame"/> method to insert a data frame into the <see cref="SampleBuffer"/>.
    /// </para>
    /// <para>
    /// Use the <see cref="ChannelData"/> property to access data frames in the sample buffer.
    /// Synchronize access to the <see cref="SampleBufferDictionary"/> class by using
    /// the <c>lock</c> statement and the <see cref="SampleBufferDictionary.SyncRoot"/> property.
    /// </para>
    /// </remarks>
    public class SampleBuffer
    {
        private SampleBufferDictionary _sampleBufferDictionary;
        private Dictionary<EdkDll.EE_DataChannel_t, double> _averages;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuffer"/> class that
        /// contains the specified number of data frames.
        /// </summary>
        /// <param name="frameCapacity">The number of data frames that
        /// the <see cref="SampleBuffer"/> contains.</param>
        public SampleBuffer(int frameCapacity)
        {
            if (frameCapacity > 0)
            {
                this.FrameCapacity = frameCapacity;
                this._sampleBufferDictionary = new SampleBufferDictionary(this.FrameCapacity);
                //this._sampleBufferDictionary.BufferFilled += new BufferFilledEventHandler( sampleBufferDictionary_BufferFilled );
            }
            else
            {
                throw new ArgumentException("must be greater than 0", "frameCapacity");
            }
        }

        /// <summary>
        /// Gets the size of the buffer, in data frames.
        /// </summary>
        public int FrameCapacity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the index of the current frame.
        /// </summary>
        public int CurrentFrameIndex
        {
            get
            {
                return this.ChannelData.CurrentFrameIndex;
            }
        }

        /// <summary>
        /// /// Gets the index of the current sample.
        /// </summary>
        /// <remarks>
        /// The <see cref="AddFrame"/> method inserts a new data frame at this index.
        /// </remarks>
        public int CurrentSampleIndex
        {
            get
            {
                return this.ChannelData.CurrentSampleIndex;
            }
        }

        /// <summary>
        /// Gets the size of the buffer, in samples.
        /// </summary>
        public int SampleBufferSize
        {
            get
            {
                return this.ChannelData.BufferSize;
            }
        }

        /// <summary>
        /// Gets the buffer of channel data.
        /// </summary>
        /// <remarks>
        /// Synchronize access to the <see cref="ChannelData"/> property by using
        /// the <c>lock</c> statement and the <see cref="SampleBufferDictionary.SyncRoot"/> property.
        /// </remarks>
        public SampleBufferDictionary ChannelData
        {
            get
            {
                return this._sampleBufferDictionary;
            }
        }

        /// <summary>
        /// Gets a dictionary that contains the running averages for each data channel.
        /// </summary>
        public Dictionary<EdkDll.EE_DataChannel_t, double> ChannelAverages
        {
            get
            {
                if (this._averages == null)
                {
                    this._averages = new Dictionary<EdkDll.EE_DataChannel_t, double>();
                }

                return this._averages;
            }
        }

        /// <summary>
        /// Adds a new data frame to the <see cref="SampleBuffer"/>.
        /// </summary>
        /// <param name="dataFrame">A dictionary that contains the new sample data.</param>
        /// <remarks>
        /// For more information, see <see cref="SampleBufferDictionary.Add"/>.
        /// </remarks>
        public void AddFrame(Dictionary<EdkDll.EE_DataChannel_t, double[]> dataFrame)
        {
            this.ChannelData.Add(dataFrame);
        }

        ///////////////////////////////////////////////////////////////////////

        #region BufferFilled Implementation

        public event BufferFilledEventHandler BufferFilled
        {
            add
            {
                this._sampleBufferDictionary.BufferFilled += value;
            }

            remove
            {
                this._sampleBufferDictionary.BufferFilled -= value;
            }
        }

        #endregion BufferFilled Implementation
    }
}
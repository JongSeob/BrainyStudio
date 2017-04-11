// Copyright © 2010 James Galasyn 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emotiv;

namespace EmoEngineClientLibrary
{
    /// <summary>
    /// Provides a collection of properties for a data channel.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use the <see cref="ChannelContext"/> class to specify properties 
    /// that apply to realtime electrode data channels. The <see cref="ChannelContext"/> class
    /// is a simple property bag implementation that maps a property name to an
    /// object. The property value can be a value type, but boxing semantics apply.
    /// </para>
    /// <para>
    /// The following list shows the default properties that are available for a
    /// channel context.
    /// <list type="bullet">
    /// <item>AddToBuffer: Boolean indicating that the data channel is included in the <see cref="SampleBuffer"/>.</item>
    /// <item>RemoveDCBias: Boolean indicating that any DC bias in the data channel is removed.</item>
    /// <item>IsElectrodeChannel: Boolean indicating that the data channel holds electrode data. </item>
    /// <item>ComputeFFT: Boolean indicating that a Fast Fourier Transform (FFT) is computed for the data channel.</item>
    /// <item>ContactQuality: A <see cref="EdkDll.EE_EEG_ContactQuality_t"/> that indicates the signal quality of the data channel.</item>
    /// </list>
    /// </para>
    /// <para>
    /// Type safety is not a fgeature of this class; it should probably be re-implemented with the 
    /// new <c>dynamic</c> keyword, which enables building "expando" types, with dynamically created properties. 
    /// </para>
    /// </remarks>
    public class ChannelContext : Dictionary<string, object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelContext"/> class.
        /// </summary>
        public ChannelContext()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelContext"/> class with the specified dictionary.
        /// </summary>
        public ChannelContext( Dictionary<string, object> properties )
        {
            foreach( KeyValuePair<string, object> kvp in properties )
            {
                this.Add( kvp.Key, kvp.Value );
            }
        }

        /// <summary>
        /// Adds a boolean property to the channel context.
        /// </summary>
        /// <param name="name">The name of the property to add.</param>
        /// <param name="initialValue">The initial value of the property.</param>
        public void AddBooleanProperty(
            string name,
            bool initialValue )
        {
            this.Add( name, initialValue );
        }

        /// <summary>
        /// Adds a double property to the channel context.
        /// </summary>
        /// <param name="name">The name of the property to add.</param>
        /// <param name="initialValue">The initial value of the property.</param>
        public void AddDoubleProperty(
            string name,
            double initialValue )
        {
            this.Add( name, initialValue );
        }

        /// <summary>
        /// Adds an integer property to the channel context.
        /// </summary>
        /// <param name="name">The name of the property to add.</param>
        /// <param name="initialValue">The initial value of the property.</param>
        public void AddIntegerProperty(
            string name,
            int initialValue )
        {
            this.Add( name, initialValue );
        }

        /// <summary>
        /// Adds a <see cref="Type"/> property to the channel context.
        /// </summary>
        /// <param name="name">The name of the property to add.</param>
        /// <param name="initialValue">The initial value of the property.</param>
        public void AddTypeProperty(
            string name,
            Type initialValue )
        {
            this.Add( name, initialValue );
        }
    }
}

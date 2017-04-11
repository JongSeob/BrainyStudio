using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmoEngineClientLibrary
{
    public delegate void BufferFilledEventHandler( object sender, BufferFilledEventArgs bfea );

    public class BufferFilledEventArgs : EventArgs 
    {
        public BufferFilledEventArgs( SampleBufferDictionary buffer )
        {
            // Defensive copy.
            this.Buffer = new SampleBufferDictionary( buffer );
        }

        public SampleBufferDictionary Buffer
        {
            get;
            private set;
        }

        //public BufferFilledEventArgs( double[] buffer, Emotiv.EdkDll.EE_DataChannel_t channel )
        //{
        //    this.Buffer = buffer;
        //    this.Channel = channel;
        //}

        //public double[] Buffer
        //{
        //    get;
        //    private set;
        //}

        //public Emotiv.EdkDll.EE_DataChannel_t Channel
        //{
        //    get;
        //    private set;
        //}
    }
}

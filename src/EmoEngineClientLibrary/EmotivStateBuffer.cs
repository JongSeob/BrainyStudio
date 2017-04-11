using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emotiv;

namespace EmoEngineClientLibrary
{
    /// <summary>
    /// Provides a synchronized collection of EmotivState instances from the Emotiv neuroheadset.
    /// </summary>
    public class EmotivStateBuffer : List<EmotivState>
    {

        public EmotivStateBuffer( int capacity ) : base( capacity )
        {
            this.FrameCount = capacity;
        }

        public EmotivStateBuffer( EmotivStateBuffer buffer ) : base( buffer )
        {
            this.FrameCount = buffer.FrameCount;
        }

        public int FrameCount
        {
            get;
            private set;
        }

        public new void Add( EmotivState state )
        {
            if( this.Count < this.FrameCount )
            {
                base.Add( state );
                this.CurrentSampleIndex++;
            }
            else
            {
                this.CurrentSampleIndex = 0;

                EmotivStateBufferFilledEventArgs bfea = new EmotivStateBufferFilledEventArgs( this );
                this.OnBufferFilled( bfea );

                this.Clear();

                base.Add( state );
            }            
        }

        public int CurrentSampleIndex
        {
            get;
            private set;
        }

        ///////////////////////////////////////////////////////////////////////
        #region Implementation

        public event EmotivStateBufferFilledEventHandler BufferFilled;

        protected virtual void OnBufferFilled( EmotivStateBufferFilledEventArgs bfea )
        {
            if( BufferFilled != null )
            {
                BufferFilled( this, bfea );
            }
        }

        #endregion
        
    }
}

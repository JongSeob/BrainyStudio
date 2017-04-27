using System;

namespace EmoEngineClientLibrary
{
    public delegate void EmotivStateBufferFilledEventHandler(object sender, EmotivStateBufferFilledEventArgs bfea);

    public class EmotivStateBufferFilledEventArgs : EventArgs
    {
        public EmotivStateBufferFilledEventArgs(EmotivStateBuffer buffer)
        {
            this.Buffer = new EmotivStateBuffer(buffer);
        }

        public EmotivStateBuffer Buffer
        {
            get;
            private set;
        }
    }
}
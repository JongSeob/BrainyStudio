using Emotiv;
using System;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            EmoEngine engine = EmoEngine.Instance;

            engine.EmoStateUpdated +=
     new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);

            engine.UserAdded +=
             new EmoEngine.UserAddedEventHandler(engine_UserAdded);

            engine.Connect();
            Console.ReadKey();
        }

        private static void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
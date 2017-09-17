using System;

namespace LiftDemo {
#if WINDOWS || LINUX
    public static class Program {
        [STAThread]
        static void Main( ) {
            using (var mainThread = new MainThread( ))
                mainThread.Run( );
        }
    }
#endif
}

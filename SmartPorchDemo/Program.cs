﻿using System;

namespace SmartPorchDemo {
#if WINDOWS || LINUX
    public static class Program {
        [STAThread]
        static void Main( ) {
            using (var thread = new MainThread( ))
                thread.Run( );
        }
    }
#endif
}

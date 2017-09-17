using Microsoft.Speech.Recognition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LiftDemo {
#if WINDOWS || LINUX
    public static class Program {

        private static MainThread _mainThread;

        private static Dictionary<string, int> _commands = new Dictionary<string, int>( ) {
            { "первый этаж", 1 },
            { "второй этаж", 2 },
            { "третий этаж", 3 },
            { "четвёртый этаж", 4 },
            { "пятый этаж", 5 },
            { "шестой этаж", 6 },
            { "седьмой этаж", 7 },
            { "восьмой этаж", 8 },
        };

        [STAThread]
        static void Main( ) {
            using (_mainThread = new MainThread( )) {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
                SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
                sre.SetInputToDefaultAudioDevice( );
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(OnSpeechRecognized);
                Choices numbers = new Choices( );
                List<string> commandsList = new List<string>( );
                foreach (var command in _commands) {
                    commandsList.Add(command.Key);
                }
                numbers.Add(commandsList.ToArray( ));
                GrammarBuilder gb = new GrammarBuilder( );
                gb.Culture = ci;
                gb.Append(numbers);
                Grammar g = new Grammar(gb);
                sre.LoadGrammar(g);
                sre.RecognizeAsync(RecognizeMode.Multiple);
                _mainThread.Run( );
            }
        }

        private static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e) {
            _mainThread.CurrentLiftFloor = (uint)_commands[e.Result.Text];
        }

        public static Point GetSize(this Texture2D texture) {
            return new Point(texture.Width, texture.Height);
        }
    }
#endif
}

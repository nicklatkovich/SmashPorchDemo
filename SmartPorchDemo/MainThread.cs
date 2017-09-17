using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SmartPorchDemo.Properties;
using System.IO;
using System.Reflection;

namespace SmartPorchDemo {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch Batch;
        Texture2D sPlayer;
        Texture2D sBrick;
        Texture2D sBrickDark;
        Texture2D sBrickDarkDark;
        char[ ][ ] Map;
        Vector2 playerPosition = new Vector2(0, 9);

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 704,
            };
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize( ) {
            // TODO: Initialization logic
            base.Initialize( );
        }

        protected override void LoadContent( ) {
            Batch = new SpriteBatch(GraphicsDevice);

            sPlayer = Content.Load<Texture2D>("sPlayer");
            sBrick = Content.Load<Texture2D>("sBrick");
            sBrickDark = Content.Load<Texture2D>("sBrickDark");
            sBrickDarkDark = Content.Load<Texture2D>("sBrickDarkDark");
            
            byte[ ] map = Resources.demoMap;
            using (MemoryStream stream = new MemoryStream(map)) {
                using (StreamReader sr = new StreamReader(stream)) {
                    uint width = uint.Parse(sr.ReadLine( ));
                    uint height = uint.Parse(sr.ReadLine( ));
                    Map = new char[width][ ];
                    for (uint i = 0; i < width; i++) {
                        Map[i] = new char[height];
                    }
                    for (uint y = 0; y < height; y++) {
                        string s = sr.ReadLine( );
                        for (uint x = 0; x < width; x++) {
                            Map[x][y] = s[(int)x];
                        }
                    }
                }
            }
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime time) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState( ).IsKeyDown(Keys.Escape))
                Exit( );
            // TODO: Update logic
            base.Update(time);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(new Color(30, 30, 30));
            Batch.Begin( );
            for (uint i = 0; i < Map.Length; i++) {
                for (uint j = 0; j < Map[i].Length; j++) {
                    Texture2D tex = null;
                    switch (Map[i][j]) {
                        case '1':
                            tex = sBrick;
                            break;
                        case 'F':
                            tex = sBrickDarkDark;
                            break;
                    }
                    if (tex != null) {
                        Batch.Draw(tex, new Rectangle((int)i * 64, (int)j * 64, 64, 64), Color.White);
                    }
                }
            }
            Batch.Draw(sPlayer, new Rectangle((playerPosition * 64).ToPoint( ), new Point(64, 64)), Color.White);
            Batch.End( );
            base.Draw(time);
        }
    }
}

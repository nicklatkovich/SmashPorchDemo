using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LiftDemo {
    public class MainThread : Game {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        public MainThread( ) {
            Graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 704,
            };
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize( ) {
            // TODO: Initialization
            base.Initialize( );
        }

        protected override void LoadContent( ) {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: Load content
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState( ).IsKeyDown(Keys.Escape)) {
                Exit( );
            }
            // TODO: Update logic
            base.Update(gameTime);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(new Color(30, 30, 30));
            // TODO: Drawing
            base.Draw(time);
        }
    }
}

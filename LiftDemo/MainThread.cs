using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LiftDemo {
    public class MainThread : Game {

        private const uint FLOORS_NUMBER = 8u;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private uint _currentLiftFloor = 1u;
        private float _currentLiftPosition = 1f;
        private float _liftScale;

        public Texture2D TextureLift { get; protected set; }

        public MainThread( ) {
            _graphics = new GraphicsDeviceManager(this) {
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureLift = Content.Load<Texture2D>("sLift");
            _liftScale = (float)_graphics.PreferredBackBufferHeight / FLOORS_NUMBER / TextureLift.Height;
        }

        protected override void UnloadContent( ) {
            // TODO: Unload any non ContentManager content
        }

        protected override void Update(GameTime time) {
            KeyboardState keyboard = Keyboard.GetState( );
            if (keyboard.IsKeyDown(Keys.Escape)) {
                Exit( );
            }
            for (uint i = 1; i <= FLOORS_NUMBER; i++) {
                if (keyboard.IsKeyDown((Keys)((uint)Keys.D0 + i))) {
                    _currentLiftPosition = i;
                }
            }
            // TODO: Update logic
            base.Update(time);
        }

        protected override void Draw(GameTime time) {
            GraphicsDevice.Clear(new Color(30, 30, 30));
            _spriteBatch.Begin( );
#pragma warning disable CS0618 // Type or member is obsolete
            _spriteBatch.Draw(TextureLift,
                position: new Vector2(
                    _graphics.PreferredBackBufferWidth - TextureLift.Width * _liftScale * 2,
                    _graphics.PreferredBackBufferHeight - _graphics.PreferredBackBufferHeight / FLOORS_NUMBER * (_currentLiftPosition - 0.5f)),
                origin: new Vector2(TextureLift.Width / 2f, TextureLift.Height / 2f),
                scale: new Vector2(_liftScale, _liftScale));
#pragma warning restore CS0618 // Type or member is obsolete
            _spriteBatch.End( );
            // TODO: Drawing
            base.Draw(time);
        }

        public uint CurrentLiftFloor {
            set {
                _currentLiftPosition = _currentLiftFloor = value;
            }
            get => _currentLiftFloor;
        }
    }
}

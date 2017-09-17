using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LiftDemo {
    public class MainThread : Game {

        private const uint FLOORS_NUMBER = 8u;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private uint _currentLiftFloor = 1u;
        private float _currentLiftPosition = 1f;
        private float _liftScale;
        public bool[ ] FloorsButtons = new bool[FLOORS_NUMBER];
        private bool _liftGoneUp = true;

        public Texture2D TextureLift { get; protected set; }
        public Texture2D TextureButtonOff { get; protected set; }
        public Texture2D TextureButtonOn { get; protected set; }

        public SpriteFont FontArial64 { get; protected set; }

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
            TextureButtonOff = Content.Load<Texture2D>("sGrayButton");
            TextureButtonOn = Content.Load<Texture2D>("sBlueButton");
            FontArial64 = Content.Load<SpriteFont>("fArial64");
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
            uint panelHorizontalNumberButtons = (uint)Math.Sqrt(FLOORS_NUMBER);
            uint panelVerticalNumberButtons = (uint)Math.Ceiling((float)FLOORS_NUMBER / panelHorizontalNumberButtons);
            uint panelWidth = (uint)TextureButtonOff.Width * panelHorizontalNumberButtons;
            uint panelHeight = panelVerticalNumberButtons * (uint)TextureButtonOff.Height;
            float buttonsScale = Math.Min(
                386f / panelHorizontalNumberButtons / TextureButtonOff.Width,
                386f / panelVerticalNumberButtons / TextureButtonOff.Height);
            for (uint i = 0; i < FLOORS_NUMBER; i++) {
                Vector2 pos = buttonsScale * new Vector2(
                        TextureButtonOff.Width * (i % panelHorizontalNumberButtons),
                        TextureButtonOff.Height * (i / panelHorizontalNumberButtons));
#pragma warning disable CS0618 // Type or member is obsolete
                _spriteBatch.Draw(FloorsButtons[FLOORS_NUMBER - i - 1] ? TextureButtonOn : TextureButtonOff,
                    position: pos,
                    scale: new Vector2(buttonsScale));
#pragma warning restore CS0618 // Type or member is obsolete
                Utils.Utils.DrawText(_spriteBatch, FontArial64, (FLOORS_NUMBER - i).ToString( ),
                    pos + new Vector2(buttonsScale / 2f) * TextureButtonOff.GetSize( ).ToVector2( ),
                    new Vector2(buttonsScale * 0.5f) * TextureButtonOff.GetSize( ).ToVector2( ), Color.Black, 0f);
                //_spriteBatch.DrawString(FontArial64, , pos + new Vector2(buttonsScale / 2f), Color.DarkGray);
            }
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

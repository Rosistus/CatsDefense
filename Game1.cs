using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Cats
{
    enum Statement
    {
        StartScreen,
        Game,
        Pause,
        Death
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle _resolution;
        Statement _state = Statement.StartScreen;
        KeyboardState _keyboardState, _oldKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _resolution = new Rectangle(0, 0, 1920, 1080);
            _graphics.PreferredBackBufferWidth = _resolution.Width;
            _graphics.PreferredBackBufferHeight = _resolution.Height;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Gameplay.Resolution = _resolution;
            StartScreen.Resolution = _resolution;
            base.Initialize();
            Gameplay.Initialize();
            Pause.Initialise();
            Death.Initialise();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Gameplay.BackgroundTexture = Content.Load<Texture2D>("BackGround");
            Gameplay.WhiteCat = Content.Load<Texture2D>("WhiteCat");
            Gameplay.spriteBatch = _spriteBatch;
            Gameplay.Font = Content.Load<SpriteFont>("StartFont");
            Death.Font = Content.Load<SpriteFont>("StartFont");
            Gameplay.GameSong = Content.Load<Song>("song");
            StartScreen.spriteBatch = _spriteBatch;
            StartScreen.Screen = Content.Load <Texture2D>("startscreen");
            StartScreen.Font = Content.Load<SpriteFont>("StartFont");
            Pause.spriteBatch = _spriteBatch;
            Pause.Texture = Content.Load<Texture2D>("PauseSign");
            Pause.DarkerBackground = Content.Load<Texture2D>("BlackSquare");
            Death.spriteBatch = _spriteBatch;
            Death.Texture = Content.Load<Texture2D>("DeadSign");
            Death.RedSquare = Content.Load<Texture2D>("BlackSquare");
            Death.SadMeow = Content.Load<SoundEffect>("Meow");
            Rules.Texture = Content.Load<Texture2D>("RuleSign");
            RockBullet.Texture = Content.Load<Texture2D>("Rock");
            RockBullet.SlapSound = Content.Load<SoundEffect>("Slap");
            IceBullet.IceSound = Content.Load<SoundEffect>("IceSound");
            IceBullet.Texture = Content.Load<Texture2D>("Ice");
            Enemy.SmallRoar = Content.Load<SoundEffect>("smallRoar");
            Enemy.MediumRoar = Content.Load<SoundEffect>("mediumRoar");
            Enemy.HugeRoar = Content.Load<SoundEffect>("HugeRoar");
            SmallEnemy.TextureLeft = Content.Load<Texture2D>("SmollMonsterLeft");
            SmallEnemy.TextureRight = Content.Load<Texture2D>("SmallMonsterRight");
            MediumEnemy.TextureLeft = Content.Load<Texture2D>("MediumMonsterLeft");
            MediumEnemy.TextureRight = Content.Load<Texture2D>("MediumMonsterRight");
            HugeEnemy.TextureLeft = Content.Load<Texture2D>("HugeMonsterLeft");
            HugeEnemy.TextureRight = Content.Load<Texture2D>("HugeMonsterRight");
            OrangeCat.PassiveTextureLeft = Content.Load<Texture2D>("OrangeCatLeftPassive");
            OrangeCat.PassiveTextureRight = Content.Load<Texture2D>("OrangeCatRightPassive");
            OrangeCat.ActiveTextureLeft = Content.Load<Texture2D>("OrangeCatLeftActive");
            OrangeCat.ActiveTextureRight = Content.Load<Texture2D>("OrangeCatRightActive");
            BlueCat.PassiveTextureLeft = Content.Load<Texture2D>("BlueCatLeftPassive");
            BlueCat.PassiveTextureRight = Content.Load<Texture2D>("BlueCatRightPassive");
            BlueCat.ActiveTextureLeft = Content.Load<Texture2D>("BlueCatLeftActive");
            BlueCat.ActiveTextureRight = Content.Load<Texture2D>("BlueCatRightActive");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.03f;
            SoundEffect.MasterVolume = 0.05f;
            MediaPlayer.Play(Gameplay.GameSong);
        }
        
        protected override void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            switch (_state)
            {
                case Statement.StartScreen:
                    MediaPlayer.Pause();
                    StartScreen.Update();
                    if(_keyboardState.IsKeyDown(Keys.Space))
                        _state = Statement.Game;
                    break;
                case Statement.Game:
                    MediaPlayer.Resume();
                    Gameplay.Update();
                    if (Gameplay.GameOver())
                        _state = Statement.Death;
                    if (_keyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape))
                        _state = Statement.Pause;
                    if (_keyboardState.IsKeyDown(Keys.A))
                    {
                        Gameplay.GetLeftCat().MakeShootingTexture();
                        if (_oldKeyboardState.IsKeyUp(Keys.A))
                            Gameplay.GetLeftCat().Shoot();
                    }
                    if (_keyboardState.IsKeyDown(Keys.D))
                    {
                        Gameplay.GetRightCat().MakeShootingTexture();
                        if (_oldKeyboardState.IsKeyUp(Keys.D))
                            Gameplay.GetRightCat().Shoot();
                    }
                    if (_keyboardState.IsKeyDown(Keys.Space) && _oldKeyboardState.IsKeyUp(Keys.Space))
                        Gameplay.Swap();
                    Gameplay.SpawnEnemy();
                    break;
                case Statement.Pause:
                    MediaPlayer.Pause();
                    Pause.Update();
                    if (_keyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape))
                        _state = Statement.Game;
                    if (_keyboardState.IsKeyDown(Keys.Space) || _keyboardState.IsKeyDown(Keys.A) || _keyboardState.IsKeyDown(Keys.D))
                        _state = Statement.Game;
                    break;
                case Statement.Death:
                    MediaPlayer.Pause();
                    Death.Update();
                    if (_keyboardState.IsKeyDown(Keys.Escape) && _oldKeyboardState.IsKeyUp(Keys.Escape))
                        Exit();
                    if (_keyboardState.IsKeyDown(Keys.Space) || _keyboardState.IsKeyDown(Keys.R))
                    {
                        Gameplay.Restart();
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Gameplay.GameSong);
                        Death.Clear();
                        _state = Statement.Game;
                    }
                    break;
            }
            _oldKeyboardState = _keyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            _spriteBatch.Begin();
            switch(_state)
            {
                case Statement.StartScreen:
                    StartScreen.Draw();
                    break;
                case Statement.Game:
                    Gameplay.Draw();
                    break;
                case Statement.Pause:
                    Gameplay.Draw();
                    Pause.Draw();
                    break;
                case Statement.Death:
                    Gameplay.Draw();
                    Death.Draw();
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
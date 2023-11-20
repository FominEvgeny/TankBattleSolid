using System.Collections.Concurrent;
using System.Linq;
using GamePlayerMonogame.Config;
using GamePlayerMonogame.GamableMg;
using LibraryClasses.Commands;
using LibraryClasses.GameObjects;
using LibraryClasses.Ioc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamePlayerMonogame
{
    public class Game1 : Game
    {
        private readonly BlockingCollection<UObject> _allObjectsGame;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _backgroundTexture2D;
        private Texture2D _treesTexture2D;
        private Texture2D _fireTexture2D;

        Vector2 positionSpellFire = new Vector2(800, 50);
        Vector2 position = Vector2.Zero;
        int frameWidthFire = 64;
        int frameHeightFire = 64;
        int frameWidthSmoke = 49;
        int frameHeightSmoke = 49;
        Point currentFrameFire = new Point(0, 0);
        Point currentFrameSmoke = new Point(0, 0);
        Point spriteSizeFire = new Point(10, 6);
        Point spriteSizeSmoke = new Point(8, 1);

        public Game1(BlockingCollection<UObject> allObjectsGame)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _allObjectsGame = allObjectsGame;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GameSettingsInit.GameWindowWidthDefault;
            _graphics.PreferredBackBufferHeight = GameSettingsInit.GameWindowHeightDefault;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundTexture2D = Texture2D.FromFile(this.GraphicsDevice, "Content/back_texture.png");
            _treesTexture2D = Texture2D.FromFile(this.GraphicsDevice, "Content/trees_texture.png");
            _fireTexture2D = Texture2D.FromFile(this.GraphicsDevice, "Content/fire5_64.png");

            IoC.Resolve<ICommand>("TexturesGameObjectsInitCommand", GraphicsDevice, _spriteBatch).Execute();
            _font = Content.Load<SpriteFont>("gamefont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (var gameUObject in _allObjectsGame)
            {
                var adapterGameObject = (IGamableMg)gameUObject.GetProperty("GamableAdapter");
                if (adapterGameObject != null) adapterGameObject.Update();
            }

            ++currentFrameFire.X;
            if (currentFrameFire.X >= spriteSizeFire.X)
            {
                currentFrameFire.X = 0;
                ++currentFrameFire.Y;
                if (currentFrameFire.Y >= spriteSizeFire.Y)
                    currentFrameFire.Y = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture2D, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            foreach (var gameUObject in _allObjectsGame)
            {
                var adapterGameObject = (IGamableMg)gameUObject.GetProperty("GamableAdapter");
                if (adapterGameObject != null) adapterGameObject.Draw();
            }

            _spriteBatch.Draw(_treesTexture2D, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.Draw(_fireTexture2D,
                position,
                new Rectangle(currentFrameFire.X * frameWidthFire,
                    currentFrameFire.Y * frameHeightFire,
                    frameWidthFire, frameHeightFire),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

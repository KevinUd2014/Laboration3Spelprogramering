using Laboration3.Model;
using Laboration3.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Laboration3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MasterController : Game
    {
        Camera camera;

        Explosion explosion;
        BallView ballview;
        BallSimulation ballSimulation;
        float timeElapsed;

        private MouseState oldMouseState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080/2;
            graphics.PreferredBackBufferWidth = 1920/2;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(graphics.GraphicsDevice.Viewport);
            Texture2D smokee = Content.Load<Texture2D>("particlesmokee");
            Texture2D spark = Content.Load<Texture2D>("spark");
            Texture2D bangExplosion = Content.Load<Texture2D>("explosion");
            Texture2D masterBall = Content.Load<Texture2D>("master_ball");

            ballSimulation = new BallSimulation();
            explosion = new Explosion(spriteBatch, spark, camera, smokee, bangExplosion);
            ballview = new BallView(graphics, ballSimulation, Content, masterBall);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 vec = new Vector2(newMouseState.X, newMouseState.Y);
                explosion.Click(vec);
                //timeElapsed = 0;
            }
            oldMouseState = newMouseState;
            
            explosion.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

           // ballSimulation.update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Crimson);

            // spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetMatrix());
            spriteBatch.Begin();
           // ballview.Draw(spriteBatch);
            explosion.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

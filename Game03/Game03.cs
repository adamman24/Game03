using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game03
{
    public class Game03 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FoxSprite fox;
        private DogSprite dog;
        private ResetBox box;
        private bool running = true;
        private KeyboardState current;
        private KeyboardState prior;

        //layer textures
        private Texture2D _foreground;
        private Texture2D _back;
        private Texture2D _middle;
        private Texture2D _front;
        private SpriteFont spriteFont;
        private Tilemap _tilemap;

        public Game03()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            fox = new FoxSprite();
            dog = new DogSprite();
            box = new ResetBox();
            spriteFont = Content.Load<SpriteFont>("arial");

            //load textures
            _foreground = Content.Load<Texture2D>("Foreground");
            _front = Content.Load<Texture2D>("FrontTrees");
            _middle = Content.Load<Texture2D>("MiddleTrees");
            _back = Content.Load<Texture2D>("BackTrees");
            RainParticleSystem rain = new RainParticleSystem(this, new Rectangle(0, -20, 800, 10));
            Components.Add(rain);
            _tilemap = new Tilemap("map.txt");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            fox.LoadContent(Content);
            dog.LoadContent(Content);
            box.LoadContent(Content);
            _tilemap.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prior = current;
            current = Keyboard.GetState();
            running = true;
            // TODO: Add your update logic here
            if (fox.Bounds.CollidesWith(dog.Bounds))
            {
                fox.color = Color.Red;
                running = false;
            }

            //allows user to continue playing game if they hit the dog and lose
            if(!running && current.IsKeyDown(Keys.Enter) && prior.IsKeyUp(Keys.Enter))
            {
                running = true;
                dog.Position = new Vector2(810, 370);
            }

            if(running)
            {
                fox.Update(gameTime);
                dog.Update(gameTime);

                //fox turns red if colliding with dog
                //so this resets it to white
                fox.color = Color.White;


                //if dog hits reset box it will reset dogs position
                if (dog.Bounds.CollidesWith(box.Bounds))
                {
                    dog.Position = new Vector2(810, 370);
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// draw all things necessary for the game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float offset = 50 - fox.position.X;
            Matrix transform;
            int xPosition = 0;

            // TODO: Add your drawing code here
            
            
           
            var source = new Rectangle(0, 0, 320,179);

            //back trees
            transform = Matrix.CreateTranslation(offset * .11f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            for(int i = 0; i < 20; i++)
            {
                spriteBatch.Draw(_back, new Vector2(xPosition, 0), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
                xPosition += 950;
            }
            spriteBatch.End();

            //middle trees
            xPosition = 0;
            transform = Matrix.CreateTranslation(offset * .33f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            for (int i = 0; i < 20; i++)
            {
                spriteBatch.Draw(_middle, new Vector2(xPosition, 0), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
                xPosition += 800;
            }
            spriteBatch.End();

            //front trees
            xPosition = 0;
            transform = Matrix.CreateTranslation(offset* .66f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            for (int i = 0; i < 50; i++)
            {
                spriteBatch.Draw(_front, new Vector2(xPosition, 0), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
                xPosition += 800;
            }
            spriteBatch.End();

            //foreground
            xPosition = 0;
            transform = Matrix.CreateTranslation(offset, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            for(int i = 0; i < 50; i++)
            {
                spriteBatch.Draw(_foreground, new Vector2(xPosition, -145), source, Color.White, 0f, Vector2.Zero, new Vector2(5, 3.5f), SpriteEffects.None, 0);
                xPosition += 800;
            }
            fox.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            //tilemap work
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(0, 50, 0));
            _tilemap.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            
            dog.Draw(gameTime, spriteBatch);
            box.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(spriteFont,
                "Press SPACE or UP to jump \nPress enter to continue",
                new Vector2(2, 2), Color.Yellow, 0f, Vector2.Zero, .5f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game03
{
    public class Game03 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FoxSprite fox;
        private DogSprite dog;
        private ResetBox box;

        //layer textures
        private Texture2D _foreground;
        private Texture2D _back;
        private Texture2D _middle;
        private Texture2D _front;

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

            //load textures
            _foreground = Content.Load<Texture2D>("Foreground");
            _front = Content.Load<Texture2D>("FrontTrees");
            _middle = Content.Load<Texture2D>("MiddleTrees");
            _back = Content.Load<Texture2D>("BackTrees");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            fox.LoadContent(Content);
            dog.LoadContent(Content);
            box.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            fox.Update(gameTime);
            dog.Update(gameTime);

            //fox turns red if colliding with dog
            fox.color = Color.White;
            if(fox.Bounds.CollidesWith(dog.Bounds))
            {
                fox.color = Color.Red;
            }

            //if dog hits reset box it will reset dogs position
            if(dog.Bounds.CollidesWith(box.Bounds))
            {
                dog.Position = new Vector2(810, 370);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float offset = 50 - fox.position.X;
            Matrix transform; 

            // TODO: Add your drawing code here

            var source = new Rectangle(0, 0, 320,179);

            //back trees
            transform = Matrix.CreateTranslation(offset * .11f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_back, new Vector2(0, 00), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
            spriteBatch.End();

            //middle trees
            transform = Matrix.CreateTranslation(offset * .33f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_middle, new Vector2(0, 00), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
            spriteBatch.End();

            //front trees
            transform = Matrix.CreateTranslation(offset* .66f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_front, new Vector2(0, 00), source, Color.White, 0f, Vector2.Zero, new Vector2(3, 3.5f), SpriteEffects.None, 0);
            spriteBatch.End();

            //foreground
            transform = Matrix.CreateTranslation(offset, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_foreground, new Vector2(0, -145), source, Color.White, 0f, Vector2.Zero, new Vector2(5, 3.5f), SpriteEffects.None, 0);
            fox.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            dog.Draw(gameTime, spriteBatch);
            box.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

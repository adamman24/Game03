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

        public Game03()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            fox = new FoxSprite();
            dog = new DogSprite();
            box = new ResetBox();

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
                dog.Position = new Vector2(810, 300);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            fox.Draw(gameTime, spriteBatch);
            dog.Draw(gameTime, spriteBatch);
            box.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

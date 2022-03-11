using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Game03.Collisions;

namespace Game03
{
    public class FoxSprite
    {
        private Texture2D texture;
        private double animationTimer;
        private short animationFrame = 1;
        private bool jumping = false;
        private short jumpFrame = 1;

        /// <summary>
        /// used for keyboard input
        /// </summary>
        private KeyboardState previousState;
        private KeyboardState currentState;

        //position of sprite
        private Vector2 position = new Vector2(50, 300);

        private BoundingRectangle bounds; 

        /// <summary>
        /// bounding volume for sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// color of sprite
        /// </summary>
        public Color color { get; set; } = Color.White;

        /// <summary>
        /// load the fox sprite
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("BrownFox");
            bounds = new BoundingRectangle(new Vector2(position.X - 32, position.Y - 32), 50,  40);
        }

        /// <summary>
        /// when space pressed the fox will jump and hopefully update
        /// the sprite to jump in the air
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Up) || currentState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (currentState.IsKeyDown(Keys.Down) || currentState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
            }
            if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
            }

            if(currentState.IsKeyDown(Keys.Space))
            {
                jumping = true;
            }

            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// draw the fox and update its animation to make it
        /// look like it is animated and moving
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle source; 

            if (!jumping)
            {
                if (animationTimer > .1)
                {
                    animationFrame++;
                    if (animationFrame > 7)
                    {
                        animationFrame = 1;
                    }
                    animationTimer -= .1;
                }
                source = new Rectangle(animationFrame * 32, 2 * 32, 32, 32);
                spriteBatch.Draw(texture, position, source, color, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0);
            }
            if(jumping)
            {
                if (animationFrame != jumpFrame) animationFrame = jumpFrame;
                if(animationTimer > .125)
                {
                    animationFrame++;
                    jumpFrame++;
                    if(animationFrame > 10)
                    {
                        animationFrame = 1;
                    }
                    animationTimer -= .125;
                }
                source = new Rectangle(animationFrame * 32, 3 * 32, 32, 32);
                spriteBatch.Draw(texture, position, source, color, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0);
            }

            if(jumping && jumpFrame >= 10)
            {
                jumping = false;
                jumpFrame = 1;
                animationFrame = 1;
            }
            
        }
    }
}

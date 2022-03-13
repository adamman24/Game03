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
        private Vector2 velocity;
        private Vector2 acceleration;

        /// <summary>
        /// used for keyboard input
        /// </summary>
        private KeyboardState previousState;
        private KeyboardState currentState;

        //position of sprite
        public Vector2 position = new Vector2(50, 360);

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
            bounds = new BoundingRectangle(new Vector2(position.X - 32, position.Y - 32), 80,  40);
            //velocity = new Vector2(0, 10);
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
            

            /*if (currentState.IsKeyDown(Keys.Up) || currentState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (currentState.IsKeyDown(Keys.Down) || currentState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
            }
            if (currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
            }*/

            if(currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space) ||
               currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
            {
                jumping = true;
            }

            if(jumping)
            {
                Jump();
            }else
            {
                acceleration.Y = 0;
                velocity.Y = 0;
                position.Y = 360;
            }
            position.X += 5;
            //bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// this function controls the fox y position
        /// it will create a jump like movement 
        /// pretty sure this is not the right way
        /// but it works and brings great joy
        /// </summary>
        public void Jump()
        {
            if(position.Y == 360)
            {
                acceleration.Y = -.1f;
            }
            
            if (position.Y < 340)
            {
                acceleration.Y = .1f;
            }

            velocity.Y += acceleration.Y;
            position.Y += velocity.Y;

            if (position.Y > 3600)
            {
                position.Y = 360;
                acceleration.Y = 0;
            }
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

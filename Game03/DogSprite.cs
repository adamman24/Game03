using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Game03.Collisions;

namespace Game03
{
    public class DogSprite
    {
        private Texture2D texture;
        private double animationTimer;
        private short animationFrame = 4;

        public Vector2 Position = new Vector2(810, 370);

        private BoundingRectangle dogBounds;

        /// <summary>
        /// bounds for sprite
        /// </summary>
        public BoundingRectangle Bounds => dogBounds;

        /// <summary>
        /// color of sprite
        /// </summary>
        public Color color { get; set; } = Color.White;

        /// <summary>
        /// load all content for the game
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("LazyDog");
            dogBounds = new BoundingRectangle(Position, 30, 20);
        }

        /// <summary>
        /// update the dog position and bounds 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Position += new Vector2(-5 , 0);
            dogBounds.X = Position.X;
            dogBounds.Y = Position.Y;
        }

        /// <summary>
        /// draw dog and update sprite frame
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if(animationTimer > .1)
            {
                animationFrame++;
                if(animationFrame > 9)
                {
                    animationFrame = 4;
                }
                animationTimer -= .1;
            }

            var source = new Rectangle(animationFrame * 128, 0, 128, 128);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, Vector2.Zero, .5f, SpriteEffects.None, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game03.Collisions;
using Microsoft.Xna.Framework.Content;

namespace Game03
{
    /// <summary>
    /// this will create a finish line behind the fox so that
    /// when a dog hits that line its possition will be 
    /// reset to the other side of the screen so that the
    /// game can continue onward 
    /// </summary>
    public class ResetBox
    {
        private Vector2 _position = new Vector2(-70, 300);

        private Texture2D texture;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(-70, 300), 10, 300);

        /// <summary>
        /// bounds volume of finihsline
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("finishLine");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, _position, null, Color.White, 1.57f, new Vector2(64, 64), 1, SpriteEffects.None, 0);
        }
    }
}

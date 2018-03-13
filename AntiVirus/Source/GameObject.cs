using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AntiVirus.Source
{
    public abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 size;

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Vector2 Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

		public virtual void Translate(Vector2 translationVector)
		{
			this.position += translationVector;
		}

		public virtual void ReachedDestination()
		{

		}
    }
}

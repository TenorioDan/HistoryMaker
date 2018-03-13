using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AntiVirus.Source
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }

        private int animationCount;
        private Animation[] animations;
        private Animation currentAnimation;

        public AnimatedSprite(Texture2D texture, int maxAnimations)
        {
            Texture = texture;
            animationCount = 0;
            animations = new Animation[maxAnimations];
        }

        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw the current animation
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location, Vector2 size)
        {
            Vector2 sourcePosition = currentAnimation.GetNextFrame(gameTime);
            Rectangle sourceRectangle = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, (int)size.X, (int)size.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
        }

        /// <summary>
        /// Add an animation to the list of animations. Each animation contains a total frame count and
        /// a location in its respective spritesheet. Time between frames is in milliseconds
        /// </summary>
        public void AddAnimation(int frameCount, int timeBetweenFrames, Vector2 location)
        {
            Animation animation = new Animation(frameCount, timeBetweenFrames, location);
            animations[animationCount] = animation;
            animationCount++;
        }

        /// <summary>
        /// Set the new animation based on the integer position of the array
        /// </summary>
        public void SetAnimation(int animation)
        {
            currentAnimation = animations[animation];
			currentAnimation.ResetAnimation();
		}

        /// <summary>
        /// Animation wrapper class. Contains the data for an animation including it starting position in the 
        /// sprite sheet and the current frame the animation is at
        /// </summary>
        class Animation
        {
            public Vector2 sourcePosition;
            public int frameCount;

            // Code to determine when to switch between frames. 
            // Important to keep this in the animation class as different animations may
            // have different times to switch between
            private int timeBetweenFrames;
            private int currentAnimationTime = 0;
            private int currentFrame = 0;

            public Animation(int frameCount, int timeBetweenFrames, Vector2 sourcePosition)
            {
                this.sourcePosition = sourcePosition;
                this.frameCount = frameCount;
                this.timeBetweenFrames = timeBetweenFrames;
                //lastFrameUpate = new GameTime();
            }

            public void ResetAnimation()
            {
                currentFrame = 0;
            }

            public Vector2 GetNextFrame(GameTime gameTime)
            {
                currentAnimationTime += gameTime.ElapsedGameTime.Milliseconds;

                if (currentAnimationTime >= timeBetweenFrames)
                {
                    currentFrame++;

                    if (currentFrame > frameCount)
                    {
                        ResetAnimation();
                    }

                    currentAnimationTime = 0;
                }

                return new Vector2(sourcePosition.X * currentFrame, sourcePosition.Y);
            }
        }
    }
}

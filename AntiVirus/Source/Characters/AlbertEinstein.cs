using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source.Characters
{
	public class AlbertEinstein : Character
	{
		public AlbertEinstein(Texture2D spriteSheet)
			: base(spriteSheet)
		{
			animationCount = 8;
			IDLE_ANIMATION_UP_INDEX = 0;
			IDLE_ANIMATION_LEFT_INDEX = 0;
			IDLE_ANIMATION_DOWN_INDEX = 0;
			IDLE_ANIMATION_RIGHT_INDEX = 0;
			WALK_ANIMATION_UP_INDEX = 0;
			WALK_ANIMATION_LEFT_INDEX = 0;
			WALK_ANIMATION_DOWN_INDEX = 0;
			WALK_ANIMATION_RIGHT_INDEX = 0;

			// Idle Animations
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 0)); // Idle Up
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 0)); // Idle Left
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 0)); // Idle Down
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 0)); // Idle Right

			// Movement Animations
			animationSprite.AddAnimation(1, 60, new Vector2(0, Height * 0)); // Walking Up
			animationSprite.AddAnimation(1, 60, new Vector2(0, Height * 0)); // Walking Left
			animationSprite.AddAnimation(1, 60, new Vector2(0, Height * 0)); // Walking Down
			animationSprite.AddAnimation(1, 60, new Vector2(0, Height * 0)); // Walking Right

			animationSprite.SetAnimation(2);
		}
	}
}

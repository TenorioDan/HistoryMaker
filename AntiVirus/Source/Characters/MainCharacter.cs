using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source.Characters
{
	/// <summary>
	/// The main character of our story.
	/// TODO: Change this class name to the Character's name once decided
	/// </summary>
	public class MainCharacter : Character
	{
		public MainCharacter(Texture2D spriteSheet)
			: base(spriteSheet)
		{
			animationCount = 8;
			IDLE_ANIMATION_UP_INDEX = 0;
			IDLE_ANIMATION_LEFT_INDEX = 1;
			IDLE_ANIMATION_DOWN_INDEX = 2;
			IDLE_ANIMATION_RIGHT_INDEX = 3;
			WALK_ANIMATION_UP_INDEX = 4;
			WALK_ANIMATION_LEFT_INDEX = 5;
			WALK_ANIMATION_DOWN_INDEX = 6;
			WALK_ANIMATION_RIGHT_INDEX = 7;

			// Idle Animations
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 0)); // Idle Up
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 1)); // Idle Left
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 2)); // Idle Down
			animationSprite.AddAnimation(1, 0, new Vector2(0, Height * 3)); // Idle Right

			// Movement Animations
			animationSprite.AddAnimation(8, 60, new Vector2(64, Height * 0)); // Walking Up
			animationSprite.AddAnimation(8, 60, new Vector2(64, Height * 1)); // Walking Left
			animationSprite.AddAnimation(8, 60, new Vector2(64, Height * 2)); // Walking Down
			animationSprite.AddAnimation(8, 60, new Vector2(64, Height * 3)); // Walking Right

			animationSprite.SetAnimation(2);

			characterName = "TestChar";
		}
	}
}

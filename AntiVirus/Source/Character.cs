using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AntiVirus.Source
{
	public class Character : BattleSceneGameObject
	{
		protected enum AnimationState
		{
			IDLE_RIGHT,
			IDLE_LEFT,
			IDLE_UP,
			IDLE_DOWN,
			WALK_LEFT,
			WALK_UP,
			WALK_RIGHT,
			WALK_DOWN
		}

		#region Constants

		private const int animationCount = 8;
		private const int IDLE_ANIMATION_UP_INDEX = 0;
		private const int IDLE_ANIMATION_LEFT_INDEX = 1;
		private const int IDLE_ANIMATION_DOWN_INDEX = 2;
		private const int IDLE_ANIMATION_RIGHT_INDEX = 3;
		private const int WALK_ANIMATION_UP_INDEX = 4;
		private const int WALK_ANIMATION_LEFT_INDEX = 5;
		private const int WALK_ANIMATION_DOWN_INDEX = 6;
		private const int WALK_ANIMATION_RIGHT_INDEX = 7;

		#endregion

		#region PrivateVariables

		private AnimatedSprite animationSprite;
		private Vector2 lastTranslation;

		#endregion

		#region ProtectedVariables

		protected AnimationState currentAnimationState;
		protected float initiative;
		protected float moveDistance;

		#endregion

		#region Properties

		private UIClickable uiCharacterSelect;
		public UIClickable UICharacterSelect
		{
			get { return uiCharacterSelect; }
			set
			{
				this.uiCharacterSelect = value;
				this.uiCharacterSelect.Parent = this;
			}
		}
		public int Width { get; }
		public int Height { get; }
		#endregion

		public Character(Texture2D spriteSheet)
		{
			animationSprite = new AnimatedSprite(spriteSheet, animationCount);

			// TODO: Remove test code
			this.position = new Vector2(0, 0);
			this.Width = 64;
			this.Height = 64;

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
			currentAnimationState = AnimationState.IDLE_DOWN;
		}

		public override void Update(GameTime gameTime)
		{

		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			animationSprite.Draw(spriteBatch, gameTime, this.Position, new Vector2(Width, Height));
		}

		public override void Translate(Vector2 translationVector)
		{
			base.Translate(translationVector);
			UICharacterSelect.Translate(translationVector);

			if (lastTranslation != translationVector)
			{
				lastTranslation = translationVector;

				if (translationVector.X != 0)
				{
					bool vectorCheck = translationVector.X > 0;
					animationSprite.SetAnimation(vectorCheck ? WALK_ANIMATION_RIGHT_INDEX : WALK_ANIMATION_LEFT_INDEX);
					currentAnimationState = vectorCheck ? AnimationState.WALK_RIGHT : AnimationState.WALK_LEFT;
					
				}
				else if (translationVector.Y != 0)
				{
					bool vectorCheck = translationVector.Y > 0;
					animationSprite.SetAnimation(vectorCheck ? WALK_ANIMATION_DOWN_INDEX : WALK_ANIMATION_UP_INDEX);
					currentAnimationState = vectorCheck ? AnimationState.WALK_DOWN : AnimationState.WALK_UP;
				}
			}
		}

		public override void ReachedDestination()
		{
			base.ReachedDestination();

			switch (currentAnimationState)
			{
				case AnimationState.WALK_LEFT:
					animationSprite.SetAnimation(IDLE_ANIMATION_LEFT_INDEX);
					currentAnimationState = AnimationState.IDLE_LEFT;
					break;
				case AnimationState.WALK_RIGHT:
					animationSprite.SetAnimation(IDLE_ANIMATION_RIGHT_INDEX);
					currentAnimationState = AnimationState.IDLE_RIGHT;
					break;
				case AnimationState.WALK_DOWN:
					animationSprite.SetAnimation(IDLE_ANIMATION_DOWN_INDEX);
					currentAnimationState = AnimationState.IDLE_DOWN;
					break;
				case AnimationState.WALK_UP:
					animationSprite.SetAnimation(IDLE_ANIMATION_UP_INDEX);
					currentAnimationState = AnimationState.IDLE_UP;
					break;
			}

			lastTranslation = Vector2.Zero;
		}
	}
}

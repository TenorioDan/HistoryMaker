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

		protected int animationCount = 8;
		protected int IDLE_ANIMATION_UP_INDEX = 0;
		protected int IDLE_ANIMATION_LEFT_INDEX = 1;
		protected int IDLE_ANIMATION_DOWN_INDEX = 2;
		protected int IDLE_ANIMATION_RIGHT_INDEX = 3;
		protected int WALK_ANIMATION_UP_INDEX = 4;
		protected int WALK_ANIMATION_LEFT_INDEX = 5;
		protected int WALK_ANIMATION_DOWN_INDEX = 6;
		protected int WALK_ANIMATION_RIGHT_INDEX = 7;

		#endregion

		#region PrivateVariables

		#endregion

		#region ProtectedVariables

		protected AnimatedSprite animationSprite;
		protected Vector2 lastTranslation;
		protected AnimationState currentAnimationState;

		protected string characterName;
		protected float moveDistance;

		public int Initiative { get; set; }
		public int ActionPoints { get; set; }

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
			// animation stuff
			animationSprite = new AnimatedSprite(spriteSheet, animationCount);
			currentAnimationState = AnimationState.IDLE_DOWN;

			// TODO: Remove test code
			this.position = new Vector2(0, 0);
			this.Width = 64;
			this.Height = 64;
			this.ActionPoints = 2;

			// UI Stuff
			UICharacterSelect = new UIClickable(this.Position, this.Width, this.Height);
		}

		public override void Update(GameTime gameTime)
		{

		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			animationSprite.Draw(spriteBatch, gameTime, this.Position, new Vector2(Width, Height));
		}

		/// <summary>
		/// Move the character across the screen and change animations accordingly
		/// </summary>
		/// <param name="translationVector"></param>
		public override void Translate(Vector2 translationVector)
		{
			// Remove two action points for moving
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

		/// <summary>
		/// Once the character has reached their destination set the appropriate animation states
		/// </summary>
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

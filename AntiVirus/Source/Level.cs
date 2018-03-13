using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AntiVirus.Source
{
	/// <summary>
	/// Level Class that will facilitate communication between input and gameplay scenes
	/// </summary>
	class Level
	{
		private BattleScene currentBattleScene;
		private Character mainCharacter;
		private Character jesusChrist;
		private Character adolfHilter;
		private Character albertEinstein;
		private Character georgeWashington;

		private Character currentCharacter;
		private Camera camera;

		// TODO: Create UI Manager class and tie it to an Input Manager class
		List<UIClickable> buttons;

		#region Textures

		Texture2D testCharacterSpriteSheet;
		Texture2D jesusSpriteSheet;
		Texture2D hitlerSpriteSheet;
		Texture2D einsteinSpriteSheet;
		Texture2D washingtonSpriteSheet;

		Texture2D testLevelTileSheet;
		#endregion

		#region Input States

		#endregion

		public Level()
		{
			Vector2 minBounds = Vector2.Zero;
			Vector2 maxBounds = new Vector2((20 * 64), (15 * 64));
			camera = new Camera(new Vector2(0.0f, 0.0f), minBounds, maxBounds);

			// UI stuff
			buttons = new List<UIClickable>();
		}

		public void LoadContent(ContentManager Content)
		{
			// Load all the sprite sheets
			testCharacterSpriteSheet = Content.Load<Texture2D>("Images/character_test");
			jesusSpriteSheet = Content.Load<Texture2D>("Images/Characters/sprites_characters_jesus1");
			hitlerSpriteSheet = Content.Load<Texture2D>("Images/Characters/sprites_characters_hitler1");
			einsteinSpriteSheet = Content.Load<Texture2D>("Images/Characters/sprites_characters_einstein1");
			washingtonSpriteSheet = Content.Load<Texture2D>("Images/Characters/sprites_characters_washington1");
			testLevelTileSheet = Content.Load<Texture2D>("Images/TileSets/PathAndObjects");

			// Create the character objects after their respective spritesheets have been loaded
			mainCharacter = new Character(testCharacterSpriteSheet);
			UIClickable mainCharacterButton = new UIClickable(mainCharacter.Position, mainCharacter.Width, mainCharacter.Height);
			mainCharacterButton.UIClicked += b_SetCurrentCharacter;
			mainCharacter.UICharacterSelect = mainCharacterButton;

			buttons.Add(mainCharacterButton);

			// Set up the game
			currentBattleScene = new BattleScene(20, 15, testLevelTileSheet);
			//currentCharacter = mainCharacter;
		}

		public void Update(GameTime gameTime)
		{
			ReceiveInput();
			currentBattleScene.Update(gameTime);
			mainCharacter.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Begin(transformMatrix: camera.GetModelMatrix());

			currentBattleScene.Draw(spriteBatch);
			mainCharacter.Draw(spriteBatch, gameTime);

			spriteBatch.End();
		}

		public void ReceiveInput()
		{
			CheckKeyboardInput(Keyboard.GetState());
			CheckMouseInput(Mouse.GetState());
		}

		private void CheckKeyboardInput(KeyboardState keyboardState)
		{
			// TODO: Remove magic numbers from camera translations
			Vector2 cameraTranslation = Vector2.Zero;

			if (keyboardState.IsKeyDown(Keys.A))
				cameraTranslation += new Vector2(-3.5f, 0);

			if (keyboardState.IsKeyDown(Keys.D))
				cameraTranslation += new Vector2(3.5f, 0);

			if (keyboardState.IsKeyDown(Keys.W))
				cameraTranslation += new Vector2(0, -3.5f);

			if (keyboardState.IsKeyDown(Keys.S))
				cameraTranslation += new Vector2(0, 3.5f);

			if (keyboardState.IsKeyDown(Keys.LeftShift))
				cameraTranslation *= 0.5f;

			camera.MoveCamera(cameraTranslation);
		}

		// Check all buttons and then we can check character shit
		private void CheckMouseInput(MouseState mouseState)
		{
			bool clickHandled = false;

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				foreach (UIClickable button in buttons)
				{
					if (button.Bounds.Contains(mouseState.Position))
					{
						clickHandled = true;
						button.WasUIClicked(clickHandled);
					}
				}

				// We already did something with our mouse click, don't have to have ui click overlap
				if (!clickHandled && currentCharacter != null)
				{
					Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
					Vector2 worldPosition = mousePosition + camera.GetScaledPosition();
					currentBattleScene.MoveObjectToTileAtPosition(currentCharacter, worldPosition);
				}
			}
		}

		public void b_SetCurrentCharacter(Object sender, UIClickable.UIClickedEventArgs e)
		{
			currentCharacter = (Character)e.Parent;
		}
	}
}

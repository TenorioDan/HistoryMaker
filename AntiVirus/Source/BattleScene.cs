using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using AntiVirus.Source.Input;
using AntiVirus.Source.UI;
using AntiVirus.Source.Characters;

namespace AntiVirus.Source
{
	/// <summary>
	/// Battle Scene that contains everything that happens during combat
	/// </summary>
	class BattleScene
	{
		private TileManager tileManager;
		private Character mainCharacter;
		private Character jesusChrist;
		private Character adolfHilter;
		private Character albertEinstein;
		private Character georgeWashington;

		private Character currentCharacter;
		private Camera camera;

		private MouseState currentMouseState;
		private MouseState previousMouseState;
		private Vector2 cursorPosition;
		private Texture2D cursorTexture;

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

		/// <summary>
		///  Initialize all manager classes in this level. Manager classes switch between
		///  battle scenes and overworld scenes
		/// </summary>
		public BattleScene()
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
			cursorTexture = Content.Load<Texture2D>("Images/UI/Cursor");
			// Load UI Elements


			// Create the character objects after their respective spritesheets have been loaded
			// Each playable characte object will have a UI Select associated with them for either choosing them
			// as the current character or viewing their stats
			mainCharacter = new MainCharacter(testCharacterSpriteSheet);
			mainCharacter.UICharacterSelect.UIClicked += SetCurrentCharacter;
			buttons.Add(mainCharacter.UICharacterSelect);

			albertEinstein = new AlbertEinstein(einsteinSpriteSheet);
			albertEinstein.UICharacterSelect.UIClicked += SetCurrentCharacter;
			buttons.Add(albertEinstein.UICharacterSelect);
			albertEinstein.Translate(new Vector2(320, 320));

			// Set up the game
			tileManager = new TileManager(20, 15, testLevelTileSheet);
			//currentCharacter = mainCharacter;
		}
		
		public void Update(GameTime gameTime)
		{
			ReceiveInput();
			tileManager.Update(gameTime);

			// Update Characters
			mainCharacter.Update(gameTime);
			albertEinstein.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

			tileManager.Draw(spriteBatch);

			// Draw Characters
			mainCharacter.Draw(spriteBatch, gameTime);
			albertEinstein.Draw(spriteBatch, gameTime);
			spriteBatch.Draw(cursorTexture, cursorPosition, Color.White);

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

			MoveCameraAroundBattlefield(cameraTranslation);
		}

		// Check all buttons and then we can check character shit
		private void CheckMouseInput(MouseState mouseState)
		{
			previousMouseState = currentMouseState;
			currentMouseState = mouseState;

			cursorPosition = camera.ScreenToWorld(XnaHelpers.Point2Vector(currentMouseState.Position));

			bool clickHandled = false;

			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				foreach (UIClickable button in buttons)
				{
					if (button.Bounds.Contains(cursorPosition))
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
					tileManager.MoveObjectToTileAtPosition(currentCharacter, cursorPosition);
				}
			}
		}

		private void MoveCameraAroundBattlefield(Vector2 moveVector)
		{
			camera.MoveCamera(moveVector);
		}

		#region Events

		public void SetCurrentCharacter(Object sender, UIClickable.UIClickedEventArgs e)
		{
			currentCharacter = (Character)e.Parent;
		}

		#endregion
	}
}

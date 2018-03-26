using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source.UI
{
	class BattleSceneUI
	{
		#region Fonts

		SpriteFont battleTextFont;

		#endregion

		public string CurrentCharacterName { get; set; }
		public int CurrentActionPoints { get; set; }

		private Vector2 characterTextPosition;
		private Vector2 actionPointsTextPosition;

		public BattleSceneUI()
		{
			CurrentCharacterName = "";
			CurrentActionPoints = 0;
			characterTextPosition = new Vector2(100, 100);
			actionPointsTextPosition = new Vector2(100, 150);

		}

		public void LoadContent(ContentManager Content)
		{
			battleTextFont = Content.Load<SpriteFont>("Fonts/BattleText");
		}

		public void Update(GameTime gameTime)
		{

		}

		/// <summary>
		/// Draw stuff like we normally but since UI doesn't typically move, we need to make sure they
		/// stay visible
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="camera"></param>
		public void Draw(SpriteBatch spriteBatch, Camera camera)
		{
			Vector2 worldPosition = camera.ScreenToWorld(characterTextPosition);
			spriteBatch.DrawString(battleTextFont, "Current Turn: " + CurrentCharacterName, worldPosition, Color.White);

			worldPosition = camera.ScreenToWorld(actionPointsTextPosition);
			spriteBatch.DrawString(battleTextFont, "AP: " + CurrentActionPoints, worldPosition, Color.White);
		}
	}
}

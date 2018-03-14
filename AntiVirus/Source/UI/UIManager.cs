using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source.UI
{
	public class UIManager
	{
		// We will need lists of all the different kinds of UI Elements
		List<UIClickable> clickables;

		public UIManager()
		{
			clickables = new List<UIClickable>();
		}

		public void AddUIClickable(UIClickable ui)
		{
			clickables.Add(ui);
		}

		public void Update(GameTime gameTime)
		{
			
		}

		public void Draw(SpriteBatch spriteBatch)
		{

		}

		/// <summary>
		/// Check if any clickable UI Elements have been clicked
		/// </summary>
		private void CheckUIClickables(Point mousePosition)
		{
			foreach (UIClickable button in clickables)
			{
				button.WasUIClicked(button.Bounds.Contains(mousePosition));
			}
		}
	}
}

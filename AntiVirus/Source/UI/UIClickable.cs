using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source
{
	/// <summary>
	/// As it stands this assume that a UIClickable is attached to a game object like a characters.
	/// This is helpful because we can attach an event to this type of ui that is need when something
	/// happens to the game object it is attached to. Character specific UI may need this even more
	/// such as changing health bars, portraits, and abilities
	/// </summary>
	public class UIClickable
	{
		#region Properties

		private Rectangle bounds;
		public Rectangle Bounds
		{
			get { return bounds; }
		}

		

		// TODO: Create new UIClickable class that is independent of game objects. Menu UI should have
		// functionality that does not depend on a Game Object. 
		public GameObject Parent { get; set; }

		#endregion

		public event EventHandler<UIClickedEventArgs> UIClicked;

		public UIClickable(Vector2 position, int width, int height)
		{
			int scaledWidth = (int)(width * Globals.ScaleX);
			int scaledHeight = (int)(height * Globals.ScaleY);
			bounds = new Rectangle((int)position.X, (int)position.Y, scaledWidth, scaledHeight);
		}

		public void WasUIClicked(bool wasClicked)
		{
			if (wasClicked)
			{
				UIClickedEventArgs args = new UIClickedEventArgs
				{
					Parent = this.Parent
				};

				OnUIClicked(args);
			}
		}

		protected virtual void OnUIClicked(UIClickedEventArgs e)
		{
			UIClicked?.Invoke(this, e);
		}

		public void Translate(Vector2 translationVector)
		{
			this.bounds.X += (int)translationVector.X;
			this.bounds.Y += (int)translationVector.Y;
		}

		public void ResizeBounds(int width, int height)
		{
			this.bounds.Width = (int)(width * Globals.ScaleX);
			this.bounds.Height = (int)(height * Globals.ScaleY);
		}

		public class UIClickedEventArgs : EventArgs
		{
			public GameObject Parent { get; set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AntiVirus.Source.UI;


namespace AntiVirus.Source.Input
{
	public class InputManager
	{
		private MouseState currentMouseState;
		private KeyboardState currentKeyboardState;
		public MouseState CurrentMouseState { get { return currentMouseState; } }
		public KeyboardState CurrentKeyboardState { get { return currentKeyboardState; } }

		private UIManager uiManager;

		/// <summary>
		/// Does it make sense to have a reference to the existing UI manager? Input really only matters if
		/// there is a corresponding UI right? This way we can change out existing UIs with the current input 
		/// manager? I guess we'll find out
		/// </summary>
		public InputManager()
		{

		}

		public void Update(GameTime gameTime)
		{
			ReceiveKeyboardInput();
			ReceiveMouseInput();
		}

		public void ReceiveKeyboardInput()
		{
			currentKeyboardState = Keyboard.GetState();
		}

		public void ReceiveMouseInput()
		{
			currentMouseState = Mouse.GetState();
		}
	}
}

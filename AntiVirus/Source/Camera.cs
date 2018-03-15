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
	public class Camera
	{
		private Vector3 position;
		private Vector2 minBounds;
		private Vector2 maxBounds;

		public Vector2 Origin { get; set; }
		public Vector2 Position
		{
			get { return new Vector2(this.position.X, this.position.Y); }
		}

		public float ZoomX { get; set; }
		public float ZoomY { get; set; }
		public float Rotation { get; set; }

		public Camera(Vector2 position, Vector2 minBounds, Vector2 maxBounds)
		{

			this.position = new Vector3(position.X, position.Y, 0.0f);
			this.minBounds = minBounds;
			this.maxBounds = maxBounds;
			this.Origin = new Vector2(Globals.ResolutionWidth / 2.0f, Globals.ResolutionHeight / 2.0f);
		}

		public void MoveCamera(Vector2 translationVector)
		{
			this.position += new Vector3(translationVector.X, translationVector.Y, 0);

			//if (this.position.X < minBounds.X * Globals.ScaleX)
			//	this.position.X = minBounds.X;
			//else if (GetScaledPosition().X >= maxBounds.X * Globals.ScaleX - Globals.ResolutionWidth)
			//	this.position.X = (maxBounds.X * Globals.ScaleX - Globals.ResolutionWidth) / Globals.ScaleX;

			//if (this.position.Y < minBounds.Y * Globals.ScaleY)
			//	this.position.Y = minBounds.Y;
			//else if (GetScaledPosition().Y >= maxBounds.Y * Globals.ScaleY - Globals.ResolutionHeight)
			//	this.position.Y = (maxBounds.Y * Globals.ScaleY - Globals.ResolutionHeight) / Globals.ScaleY;
		}

		public Matrix GetViewMatrix()
		{
			return Matrix.CreateTranslation(-this.position) *
				Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(Globals.ScaleX, Globals.ScaleY, 1) *
				Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
		}

		// Because the game is scaled to fit the selected resolution, we may need to use a virtual position
		// that is scaled to the resolution
		public Vector2 GetScaledPosition()
		{
			return new Vector2(this.position.X, this.position.Y) * new Vector2(Globals.ScaleX, Globals.ScaleY);
		}

		public Vector2 WorldToScreen(Vector2 worldPosition)
		{
			return Vector2.Transform(worldPosition, GetViewMatrix());
		}

		public Vector2 ScreenToWorld(Vector2 screenPosition)
		{
			return Vector2.Transform(screenPosition, Matrix.Invert(GetViewMatrix()));
		}
	}
}

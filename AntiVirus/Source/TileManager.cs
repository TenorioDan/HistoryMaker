using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source
{
	public class TileManager
	{
		#region Constants

		public const int TILE_WIDTH = 64;
		public const int TILE_HEIGHT = 64;

		#endregion

		private Tile[,] tileset;
		private int tilesetWidth;
		private int tilesetHeight;

		private BattleSceneGameObject currentObject;
		private Tile tileToMoveTo;
		private Texture2D tilesheet;
		private Rectangle[] tileSourceRectangles;

		private bool performingMove;
		public bool PerformingMove { get { return performingMove; } }

		public TileManager(int tilesetWidth, int tilesetHeight, Texture2D tilesheet)
		{
			this.performingMove = false;
			this.tilesetWidth = tilesetWidth;
			this.tilesetHeight = tilesetHeight;
			this.tilesheet = tilesheet;
			tileset = new Tile[tilesetWidth, tilesetHeight];
			Random random = new Random();

			for (int x = 0; x < this.tilesetWidth; ++x)
			{
				for (int y = 0; y < this.tilesetHeight; ++y)
				{
					Tile tile = new Tile
					{
						position = new Vector2(x * TILE_WIDTH, y * TILE_HEIGHT),
						tileID = random.Next(0, 4),
						occupied = false
					};

					tileset[x, y] = tile;
				}
			}

			// TODO: Implement dynamic tile loading based on tile mapping
			tileSourceRectangles = new Rectangle[4];
			tileSourceRectangles[0] = new Rectangle(0, 0, TILE_WIDTH, TILE_HEIGHT);
			tileSourceRectangles[1] = new Rectangle(64, 0, TILE_WIDTH, TILE_HEIGHT);
			tileSourceRectangles[2] = new Rectangle(128, 0, TILE_WIDTH, TILE_HEIGHT);
			tileSourceRectangles[3] = new Rectangle(192, 0, TILE_WIDTH, TILE_HEIGHT);
		}

		public void Update(GameTime gameTime)
		{
			UpdateGameObjects();
		}

		/// <summary>
		/// Loop through the tileset and draw each tile according to the tile id
		/// </summary>
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int x = 0; x < this.tilesetWidth; ++x)
			{
				for (int y = 0; y < this.tilesetHeight; ++y)
				{
					Rectangle sourceRectangle = tileSourceRectangles[this.tileset[x, y].tileID];
					Rectangle destinationRectangle = new Rectangle(x * TILE_WIDTH, y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
					spriteBatch.Draw(this.tilesheet, destinationRectangle, sourceRectangle, Color.White);
				}
			}
		}

		/// <summary>
		/// Find the tile at the position clicked and move the object to that position
		/// set the objects previous tile to unoccupied
		/// </summary>
		public void MoveObjectToTileAtPosition(Character character, Vector2 position)
		{
			int tileX = (int)(position.X / (TILE_WIDTH));
			int tileY = (int)(position.Y / (TILE_HEIGHT));

			if (tileX >= 0 && tileX <= tilesetWidth &&
				tileY >= 0 && tileY <= tilesetHeight && !performingMove)
			{
				performingMove = true;
				character.ActionPoints -= 2;

				Tile previousTile = character.CurrentTile;
				previousTile.occupied = false;

				tileToMoveTo = tileset[tileX, tileY];
				tileToMoveTo.currentObject = character;
				tileToMoveTo.occupied = true;

				currentObject = character;
				currentObject.CurrentTile = tileToMoveTo;
			}
		}

		private void UpdateGameObjects()
		{
			if (currentObject != null)
			{
				Vector2 translationVector = new Vector2();

				// TODO: There has to a better way to do this
				if (currentObject.Position.X < tileToMoveTo.position.X)
				{
					translationVector += new Vector2(1.0f, 0.0f);
				} 
				else if (currentObject.Position.X > tileToMoveTo.position.X)
				{
					translationVector += new Vector2(-1.0f, 0.0f);
				}
				else if (currentObject.Position.Y < tileToMoveTo.position.Y)
				{
					translationVector += new Vector2(0.0f, 1.0f);
				}
				else if (currentObject.Position.Y > tileToMoveTo.position.Y)
				{
					translationVector += new Vector2(0.0f, -1.0f);
				}

				currentObject.Translate(translationVector);

				if (currentObject.Position == tileToMoveTo.position)
				{
					currentObject.ReachedDestination();
					currentObject = null;
					performingMove = false;
				}
			}
		}


		public struct Tile
		{
			public Vector2 position;
			public GameObject currentObject;

			public int tileID;
			public bool occupied;
		}
	}
}

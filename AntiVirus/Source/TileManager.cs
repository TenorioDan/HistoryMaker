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

		// Should only ever be changed by tilemanager
		private bool performingMove;
		public bool PerformingMove { get { return performingMove; } }

		/// <summary>
		/// Creates the board for battle scenes. This constructor will be responsible for
		/// creating all tiles and assigning their texture IDs and neighbors.
		/// </summary>
		/// <param name="tilesetWidth"></param>
		/// <param name="tilesetHeight"></param>
		/// <param name="tilesheet"></param>
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
					Vector2 position = new Vector2(x * TILE_WIDTH, y * TILE_HEIGHT);
					int tileID = random.Next(0, 4); // TODO: rename to texture ID?

					Tile tile = new Tile(position, null, tileID);

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
					Rectangle sourceRectangle = tileSourceRectangles[this.tileset[x, y].TileID];
					Rectangle destinationRectangle = new Rectangle(x * TILE_WIDTH, y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
					spriteBatch.Draw(this.tilesheet, destinationRectangle, sourceRectangle, Color.White);
				}
			}
		}

		/// <summary>
		///  Find the tile at the position clicked and move the object to that position
		///  This has to take a character object right now. Not sure if they will ever have to
		///  to take anything other than a character
		/// </summary>
		/// <param name="character"></param>
		/// <param name="position"></param>
		public void MoveObjectToTileAtPosition(Character character, Vector2 position)
		{
			int tileX = (int)(position.X / (TILE_WIDTH));
			int tileY = (int)(position.Y / (TILE_HEIGHT));
			tileToMoveTo = tileset[tileX, tileY];

			if (!tileToMoveTo.Occupied &&
				tileX >= 0 && tileX <= tilesetWidth &&
				tileY >= 0 && tileY <= tilesetHeight && !performingMove)
			{
				performingMove = true;
				character.ActionPoints -= 2;

				Tile previousTile = character.CurrentTile;
				previousTile.Occupied = false;

				tileToMoveTo.CurrentObject = character;
				tileToMoveTo.Occupied = true;

				currentObject = character;
				currentObject.CurrentTile = tileToMoveTo;
			}
		}

		private void UpdateGameObjects()
		{
			if (currentObject != null)
			{
				Vector2 translationVector = new Vector2();

				// TODO: replace with A* pathfinding
				if (currentObject.Position.X < tileToMoveTo.Position.X)
				{
					translationVector += new Vector2(1.0f, 0.0f);
				} 
				else if (currentObject.Position.X > tileToMoveTo.Position.X)
				{
					translationVector += new Vector2(-1.0f, 0.0f);
				}
				else if (currentObject.Position.Y < tileToMoveTo.Position.Y)
				{
					translationVector += new Vector2(0.0f, 1.0f);
				}
				else if (currentObject.Position.Y > tileToMoveTo.Position.Y)
				{
					translationVector += new Vector2(0.0f, -1.0f);
				}

				currentObject.Translate(translationVector);

				if (currentObject.Position == tileToMoveTo.Position)
				{
					currentObject.ReachedDestination();
					currentObject = null;
					performingMove = false;
				}
			}
		}

		public Tile GetTileAtPosition(Vector2 position)
		{
			// Variables can be taken out but they improve readability
			int tileX = (int)(position.X / (TILE_WIDTH));
			int tileY = (int)(position.Y / (TILE_HEIGHT));
			return tileset[tileX, tileY];
		}

		/// <summary>
		/// Place the object at the tile by passed position and set the tile properties
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="position"></param>
		public void SetObjectToTilePosition(BattleSceneGameObject gameObject, Vector2 position)
		{
			Tile newTile = GetTileAtPosition(position);
			newTile.Occupied = true;
			gameObject.CurrentTile = newTile;
			gameObject.Position = newTile.Position;

		}

		public class Tile
		{
			// this should never change so it will only need an accessor
			private Vector2 position;

			// this might change but for now lets assume it doesn't
			private int tileID;

			Tile[] neighbors;

			public Vector2 Position { get { return this.position; } }
			public GameObject CurrentObject { get; set; }
			public int TileID { get { return this.tileID; } }
			public bool Occupied { get; set; }

			public Tile(Vector2 position, GameObject currentObject, int tileID)
			{
				this.position = position;
				this.CurrentObject = currentObject;
				this.tileID = tileID;
				Occupied = false;
			}
		}
	}
}

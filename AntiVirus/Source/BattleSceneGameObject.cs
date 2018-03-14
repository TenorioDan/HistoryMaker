using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AntiVirus.Source
{
	public abstract class BattleSceneGameObject : GameObject
	{
		protected TileManager.Tile currentTile;

		public TileManager.Tile CurrentTile
		{
			get { return currentTile; }
			set { currentTile = value; }
		}

		public override void Translate(Vector2 translationVector)
		{
			base.Translate(translationVector);
		}

		public override void ReachedDestination()
		{
			base.ReachedDestination();
		}
	}
}

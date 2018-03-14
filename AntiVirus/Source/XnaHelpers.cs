using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntiVirus.Source
{
	public class XnaHelpers
	{
		public static Vector2 Point2Vector(Point p)
		{
			return new Vector2(p.X, p.Y);
		}
	}
}

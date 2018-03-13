using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
	public class Globals
	{
		private static float scaleX;
		private static float scaleY;
		private static float resolutionWidth;
		private static float resolutionHeight;

		public static float ScaleX
		{
			get { return scaleX; }
			set { scaleX = value; }
		}

		public static float ScaleY
		{
			get { return scaleY; }
			set { scaleY = value; }
		}

		public static float ResolutionWidth
		{
			get { return resolutionWidth; }
			set { resolutionWidth = value; }
		}

		public static float ResolutionHeight
		{
			get { return resolutionHeight; }
			set { resolutionHeight = value; }
		}

	}
}

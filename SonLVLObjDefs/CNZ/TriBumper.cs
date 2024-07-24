using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class TriBumper : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			// values taken from the script
			int[][] sizes = {new int[] {-32, -32, 32, 32}, new int[] {-32, -32, 32, 32}, new int[] {-64, -8, 64, 8}, new int[] {-64, -8, 64, 8}, new int[] {-8, -64, 8, 64}, new int[] {-8, -64, 8, 64}};
			debug = new Sprite[sizes.Length];
			for (int i = 0; i < sizes.Length; i++)
			{
				BitmapBits bitmap = new BitmapBits(sizes[i][2] - sizes[i][0] + 1, sizes[i][3] - sizes[i][1] + 1);
				bitmap.DrawRectangle(6, 0, 0, bitmap.Width-1, bitmap.Height-1); // LevelData.ColorWhite
				debug[i] = new Sprite(bitmap, sizes[i][0], sizes[i][1]);
			}
			
			properties[0] = new PropertySpec("Size", typeof(bool), "Extended",
				"What type of bumper this object is.", null, new Dictionary<string, int>
				{
					{ "Small (Left)", 0 },
					{ "Small (Right)", 1 },
					{ "Large (Upwards)", 2 },
					{ "Large (Downwards)", 3 },
					{ "Large (Left)", 4 },
					{ "Large (Right)", 5 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			if (subtype > 5) subtype = 6;
			
			string[] names = {"Small (Bounce Left)", "Small (Bounce Right)", "Large (Bounce Upwards)", "Large (Bounce Downwards)", "Large (Bounce Left)", "Large (Bounce Right)", "Unknown"};
			return names[subtype];
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 5) ? null : debug[obj.PropertyValue];
		}
	}
}
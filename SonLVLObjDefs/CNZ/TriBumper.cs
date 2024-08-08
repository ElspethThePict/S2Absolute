using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class TriBumper : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[6];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			// Small, facing left
			BitmapBits bitmap = new BitmapBits(64, 64);
			bitmap.DrawLine(6, 63, 0, 63, 63);
			bitmap.DrawLine(6, 63, 63, 0, 63);
			bitmap.DrawLine(6, 63, 0, 0, 63);
			debug[0] = new Sprite(bitmap, -32, -32);
			
			// Small, facing right
			debug[1] = new Sprite(debug[0], true, false);
			
			// Large, upwards
			bitmap = new BitmapBits(128, 128);
			bitmap.DrawLine(6, 0, 15, 127, 15);
			bitmap.DrawLine(6, 0, 15, 63, 0);
			bitmap.DrawLine(6, 63, 0, 127, 15);
			debug[2] = new Sprite(bitmap, -64, -8);
			
			// Large, downwards
			debug[3] = new Sprite(debug[2], false, true);
			
			bitmap.Rotate(1);
			
			// Large, facing left
			debug[4] = new Sprite(bitmap, -8, -64);
			
			// Large, facing right
			debug[5] = new Sprite(debug[4], true, false);
			
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
		
		public override bool Debug
		{
			get { return true; }
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
using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class ShipFG : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[9];
		private Sprite[] debug = new Sprite[9];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(153, 223, 64, 32), -32, -16);
			sprites[1] = new Sprite(sheet.GetSection(344, 157, 80, 16), -40, -8);
			sprites[2] = new Sprite(sheet.GetSection(344, 157, 16, 16), -8, -8);
			sprites[3] = new Sprite(sheet.GetSection(344, 141, 64, 16), -40, -8);
			sprites[4] = new Sprite(sheet.GetSection(392, 141, 32, 16), -16, -8);
			sprites[5] = new Sprite(sheet.GetSection(392, 157, 32, 16), -16, -8);
			sprites[6] = new Sprite(sheet.GetSection(153, 215, 64, 8), -32, -4);
			sprites[7] = new Sprite(sheet.GetSection(344, 141, 16, 8), -8, -4);
			sprites[8] = new Sprite(sheet.GetSection(72, 247, 80, 8), -40, -4);
			
			for (int i = 0; i < sprites.Length; i++)
			{
				Rectangle bounds = sprites[i].Bounds;
				BitmapBits overlay = new BitmapBits(bounds.Size);
				overlay.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1);
				debug[i] = new Sprite(overlay, bounds.X, bounds.Y);
			}
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object should display.", null, new Dictionary<string, int>
				{
					{ "Frame 1", 0 },
					{ "Frame 2", 1 },
					{ "Frame 3", 2 },
					{ "Frame 4", 3 },
					{ "Frame 5", 4 },
					{ "Frame 6", 5 },
					{ "Frame 7", 6 },
					{ "Frame 8", 7 },
					{ "Frame 9", 8 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Frame " + (subtype + 1);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue];
		}
	}
}
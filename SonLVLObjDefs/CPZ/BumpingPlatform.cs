using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class BumpingPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private readonly Sprite[] sprites = new Sprite[6];
		private readonly Sprite[] debug = new Sprite[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
			{
				sprites[4] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(6, 204, 48, 14), -24, -8);
			}
			else
			{
				sprites[4] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(469, 692, 48, 14), -24, -8);
			}
			
			sprites[5] = new Sprite(new Sprite(sprites[4], -24, 0), new Sprite(sprites[4], 24, 0));
			
			// (copy of BumpingPlatform_offsetTable from the object's script)
			int[][] offset_table = {
				new int[] {-0x680000, 0x000000},
				new int[] {-0xB00000, 0x400000},
				new int[] {-0x780000, 0x800000},
				new int[] { 0x670000, 0x000000}
			};
			
			for (int i = 0; i < offset_table.Length; i++)
			{
				sprites[i] = new Sprite(sprites[4], offset_table[i][0] >> 16, 0);
				if (offset_table[i][1] != 0)
					sprites[i] = new Sprite(sprites[i], new Sprite(sprites[4], offset_table[i][1] >> 16, 0));
			}
			
			BitmapBits overlay = new BitmapBits(1024, 2);
			
			overlay.DrawLine(6, 0, 0, 255, 0); // LevelData.ColorWhite
			debug[0] = new Sprite(overlay, -128, -2);
			
			overlay.DrawLine(6, 0, 0, 383, 0); // LevelData.ColorWhite
			debug[1] = new Sprite(overlay, -192, -2);
			
			overlay.DrawLine(6, 0, 0, 511, 0); // LevelData.ColorWhite
			debug[2] = new Sprite(overlay, -256, -2);
			
			// Instead of noting platform count, let's note platform distance
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far this platform should move.", null, new Dictionary<string, int>
				{
					{ "256px (Start From Left)", 0 },
					{ "256px (Start From Right)", 3 },
					{ "384px", 1 },
					{ "512px", 2 },
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 3, 1, 2}); }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
				default:
					return "256px (Start From Left)";
				case 3:
					return "256px (Start From Right)";
				case 1:
					return "384px (Two Platforms)";
				case 2:
					return "512px (Two Platforms)";
			}
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 1 || subtype == 2) ? 5 : 4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue < 4) ? obj.PropertyValue : 4];
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 1 || obj.PropertyValue == 2) ? obj.PropertyValue : 0];
		}
	}
}

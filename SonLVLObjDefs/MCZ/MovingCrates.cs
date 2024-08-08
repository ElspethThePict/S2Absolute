using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class MovingCrates : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(136, 1, 64, 64), -32, -32);
			}
			else
			{
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(797, 697, 64, 64), -32, -32);
			}
			
			// sprites[0] - single crate (icon)
			// sprites[1] - all three crates (used in-game)
			sprites[1] = new Sprite(sprites[0], new Sprite(sprites[0],  0x40, 0x40), new Sprite(sprites[0], -0x40, 0x40));
			
			int[] positions = {
				96, 32,
				160, 96,
				32, 96
			};
			
			int[][] offsets = new int[][] {
				new int[] {40, 0, -40, 0, 0, -40},
				new int[] {-40, 0, 0, -40, 40, 0}
			};
			
			for (int i = 0; i < offsets.Length; i++)
			{
				BitmapBits bitmap = new BitmapBits(192, 128);
				for (int j = 0; j < offsets[i].Length; j += 2)
					bitmap.DrawArrow(6, positions[j], positions[j+1], positions[j] + offsets[i][j], positions[j+1] + offsets[i][j+1]);
				
				debug[i] = new Sprite(bitmap, -96, -32);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction these Crates should rotate in.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Rotate Clockwise" : "Rotate Counter-Clockwise";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
	
	public static class BitmapBitsExtensions
	{
		public static void DrawArrow(this BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
		}
	}
}
using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class BallCannon : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[8];
		private Sprite[] debug = new Sprite[8];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[6];
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(133, 142, 48, 64), -24, -40);
				frames[1] = new Sprite(sheet.GetSection(133, 85, 48, 56), -24, -32);
				frames[2] = new Sprite(sheet.GetSection(133, 93, 48, 48), -24, -24);
				frames[3] = new Sprite(sheet.GetSection(1, 101, 64, 48), -24, -24);
				frames[4] = new Sprite(sheet.GetSection(1, 150, 56, 48), -24, -24);
				frames[5] = new Sprite(sheet.GetSection(1, 150, 48, 48), -24, -24);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				frames[0] = new Sprite(sheet.GetSection(813, 861, 48, 64), -24, -40);
				frames[1] = new Sprite(sheet.GetSection(764, 869, 48, 56), -24, -32);
				frames[2] = new Sprite(sheet.GetSection(764, 877, 48, 48), -24, -24);
				frames[3] = new Sprite(sheet.GetSection(763, 926, 64, 48), -24, -24);
				frames[4] = new Sprite(sheet.GetSection(771, 975, 56, 48), -24, -24);
				frames[5] = new Sprite(sheet.GetSection(771, 975, 48, 48), -24, -24);
			}
			
			// From this point on everything kind of becomes a mess, sorry...
			
			// in, out dir
			int[] types = {
				0, 3, // u, r
				3, 1, // r, d
				1, 2, // d, l
				2, 0, // l, u
				3, 0, // r, u
				1, 3, // d, r
				2, 1, // l, d
				0, 2  // u, l
			};
			
			// frame, direction
			int[] frameInfo = {
				0, 0, // up
				0, 2, // down
				3, 1, // left
				3, 0  // right
			};
			
			int[] debugInfo = {
				 0, -1, // up
				 0,  1, // down
				-1,  0, // left
				 1,  0  // right
			};
			
			for (int i = 0; i < 8; i++)
			{
				int inType = types[i * 2];
				int outType = types[(i * 2) + 1];
				
				sprites[i] = new Sprite(frames[frameInfo[inType * 2]]);
				sprites[i].Flip((frameInfo[(inType * 2) + 1] & 1) == 1, (frameInfo[(inType * 2) + 1] & 2) == 2);
				
				BitmapBits dbg = new BitmapBits(91, 91);
				dbg.DrawLine(6, 45, 45, debugInfo[inType * 2] * 45 + 45, debugInfo[(inType * 2) + 1] * 45 + 45); // LevelData.ColorWhite
				dbg.DrawArrow(6, 45, 45, debugInfo[outType * 2] * 38 + 45, debugInfo[(outType * 2) + 1] * 38 + 45);
				debug[i] = new Sprite(dbg, -45, -45);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Cannon should point from and fire towards.", null, new Dictionary<string, int>
				{
					{ "Up -> Right", 0 },
					{ "Right -> Down", 1 },
					{ "Down -> Left", 2 },
					{ "Left -> Up", 3 },
					{ "Right -> Up", 4 },
					{ "Down -> Right", 5 },
					{ "Left -> Down", 6 },
					{ "Up -> Left", 7 }
				},
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | (int)value));
			
			properties[1] = new PropertySpec("Release Controls", typeof(bool), "Extended",
				"Controls whether or not this Cannon will lock controls after launching the player.", null,
				(obj) => obj.PropertyValue > 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 7) | ((bool)value ? 8 : 0)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string[] directions = {"Up -> Right", "Right -> Down", "Down -> Left", "Left -> Up", "Right -> Up", "Down -> Right", "Left -> Down", "Up -> Left"};
			string name = directions[subtype & 7];
			if (subtype > 7) name += " (Release Controls)";
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 7];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 7];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 7];
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
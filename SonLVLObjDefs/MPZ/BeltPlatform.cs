using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2AObjectDefinitions.MPZ
{
	class BeltPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[5];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(34, 67, 32, 16), -16, -8);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Where this platform should start from.", null, new Dictionary<string, int>
				{
					{ "Top", 1 },
					{ "Left", 3 },
					{ "Bottom", 6 },
					{ "Right", 8 }
				},
				(obj) => obj.PropertyValue & 0x0f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | (int)value));
			
			
			properties[1] = new PropertySpec("Path", typeof(int), "Extended",
				"Which path this platform should follow.", null, new Dictionary<string, int>
				{
					{ "256px Tall", 0x00 },
					{ "384px Tall", 0x10 },
					{ "512px Tall", 0x20 }
				},
				(obj) => obj.PropertyValue & 0xf0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xf0) | (int)value));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the platform should move in.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => (int)((V4ObjectEntry)obj).Direction,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
			
			properties[3] = new PropertySpec("Base X", typeof(int), "Extended",
				"The base X position of this platform's path. Should be the middle of the conveyor belt.", null,
				(obj) => ((V4ObjectEntry)obj).Value0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = (int)value);
			
			properties[4] = new PropertySpec("Base Y", typeof(int), "Extended",
				"The base Y position of this platform's path. Should be the top of the conveyor belt.", null,
				(obj) => ((V4ObjectEntry)obj).Value1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (int)value);
			
			int[][] movementTables = new int[][] {
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 224,
					-22, 246,
					  0, 256,
					 22, 246,
					 32, 224,
					 32,  32,
					 22,  10,
					  0,   0
				},
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 352,
					-22, 374,
					  0, 384,
					 22, 374,
					 32, 352,
					 32,  32,
					 22,  10,
					  0,   0
				},
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 480,
					-22, 502,
					  0, 512,
					 22, 502,
					 32, 480,
					 32,  32,
					 22,  10,
					  0,   0
				}
			};
			
			for (int table = 0; table < 3; table++)
			{
				int xmin = 0;
				int ymin = 0;
				int xmax = 0;
				int ymax = 0;
				
				for (int i = 0; i < movementTables[table].Length; i += 2)
				{
					xmin = Math.Min(xmin, movementTables[table][i]);
					ymin = Math.Min(ymin, movementTables[table][i+1]);
					xmax = Math.Max(xmax, movementTables[table][i]);
					ymax = Math.Max(ymax, movementTables[table][i+1]);
				}
				
				BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
				for (int i = 2; i < movementTables[table].Length; i += 2)
				{
					bitmap.DrawLine(6, (movementTables[table][i-2]) - xmin, (movementTables[table][i-1]) - ymin, (movementTables[table][i]) - xmin, (movementTables[table][i+1]) - ymin); // LevelData.ColorWhite
				}
				
				debug[table] = new Sprite(bitmap, xmin, ymin);
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		// well there's DefaultSubtype, but if only there was a way to have defaults for object values..
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
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
			/*
			// original idea was (in addition to what's here already) to draw an arrow based on the platform's `"Start From", but that felt kind of too cluttered
			int[][] movementTables = new int[][] {
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 224,
					-22, 246,
					  0, 256,
					 22, 246,
					 32, 224,
					 32,  32,
					 22,  10,
					  0,   0
				},
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 352,
					-22, 374,
					  0, 384,
					 22, 374,
					 32, 352,
					 32,  32,
					 22,  10,
					  0,   0
				},
				new int[] {
					  0,   0,
					-22,  10,
					-32,  32,
					-32, 480,
					-22, 502,
					  0, 512,
					 22, 502,
					 32, 480,
					 32,  32,
					 22,  10,
					  0,   0
				}
			};
			*/
			
			// #Absolute - they removed belt activators, so..
			// we don't need to worry about 'em here
			
			int sx = ((V4ObjectEntry)obj).Value0 - obj.X;
			int sy = ((V4ObjectEntry)obj).Value1 - obj.Y;
			
			return new Sprite(debug[obj.PropertyValue >> 4], sx, sy);
		}
	}
}
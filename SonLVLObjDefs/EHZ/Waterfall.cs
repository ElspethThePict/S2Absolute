using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.EHZ
{
	class Waterfall : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[10];
		private ReadOnlyCollection<byte> subtypes;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			// Set up the debug visualisation for when the current frame is an empty sprite
			BitmapBits bitmap = new BitmapBits(65, 65);
			bitmap.DrawRectangle(6, 0, 0, 64, 64); // LevelData.ColorWhite
			sprites[9] = new Sprite(bitmap, -32, -32);
			
			// #Absolute - they rearranged this object.. but left the MBZ stuff alone and we technically have to support that even if the level doesn't exist so woo ig we're doing it this way now
			
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(192, 0, 64, 16), -32, -8);
				sprites[1] = new Sprite(sheet.GetSection(192, 0, 64, 256), -32, -128);
				sprites[2] = new Sprite(sheet.GetSection(192, 64, 64, 160), -32, -8);
				sprites[3] = new Sprite(sheet.GetSection(192, 48, 64, 128), -32, -8);
				sprites[4] = sprites[9];
				sprites[5] = new Sprite(sheet.GetSection(192, 48, 64, 48), -32, -8);
				sprites[6] = new Sprite(sheet.GetSection(192, 64, 64, 16), -32, -8);
				sprites[7] = new Sprite(sheet.GetSection(192, 0, 64, 192), -32, -128);
				sprites[8] = new Sprite(sheet.GetSection(192, 64, 64, 96), -32, -32);
				
				// (we skip 4 because it's blank)
				properties[0] = new PropertySpec("Length", typeof(int), "Extended",
					"How long this Waterfall is.", null, new Dictionary<string, int>
					{
						{ "Top", 0 },
						{ "16 px", 6 },
						{ "48 px", 5 },
						{ "96 px", 8 },
						{ "128 px", 3 },
						{ "160 px", 2 },
						{ "192 px", 7 },
						{ "256 px", 1 },
					},
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] {0, 6, 5, 8, 3, 2, 7, 1});
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 435, 64, 16), -32, -128);
				sprites[1] = new Sprite(sheet.GetSection(1, 435, 64, 256), -32, -128);
				sprites[2] = sprites[9]; // normally blank, let's give it a box
				sprites[3] = new Sprite(sheet.GetSection(1, 451, 64, 64), -32, -32);
				sprites[4] = sprites[9]; // normally blank, let's give it a box
				sprites[5] = new Sprite(sheet.GetSection(1, 451, 64, 160), -32, -64);
				sprites[6] = new Sprite(sheet.GetSection(1, 435, 64, 16), -32, -128); // not used, but set it up anyways
				sprites[7] = new Sprite(sheet.GetSection(1, 435, 64, 192), -32, -128);
				sprites[8] = new Sprite(sheet.GetSection(1, 499, 64, 96), -32, -32);
				
				// we skip a few numbers in this list, 2 and 4 are skipped because they're blank, 6 is skipped because it's the same as 0
				properties[0] = new PropertySpec("Length", typeof(int), "Extended",
					"How long this Waterfall is.", null, new Dictionary<string, int>
					{
						{ "Top", 0 },
						{ "64 px", 3 },
						{ "96 px", 8 },
						{ "160 px", 5 },
						{ "192 px", 7 },
						{ "256 px", 1 }
					},
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] {0, 3, 8, 5, 7, 1});
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return subtypes; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			if (properties[0].Enumeration.ContainsValue(subtype))
				return properties[0].Enumeration.GetKey(subtype) + " Frame";
			
			return "Blank"; // well technically MBZ's "Top 2" falls under here too, but let's just ignore that since it's not on the subtypes list anyways (and MBZ isn't even real in S2A either-)
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min((int)subtype, 9)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[Math.Min((int)obj.PropertyValue, 9)];
		}
	}
}
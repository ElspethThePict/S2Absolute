using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class WFZInvBlock : S2AObjectDefinitions.Global.InvisibleBlock
	{
		public override bool WFZBlock { get { return true; } }
	}
}

namespace S2AObjectDefinitions.Global
{
	class InvisibleBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[] sprites = new Sprite[5];
		
		// there's a couple of things we have to change, instead of overriding entire functions let's just do this instead
		public virtual bool WFZBlock { get { return false; } }
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 176, 16, 14), -8, -7);
			sprites[1] = new Sprite(sheet.GetSection(17, 176, 16, 14), -8, -7);
			sprites[2] = new Sprite(sheet.GetSection(1, 190, 16, 14), -8, -7);
			sprites[3] = new Sprite(sheet.GetSection(117, 19, 16, 14), -8, -7); // in-game uses a Mission Block, but let's stick with a Knuckles cube
			
			sprites[3] = new Sprite(new Sprite(sprites[3], -8, -8), new Sprite(sprites[3],  8, -8),
			                        new Sprite(sprites[3], -8,  8), new Sprite(sprites[3],  8,  8));
			
			// object icon, 2x2 box
			sprites[4] = new Sprite(new Sprite(sprites[0], -8, -8), new Sprite(sprites[0],  8, -8),
			                        new Sprite(sprites[0], -8,  8), new Sprite(sprites[0],  8,  8));
			
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the Invisible Block will be.", null,
				(obj) => (((V4ObjectEntry)obj).State != 3) ? ((obj.PropertyValue >> 4) + 1) : -1,
				(obj, value) => obj.PropertyValue = (((V4ObjectEntry)obj).State != 3) ? ((byte)((obj.PropertyValue & ~0xf0) | (Math.Min(Math.Max((int)value - 1, 0), 15) << 4))) : obj.PropertyValue); // could've sworn a Math had a Clamp function.. but ig it doesn't?
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the Invisible Block will be.", null,
				(obj) => (((V4ObjectEntry)obj).State != 3) ? ((obj.PropertyValue & 0x0f) + 1) : -1,
				(obj, value) => obj.PropertyValue = (((V4ObjectEntry)obj).State != 3) ? ((byte)((obj.PropertyValue & ~0x0f) | Math.Min(Math.Max((int)value - 1, 0), 15))) : obj.PropertyValue);
			
			if (WFZBlock)
			{
				properties[2] = new PropertySpec("Time Attack Only", typeof(bool), "Extended",
					"If this Block should only be in Time Attack.", null,
					(obj) => ((V4ObjectEntry)obj).Value0 == 1,
					(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value ? 1 : 0));
			}
			else
			{
				properties[2] = new PropertySpec("Mode", typeof(int), "Extended",
					"How this Invisible Block will act.", null, new Dictionary<string, int>
					{
						{ "Solid", 0 },
						{ "Eject Left", 1 },
						{ "Eject Right", 2 },
						{ "Climbable", 3 }
					},
					(obj) => ((V4ObjectEntry)obj).State,
					(obj, value) => ((V4ObjectEntry)obj).State = (int)value);
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x11}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x11; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype >> 4) + 1) + "x" + ((subtype & 0x0f) + 1) + " blocks";
		}

		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (((V4ObjectEntry)obj).State == 3 && !WFZBlock) // If we're a climbable block
				return sprites[3];
			
			int width = (obj.PropertyValue >> 4) + 1;
			int height = (obj.PropertyValue & 0x0f) + 1;
			
			int sx = (obj.PropertyValue & 0xf0) >> 1;
			int sy = (obj.PropertyValue & 0x0f) << 3;
			
			int index = (!WFZBlock && ((V4ObjectEntry)obj).State < 3) ? ((V4ObjectEntry)obj).State : 0;
			
			Sprite row = new Sprite();
			for (int i = 0; i < width; i++) // make a row, first
				row = new Sprite(row, new Sprite(sprites[index], -sx + (i * 16), 0));
			
			Sprite sprite = new Sprite();
			for (int i = 0; i < height; i++) // now, combine all the rows
				sprite = new Sprite(sprite, new Sprite(row, 0, -sy + (i * 16)));
			
			return sprite;
		}
	}
}
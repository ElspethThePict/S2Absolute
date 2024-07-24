using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Credits
{
	class CreditsEggman : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[10];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Ending/Credits.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 1, 60, 50), -29, -23);
			sprites[1] = new Sprite(sheet.GetSection(52, 53, 58, 39), -29, -19);
			sprites[2] = new Sprite(sheet.GetSection(1, 52, 16, 16), -8, -8);
			sprites[3] = new Sprite(sheet.GetSection(18, 52, 16, 16), -8, -8);
			sprites[4] = new Sprite(sheet.GetSection(35, 52, 16, 16), -8, -8);
			sprites[5] = new Sprite(sheet.GetSection(1, 69, 16, 16), -8, -8);
			sprites[6] = new Sprite(sheet.GetSection(18, 69, 16, 16), -8, -8);
			sprites[7] = new Sprite(sheet.GetSection(35, 69, 16, 16), -8, -8);
			sprites[8] = new Sprite(sheet.GetSection(1, 86, 16, 16), -8, -8);
			sprites[9] = new Sprite(new Sprite(sheet.GetSection(52, 211, 170, 44), -85, -22), new Sprite(sheet.GetSection(128, 203, 12, 7), 38, -5)); // knux uses two frames
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object will use.", null, new Dictionary<string, int>
				{
					{ "Eggman - Juggling", 0 },
					{ "Eggman - Tantrum", 1 },
					{ "Emerald 1", 2 },
					{ "Emerald 2", 3 },
					{ "Emerald 3", 4 },
					{ "Emerald 4", 5 },
					{ "Emerald 5", 6 },
					{ "Emerald 6", 7 },
					{ "Emerald 7", 8 },
					{ "Knuckles", 9 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => {
						obj.PropertyValue = (byte)((int)value);
						
						// Knuckles needs object.frame and object.inkEffect set, otherwise reset them
						if (obj.PropertyValue == 9)
						{
							((V4ObjectEntry)obj).Frame = 20;
							((V4ObjectEntry)obj).InkEffect = RSDKv4.Scene.Entity.InkEffects.Add;
						}
						else
						{
							((V4ObjectEntry)obj).Frame = 0;
							((V4ObjectEntry)obj).InkEffect = RSDKv4.Scene.Entity.InkEffects.None;
						}
					}
				);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Eggman - Juggling";
				case 1: return "Eggman - Tantrum";
				case 2: return "Emerald 1";
				case 3: return "Emerald 2";
				case 4: return "Emerald 3";
				case 5: return "Emerald 4";
				case 6: return "Emerald 5";
				case 7: return "Emerald 6";
				case 8: return "Emerald 7";
				case 9: return "Knuckles";
				default: return "Unknown";
			}
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
			// we're simplying the object.frame process by combining Knuckles's sprites into 1
			return sprites[obj.PropertyValue];
		}
	}
}

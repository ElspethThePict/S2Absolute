using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HFlipper : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(26, 185, 47, 26), -25, -9);
			else
				sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(189, 402, 47, 26), -25, -9);
			
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Flipper is facing.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
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
			return (subtype == 0) ? "Facing Right" : "Facing Left";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// well technically you'd want to cast prop val to dir and then use that if you want to be accurate to in game.. but the obj will still act like a left flipper in-game, so let's keep it like this
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}
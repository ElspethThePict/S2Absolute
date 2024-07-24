using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2AObjectDefinitions.WFZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
				sprite = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 256, 38, 52), -22, -28); // #Absolute - change sprite bounds
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(737, 58, 32, 52), -16, -28);
			
			properties[0] = new PropertySpec("Set Bounds", typeof(bool), "Extended",
				"If this Eggman object should set left stage bounds or not.", null,
				(obj) => obj.PropertyValue == 1,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 1 : 0));
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
			return (subtype == 1) ? "Set Bounds" : "Don't Set Bounds";
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
	}
}
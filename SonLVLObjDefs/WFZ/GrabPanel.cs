using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.WFZ
{
	class GrabPanel : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 69, 64, 64), -64, -32);
			
			BitmapBits sprite = new BitmapBits(65, 65);
			sprite.DrawRectangle(6, 0, 0, 64, 64); // LevelData.ColorWhite
			sprites[1] = new Sprite(sprite, -64, -32);
			
			properties[0] = new PropertySpec("Grab Duration", typeof(int), "Extended",
				"How long, in seconds, Sonic should be able to grab onto this Panel for.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Cutscene", typeof(bool), "Extended",
				"If this Panel is used for the ending cutscene.", null,
				(obj) => (obj.PropertyValue == 0x7F),
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 0x7F : 0));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 2; }
		}
		
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0x7F) ? 1 : 0];
		}
	}
}
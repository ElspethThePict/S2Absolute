using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class Nut : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(130, 156, 64, 24), -32, -12);
			
			properties[0] = new PropertySpec("Drop", typeof(bool), "Extended",
				"If the Nut should drop once it reaches a certain point.", null,
				(obj) => ((obj.PropertyValue & 0x80) == 0x80),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0)));
			
			// even if it's set by non-falling Nuts, it's only used by falling ones
			properties[1] = new PropertySpec("Drop Distance", typeof(int), "Extended",
				"Only used if Drop is true. How far down in pixels the Nut has to be in order to start falling.", null,
				(obj) => (obj.PropertyValue & 0x7F) << 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7F) | (((int)value >> 3) & 0x7F)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			if ((obj.PropertyValue & 0x80) == 0) // if we can't fall, then there's no reason to show drop distance
				return null;
			
			int height = (obj.PropertyValue & 0x7f) << 3;
			BitmapBits bitmap = new BitmapBits(2, height + 1);
			bitmap.DrawLine(6, 0, 0, 0, height); // LevelData.ColorWhite
			return new Sprite(bitmap);
		}
	}
}
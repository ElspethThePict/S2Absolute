using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class BossWall : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(127, 256, 128, 128), -64, -64);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(716, 358, 128, 128), -64, -64);
			}
			
			BitmapBits bitmap = new BitmapBits(129, 129);
			bitmap.DrawRectangle(6, 0, 0, 128, 128); // LevelData.ColourWhite
			sprite = new Sprite(sprite, new Sprite(bitmap, -64, -64));
			
			properties[0] = new PropertySpec("Start Closed", typeof(bool), "Extended",
				"If the Wall should start closed or not. Used to differenciate the enterance wall from the exit wall.", null,
				(obj) => (obj.PropertyValue == 1),
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
			return (subtype == 1) ? "Start Closed" : "Start Open";
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
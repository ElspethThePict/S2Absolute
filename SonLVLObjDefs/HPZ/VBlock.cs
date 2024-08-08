using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HPZ
{
	class VBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("HPZ/Objects.gif").GetSection(353, 1, 64, 64), -32, -32);
			sprites[0] = new Sprite(sprites[2], 0, 64);
			sprites[1] = new Sprite(sprites[2], 0, -64);
			
			BitmapBits bitmap = new BitmapBits(1, (64 * 2) + 1);
			bitmap.DrawLine(6, 0, 0, 0 * 2, 64 * 2); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -64);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this block should start from.", null, new Dictionary<string, int>
				{
					{ "Bottom", 0 },
					{ "Top", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
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
			return (subtype == 1) ? "Start From Top" : "Start From Bottom";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}
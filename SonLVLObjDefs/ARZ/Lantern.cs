using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2AObjectDefinitions.ARZ
{
	class PullVine : ARZ.Lantern
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects4.gif").GetSection(90, 176, 24, 80), -12, -40);
		}
	}
	
	class Lantern : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(116, 42, 10, 16), -5, 0);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			properties[0] = new PropertySpec("Tag", typeof(int), "Extended",
				"The tag that this object has. Used to link a Lantern to its Vine.", null,
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
	}
}
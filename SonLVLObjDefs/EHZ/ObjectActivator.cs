using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class ObjectActivator : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Activate Offset", typeof(int), "Extended",
				"The Entity Pos offset of the Object to be activated by this Activator.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {1, 2}); }
		}
		
		public override bool Debug
		{
			get { return true; }
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
			return "Active object [+" + subtype + "]";
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
			int index = LevelData.Objects.IndexOf(obj) + obj.PropertyValue;
			
			if ((index > LevelData.Objects.Count) || (obj.PropertyValue == 0))
				return null;
			
			try
			{
				short xmin = Math.Min(obj.X, LevelData.Objects[index].X);
				short ymin = Math.Min(obj.Y, LevelData.Objects[index].Y);
				short xmax = Math.Max(obj.X, LevelData.Objects[index].X);
				short ymax = Math.Max(obj.Y, LevelData.Objects[index].Y);
				
				BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, LevelData.Objects[index].X - xmin, LevelData.Objects[index].Y - ymin); // LevelData.ColorWhite
				
				return new Sprite(bitmap, xmin - obj.X, ymin - obj.Y);
			}
			catch
			{
			}
			
			return null;
		}
	}
}
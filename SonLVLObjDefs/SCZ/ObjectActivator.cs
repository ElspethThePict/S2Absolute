using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.SCZ
{
	class ObjectActivator : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Activate Count", typeof(int), "Extended",
				"How many of the following objects should be activated by this Activator.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			if (obj.PropertyValue == 0)
				return null;
			
			// In Origins' SCZ missions, they deleted some objects and threw off the activators
			// Because of that, there are now lines going all across the stage, up and down, left and right, 'n all around
			// Activators pointing to activators pointing to activators, it ain't anything pretty i tell you
			// Not much we can do from here unfortunately, as that's what happens in-game too :(
			
			List<ObjectEntry> objs = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj)).TakeWhile(a => LevelData.Objects.IndexOf(a) <= (LevelData.Objects.IndexOf(obj) + obj.PropertyValue)).ToList();
			if (objs.Count == 0)
				return null;
			
			short xmin = Math.Min(obj.X, objs.Min(a => a.X));
			short ymin = Math.Min(obj.Y, objs.Min(a => a.Y));
			short xmax = Math.Max(obj.X, objs.Max(a => a.X));
			short ymax = Math.Max(obj.Y, objs.Max(a => a.Y));
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 0; i < objs.Count - 1; i++)
				bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, objs[i + 1].X - xmin, objs[i + 1].Y - ymin); // LevelData.ColorWhite
			
			return new Sprite(bitmap, xmin - obj.X, ymin - obj.Y);
		}
	}
}
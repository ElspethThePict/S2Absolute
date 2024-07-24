using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.WFZ
{
	class WindCurrent : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Child Type", typeof(int), "Extended",
				"Which object holds the other corner of this Wind Current box.", null, new Dictionary<string, int>
				{
					{ "Next Slot", 0 },
					{ "Previous Slot", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
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
			return (subtype > 0) ? "Use Previous Slot" : "Use Next Slot";
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
		
		bool updateOverlay = false;
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			try
			{
				ObjectEntry other = LevelData.Objects[LevelData.Objects.IndexOf(obj) + ((obj.PropertyValue == 0) ? 1 : -1)];
				
				short xmin = Math.Min(obj.X, other.X);
				short ymin = Math.Min(obj.Y, other.Y);
				short xmax = Math.Max(obj.X, other.X);
				short ymax = Math.Max(obj.Y, other.Y);
				BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				bitmap.DrawRectangle(6, 0, 0, xmax - xmin, ymax - ymin); // LevelData.ColorWhite
				
				Sprite debug = new Sprite(bitmap, xmin - obj.X, ymin - obj.Y);
				
				// yeah this is kinda iffy, but it's the best i can think of for now..
				// (we don't want an unlimited loop where the two objects keep calling each other's UpdateDebugOverlay())
				if ((other.Type == obj.Type) && !updateOverlay)
				{
					if (other.DebugOverlay == null)
						updateOverlay = true;
					else if (!other.DebugOverlay.Size.Equals(debug.Width))
						updateOverlay = true;
					
					if (updateOverlay)
						other.UpdateDebugOverlay();
				}
				
				updateOverlay = false;
				
				return debug;
			}
			catch
			{
			}
			
			updateOverlay = false;
			return null;
		}
	}
}
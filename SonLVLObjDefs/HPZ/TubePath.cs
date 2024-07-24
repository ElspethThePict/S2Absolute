using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.HPZ
{
	class TubePath : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Marker Type", typeof(int), "Extended",
				"If this Tube Path is an entrance or an exit.", null, new Dictionary<string, int>
				{
					{ "Entrance", 0 },
					{ "Exit", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			switch (subtype)
			{
				case 0:
					return "Entrance";
				case 1:
					return "Exit";
				default:
					return "Unknown";
			}
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
			if (obj.PropertyValue != 0)
				return null;
			
			try
			{
				// p much copied directly from SCD UFO but hey, if it works it works
				// Do note, in-game behaviour doesn't enforce that the path ends with a Tube Path exit - it just has to be a non-zero obj type
				
				List<ObjectEntry> nodes = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) + 1).TakeWhile(a => a.Name == "Blank Object").ToList();
				if (nodes.Count == 0)
					return null;
				
				// add this obj itself to the start
				nodes.Insert(0, obj);
				
				// and add the exit tube path obj to the end too
				if ((LevelData.Objects.IndexOf(nodes[nodes.Count - 1]) + 1) < LevelData.Objects.Count)
					nodes.Add(LevelData.Objects[LevelData.Objects.IndexOf(nodes[nodes.Count - 1]) + 1]);
				
				short xmin = Math.Min(obj.X, nodes.Min(a => a.X));
				short ymin = Math.Min(obj.Y, nodes.Min(a => a.Y));
				short xmax = Math.Max(obj.X, nodes.Max(a => a.X));
				short ymax = Math.Max(obj.Y, nodes.Max(a => a.Y));
				
				// let's give the path line an outline to help make it more visible, white is high with black below
				BitmapBits white = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				BitmapBits black = new BitmapBits(white.Width + 1, white.Height + 1);
				
				for (int i = 0; i < nodes.Count - 1; i++)
				{
					white.DrawLine(6, nodes[i].X - xmin, nodes[i].Y - ymin, nodes[i + 1].X - xmin, nodes[i + 1].Y - ymin); // LevelData.ColorWhite
					
					// black
					black.DrawLine(1, nodes[i].X - xmin, nodes[i].Y - ymin + 1, nodes[i + 1].X - xmin, nodes[i + 1].Y - ymin + 1);
					black.DrawLine(1, nodes[i].X - xmin + 1, nodes[i].Y - ymin, nodes[i + 1].X - xmin + 1, nodes[i + 1].Y - ymin);
					black.DrawLine(1, nodes[i].X - xmin + 1, nodes[i].Y - ymin + 1, nodes[i + 1].X - xmin + 1, nodes[i + 1].Y - ymin + 1);
				}
				
				return new Sprite(new Sprite(black, xmin - obj.X, ymin - obj.Y), new Sprite(white, xmin - obj.X, ymin - obj.Y));
			}
			catch
			{
			}
			
			return null;
		}
	}
}
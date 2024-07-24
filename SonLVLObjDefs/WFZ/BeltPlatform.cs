using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class BeltPlatform : ObjectDefinition
	{
		private Sprite[,,] sprites = new Sprite[3, 8, 2]; // type, order, flip
		
		public override void Init(ObjectData data)
		{
			sprites[2, 0, 0] = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(80, 177, 48, 8), -24, -4);
			
			int[] lengths = {199, 455};
			int[] counts = {4, 8};
			
			for (int i = 0; i < counts.Length; i++)
			{
				for (int j = 0; j < counts[i]; j++)
				{
					int offset = (lengths[i] / counts[i]) * (j + 1);
					sprites[i, j, 0] = new Sprite(sprites[2, 0, 0], 0, -offset);
					sprites[i, j, 1] = new Sprite(sprites[2, 0, 0], 0,  offset);
				}
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[2, 0, 0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2, 0, 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// instead of showing the platforms at their origin pos, let's spread them out across their path if possible
			
			int index = LevelData.Objects.IndexOf(obj) - 1;
			int offset = 0;
			while (index > 0)
			{
				if (LevelData.Objects[index].Name == "Belt Platform S")
					break;
				
				index--;
				offset++;
				
				if (offset == 8)
				{
					return sprites[2, 0, 0];
				}
			}
			
			ObjectEntry spawner = LevelData.Objects[index];
			int distance = spawner.PropertyValue & 1;
			if ((distance == 0) && (offset > 3)) // check if this is a 199px spawner, since that should only have 4 platforms and not 8
				return sprites[2, 0, 0];
			
			int direction = (spawner.PropertyValue & 2) >> 1;
			
			return new Sprite(sprites[distance, offset, direction], spawner.X - obj.X, spawner.Y - obj.Y);
		}
	}
}
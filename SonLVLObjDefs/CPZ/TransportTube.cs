using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class TransportTube : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[6];
		private Sprite[] debug = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			// main object - Script box
			// transporters - t icon
			BitmapBits bitmap = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(bitmap.GetSection(1, 143, 32, 32), -16, -16);
			sprites[5] = new Sprite(bitmap.GetSection(168, 18, 16, 16), -8, -8);
			
			bitmap = new BitmapBits(16, 16);
			bitmap.DrawRectangle(6, 0, 0, 15, 15);
			sprites[5] = new Sprite(sprites[5], new Sprite(bitmap, -8, -8));
			
			sprites[1] = new Sprite(sprites[5], 8 + 16, -8);
			sprites[2] = new Sprite(sprites[5], 8 + 32, -8);
			sprites[3] = new Sprite(sprites[5], 8 + 16,  8);
			sprites[4] = new Sprite(sprites[5], 8 + 32,  8);
			
			int[] sizes = {160, 256, 288};
			for (int i = 0; i < sizes.Length; i++)
			{
				bitmap = new BitmapBits(sizes[i], 128);
				bitmap.DrawRectangle(6, 0, 0, sizes[i]-1, 127);
				debug[i] = new Sprite(bitmap);
			}
			
			properties[0] = new PropertySpec("Entry Size", typeof(int), "Extended",
				"What type of entrance this tube has. A normal Entry object should be followed by 4 Transporters.", null, new Dictionary<string, int> // well really it's [playerCount], not necessarily 4, but that's what the base game does, so
				{
					{ "160 px", 0 },
					{ "256 px", 1 },
					{ "288 px", 2 },
					{ "Transporter", 0xff }
				},
				(obj) => (obj.PropertyValue == 0xff) ? 0xff : (obj.PropertyValue & 3),
				(obj, value) => {
						// based off RE2's Transport Tube edit scripts
						byte val = (byte)((int)value);
						if (val == 0xff) obj.PropertyValue = val;
						else {
							if (obj.PropertyValue == 0xff)
							{
								// was a transporter, let's reset prop val
								obj.PropertyValue = 0;
							}
							else {
								// not transporter, keep our path value
								obj.PropertyValue = (byte)(obj.PropertyValue & ~3);
							}
							
							obj.PropertyValue |= val;
						}
					}
				);
			
			// yeah this thing is kinda weird and i still don't have it figured out myself either, but i'll just leave it for now...
			properties[1] = new PropertySpec("Path", typeof(int), "Extended",
				"Which path this Tube should follow. Only used by Entry tubes.", null,
				(obj) => ((obj.PropertyValue == 0xff) ? -1 : ((obj.PropertyValue >> 2) & 0x0f)),
				(obj, value) => {
						if (obj.PropertyValue != 0xff) // don't set it if we're a transporter
							obj.PropertyValue = (byte)((obj.PropertyValue & ~(0x0f << 2)) | ((int)value & 0x0f) << 2);
					}
				);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override bool Debug
		{
			get { return true; }
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
			return (subtype == 0xff) ? sprites[5] : sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (obj.PropertyValue != 0xff) // we're a main object, let's return the normal sprite
				return sprites[0];
			
			// if we're a transporter, let's offset ourselves as needed so that we're not stacked
			for (int i = 1; i <= 4; i++)
			{
				int index = LevelData.Objects.IndexOf(obj) - i;
				if (index < 0) break;
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue != 0xff))
					return sprites[i];
			}
			
			return sprites[5];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0xff)
				return null;
			
			// All these values are copied from the original script, so if you modified those (really it would've just been easier to use Tube Path, but it's up to you..)
			// then you can just paste them here
			
			// these are relative
			int[][] paths_enterance = {
				new int[] {
				0x900000, 0x100000,
				0x900000, 0x700000,
				0x400000, 0x700000,
				0x350000, 0x6F0000,
				0x280000, 0x6A0000,
				0x1E0000, 0x620000,
				0x150000, 0x580000,
				0x110000, 0x4A0000,
				0x100000, 0x400000,
				0x110000, 0x350000,
				0x150000, 0x270000,
				0x1E0000, 0x1E0000,
				0x280000, 0x150000,
				0x350000, 0x110000,
				0x400000, 0x100000,
				0x500000, 0x100000,
				0x5E0000, 0x120000,
				0x680000, 0x180000,
				0x6D0000, 0x240000,
				0x700000, 0x300000,
				0x6D0000, 0x3D0000,
				0x680000, 0x480000,
				0x5E0000, 0x4E0000,
				0x500000, 0x500000,
				0x300000, 0x500000,
				0x220000, 0x520000,
				0x170000, 0x5A0000,
				0x110000, 0x630000,
				0x100000, 0x700000},
				new int[] {
				0x900000, 0x100000,
				0x900000, 0x700000,
				0x400000, 0x700000,
				0x2E0000, 0x6E0000,
				0x1D0000, 0x620000,
				0x130000, 0x530000,
				0x100000, 0x400000,
				0x130000, 0x2D0000,
				0x1D0000, 0x1E0000,
				0x2E0000, 0x130000,
				0x400000, 0x100000,
				0x580000, 0x100000,
				0x640000, 0x140000,
				0x6C0000, 0x1A0000,
				0x700000, 0x280000,
				0x6C0000, 0x360000,
				0x640000, 0x3C0000,
				0x580000, 0x400000,
				0x4B0000, 0x3D0000,
				0x400000, 0x380000,
				0x360000, 0x320000,
				0x280000, 0x300000,
				0x100000, 0x300000},
				new int[] {
				0x100000, 0x700000,
				0x110000, 0x630000,
				0x170000, 0x5A0000,
				0x220000, 0x520000,
				0x300000, 0x500000,
				0x500000, 0x500000,
				0x5E0000, 0x4E0000,
				0x680000, 0x480000,
				0x6D0000, 0x3D0000,
				0x700000, 0x300000,
				0x6D0000, 0x240000,
				0x680000, 0x180000,
				0x5E0000, 0x120000,
				0x500000, 0x100000,
				0x400000, 0x100000,
				0x350000, 0x110000,
				0x280000, 0x150000,
				0x1E0000, 0x1E0000,
				0x150000, 0x270000,
				0x110000, 0x350000,
				0x100000, 0x400000,
				0x110000, 0x4A0000,
				0x150000, 0x580000,
				0x1E0000, 0x620000,
				0x280000, 0x6A0000,
				0x350000, 0x6F0000,
				0x400000, 0x700000,
				0x900000, 0x700000,
				0x900000, 0x100000},
				new int[] {
				0x100000, 0x300000,
				0x280000, 0x300000,
				0x360000, 0x320000,
				0x400000, 0x380000,
				0x4B0000, 0x3D0000,
				0x580000, 0x400000,
				0x640000, 0x3C0000,
				0x6C0000, 0x360000,
				0x700000, 0x280000,
				0x6C0000, 0x1A0000,
				0x640000, 0x140000,
				0x580000, 0x100000,
				0x400000, 0x100000,
				0x2E0000, 0x130000,
				0x1D0000, 0x1E0000,
				0x130000, 0x2D0000,
				0x100000, 0x400000,
				0x130000, 0x530000,
				0x1D0000, 0x620000,
				0x2E0000, 0x6E0000,
				0x400000, 0x700000,
				0x900000, 0x700000,
				0x900000, 0x100000},
				new int[] {
				0x100000, 0x100000,
				0x100000, 0x700000,
				0xC00000, 0x700000,
				0xCA0000, 0x6F0000,
				0xD40000, 0x6C0000,
				0xDB0000, 0x680000,
				0xE30000, 0x620000,
				0xE80000, 0x5A0000,
				0xED0000, 0x520000,
				0xEF0000, 0x480000,
				0xF00000, 0x400000,
				0xEF0000, 0x360000,
				0xED0000, 0x2E0000,
				0xE80000, 0x260000,
				0xE30000, 0x1E0000,
				0xDB0000, 0x170000,
				0xD40000, 0x140000,
				0xCA0000, 0x120000,
				0xC00000, 0x100000,
				0xB70000, 0x110000,
				0xAF0000, 0x120000,
				0xA60000, 0x170000,
				0x9E0000, 0x1E0000,
				0x970000, 0x260000,
				0x930000, 0x2E0000,
				0x910000, 0x360000,
				0x900000, 0x400000,
				0x900000, 0x700000},
				new int[] {
				0x100000, 0x100000,
				0x100000, 0x700000,
				0xC00000, 0x700000,
				0xD20000, 0x6E0000,
				0xE30000, 0x620000,
				0xED0000, 0x530000,
				0xF00000, 0x400000,
				0xED0000, 0x2D0000,
				0xE30000, 0x1E0000,
				0xD20000, 0x130000,
				0xC00000, 0x100000,
				0xA80000, 0x100000,
				0x9C0000, 0x140000,
				0x940000, 0x1A0000,
				0x900000, 0x280000,
				0x940000, 0x360000,
				0x9C0000, 0x3C0000,
				0xA80000, 0x400000,
				0xB50000, 0x3D0000,
				0xC00000, 0x380000,
				0xCA0000, 0x320000,
				0xD80000, 0x300000,
				0xF00000, 0x300000},
				new int[] {
				0x900000, 0x700000,
				0x900000, 0x400000,
				0x910000, 0x360000,
				0x930000, 0x2E0000,
				0x970000, 0x260000,
				0x9E0000, 0x1E0000,
				0xA60000, 0x170000,
				0xAF0000, 0x120000,
				0xB70000, 0x110000,
				0xC00000, 0x100000,
				0xCA0000, 0x120000,
				0xD40000, 0x140000,
				0xDB0000, 0x170000,
				0xE30000, 0x1E0000,
				0xE80000, 0x260000,
				0xED0000, 0x2E0000,
				0xEF0000, 0x360000,
				0xF00000, 0x400000,
				0xEF0000, 0x480000,
				0xED0000, 0x520000,
				0xE80000, 0x5A0000,
				0xE30000, 0x620000,
				0xDB0000, 0x680000,
				0xD40000, 0x6C0000,
				0xCA0000, 0x6F0000,
				0xC00000, 0x700000,
				0x100000, 0x700000,
				0x100000, 0x100000},
				new int[] {
				0xF00000, 0x300000,
				0xD80000, 0x300000,
				0xCA0000, 0x320000,
				0xC00000, 0x380000,
				0xB50000, 0x3D0000,
				0xA80000, 0x400000,
				0x9C0000, 0x3C0000,
				0x940000, 0x360000,
				0x900000, 0x280000,
				0x940000, 0x1A0000,
				0x9C0000, 0x140000,
				0xA80000, 0x100000,
				0xC00000, 0x100000,
				0xD20000, 0x130000,
				0xE30000, 0x1E0000,
				0xED0000, 0x2D0000,
				0xF00000, 0x400000,
				0xED0000, 0x530000,
				0xE30000, 0x620000,
				0xD20000, 0x6E0000,
				0xC00000, 0x700000,
				0x100000, 0x700000,
				0x100000, 0x100000},
				new int[] {
				0x1100000, 0x100000,
				0x1100000, 0x700000,
				0x400000, 0x700000,
				0x350000, 0x6F0000,
				0x280000, 0x6A0000,
				0x1E0000, 0x620000,
				0x150000, 0x580000,
				0x110000, 0x4A0000,
				0x100000, 0x400000,
				0x110000, 0x350000,
				0x150000, 0x270000,
				0x1E0000, 0x1E0000,
				0x280000, 0x150000,
				0x350000, 0x110000,
				0x400000, 0x100000,
				0x500000, 0x100000,
				0x5E0000, 0x120000,
				0x680000, 0x180000,
				0x6D0000, 0x240000,
				0x700000, 0x300000,
				0x6D0000, 0x3D0000,
				0x680000, 0x480000,
				0x5E0000, 0x4E0000,
				0x500000, 0x500000,
				0x300000, 0x500000,
				0x220000, 0x520000,
				0x170000, 0x5A0000,
				0x110000, 0x630000,
				0x100000, 0x700000},
				new int[] {
				0x1100000, 0x100000,
				0x1100000, 0x700000,
				0x400000, 0x700000,
				0x2E0000, 0x6E0000,
				0x1D0000, 0x620000,
				0x130000, 0x530000,
				0x100000, 0x400000,
				0x130000, 0x2D0000,
				0x1D0000, 0x1E0000,
				0x2E0000, 0x130000,
				0x400000, 0x100000,
				0x580000, 0x100000,
				0x640000, 0x140000,
				0x6C0000, 0x1A0000,
				0x700000, 0x280000,
				0x6C0000, 0x360000,
				0x640000, 0x3C0000,
				0x580000, 0x400000,
				0x4B0000, 0x3D0000,
				0x400000, 0x380000,
				0x360000, 0x320000,
				0x280000, 0x300000,
				0x100000, 0x300000},
				new int[] {
				0x100000, 0x700000,
				0x110000, 0x630000,
				0x170000, 0x5A0000,
				0x220000, 0x520000,
				0x300000, 0x500000,
				0x500000, 0x500000,
				0x5E0000, 0x4E0000,
				0x680000, 0x480000,
				0x6D0000, 0x3D0000,
				0x700000, 0x300000,
				0x6D0000, 0x240000,
				0x680000, 0x180000,
				0x5E0000, 0x120000,
				0x500000, 0x100000,
				0x400000, 0x100000,
				0x350000, 0x110000,
				0x280000, 0x150000,
				0x1E0000, 0x1E0000,
				0x150000, 0x270000,
				0x110000, 0x350000,
				0x100000, 0x400000,
				0x110000, 0x4A0000,
				0x150000, 0x580000,
				0x1E0000, 0x620000,
				0x280000, 0x6A0000,
				0x350000, 0x6F0000,
				0x400000, 0x700000,
				0x1100000, 0x700000,
				0x1100000, 0x100000},
				new int[] {
				0x100000, 0x300000,
				0x280000, 0x300000,
				0x360000, 0x320000,
				0x400000, 0x380000,
				0x4B0000, 0x3D0000,
				0x580000, 0x400000,
				0x640000, 0x3C0000,
				0x6C0000, 0x360000,
				0x700000, 0x280000,
				0x6C0000, 0x1A0000,
				0x640000, 0x140000,
				0x580000, 0x100000,
				0x400000, 0x100000,
				0x2E0000, 0x130000,
				0x1D0000, 0x1E0000,
				0x130000, 0x2D0000,
				0x100000, 0x400000,
				0x130000, 0x530000,
				0x1D0000, 0x620000,
				0x2E0000, 0x6E0000,
				0x400000, 0x700000,
				0x1100000, 0x700000,
				0x1100000, 0x100000}
			};
			
			// while these are absolute
			int[][] paths_tube = {
				new int[] {
				0x7900000, 0x3B00000,
				0x7100000, 0x3B00000,
				0x7100000, 0x6B00000,
				0xA900000, 0x6B00000,
				0xA900000, 0x6700000},
				new int[] {
				0x7900000, 0x3B00000,
				0x7100000, 0x3B00000,
				0x7100000, 0x6B00000,
				0xA900000, 0x6B00000,
				0xA900000, 0x6700000},
				new int[] {
				0x7900000, 0x3F00000,
				0x7900000, 0x4B00000,
				0xA000000, 0x4B00000,
				0xC100000, 0x4B00000,
				0xC100000, 0x3300000,
				0xD900000, 0x3300000,
				0xD900000, 0x1B00000,
				0xF100000, 0x1B00000,
				0xF100000, 0x2B00000,
				0xF900000, 0x2B00000},
				new int[] {
				0xAF00000, 0x6300000,
				0xE900000, 0x6300000,
				0xE900000, 0x6B00000,
				0xF900000, 0x6B00000,
				0xF900000, 0x6700000},
				new int[] {
				0xF900000, 0x2F00000,
				0xF900000, 0x4B00000,
				0xF100000, 0x4B00000,
				0xF100000, 0x6300000,
				0xF900000, 0x6300000},
				new int[] {
				0x14100000, 0x5300000,
				0x11900000, 0x5300000,
				0x11900000, 0x6B00000,
				0x14100000, 0x6B00000,
				0x14100000, 0x5700000},
				new int[] {
				0x1AF00000, 0x5300000,
				0x1B900000, 0x5300000,
				0x1B900000, 0x3300000,
				0x1E100000, 0x3300000},
				new int[] {
				0x1A900000, 0x5700000,
				0x1A900000, 0x5B00000,
				0x1C100000, 0x5B00000,
				0x1C100000, 0x4300000,
				0x1E100000, 0x4300000,
				0x1E100000, 0x3700000},
				new int[] {
				0x24900000, 0x3700000,
				0x24900000, 0x3D00000,
				0x23900000, 0x3D00000,
				0x23900000, 0x5D00000,
				0x25100000, 0x5D00000,
				0x25100000, 0x5700000},
				new int[] {
				0x24F00000, 0x3300000,
				0x25900000, 0x3300000,
				0x25900000, 0x5300000,
				0x25700000, 0x5300000},
				new int[] {
				0x3100000, 0x3300000,
				0x2900000, 0x3300000,
				0x2900000, 0x2300000,
				0x4900000, 0x2300000},
				new int[] {
				0x3100000, 0x3700000,
				0x3100000, 0x3B00000,
				0x4100000, 0x3B00000,
				0x4100000, 0x2B00000,
				0x4900000, 0x2B00000,
				0x4900000, 0x2700000},
				new int[] {
				0x4900000, 0x6F00000,
				0x4900000, 0x7300000,
				0x6900000, 0x7300000,
				0x8900000, 0x7300000,
				0x8900000, 0x6F00000},
				new int[] {
				0xBF00000, 0x3300000,
				0xD900000, 0x3300000,
				0xD900000, 0x2F00000},
				new int[] {
				0xD900000, 0x2B00000,
				0xC900000, 0x2B00000,
				0xC900000, 0xB00000,
				0xE800000, 0xB00000,
				0x11100000, 0xB00000,
				0x11100000, 0x2300000,
				0x10F00000, 0x2300000}
			};
			
			int[] nodeFlags = {
					  2,   1, 0, 0,
					 -1,   3, 0, 0,
					  4,  -2, 0, 0,
					 -3,  -4, 0, 0,
					 -5,  -5, 0, 0,
					  7,   6, 0, 0,
					 -7,  -6, 0, 0,
					  8,   9, 0, 0,
					 -8,  -9, 0, 0,
					 11,  10, 0, 0,
					 12,   0, 0, 0,
					-11, -10, 0, 0,
					-12,   0, 0, 0,
					  0,  13, 0, 0,
					-13,  14, 0, 0,
					  0, -14, 0, 0
			};
			
			int[] paths = {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 1, 2, 1};
			int transportPath = paths[(obj.PropertyValue >> 2) & 0x0f];
			int tubeType = (obj.PropertyValue & 3) << 2;
			int index = ((obj.PropertyValue >> 2) & 0x0f);
			
			index <<= 2;
			
			Sprite dbg = debug[obj.PropertyValue & 3];
			
			try
			{
				if (transportPath == 2) // random, draw both paths
				{
					dbg = new Sprite(dbg, DrawPath(paths_enterance[tubeType], 191)); // green
					dbg = new Sprite(dbg, DrawPath(paths_enterance[tubeType + 1], 175)); // red
					
					dbg = new Sprite(dbg, new Sprite(DrawPath(paths_tube[Math.Abs(nodeFlags[index])], 191), -obj.X, -obj.Y)); // green
					dbg = new Sprite(dbg, new Sprite(DrawPath(paths_tube[Math.Abs(nodeFlags[index + 1])], 175), -obj.X, -obj.Y)); // red
				}
				else
				{
					int colour = (transportPath == 0) ? 191 : 175; // either green or red (in that order)
					dbg = new Sprite(dbg, DrawPath(paths_enterance[tubeType + transportPath], colour));
					dbg = new Sprite(dbg, new Sprite(DrawPath(paths_tube[Math.Abs(nodeFlags[index + transportPath])], colour), -obj.X, -obj.Y));
				}
			}
			catch
			{
			}
			
			return dbg;
		}
		
		//private Sprite DrawPath(int[] path, int index = 6) => DrawPath(path, (byte)index);
		private Sprite DrawPath(int[] path, int index = 6)
		{
			return DrawPath(path, (byte)index);
		}
		
		private Sprite DrawPath(int[] path, byte index = 6) // default to LevelData.ColorWhite
		{
			int xmin = 0x7fff;
			int ymin = 0x7fff;
			int xmax = -0x7fff;
			int ymax = -0x7fff;
			
			for (int i = 0; i < path.Length; i += 2)
			{
				xmin = Math.Min(xmin, path[i] >> 16);
				ymin = Math.Min(ymin, path[i+1] >> 16);
				xmax = Math.Max(xmax, path[i] >> 16);
				ymax = Math.Max(ymax, path[i+1] >> 16);
			}
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 2; i < path.Length; i += 2)
				bitmap.DrawLine(index, (path[i-2] >> 16) - xmin, (path[i-1] >> 16) - ymin, (path[i] >> 16) - xmin, (path[i+1] >> 16) - ymin);
			
			return new Sprite(bitmap, xmin, ymin);
		}
	}
}
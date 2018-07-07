
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class RedFireplace : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.RedBrickFurniture;
		}
		
		const int animationFrameWidth = 54;
		
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLighted[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 18 };
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			var name = CreateMapEntryName();
			name.SetDefault("Red Fireplace");

			name.AddTranslation(GameCulture.Russian, "Краcный камин");
			name.AddTranslation(GameCulture.Chinese, "红色壁炉");

			AddMapEntry(Color.Red, name);
			disableSmartCursor = true;
			animationFrameHeight = 38;
			adjTiles = new int[]{ TileID.Fireplace };
		}
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
		}
		
		public override void NearbyEffects(int i, int j, bool closer)
		{
			if(closer)
			{
				Main.campfire = true;
			}
		}
		
		//Don't animate if deactivated
		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			if(Main.tile[i, j].frameX >= animationFrameWidth)
			{
				frameYOffset = 0;
			}
		}
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = (Main.tileFrame[TileID.Fireplace] + 3) % 8;
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].frameX < animationFrameWidth)
			{
				float rand = (float)Main.rand.Next(28, 42) * 0.005f;
				rand += (float)(270 - (int)Main.mouseTextColor) / 700f;
				r = 0.7f + rand;
				g = 1f + rand;
				b = 0.5f + rand;
			}
		}
		
		public override void HitWire(int i, int j)
		{
			//Top left tile
			int x = i - Main.tile[i, j].frameX % animationFrameWidth / 18;
			int y = j - Main.tile[i, j].frameY % animationFrameHeight / 18;
			
			Wiring.SkipWire(x, y);
			Wiring.SkipWire(x, y + 1);
			Wiring.SkipWire(x + 1, y);
			Wiring.SkipWire(x + 1, y + 1);
			Wiring.SkipWire(x + 2, y);
			Wiring.SkipWire(x + 2, y + 1);
			
			bool activate = Main.tile[x, y].frameX != 0;
			for(int l = x; l < x + 3; l++)
			{
				for(int m = y; m < y + 2; m++)
				{
					if(Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					if(Main.tile[l, m].active() && Main.tile[l, m].type == Type)
					{
						if(activate)
						{
							Main.tile[l, m].frameX -= animationFrameWidth;
						}
						else
						{
							Main.tile[l, m].frameX += animationFrameWidth;
						}
					}
				}
			}
			NetMessage.SendTileSquare(-1, x + 1, y + 1, 3);
		}
	}
}
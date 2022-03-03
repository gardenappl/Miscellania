
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class RedFireplace : ModTile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RedBrickFurniture;
		}
		
		const int animationFrameWidth = 54;
		
		public override void SetStaticDefaults()
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
			AddMapEntry(Color.Red);
			AnimationFrameHeight = 38;
			AdjTiles = new int[]{ TileID.Fireplace };
		}
		
		public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.RedFireplace>());
		}
		
		public override void NearbyEffects(int i, int j, bool closer)
		{
			if(closer)
			{
				Main.SceneMetrics.HasCampfire = true;
			}
		}
		
		//Don't animate if deactivated
		public override void AnimateIndividualTile(int type, int i, int j, ref int TileFrameXOffset, ref int TileFrameYOffset)
		{
			if(Main.tile[i, j].TileFrameX >= animationFrameWidth)
			{
				TileFrameYOffset = 0;
			}
		}
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = (Main.tileFrame[TileID.Fireplace] + 3) % 8;
		}
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX < animationFrameWidth)
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
			int x = i - Main.tile[i, j].TileFrameX % animationFrameWidth / 18;
			int y = j - Main.tile[i, j].TileFrameY % AnimationFrameHeight / 18;
			
			Wiring.SkipWire(x, y);
			Wiring.SkipWire(x, y + 1);
			Wiring.SkipWire(x + 1, y);
			Wiring.SkipWire(x + 1, y + 1);
			Wiring.SkipWire(x + 2, y);
			Wiring.SkipWire(x + 2, y + 1);
			
			bool activate = Main.tile[x, y].TileFrameX != 0;
			for(int l = x; l < x + 3; l++)
			{
				for(int m = y; m < y + 2; m++)
				{
					if(Main.tile[l, m].HasTile && Main.tile[l, m].TileType == Type)
					{
						if(activate)
						{
							Main.tile[l, m].TileFrameX -= animationFrameWidth;
						}
						else
						{
							Main.tile[l, m].TileFrameX += animationFrameWidth;
						}
					}
				}
			}
			NetMessage.SendTileSquare(-1, x + 1, y + 1, 3);
		}
	}
}
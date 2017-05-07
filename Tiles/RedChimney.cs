
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class RedChimney : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.RedBrickFurniture;
		}
		
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new []{ 16, 16, 18 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity("RedChimneyTE").Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Red, "Red Chimney");
			disableSmartCursor = true;
			animationFrameHeight = 56;
		}
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, mod.ItemType(GetType().Name));
			mod.GetTileEntity("RedChimneyTE").Kill(i, j);
		}
		
		//Don't animate if deactivated
		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			//Top left tile
			int x = i - Main.tile[i, j].frameX / 18;
			int y = j - Main.tile[i, j].frameY % animationFrameHeight / 18;
			var tileEntity = (RedChimneyTE)TileEntity.ByPosition[new Point16(x, y)];
			if(tileEntity.CurrentState == RedChimneyTE.State.Deactivated)
			{
				frameYOffset = animationFrameHeight * 6;
			}
		}
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = (Main.tileFrame[TileID.Chimney] + 4) % 6;
		}
		
		public override void HitWire(int i, int j)
		{
			//Top left tile
			int x = i - Main.tile[i, j].frameX / 18;
			int y = j - Main.tile[i, j].frameY % animationFrameHeight / 18;
			
			Wiring.SkipWire(x, y);
			Wiring.SkipWire(x, y + 1);
			Wiring.SkipWire(x, y + 2);
			Wiring.SkipWire(x + 1, y);
			Wiring.SkipWire(x + 1, y + 1);
			Wiring.SkipWire(x + 1, y + 2);
			Wiring.SkipWire(x + 2, y);
			Wiring.SkipWire(x + 2, y + 1);
			Wiring.SkipWire(x + 2, y + 2);
			
			var tileEntity = (RedChimneyTE)TileEntity.ByPosition[new Point16(x, y)];
			tileEntity.CurrentState = tileEntity.CurrentState.NextEnum();
		}
	}
}

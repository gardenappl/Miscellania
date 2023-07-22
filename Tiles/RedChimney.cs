
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class RedChimney : ModTile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RedBrickFurniture;
		}
		
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<RedChimneyTE>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Red);
			AnimationFrameHeight = 56;
		}
		
		public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
		{
            ModContent.GetInstance<RedChimneyTE>().Kill(i, j);
		}
		
		//Don't animate if deactivated
		public override void AnimateIndividualTile(int type, int i, int j, ref int TileFrameXOffset, ref int TileFrameYOffset)
		{
			//Top left tile
			int x = i - Main.tile[i, j].TileFrameX / 18;
			int y = j - Main.tile[i, j].TileFrameY % AnimationFrameHeight / 18;
			try
			{
				var tileEntity = (RedChimneyTE)TileEntity.ByPosition[new Point16(x, y)];
				if(tileEntity.CurrentState == RedChimneyTE.State.Deactivated)
				{
					TileFrameYOffset = AnimationFrameHeight * 6;
				}
			}
			catch(KeyNotFoundException e)
            {
                GoldensMisc.Log(e);
                Main.NewText("Error! Report this error and send the Terraria/Modloader/Logs/client.log file!");
            }
		}
		
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = (Main.tileFrame[TileID.Chimney] + 4) % 6;
		}
		
		public override void HitWire(int i, int j)
		{
			//Top left tile
			int x = i - Main.tile[i, j].TileFrameX / 18;
			int y = j - Main.tile[i, j].TileFrameY % AnimationFrameHeight / 18;
			
			Wiring.SkipWire(x, y);
			Wiring.SkipWire(x, y + 1);
			Wiring.SkipWire(x, y + 2);
			Wiring.SkipWire(x + 1, y);
			Wiring.SkipWire(x + 1, y + 1);
			Wiring.SkipWire(x + 1, y + 2);
			Wiring.SkipWire(x + 2, y);
			Wiring.SkipWire(x + 2, y + 1);
			Wiring.SkipWire(x + 2, y + 2);
			
			try
			{
				var tileEntity = (RedChimneyTE)TileEntity.ByPosition[new Point16(x, y)];
				tileEntity.CurrentState = tileEntity.CurrentState.NextEnum();
				NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, tileEntity.ID, x, y);
			}
			catch(Exception e)
			{
				GoldensMisc.Log("Chimney TileEntity not found! Error! " + e);
			}
		}
	}
}

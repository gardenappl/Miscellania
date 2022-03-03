using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class ChestVacuum : ModTile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().ChestVacuum;
		}

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
			TileObjectData.newTile.CoordinateHeights = new[] { 24 };
			TileObjectData.newTile.Origin = new Point16(0, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.AlternateTile, 2, 0);
			TileObjectData.newTile.AnchorAlternateTiles = new int[]
			{
				TileID.Containers,
				TileID.Containers2
			};
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<ChestVacuumTE>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Gray, CreateMapEntryName());
			AnimationFrameHeight = 26;
		}

		//add modded chests
		public void SetDefaultsPostContent()
		{
			var tileObjectData = TileObjectData.GetTileData(Type, 0);
			var anchorTileList = new List<int>(tileObjectData.AnchorAlternateTiles);
			for(int i = TileID.Count; i < TileLoader.TileCount; i++)
			{
				if(TileLoader.ContainerName(i) != String.Empty)
				{
					anchorTileList.Add(i);
				}
			}
			tileObjectData.AnchorAlternateTiles = anchorTileList.ToArray();
		}

		public override bool CanPlace(int i, int j)
		{
			Tile tile1 = Main.tile[i, j + 1];
			Tile tile2 = Main.tile[i + 1, j + 1];
			if ((tile2 != null || tile2.HasTile) && tile1.TileFrameX != (tile2.TileFrameX - 18))
			{
				return false;
			}
			else
			{ 
				return true;
			}
		}

        public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ModContent.ItemType<Items.Placeable.ChestVacuum>());
            ModContent.GetInstance<ChestVacuumTE>().Kill(i, j);
		}

		public override bool RightClick(int i, int j)
		{
			int teX = i;
			int teY = j;

			var tile = Main.tile[i, j];
			if(tile.TileFrameX != 0)
				teX--;

			var te = TileEntity.ByPosition[new Point16(teX, teY)] as ChestVacuumTE;
			if(te == null)
				return true;
			if(te.smartStack)
			{
                te.smartStack = false;
                te.smartStackChanged = true;
                Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOff"));
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOff2"));
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    var netMessage = Mod.GetPacket();
                    netMessage.Write((byte)MiscMessageType.UpdateVacuumSmartStack);
                    netMessage.Write(te.ID);
                    netMessage.Write(te.smartStack);
                    netMessage.Send();
                }
            }
			else
			{
                te.smartStack = true;
                te.smartStackChanged = true;
                Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOn"));
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOn2"));
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    var netMessage = Mod.GetPacket();
                    netMessage.Write((byte)MiscMessageType.UpdateVacuumSmartStack);
                    netMessage.Write(te.ID);
                    netMessage.Write(te.smartStack);
                    netMessage.Send();
                }
            }
            return true;
		}

		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.noThrow = 2;
			Main.LocalPlayer.cursorItemIconEnabled = true;
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Items.Placeable.ChestVacuum>();
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if(frameCounter >= 20)
			{
				frameCounter = 0;
				frame++;
				if(frame >= 4)
				{
					frame = 0;
				}
			}
		}
	}
}

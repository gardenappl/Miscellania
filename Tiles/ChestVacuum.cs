using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.ChestVacuum;
		}

		public override void SetDefaults()
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
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<ChestVacuumTE>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Gray, CreateMapEntryName());
			disableSmartCursor = true;
			animationFrameHeight = 26;
		}

		//add modded chests
		public void SetDefaultsPostContent()
		{
			var tileObjectData = TileObjectData.GetTileData(Type, 0);
			var anchorTileList = new List<int>(tileObjectData.AnchorAlternateTiles);
			for(int i = TileID.Count; i < TileLoader.TileCount; i++)
			{
				if(TileLoader.ModChestName(i) != String.Empty)
				{
					anchorTileList.Add(i);
				}
			}
			tileObjectData.AnchorAlternateTiles = anchorTileList.ToArray();
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
			mod.GetTileEntity<ChestVacuumTE>().Kill(i, j);
		}

		public override void RightClick(int i, int j)
		{
			int teX = i;
			int teY = j;

			var tile = Main.tile[i, j];
			if(tile.frameX != 0)
				teX--;

			var te = TileEntity.ByPosition[new Point16(teX, teY)] as ChestVacuumTE;
			if(te == null)
				return;
			if(te.SmartStack)
			{
				te.SmartStack = false;
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOff"));
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOff2"));
			}
			else
			{
				te.SmartStack = true;
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOn"));
				Main.NewText(Language.GetTextValue("Mods.GoldensMisc.ChestVacuum.SmartStackOn2"));
			}
		}

		public override void MouseOver(int i, int j)
		{
			Main.LocalPlayer.noThrow = 2;
			Main.LocalPlayer.showItemIcon = true;
			Main.LocalPlayer.showItemIcon2 = mod.ItemType(GetType().Name);
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

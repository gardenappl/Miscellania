using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace GoldensMisc.Tiles
{
	public class Autofisher : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return Config.Autofisher;
		}

		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 0);
			//TileObjectData.newTile.StyleWrapLimit = 2;
			//TileObjectData.newTile.StyleMultiplier = 2;
			//TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<AutofisherTE>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 2, 1);
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Gray, CreateMapEntryName());
			disableSmartCursor = true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType(GetType().Name));
			mod.GetTileEntity<AutofisherTE>().Kill(i, j);
		}

		//public override void RightClick(int i, int j)
		//{
		//	//Top left tile
		//	int x = i - Main.tile[i, j].frameX % 54 / 18;
		//	int y = j - Main.tile[i, j].frameY % 38 / 18;

		//	try
		//	{
		//		var te = (AutofisherTE)TileEntity.ByPosition[new Point16(x, y)];
		//		if(Main.netMode == NetmodeID.MultiplayerClient)
		//		{
		//			var message = mod.GetPacket();
		//			message.Write((byte)MiscMessageType.PrintAutofisherInfo);
		//			message.Write(te.ID);
		//			message.Send();
		//		}
		//		else
		//		{
		//			Main.NewText(te.DisplayedFishingInfo);
		//		}
		//	}
		//	catch(Exception e)
		//	{
		//		ErrorLogger.Log(e);
		//		Main.NewText("Error! Report this error and send the Terraria/Logs/Logs.txt file!");
		//	}
		//}
	}
}

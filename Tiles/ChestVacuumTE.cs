using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace GoldensMisc.Tiles
{
	public class ChestVacuumTE : ModTileEntity
	{
		public bool SmartStack = false;
		const float PickupRadiusSq = 160f * 160f;

		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().ChestVacuum;
		}

		public override bool ValidTile(int i, int j)
		{
			var tile = Main.tile[i, j];
			return tile.active() && tile.type == ModContent.TileType<ChestVacuum>() && tile.frameX == 0;
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if(Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
				return -1;
			}
			return Place(i, j);
		}

		public override void Update()
		{
			try
			{
				base.Update();

				if((int)(Main.GlobalTime * 60f) % 100 == GetPickupTime())
				{
					var chest = Main.chest[Chest.FindChest(Position.X, Position.Y + 1)];
					var myPos = Position.ToWorldCoordinates(16f, 8f);
					for(int i = 0; i < Main.maxItems; i++)
					{
						var item = Main.item[i];
						if(item.active && item.stack > 0 && item.DistanceSQ(myPos) < PickupRadiusSq)
						{
							var itemPos = item.position;
							int itemWidth = item.width;
							int itemHeight = item.height;
							//string itemName = item.Name;
							int oldStack = item.stack;

							MiscUtils.PutItem(chest, item, SmartStack);

							if(item.type == 0 || oldStack - item.stack > 0)
							{
								if(item.stack == 0 || item.type == 0)
									item.active = false;
								Main.PlaySound(SoundID.Item8, itemPos);
								Main.PlaySound(SoundID.Grab, myPos);
								for(int j = 0; j < 10; j++)
								{
									Dust.NewDust(itemPos, itemWidth, itemHeight, 159);
									Dust.NewDust(Position.ToWorldCoordinates(0, 0), 32, 16, 159);
								}
								//if(oldStack - item.stack > 1)
								//	itemName += "(" + (oldStack - item.stack) + ")";
								//if(Main.netMode == NetmodeID.Server)
								//{
								//	NetMessage.SendData(MessageID.CombatTextString,
								//		text: NetworkText.FromLiteral(itemName),
								//		number: (int)Color.White.PackedValue,
								//		number2: myPos.X,
								//		number3: myPos.Y);
								//}
								//else
								//{
								//	CombatText.NewText(new Rectangle(Position.X * 16, Position.Y * 16, 32, 16), Color.White, itemName);
								//}
							}
						}
					}
				}
			}
			catch(Exception e)
			{
				Main.NewText("chest vacuum error. look at Logs.txt");
				GoldensMisc.Log(e);
			}
		}

		int GetPickupTime()
		{
			return (Position.X * 10 + Position.Y * 5) % 100;
		}
	}
}

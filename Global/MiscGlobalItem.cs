
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Global
{
	public class MiscGlobalItem : GlobalItem
	{
		public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			switch(item.type)
			{
				case ItemID.Heart:
				case ItemID.CandyApple:
				case ItemID.CandyCane:
				case ItemID.Star:
				case ItemID.SoulCake:
				case ItemID.SugarPlum:
				case ItemID.CopperCoin:
				case ItemID.SilverCoin:
				case ItemID.GoldCoin:
				case ItemID.PlatinumCoin:
				case ItemID.NebulaPickup1:
				case ItemID.NebulaPickup2:
				case ItemID.NebulaPickup3:
					return; //Ignore all of these
			}
			for (int acc = 3; acc < 8 + player.extraAccessorySlots; acc++)
			{
				if (player.armor[acc].type == mod.ItemType("UniversalMagnet") || player.armor[acc].type == mod.ItemType("MagnetismRing"))
				{
					grabRange += 20 * 16; //20 tiles, 16 pixels each
					break;
				}
			}
		}
	}
}

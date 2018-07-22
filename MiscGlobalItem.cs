
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public class MiscGlobalItem : GlobalItem
	{
		public override void GrabRange(Item item, Player player, ref int grabRange)
		{
			if(player.GetModPlayer<MiscPlayer>(mod).Magnet)
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
				grabRange += 300;
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if(item.type == ItemID.MechanicsRod && Config.Autofisher)
			{
				for(int i = 0; i < tooltips.Count; i++)
				{
					if(tooltips[i].Name == "Material")
					{
						tooltips.Insert(i + 1, new TooltipLine(mod, "AutofisherMaterial", Language.GetTextValue("Mods.GoldensMisc.ItemTooltip.MechanicsRod")));
						return;
					}
				}
			}
		}
	}
}
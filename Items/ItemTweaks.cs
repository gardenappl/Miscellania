﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items
{
	public class ItemTweaks : GlobalItem
	{
		/*
		 * Staff stats:
		 * materials      |dmg|use time|velocity|knockback|mana|sell|autofire|pierce
		 * --------------------------------------------------------------------------
		 * Amethyst-Copper|15 |37      |6f      |3.25f    |5   |4   |false   |0
		 * Amethyst-Tin   |15 |36      |6.25f   |3.5f     |6   |6   |false   |0
		 * --------------------------------------------------------------------------
		 * Topaz-Copper   |16 |36      |6.5f    |3.25f    |5   |8   |false   |0
		 * Topaz-Tin      |17 |35      |6.75f   |3.5f     |4   |10  |false   |0
		 * --------------------------------------------------------------------------
		 * Sapphire-Silver|18 |34      |7.5f    |4f       |6   |20  |true    |1
		 * Sapphire-Tungsn|19 |33      |7.75f   |4f       |7   |25  |true    |1
		 * --------------------------------------------------------------------------
		 * Emerald-Silver |19 |32      |8f      |4.25f    |5   |30  |true    |1
		 * Emerald-Tungsn |19 |31      |8.25f   |4.5f     |6   |35  |true    |1
		 * --------------------------------------------------------------------------
		 * Ruby-Gold      |21 |27      |9f      |4.75f    |7   |40  |true    |2
		 * Ruby-Platinum  |22 |27      |9.25f   |5f       |8   |45  |true    |2
		 * --------------------------------------------------------------------------
		 * Diamond-Gold   |23 |25      |9.25f   |5.25f    |7   |50  |true    |2
		 * Diamond-Platnum|24 |25      |9.5f    |5.5f     |8   |60  |true    |2
		 * --------------------------------------------------------------------------
		 * Amber          |22 |26      |9.25f   |5f       |7   |40  |true    |2
		 */
		
		public override void SetDefaults(Item item)
		{
			if(ModContent.GetInstance<ServerConfig>().AltStaffs)
			{
				switch(item.type)
				{
					case ItemID.TopazStaff:
						item.damage = 17;
						item.useTime = 35;
						item.useAnimation = 37;
						item.shootSpeed = 6.75f;
						item.knockBack = 3.5f;
						item.value = Item.sellPrice(0, 0, 10);
						break;
					case ItemID.EmeraldStaff:
						item.useTime = 31;
						item.useAnimation = 31;
						item.knockBack = 4.5f;
						item.value = Item.sellPrice(0, 0, 35);
						break;
					case ItemID.RubyStaff:
						item.rare = ItemRarityID.Green;
						item.useTime = 27;
						item.useAnimation = 27;
						break;
					case ItemID.DiamondStaff:
						item.damage = 24;
						item.useTime = 25;
						item.useAnimation = 25;
						break;
					case ItemID.AmberStaff:
						item.rare = ItemRarityID.Green;
						item.damage = 22;
						item.useTime = 26;
						item.useAnimation = 26;
						item.shootSpeed = 9.25f;
						item.knockBack = 5f;
						break;
				}
			}
		}
	}
}

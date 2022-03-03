
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class InertStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().MagicStones;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 50);
		}
		
		//This is necessary to prevent Life and Mana Stones from dissapearing when using Quick Heal/Mana
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
	}
}


using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GoldensMisc.Items.Equipable
{
	public class UniversalMagnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().Magnet;
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 32;
			Item.value = Item.buyPrice(0, 30);
			Item.rare = ItemRarityID.LightRed;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MiscPlayer>().Magnet = true;
		}
	}
}

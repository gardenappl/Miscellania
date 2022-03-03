
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Neck)]
	public class HeartLocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().HeartLocket;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 38;
			Item.value = Item.sellPrice(0, 2);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.longInvince = true;
			player.panic = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PanicNecklace)
				.AddIngredient(ItemID.CrossNecklace)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}


using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class EmeraldStaff : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AltStaffs;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
			Item.mana = 5;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 19;
			Item.useAnimation = 32;
			Item.useTime = 32;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.EmeraldBolt;
			Item.shootSpeed = 8f;
			Item.knockBack = 4.25f;
			Item.value = Item.sellPrice(0, 0, 30);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.SilverBar, 10)
				.AddIngredient(ItemID.Emerald, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

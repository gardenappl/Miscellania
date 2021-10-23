
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class TopazStaff : ModItem
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
			Item.mana = 3;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 15;
			Item.useAnimation = 38;
			Item.useTime = 38;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.TopazBolt;
			Item.shootSpeed = 6.5f;
			Item.knockBack = 3.25f;
			Item.value = Item.sellPrice(0, 0, 8);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CopperBar, 10)
				.AddIngredient(ItemID.Topaz, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

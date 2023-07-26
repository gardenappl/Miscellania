﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class AmethystStaff : ModItem
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
			Item.mana = 6;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 15;
			Item.useAnimation = 36;
			Item.useTime = 36;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.AmethystBolt;
			Item.shootSpeed = 6.25f;
			Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 6);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TinBar, 10)
				.AddIngredient(ItemID.Amethyst, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

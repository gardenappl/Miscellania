
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class UndyingSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().SpearofJustice;
		}
		
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(0, 7);
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 48;
			Item.height = 48;
			Item.UseSound = SoundID.Item8;
			Item.damage = 58;
			Item.knockBack = 8;
			Item.mana = 12;
			Item.shoot = ModContent.ProjectileType<Projectiles.UndyingSpear>();
			Item.shootSpeed = 16f;
			Item.noMelee = true; //So that the swing itself doesn't do damage; this weapon is projectile-only
			Item.noUseGraphic = true; //No swing animation
			Item.DamageType = DamageClass.Magic;
			Item.crit = 10;
			Item.glowMask = MiscGlowMasks.UndyingSpear;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<MagicSpear>())
				.AddIngredient(ItemID.SpectreBar, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

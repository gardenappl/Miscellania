
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class MagicSpear : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.SpearofJustice;
		}
		
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(0, 4);
			item.useStyle = 1;
			item.useAnimation = 25;
			item.useTime = 25;
			item.autoReuse = true;
			item.rare = 4;
			item.width = 42;
			item.height = 42;
			item.UseSound = SoundID.Item8;
			item.damage = 46;
			item.knockBack = 4;
			item.mana = 15;
			item.shoot = mod.ProjectileType(GetType().Name);
			item.shootSpeed = 14f;
			item.noMelee = true; //So that the swing itself doesn't do damage; this weapon is projectile-only
			item.noUseGraphic = true; //No swing animation
			item.magic = true;
			item.crit = 7;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrystalShard, 20);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddIngredient(ItemID.UnicornHorn, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}

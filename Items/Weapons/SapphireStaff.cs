
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class SapphireStaff : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.AltStaffs;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.rare = 1;
			item.mana = 6;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 18;
			item.useAnimation = 33;
			item.useTime = 33;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.SapphireBolt;
			item.shootSpeed = 7.75f;
			item.knockBack = 4f;
			item.value = Item.sellPrice(0, 0, 25);
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 10);
			recipe.AddIngredient(ItemID.Sapphire, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

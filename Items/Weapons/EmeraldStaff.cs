
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
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().AltStaffs;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.rare = 1;
			item.mana = 5;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 19;
			item.useAnimation = 32;
			item.useTime = 32;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.EmeraldBolt;
			item.shootSpeed = 8f;
			item.knockBack = 4.25f;
			item.value = Item.sellPrice(0, 0, 30);
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 10);
			recipe.AddIngredient(ItemID.Emerald, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

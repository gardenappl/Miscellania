
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
		public override bool Autoload(ref string name)
		{
			return Config.AltStaffs;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.AddTranslation(GameCulture.Russian, "Топазовый посох");
			Item.staff[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.mana = 3;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 15;
			item.useAnimation = 38;
			item.useTime = 38;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.TopazBolt;
			item.shootSpeed = 6.5f;
			item.knockBack = 3.25f;
			item.value = Item.sellPrice(0, 0, 8);
			item.magic = true;
			item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar, 10);
			recipe.AddIngredient(ItemID.Topaz, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

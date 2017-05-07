
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class AmethystStaff : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.AltStaffs;
		}
		
		public override void SetDefaults()
		{
			item.name = "Amethyst Staff";
			item.mana = 4;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 14;
			item.useAnimation = 39;
			item.useTime = 39;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.AmethystBolt;
			item.shootSpeed = 6.25f;
			item.knockBack = 3.5f;
			item.value = Item.sellPrice(0, 0, 6);
			item.magic = true;
			item.noMelee = true;
			Item.staff[item.type] = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinBar, 10);
			recipe.AddIngredient(ItemID.Amethyst, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

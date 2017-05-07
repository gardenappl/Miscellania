
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class RubyStaff : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.AltStaffs;
		}
		
		public override void SetDefaults()
		{
			item.name = "Ruby Staff";
			item.rare = 2;
			item.mana = 8;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 22;
			item.useAnimation = 27;
			item.useTime = 27;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.RubyBolt;
			item.shootSpeed = 9.25f;
			item.knockBack = 5f;
			item.value = Item.sellPrice(0, 0, 45);
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 10);
			recipe.AddIngredient(ItemID.Ruby, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

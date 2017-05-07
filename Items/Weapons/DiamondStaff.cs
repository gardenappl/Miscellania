
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class DiamondStaff : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.AltStaffs;
		}
		
		public override void SetDefaults()
		{
			item.name = "Diamond Staff";
			item.rare = 2;
			item.mana = 7;
			item.UseSound = SoundID.Item43;
			item.useStyle = 5;
			item.damage = 23;
			item.useAnimation = 25;
			item.useTime = 25;
			item.width = 40;
			item.height = 40;
			item.shoot = ProjectileID.DiamondBolt;
			item.shootSpeed = 9.25f;
			item.knockBack = 5.25f;
			item.value = Item.sellPrice(0, 0, 50);
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 10);
			recipe.AddIngredient(ItemID.Diamond, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

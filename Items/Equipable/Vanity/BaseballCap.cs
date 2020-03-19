
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class BaseballCap : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.BaseballBats;
		}
		
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.SetResult(ModContent.ItemType<BaseballCapBackwards>());
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Head)]
	public class BaseballCapBackwards : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.BaseballBats;
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(this);
			recipe.SetResult(ModContent.ItemType<BaseballCap>());
			recipe.AddRecipe();
		}
	}
}

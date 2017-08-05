
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Materials;

namespace GoldensMisc.Items.Tools
{
	public class WormholeMirror : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.WormholeMirror;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Teleports you to a party member\nClick their head on the fullscreen map");
			DisplayName.AddTranslation(GameCulture.Russian, "Зеркало-червоточина");
			Tooltip.AddTranslation(GameCulture.Russian, "Телепортирует вас к участнику команды\nЩёлкните по их голове на полноэкранной карте");
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.rare = 8;
			item.value = Item.sellPrice(0, 10);
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType<WormholeCrystal>(), 3);
			recipe.AddRecipeGroup("GoldensMisc:Silver", 10);
			recipe.AddIngredient(ItemID.Glass, 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Placeable;
using GoldensMisc.Items.Weapons;


namespace GoldensMisc
{
	public static class MiscRecipes
	{
		public static void AddRecipeGroups()
		{
			var recipeGroup = new RecipeGroup(() => Language.GetText("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.SilverBar), 
				ItemID.SilverBar,
				ItemID.TungstenBar
			);
			RecipeGroup.RegisterGroup("GoldensMisc:Silver", recipeGroup);
			if(ModContent.GetInstance<ServerConfig>().AncientMuramasa)
			{
				recipeGroup = new RecipeGroup(() => Language.GetText("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.Muramasa), 
					ItemID.Muramasa,
				    ModContent.ItemType<AncientMuramasa>()
				);
				RecipeGroup.RegisterGroup("GoldensMisc:Muramasa", recipeGroup);
			}
			if(ModContent.GetInstance<ServerConfig>().AncientForges)
			{
				recipeGroup = new RecipeGroup(() => Language.GetText("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.Hellforge),
					ItemID.Hellforge,
					ModContent.ItemType<AncientHellforge>()
				);
				RecipeGroup.RegisterGroup("GoldensMisc:Hellforge", recipeGroup);
			}
		}
		
		public static void PostAddRecipes()
		{

			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];

				if (ModContent.GetInstance<ServerConfig>().AncientMuramasa)
				{
					if (recipe.TryGetIngredient(ItemID.Muramasa, out Item ingredient))
					{
						recipe.RemoveIngredient(ingredient);
						recipe.AddRecipeGroup("GoldensMisc:Muramasa");
					}
				}
				if (ModContent.GetInstance<ServerConfig>().AncientForges)
				{
					if (recipe.TryGetIngredient(ItemID.Hellforge, out Item ingredient))
					{
						recipe.RemoveIngredient(ingredient);
						recipe.AddRecipeGroup("GoldensMisc:Hellforge");
					}
				}
				if (ModContent.GetInstance<ServerConfig>().NinjaGear)
				{
					if (recipe.TryGetIngredient(ItemID.Tabi, out Item ingredientTabi) && recipe.TryGetIngredient(ItemID.BlackBelt, out Item ingredientBelt) && !recipe.TryGetResult(ModContent.ItemType<NinjaGear>(), out Item result))
					{
						recipe.RemoveIngredient(ingredientTabi);
						recipe.RemoveIngredient(ingredientBelt);
						recipe.AddIngredient(ModContent.ItemType<NinjaGear>());
					}
				}
			}
		}
	}
}

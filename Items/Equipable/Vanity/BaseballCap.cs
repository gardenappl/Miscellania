
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class BaseballCap : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 10;
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
			Item.value = Item.sellPrice(0, 0, 50);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BaseballCapBackwards>())
				.Register();
		}
	}

	[AutoloadEquip(EquipType.Head)]
	public class BaseballCapBackwards : BaseballCap
	{

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<BaseballCap>())
				.Register();
		}
	}
}

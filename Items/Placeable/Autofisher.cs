
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class Autofisher : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().Autofisher;
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 34;
			Item.rare = ItemRarityID.Green;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.buyPrice(gold: 20);
			Item.createTile = ModContent.TileType<Tiles.Autofisher>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MechanicsRod)
				.AddRecipeGroup("IronBar", 10)
				.AddIngredient(ItemID.Wire, 25)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

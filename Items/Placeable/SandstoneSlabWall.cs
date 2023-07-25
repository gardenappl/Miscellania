
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace GoldensMisc.Items.Placeable
{
	public class SandstoneSlabWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BuildingMaterials;
		}
		
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createWall = ModContent.WallType<Tiles.SandstoneSlabWall>();
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(this, 4)
				.AddTile(TileID.WorkBenches)
				.ReplaceResult(ItemID.SandstoneSlab);

			CreateRecipe(4)
				.AddIngredient(ItemID.SandstoneSlab)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

    }
}

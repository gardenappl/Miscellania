
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using static VanillaTweaks.VanillaTweaks;

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
			Item.maxStack = 999;
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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (GoldensMisc.VanillaTweaks != null)
			{
				if(sandstoneRename)
				{
					int index = tooltips.FindIndex(line => line.Mod == "Terraria" && line.Name == "ItemName");
					if (index != -1)
					{
						tooltips[index].Text = Language.GetTextValue("Mods.VanillaTweaks.Items.SandstoneSlabWall.DisplayName");
					}
				}
			}
		}

        [JITWhenModsEnabled("VanillaTweaks")]
        public bool sandstoneRename => SandstoneRename;

    }
}

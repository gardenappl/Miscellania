using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GoldensMisc.Items.Placeable
{
	public class ChestVacuum : ModItem
	{

		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().ChestVacuum;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 22;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Green;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.buyPrice(gold: 15);
			Item.createTile = ModContent.TileType<Tiles.ChestVacuum>();
		}
	}
}

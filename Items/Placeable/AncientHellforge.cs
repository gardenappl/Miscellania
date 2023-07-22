
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GoldensMisc.Items.Placeable
{
	public class AncientHellforge : ModItem
	{

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientForges;
		}
		
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 26;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = Item.sellPrice(silver: 60);
			Item.createTile = ModContent.TileType<Tiles.AncientHellforge>();
		}
	}
}

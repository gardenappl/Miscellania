
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
			Item.CloneDefaults(ItemID.Hellforge);
			Item.width = 30;
			Item.height = 26;
			Item.createTile = ModContent.TileType<Tiles.AncientHellforge>();
		}
	}
}

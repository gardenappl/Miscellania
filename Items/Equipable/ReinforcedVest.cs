
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Body)]
	public class ReinforcedVest : ModItem
	{
		
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().ReinforcedVest;
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 15;
			Item.value = Item.buyPrice(0, 15);
		}
		
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<MiscPlayer>().ExplosionResistant = true;
		}
	
	}
}

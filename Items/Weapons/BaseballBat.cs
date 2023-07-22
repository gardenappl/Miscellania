
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class BaseballBat : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().BaseballBats;
		}
		
		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 23;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.knockBack = 17.5f;
			Item.value = Item.sellPrice(0, 2);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
		}
	}
}

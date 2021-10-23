
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class GodStone : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().GodStone;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.healLife = 1000;
			Item.healMana = 500;
			Item.potion = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item29;
			Item.rare = ItemRarityID.Purple;
		}
		
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
		
		public override bool? UseItem(Player player)
		{
			player.ClearBuff(BuffID.PotionSickness);
			player.potionDelay = 0;
			return true;
		}
		
		public override void HoldStyle(Player player, Rectangle heldItemFrame)
		{
			player.itemLocation.X -= 10 * player.direction;
			player.itemLocation.Y += 10 * player.gravDir;
		}
	}
}

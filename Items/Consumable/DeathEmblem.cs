
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GoldensMisc.Buffs;

namespace GoldensMisc.Items.Consumable
{
	public class DeathEmblem : ModItem
	{
		public override void SetDefaults()
		{
			Item.useTurn = true;
			Item.width = 26;
			Item.height = 18;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 90;
			Item.UseSound = SoundID.MoonLord;
			Item.useAnimation = 90;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.sellPrice(0, 0, 50);
			Item.consumable = true;
			Item.maxStack = 99;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.showLastDeath;
		}
		
		public override bool? UseItem(Player player)
		{
			if (Main.rand.NextBool(2))
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0.0f, 0.0f, 150, Color.Red, 1.1f);
			if (player.itemAnimation == Item.useAnimation / 2)
			{
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, (float) (player.velocity.X * 0.5), (float) (player.velocity.Y * 0.5), 150, Color.Red, 1.5f);
//				player.grappling[0] = -1;
//				player.grapCount = 0;
//				for (int index = 0; index < 1000; ++index)
//				{
//					if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
//						Main.projectile[index].Kill();
//				}
				player.Teleport(player.lastDeathPostion, -69);
				player.Center = player.lastDeathPostion;
				if(Main.netMode == NetmodeID.SinglePlayer)
					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float) player.whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 3);
				
				player.AddBuff(ModContent.BuffType<CursedMemory>(), 120 * 60);
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0.0f, 0.0f, 150, Color.Red, 1.5f);
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Bone, 15)
				.AddIngredient(ItemID.SoulofNight, 3)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

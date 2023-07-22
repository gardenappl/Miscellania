
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class OrbofLight : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientOrb;
		}
		
		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shoot = ModContent.ProjectileType<Projectiles.OrbofLight>();
			Item.width = 26;
			Item.height = 26;
			Item.UseSound = SoundID.Item8;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 4);
			Item.buffType = ModContent.BuffType<Buffs.OrbofLight>();
		}
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.GoldDust, 80)
				.AddIngredient(ItemID.SoulofSight, 10)
				.AddIngredient(ItemID.SoulofLight, 8)
				.AddIngredient(ItemID.ShadowOrb)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

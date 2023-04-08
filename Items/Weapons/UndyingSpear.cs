
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GoldensMisc.Items.Weapons
{
	public class UndyingSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().SpearofJustice;
		}
		
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(0, 7);
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.Yellow;
			Item.width = 48;
			Item.height = 48;
			Item.UseSound = SoundID.Item8;
			Item.damage = 58;
			Item.knockBack = 8;
			Item.mana = 12;
			Item.shoot = ModContent.ProjectileType<Projectiles.UndyingSpear>();
			Item.shootSpeed = 16f;
			Item.noMelee = true; //So that the swing itself doesn't do damage; this weapon is projectile-only
			Item.noUseGraphic = true; //No swing animation
			Item.DamageType = DamageClass.Magic;
			Item.crit = 10;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D glowTexture = MiscGlowMasks.UndyingSpear;
			Color color = Color.White;
			color.A = 255;
			spriteBatch.Draw(glowTexture, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D glowTexture = MiscGlowMasks.UndyingSpear;
			Color color = Color.White;
			color.A = 255;
			Rectangle glowTextureSize = new(0, 0, glowTexture.Width, glowTexture.Height);
			Vector2 glowTextureOrigin = glowTexture.Size() * .5f;
			Vector2 glowOffset = new ((float)(Item.width / 2) - glowTextureOrigin.X, (float)(Item.height - glowTexture.Height));
			Vector2 glowPosition = Item.position - Main.screenPosition + glowTextureOrigin + glowOffset;
			spriteBatch.Draw(glowTexture, glowPosition, glowTextureSize, color, rotation, glowTextureOrigin, scale, SpriteEffects.None, 0f);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<MagicSpear>())
				.AddIngredient(ItemID.SpectreBar, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
    }
}

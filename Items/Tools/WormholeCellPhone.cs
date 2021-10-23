
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Tools
{
	public class WormholeCellPhone : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().WormholeMirror && ModContent.GetInstance<ServerConfig>().WormholePhone;
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTurn = true;
			Item.useTime = 90;
			Item.UseSound = SoundID.Item6;
			Item.useAnimation = 90;
			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 10);
		}

		public override bool? UseItem(Player player)
		{
			return MiscUtils.magicMirrorRecall(player, Item);
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw((Texture2D)Mod.Assets.Request<Texture2D>("Items/Tools/CellPhone_Resprite"), position, frame, Color.White * alpha, 0f, origin, scale, 0, 0f);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			spriteBatch.Draw(TextureAssets.Item[Item.type].Value, Item.Center - Main.screenPosition, null, lightColor, rotation, Item.Size / 2, scale, 0, 0f);
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw((Texture2D)Mod.Assets.Request<Texture2D>("Items/Tools/CellPhone_Resprite"), Item.Center - Main.screenPosition, null, lightColor * alpha, rotation, Item.Size / 2, scale, 0, 0f);
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			player.accWatch = 3;
			player.accDepthMeter = 1;
			player.accCompass = 1;
			player.accFishFinder = true;
			player.accWeatherRadio = true;
			player.accCalendar = true;
			player.accThirdEye = true;
			player.accJarOfSouls = true;
			player.accCritterGuide = true;
			player.accStopwatch = true;
			player.accOreFinder = true;
			player.accDreamCatcher = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CellPhone)
				.AddIngredient(ModContent.ItemType<WormholeDoubleMirror>())
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CellPhone)
				.AddIngredient(ModContent.ItemType<WormholeMirror>())
				.AddTile(TileID.TinkerersWorkbench)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.CellPhone)
				.AddIngredient(ModContent.ItemType<WormholeIceMirror>())
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}

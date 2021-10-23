
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Tools
{
	public class WormholeIceMirror : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().WormholeMirror;
		}
		
		public override void SetDefaults()
		{
			Item.useTurn = true;
			Item.width = 24;
			Item.height = 24;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 90;
			Item.useAnimation = 90;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 10);
		}

		public override bool? UseItem(Player player)
		{
			return MiscUtils.magicMirrorRecall(player, Item);
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw(TextureAssets.Item[ItemID.IceMirror].Value, position, frame, Color.White * alpha, 0f, origin, scale, 0, 0f);
		}
		
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			spriteBatch.Draw(TextureAssets.Item[Item.type].Value, Item.Center - Main.screenPosition, null, lightColor, rotation, Item.Size / 2, scale, 0, 0f);
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw(TextureAssets.Item[ItemID.IceMirror].Value, Item.Center - Main.screenPosition, null, lightColor * alpha, rotation, Item.Size / 2, scale, 0, 0f);
			return false;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<WormholeMirror>())
				.AddIngredient(ItemID.IceMirror)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}

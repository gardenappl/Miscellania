
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class MagicSpear : ModItem
	{

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().SpearofJustice;
		}
		
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(0, 4);
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.autoReuse = true;
			Item.rare = ItemRarityID.LightRed;
			Item.width = 42;
			Item.height = 42;
			Item.UseSound = SoundID.Item8;
			Item.damage = 46;
			Item.knockBack = 6;
			Item.mana = 13;
			Item.shoot = ModContent.ProjectileType<Projectiles.MagicSpear>();
			Item.shootSpeed = 14f;
			Item.noMelee = true; //So that the swing itself doesn't do damage; this weapon is projectile-only
			Item.noUseGraphic = true; //No swing animation
			Item.DamageType = DamageClass.Magic;
			Item.crit = 7;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CrystalShard, 20)
				.AddIngredient(ItemID.SoulofFright, 15)
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddIngredient(ItemID.UnicornHorn, 3)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}

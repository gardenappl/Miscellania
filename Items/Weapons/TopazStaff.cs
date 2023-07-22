
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class TopazStaff : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AltStaffs;
		}

		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.mana = 5;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 16;
			Item.useAnimation = 36;
			Item.useTime = 36;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.TopazBolt;
			Item.shootSpeed = 6.5f;
			Item.knockBack = 3.25f;
			Item.value = Item.sellPrice(0, 0, 8);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.CopperBar, 10)
				.AddIngredient(ItemID.Topaz, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

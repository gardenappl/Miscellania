
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class DiamondStaff : ModItem
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
			Item.rare = ItemRarityID.Green;
			Item.mana = 7;
			Item.UseSound = SoundID.Item43;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.damage = 23;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.width = 40;
			Item.height = 40;
			Item.shoot = ProjectileID.DiamondBolt;
			Item.shootSpeed = 9.25f;
			Item.knockBack = 5.25f;
			Item.value = Item.sellPrice(0, 0, 50);
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.GoldBar, 10)
				.AddIngredient(ItemID.Diamond, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}

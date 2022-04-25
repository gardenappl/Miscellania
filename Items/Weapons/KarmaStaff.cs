
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GoldensMisc.Projectiles;


namespace GoldensMisc.Items.Weapons
{
	public class KarmaStaff : ModItem
	{


		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().GasterBlaster;
		}
		
		public override void SetStaticDefaults()
		{
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.sentry = true;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = Item.sellPrice(0, 3);
			Item.rare = ItemRarityID.Pink;
			Item.shoot = ModContent.ProjectileType<GasterBlaster>();
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
		{
			//projectile spawns at mouse cursor
			position = Main.MouseWorld;
			int index = Projectile.NewProjectile(source, position, speed, Item.shoot, damage, knockBack, player.whoAmI);
			Main.projectile[index].originalDamage = Item.damage;
			//don't shoot if player is right-clicking
			return false;//player.altFunctionUse != 2;
		}
		
		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				Item.UseSound = SoundID.Item1;
			}
			else
			{
                Item.UseSound = SoundID.Item78;
			}
			return true;
		}
		
		public override bool? UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(false);
			}
			return null;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Bone, 25)
				.AddIngredient(ItemID.CursedFlame, 5)
				.AddIngredient(ItemID.SoulofFright, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.Bone, 25)
				.AddIngredient(ItemID.Ichor, 5)
				.AddIngredient(ItemID.SoulofFright, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

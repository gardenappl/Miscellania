
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Projectiles;

namespace GoldensMisc.Items.Weapons
{
	public class KarmaStaff : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.GasterBlaster;
		}
		
		public override void SetStaticDefaults()
		{
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			//ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.damage = 50;
			item.summon = true;
			item.mana = 10;
			item.sentry = true;
			item.width = 46;
			item.height = 46;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 2.5f;
			item.value = Item.sellPrice(0, 3);
			item.rare = 5;
			item.shoot = mod.ProjectileType<GasterBlaster>();
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//projectile spawns at mouse cursor
			position = Main.MouseWorld;
			//don't shoot if player is right-clicking
			return player.altFunctionUse != 2;
		}
		
		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				item.UseSound = SoundID.Item1;
			}
			else
			{
                item.UseSound = SoundID.Item78;
			}
			return base.CanUseItem(player);
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddIngredient(ItemID.CursedFlame, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddIngredient(ItemID.Ichor, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

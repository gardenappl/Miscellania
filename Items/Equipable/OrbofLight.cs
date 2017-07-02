
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class OrbofLight : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Orb of Light");
			Tooltip.SetDefault("Creates a magical glowing orb");
			DisplayName.AddTranslation(GameCulture.Russian, "Древняя сфера света");
			Tooltip.AddTranslation(GameCulture.Russian, "Создаёт волшебную светящуюся сферу");
		}
		
		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = 4;
			item.shoot = mod.ProjectileType(GetType().Name);
			item.width = 26;
			item.height = 26;
			item.UseSound = SoundID.Item8;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = 5;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 4);
			item.buffType = mod.BuffType(GetType().Name);
		}
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600);
			}
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldDust, 80);
			recipe.AddIngredient(ItemID.SoulofSight, 10);
			recipe.AddIngredient(ItemID.SoulofLight, 8);
			recipe.AddIngredient(ItemID.ShadowOrb);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

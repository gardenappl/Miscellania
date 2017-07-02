
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Neck)]
	public class HeartLocket : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.HeartLocket;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases movement speed and length of invincibility after being struck");
			DisplayName.AddTranslation(GameCulture.Russian, "Медальон-сердце");
			Tooltip.AddTranslation(GameCulture.Russian, "Увеличивает продолжительность неуязвимости после получения урона\n" +
			                       "После удара увеличивает скорость движения");
		}
		
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 38;
			item.value = Item.sellPrice(0, 2);
			item.rare = 5;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.longInvince = true;
			player.panic = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PanicNecklace);
			recipe.AddIngredient(ItemID.CrossNecklace);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

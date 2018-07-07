
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Materials;
using GoldensMisc.UI;

namespace GoldensMisc.Items.Tools
{
	public class WormholeMirror : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.WormholeMirror;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Teleports you to a party member\nRight-click and select a player");

			DisplayName.AddTranslation(GameCulture.Russian, "Зеркало-червоточина");
			Tooltip.AddTranslation(GameCulture.Russian, "Телепортирует вас к участнику команды\nНажмите ПКМ и выберите игрока");

			DisplayName.AddTranslation(GameCulture.Chinese, "虫洞之镜");
			Tooltip.AddTranslation(GameCulture.Chinese, "可传送队友\n右键选择玩家");
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.useStyle = 4;
			item.useTime = 90;
			item.useAnimation = 90;
			item.rare = 5;
			item.value = Item.sellPrice(0, 10);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				if(UIWormhole.Visible)
					UIWormhole.Close();
				else
					UIWormhole.Open(item, false);
				return true;
			}
			return false;
		}

		public override bool CanRightClick()
		{
			//return Main.netMode != NetmodeID.SinglePlayer && Main.LocalPlayer.team != 0;
			return true;
		}

		public override void RightClick(Player player)
		{
			if(UIWormhole.Visible)
				UIWormhole.Close();
			else
				UIWormhole.Open(item, false);
			item.stack++;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("GoldensMisc:Silver", 10);
			recipe.AddIngredient(ItemID.Glass, 12);
			//recipe.AddIngredient(mod.ItemType<WormholeCrystal>(), 2);
			recipe.AddIngredient(ItemID.SoulofSight, 8);
			recipe.AddIngredient(ItemID.WormholePotion, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

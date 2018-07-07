
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Materials
{
	public class WormholeCrystal : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.WormholeMirror;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'You feel like it pulls you in as you hold it...'\n" +
				"This material currently has no use within Miscellania.\n" +
				"You can sell it or keep it in case of future mod updates.");
			
			DisplayName.AddTranslation(GameCulture.Russian, "Кристалл-червоточина");
			Tooltip.AddTranslation(GameCulture.Russian, "'Держа его в руке, вы чувствуете, что вас засасывет...'\n" +
				"На данный момент этот материал не имеет использований.\n" +
				"Можете или продать, или оставить себе на случай будущих апдейтов.");

			DisplayName.AddTranslation(GameCulture.Chinese, "虫洞结晶");
			Tooltip.AddTranslation(GameCulture.Chinese, "'当你握着它的时候,你觉得它把你吸引住了...'\n" +
				"这种材料在Miscellania中暂时没有用处.\n" +
				"你可以将其出售或保留以备将来的Mod更新.");

			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			//item.rare = 8;
			item.rare = -1;
			item.value = Item.sellPrice(0, 1);
			item.maxStack = 99;
			item.alpha = 50;
		}
	}
}

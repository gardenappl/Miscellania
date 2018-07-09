
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class BaseballBat : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.BaseballBats;
		}
		
		public override void SetDefaults()
		{
			item.damage = 16;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 23;
			item.useAnimation = 23;
			item.useStyle = 1;
			item.autoReuse = true;
			item.knockBack = 17.5f;
			item.value = Item.sellPrice(0, 2);
			item.rare = 1;
			item.UseSound = SoundID.Item1;
		}
	}
}


using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Buffs
{
	public class OrbofLight : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ancient Orb of Light");
			Description.SetDefault("A magical orb that provides light");

			DisplayName.AddTranslation(GameCulture.Russian, "Древняя сфера света");
			Description.AddTranslation(GameCulture.Russian, "Магическая сфера, излучающая свет");

			DisplayName.AddTranslation(GameCulture.Chinese, "远古光明珍珠");
			Description.AddTranslation(GameCulture.Chinese, "一个发出微光的魔法小球");

			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<MiscPlayer>(mod).OrbofLight = true;
			player.buffTime[buffIndex] = 18000;
			bool projSpawned = player.ownedProjectileCounts[mod.ProjectileType(GetType().Name)] > 0;
			if(!projSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType(GetType().Name), 0, 0f, player.whoAmI);
			}
		}
	}
}

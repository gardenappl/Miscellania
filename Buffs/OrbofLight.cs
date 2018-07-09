
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

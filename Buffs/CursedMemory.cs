
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Buffs
{
	public class CursedMemory : ModBuff
	{

		public override void SetStaticDefaults()
		{
			CanBeCleared = false;
			Main.debuff[Type] = true;
			LongerExpertDebuff = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = Math.Max(player.statLifeMax / 2, 100);
		}
	}
}

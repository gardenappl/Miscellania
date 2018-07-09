
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Buffs
{
	public class CursedMemory : ModBuff
	{
		public override void SetDefaults()
		{
			canBeCleared = false;
			longerExpertDebuff = true;
			Main.debuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = Math.Max(player.statLifeMax / 2, 100);
		}
	}
}

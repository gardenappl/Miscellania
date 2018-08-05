using GoldensMisc.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc
{
	internal delegate void GetFishingLevelHook(Vector2 position, Item fishingRod, Item bait, ref int fishingLevel);
	internal delegate void CatchFishHook(Vector2 position, int zoneInfo, Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk);

	internal static class AutofisherHooks
	{
		static GetFishingLevelHook fishingLevelHook;
		static CatchFishHook catchFishHook;


		public static void GetFishingLevel(AutofisherTE te, Item bait, ref int fishingLevel)
		{
			var fishingRod = new Item();
			fishingRod.SetDefaults(ItemID.MechanicsRod, true);
			fishingLevelHook(te.Position.ToWorldCoordinates(), fishingRod, bait, ref fishingLevel);
		}

		public static void CatchFish(AutofisherTE te, Zone zone, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk)
		{
			var fishingRod = new Item();
			fishingRod.SetDefaults(ItemID.MechanicsRod, true);
			catchFishHook(te.Position.ToWorldCoordinates(), (int)zone, fishingRod, bait, power, liquidType, poolSize, worldLayer, ref caughtType, ref junk);
		}


		//probably a bad system for doing this but that's ok?
		public static void RegisterHook(GetFishingLevelHook hook)
		{
			fishingLevelHook += hook;
		}

		public static void RegisterHook(CatchFishHook hook)
		{
			catchFishHook += hook;
		}

		public static void Unload()
		{
			fishingLevelHook = null;
			catchFishHook = null;
		}


		public static void TestHookGetFishingLevel(Vector2 position, Item fishingRod, Item bait, ref int fishingLevel)
		{
			fishingLevel = 9000;
		}

		public static void TestHookCatchFish(Vector2 position, int zoneInfo, Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk)
		{
			if(power == 9000)
			{
				Main.NewText((Zone)zoneInfo);
				caughtType = ItemID.Acorn;
			}
		}
	}
}

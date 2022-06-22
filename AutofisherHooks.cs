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
	//internal delegate void GetFishingLevelHook(Vector2 position, Item fishingRod, Item bait, ref int fishingLevel);
	//internal delegate void CatchFishHook(Vector2 position, int zoneInfo, Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk);

	static class AutofisherHooks
	{
		static Dictionary<HookType, List<MethodInfo>> hooks = new Dictionary<HookType, List<MethodInfo>>();

		public static void Initialize()
		{
			hooks[HookType.CatchFish] = new List<MethodInfo>();
			hooks[HookType.GetFishingLevel] = new List<MethodInfo>();
			hooks[HookType.CatchEnemy] = new List<MethodInfo>();
		}

		public static void GetFishingLevel(AutofisherTE te, Item bait, ref int fishingLevel)
		{
			var fishingRod = new Item();
			fishingRod.SetDefaults(ItemID.MechanicsRod, true);

			object[] args = new object[] { te.Position.ToWorldCoordinates(), fishingRod, bait, fishingLevel };
			var hookList = hooks[HookType.GetFishingLevel];
			foreach(var hook in hookList)
				hook.Invoke(null, args);
			fishingLevel = (int)args[3];
		}

		public static void CatchFish(AutofisherTE te, Zone zone, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk)
		{
			var fishingRod = new Item();
			fishingRod.SetDefaults(ItemID.MechanicsRod, true);

			object[] args = new object[] { te.Position.ToWorldCoordinates(), (int)zone, fishingRod, bait, power, liquidType, poolSize, worldLayer, caughtType, junk};
			var hookList = hooks[HookType.CatchFish];
			foreach(var hook in hookList)
				hook.Invoke(null, args);
			caughtType = (int)args[8];
			junk = (bool)args[9];
		}

		public static void CatchEnemy(AutofisherTE te, Zone zone, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType)
		{
			var fishingRod = new Item();
			fishingRod.SetDefaults(ItemID.MechanicsRod, true);

			object[] args = new object[] { te.Position.ToWorldCoordinates(), (int)zone, fishingRod, bait, power, liquidType, poolSize, worldLayer, caughtType};
			var hookList = hooks[HookType.CatchEnemy];
			foreach (var hook in hookList)
				hook.Invoke(null, args);
			caughtType = (int)args[8];
		}


		public static void RegisterHook(HookType type, MethodInfo hook)
		{
			hooks[type].Add(hook);
		}

		public static void Unload()
		{
			hooks.Clear();
			hooks = null;
		}


		//public static void TestHookGetFishingLevel(Vector2 position, Item fishingRod, Item bait, ref int fishingLevel)
		//{
		//	fishingLevel = 9000;
		//}

		//public static void TestHookCatchFish(Vector2 position, int zoneInfo, Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, ref int caughtType, ref bool junk)
		//{
		//	if(power == 9000)
		//	{
		//		Main.NewText((Zone)zoneInfo);
		//		caughtType = ItemID.Acorn;
		//	}
		//}

		public enum HookType
		{
			CatchEnemy,
			GetFishingLevel,
			CatchFish
		}
	}
}

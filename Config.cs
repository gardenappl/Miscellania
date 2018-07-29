
using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

using FKTModSettings;

namespace GoldensMisc
{
	//most of this will be removed once tML implements ModConfig
	public static class Config
	{
		//disabling/enabling items will be gone, too
		public static bool AltStaffs = true;
		const string AltStaffsKey = "AltStaffs";
		
		public static bool MagicStones = true;
		const string MagicStonesKey = "MagicStones";
		
		public static bool GodStone = true;
		const string GodStoneKey = "GodStone";
		
		public static bool DemonCrown = true;
		const string DemonCrownKey = "DemonCrown";
		
		public static bool HeartLocket = true;
		const string HeartLocketKey = "HeartLocket";
		
		public static bool Magnets = true;
		const string MagnetsKey = "Magnet";
		
		public static bool NinjaGear = true;
		const string NinjaGearKey = "NinjaGear";
		
		public static bool ReinforcedVest = true;
		const string ReinforcedVestKey = "ReinforcedVest";
		
		public static bool AncientForges = true;
		const string AncientForgesKey = "AncientForges";
		
		public static bool RedBrickFurniture = true;
		const string RedBrickFurnitureKey = "RedBrickFurniture";
		
		public static bool AncientMuramasa = true;
		const string AncientMuramasaKey = "AncientMuramasa";
		
		public static bool GasterBlaster = true;
		const string GasterBlasterKey = "GasterBlaster";
		
		public static bool SpearofJustice = true;
		const string SpearofJusticeKey = "SpearofJustice";
		
		public static bool WormholeMirror = true;
		const string WormholeMirrorKey = "WormholeMirror";
		
		public static bool CellPhoneUpgrade = true;
		const string CellPhoneUpgradeKey = "WormholePhone";
		
		public static bool RodofWarping = true;
		const string RodofWarpingKey = "RodofWarping";

		public static float RodofWarpingChaosState = 1f;
		const string RodofWarpingChaosStateKey = "RodofWarpingChaosState";
		
		public static bool EmblemofDeath = true;
		const string EmblemofDeathKey = "EmblemofDeath";
		
		public static bool BuildingMaterials = true;
		const string BuildingMaterialsKey = "BuildingMaterials";
		
		public static bool BaseballBats = true;
		const string BaseballBatsKey = "BaseballBats";
		
		public static bool AncientOrb = true;
		const string AncientOrbKey = "AncientOrb";	

		public static bool CellPhoneResprite = true;
		const string CellPhoneRespriteKey = "CellPhoneResprite";

		public static bool ExtraDyes = true;
		const string ExtraDyesKey = "ExtraDyes";

		public static bool Autofisher = true;
		const string AutofisherKey = "Autofisher";

		public static bool MechanicsRodOften = true;
		const string MechanicsRodOftenKey = "MechanicsRodOften";

		public static bool ChestVacuum = true;
		const string ChestVacuumKey = "ChestVacuum";
		
		static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania.json");
		
		static string OldConfigFolderPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania");
		static string OldConfigPath = Path.Combine(OldConfigFolderPath, "config.json");
		static string OldConfigVersionPath = Path.Combine(OldConfigFolderPath, "config.version");
		
		static readonly Preferences Configuration = new Preferences(ConfigPath);
		
		public static void Load()
		{
			if(Directory.Exists(OldConfigFolderPath))
			{
				if(File.Exists(OldConfigPath))
				{
					GoldensMisc.Log("Found config file in old folder! Moving config...");
					File.Move(OldConfigPath, ConfigPath);
				}
				if(File.Exists(OldConfigVersionPath))
					File.Delete(OldConfigVersionPath);
				if(Directory.GetFiles(OldConfigFolderPath).Length == 0 && Directory.GetDirectories(OldConfigFolderPath).Length == 0)
					Directory.Delete(OldConfigFolderPath);
				else
					GoldensMisc.Log("Old config folder still cotains some files/directories. They will not get deleted.");
			}
			SetDefaults();
			if(!ReadConfig())
			{
				GoldensMisc.Log("Failed to read config file!");
			}
			SaveConfig();
		}
		
		public static void SetDefaults()
		{
			AltStaffs = true;
			MagicStones = true;
			GodStone = true;
			DemonCrown = true;
			HeartLocket = true;
			Magnets = true;
			NinjaGear = true;
			ReinforcedVest = true;
			AncientForges = true;
			RedBrickFurniture = true;
			AncientMuramasa = true;
			GasterBlaster = true;
			SpearofJustice = true;
			WormholeMirror = true;
			CellPhoneUpgrade = true;
			CellPhoneResprite = true;
			RodofWarping = true;
			RodofWarpingChaosState = 1f;
			EmblemofDeath = true;
			BuildingMaterials = true;
			BaseballBats = true;
			AncientOrb = true;
			CellPhoneResprite = true;
			ExtraDyes = true;
			Autofisher = true;
			MechanicsRodOften = true;
			ChestVacuum = true;
		}
		
		public static bool ReadConfig()
		{
			if(Configuration.Load())
			{
				Configuration.Get(AltStaffsKey, ref AltStaffs);
				Configuration.Get(MagicStonesKey, ref MagicStones);
				Configuration.Get(GodStoneKey, ref GodStone);
				Configuration.Get(DemonCrownKey, ref DemonCrown);
				Configuration.Get(HeartLocketKey, ref HeartLocket);
				Configuration.Get(MagnetsKey, ref Magnets);
				Configuration.Get(NinjaGearKey, ref NinjaGear);
				Configuration.Get(ReinforcedVestKey, ref ReinforcedVest);
				Configuration.Get(AncientForgesKey, ref AncientForges);				
				Configuration.Get(RedBrickFurnitureKey, ref RedBrickFurniture);
				Configuration.Get(AncientMuramasaKey, ref AncientMuramasa);
				Configuration.Get(GasterBlasterKey, ref GasterBlaster);
				Configuration.Get(SpearofJusticeKey, ref SpearofJustice);
				Configuration.Get(WormholeMirrorKey, ref WormholeMirror);
				Configuration.Get(CellPhoneUpgradeKey, ref CellPhoneUpgrade);
				Configuration.Get(CellPhoneRespriteKey, ref CellPhoneResprite);
				Configuration.Get(RodofWarpingKey, ref RodofWarping);
				Configuration.Get(RodofWarpingChaosStateKey, ref RodofWarpingChaosState);
				Configuration.Get(EmblemofDeathKey, ref EmblemofDeath);
				Configuration.Get(BuildingMaterialsKey, ref BuildingMaterials);
				Configuration.Get(BaseballBatsKey, ref BaseballBats);
				Configuration.Get(ExtraDyesKey, ref ExtraDyes);
				Configuration.Get(AutofisherKey, ref Autofisher);
				Configuration.Get(MechanicsRodOftenKey, ref MechanicsRodOften);
				Configuration.Get(ChestVacuumKey, ref ChestVacuum);
				return true;
			}
			return false;
		}
		
		public static void SaveConfig()
		{
			Configuration.Clear();
			Configuration.Put(AltStaffsKey, AltStaffs);
			Configuration.Put(MagicStonesKey, MagicStones);
			Configuration.Put(GodStoneKey, GodStone);
			Configuration.Put(DemonCrownKey, DemonCrown);
			Configuration.Put(HeartLocketKey, HeartLocket);
			Configuration.Put(MagnetsKey, Magnets);
			Configuration.Put(NinjaGearKey, NinjaGear);
			Configuration.Put(ReinforcedVestKey, ReinforcedVest);
			Configuration.Put(AncientForgesKey, AncientForges);				
			Configuration.Put(RedBrickFurnitureKey, RedBrickFurniture);
			Configuration.Put(AncientMuramasaKey, AncientMuramasa);
			Configuration.Put(GasterBlasterKey, GasterBlaster);
			Configuration.Put(SpearofJusticeKey, SpearofJustice);
			Configuration.Put(WormholeMirrorKey, WormholeMirror);
			Configuration.Put(CellPhoneUpgradeKey, CellPhoneUpgrade);
			Configuration.Put(CellPhoneRespriteKey, CellPhoneResprite);
			Configuration.Put(RodofWarpingKey, RodofWarping);
			Configuration.Put(RodofWarpingChaosStateKey, RodofWarpingChaosState);
			Configuration.Put(EmblemofDeathKey, EmblemofDeath);
			Configuration.Put(BuildingMaterialsKey, BuildingMaterials);
			Configuration.Put(BaseballBatsKey, BaseballBats);
			Configuration.Put(AncientOrbKey, AncientOrb);
			Configuration.Put(ExtraDyesKey, ExtraDyes);
			Configuration.Put(AutofisherKey, Autofisher);
			Configuration.Put(MechanicsRodOftenKey, MechanicsRodOften);
			Configuration.Put(ChestVacuumKey, ChestVacuum);
			Configuration.Save();
		}
		
		public static void LoadFKConfig()
		{
			var setting = ModSettingsAPI.CreateModSettingConfig(GoldensMisc.Instance);
			
			setting.AddComment("All these features require a mod reload to change properly.");
			setting.AddComment("Features marked with an asterisk (*) require a new world or /miscWorldGen chat command.");
			setting.AddComment("For more information, visit the forum page.");
			
			const float commentScale = 0.8f;
			
			setting.AddComment("Stat changes and new early game gem staves", commentScale);
			setting.AddBool(AltStaffsKey, "Alternate Staves", false);
			setting.AddComment("Magic stones that replуnish health and mana for a number of uses.", commentScale);
			setting.AddBool(MagicStonesKey, "Magic Stones", false);
			setting.AddComment("Unobtainable God Mode item(use Cheat Sheet or another mod.)", commentScale);
			setting.AddBool(GodStoneKey, "God Stone", false);
			setting.AddComment("A rare drop from Demons in Hardmode. Boosts your magical abilities and summons a Red Crystal to protect you.", commentScale);
			setting.AddBool(DemonCrownKey, "Demon Crown", false);
			setting.AddComment("Heart Locket = Panic Necklace + Cross Necklace.", commentScale);
			setting.AddBool(HeartLocketKey, "Heart Locket", false);
			setting.AddComment("Item magnet and accessory combos", commentScale);
			setting.AddBool(MagnetsKey, "Magnets", false);
			setting.AddComment("Ninja Gear = Tabi + Black Belt.\n" +
			                   "Master Ninja Gear = Ninja Gear + Climbing Gear", commentScale);
			setting.AddBool(NinjaGearKey, "Change Ninja Gear", false);
			setting.AddComment("Armor item that protects you from your own explosives\n" +
			                   "If Vanilla Tweaks is installed, equipping a SWAT Helmet and a Reinforced Vest grants a set bonus.", commentScale);
			setting.AddBool(ReinforcedVestKey, "Reinforced Vest", false);
			setting.AddComment("Adds old style Forges (Furnace) and Hellforges.", commentScale);
			setting.AddBool(AncientForgesKey, "Ancient Forges*", false);
			setting.AddComment("Adds the Red Brick Fireplace and Chimney.", commentScale);
			setting.AddBool(RedBrickFurnitureKey, "Red Brick Furniture", false);
			setting.AddComment("A rare drop from pre-Hardmode Dungeon monsters. Gives off light just like the pre-1.2 Muramasa, but the stats are the same as the current version.", commentScale);
			setting.AddBool(AncientMuramasaKey, "Ancient Muramasa", false);
			setting.AddComment("Mech-tier summoner weapon. Summons a Gaster Blaster sentry minion.", commentScale);
			setting.AddBool(GasterBlasterKey, "Adds the Karma Staff", false);
			setting.AddComment("Rapidly throws piercing magical spears which also spawn mini-spears upon hitting an enemy.", commentScale);
			setting.AddBool(SpearofJusticeKey, "Spear of Justice", false);
			setting.AddComment("Endless Wormhole Potion", commentScale);
			setting.AddBool(WormholeMirrorKey, "Wormhole Mirror", false);
			setting.AddComment("Upgraded Cell Phone with Wormhole functionality.", commentScale);
			setting.AddBool(CellPhoneUpgradeKey, "Cell Phone Upgrade", false);
			setting.AddComment("Resprite the vanilla Cell Phone to match the Magic Mirror palette.", commentScale);
			setting.AddBool(CellPhoneRespriteKey, "Cell Phone Resprite", true);
			setting.AddComment("Post-Lunar upgrade for the Rod of Discord", commentScale);
			setting.AddBool(RodofWarpingKey, "Rod of Warping", false);
			setting.AddComment("Duration of Chaos State when using Rod of Warping (in seconds). Can be set to 0.", commentScale);
			setting.AddFloat(RodofWarpingChaosStateKey, "Rod of Warping Chaos State", 0f, 6f, false);
			setting.AddComment("Consumable item that teleports you to your last death position at a cost", commentScale);
			setting.AddBool(EmblemofDeathKey, "Emblem of Death", false);
			setting.AddComment("Sandstone Slab Wall", commentScale);
			setting.AddBool(BuildingMaterialsKey, "Building Materials", false);
			setting.AddComment("Baseball Bats can spawn in caves", commentScale);
			setting.AddBool(BaseballBatsKey, "Baseball Bats", false);
			setting.AddComment("Pre-1.2 style Shadow Orb for Hardmode", commentScale);
			setting.AddBool(AncientOrbKey, "Ancient Orb of light", false);
			setting.AddComment("Extra dyes (reflective Hardmode ore dyes and others)", commentScale);
			setting.AddBool(ExtraDyesKey, "Extra Dyes", false);
			setting.AddComment("Automatic fisher crafted using Mechanic's Rod", commentScale);
			setting.AddBool(ExtraDyesKey, "Autofisher", false);
			setting.AddComment("Mechanics sells her fishing rod more often", commentScale);
			setting.AddBool(MechanicsRodOftenKey, "Mechanic's Rod More Often", false);
			setting.AddComment("Sucks up items into chest below", commentScale);
			setting.AddBool(ChestVacuumKey, "Chest Vacuum", false);
		}
		
		public static void UpdateFKConfig()
		{
			ModSetting setting;
			if(ModSettingsAPI.TryGetModSetting(GoldensMisc.Instance, out setting))
			{
				setting.Get(AltStaffsKey, ref AltStaffs);
				setting.Get(MagicStonesKey, ref MagicStones);
				setting.Get(GodStoneKey, ref GodStone);
				setting.Get(DemonCrownKey, ref DemonCrown);
				setting.Get(HeartLocketKey, ref HeartLocket);
				setting.Get(MagnetsKey, ref Magnets);
				setting.Get(NinjaGearKey, ref NinjaGear);
				setting.Get(ReinforcedVestKey, ref ReinforcedVest);
				setting.Get(AncientForgesKey, ref AncientForges);				
				setting.Get(RedBrickFurnitureKey, ref RedBrickFurniture);
				setting.Get(AncientMuramasaKey, ref AncientMuramasa);
				setting.Get(GasterBlasterKey, ref GasterBlaster);
				setting.Get(SpearofJusticeKey, ref SpearofJustice);
				setting.Get(WormholeMirrorKey, ref WormholeMirror);
				setting.Get(CellPhoneUpgradeKey, ref CellPhoneUpgrade);
				setting.Get(CellPhoneRespriteKey, ref CellPhoneResprite);
				setting.Get(RodofWarpingKey, ref RodofWarping);
				setting.Get(RodofWarpingChaosStateKey, ref RodofWarpingChaosState);
				setting.Get(EmblemofDeathKey, ref EmblemofDeath);
				setting.Get(BuildingMaterialsKey, ref BuildingMaterials);
				setting.Get(BaseballBatsKey, ref BaseballBats);
				setting.Get(AncientOrbKey, ref AncientOrb);
				setting.Get(ExtraDyesKey, ref ExtraDyes);
				setting.Get(AutofisherKey, ref Autofisher);
				setting.Get(MechanicsRodOftenKey, ref MechanicsRodOften);
				setting.Get(ChestVacuumKey, ref ChestVacuum);
			}
		}
		
//		class MultiplayerSyncWorld : ModWorld
//		{
//			public override void NetSend(BinaryWriter writer)
//			{
//				var data = new BitsByte();
//				data[0] = AltStaffs;
//				data[1] = MagicStones;
//				data[2] = GodStone;
//				data[3] = DemonCrown;
//				data[4] = HeartLocket;
//				data[5] = Magnets;
//				data[6] = NinjaGear;
//				data[7] = ReinforcedVest;
//				writer.Write((byte)data);
//				data.ClearAll();
//				data[0] = AncientForges;
//				data[1] = RedBrickFurniture;
//				data[2] = AncientMuramasa;
//				data[3] = GasterBlaster;
//				data[4] = SpearofJustice;
//				data[5] = WormholeMirror;
//				data[6] = CellPhoneUpgrade;
//				data[7] = RodofWarping;
//				writer.Write((byte)data);
//				data.ClearAll();
//				data[0] = EmblemofDeath;
//				data[1] = BuildingMaterials;
//				data[2] = BaseballBats;
//				data[3] = AncientOrb;
//			}
//			
//			public override void NetReceive(BinaryReader reader)
//			{
//				SaveConfig();
//				var data = (BitsByte)reader.ReadByte();
//				AltStaffs = data[0];
//				MagicStones = data[1];
//				GodStone = data[2];
//				DemonCrown = data[3];
//				HeartLocket = data[4];
//				Magnets = data[5];
//				NinjaGear = data[6];
//				ReinforcedVest = data[7];
//				data = (BitsByte)reader.ReadByte();
//				AncientForges = data[0];
//				RedBrickFurniture = data[1];
//				AncientMuramasa = data[2];
//				GasterBlaster = data[3];
//				SpearofJustice = data[4];
//				WormholeMirror = data[5];
//				CellPhoneUpgrade = data[6];
//				RodofWarping = data[7];
//				data = (BitsByte)reader.ReadByte();
//				EmblemofDeath = data[0];
//				BuildingMaterials = data[1];
//				BaseballBats = data[2];
//				AncientOrb = data[3];
//			}
//		}
//		
//		class MultiplayerSyncPlayer : ModPlayer
//		{	
//			public override void PlayerDisconnect(Player player)
//			{
//				ReadConfig();
//			}
//		}
	}
}


using log4net;
using log4net.Core;
using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace GoldensMisc
{
    //This used to be the old way of handling configs, before tModLoader added the "official" ModConfig.

	public static class LegacyConfig
	{
		static bool AltStaffs = true;
		const string AltStaffsKey = "AltStaffs";
		
		static bool MagicStones = true;
		const string MagicStonesKey = "MagicStones";
		
		static bool GodStone = true;
		const string GodStoneKey = "GodStone";
		
		static bool DemonCrown = true;
		const string DemonCrownKey = "DemonCrown";
		
		static bool HeartLocket = true;
		const string HeartLocketKey = "HeartLocket";
		
		static bool Magnets = true;
		const string MagnetsKey = "Magnet";
		
		static bool NinjaGear = true;
		const string NinjaGearKey = "NinjaGear";
		
		static bool ReinforcedVest = true;
		const string ReinforcedVestKey = "ReinforcedVest";
		
		static bool AncientForges = true;
		const string AncientForgesKey = "AncientForges";
		
		static bool RedBrickFurniture = true;
		const string RedBrickFurnitureKey = "RedBrickFurniture";
		
		static bool AncientMuramasa = true;
		const string AncientMuramasaKey = "AncientMuramasa";
		
		static bool GasterBlaster = true;
		const string GasterBlasterKey = "GasterBlaster";
		
		static bool SpearofJustice = true;
		const string SpearofJusticeKey = "SpearofJustice";
		
		static bool WormholeMirror = true;
		const string WormholeMirrorKey = "WormholeMirror";
		
		static bool CellPhoneUpgrade = true;
		const string CellPhoneUpgradeKey = "WormholePhone";
		
		static bool RodofWarping = true;
		const string RodofWarpingKey = "RodofWarping";

		static float RodofWarpingChaosState = 1f;
		const string RodofWarpingChaosStateKey = "RodofWarpingChaosState";
		
		static bool EmblemofDeath = true;
		const string EmblemofDeathKey = "EmblemofDeath";
		
		static bool BuildingMaterials = true;
		const string BuildingMaterialsKey = "BuildingMaterials";
		
		static bool BaseballBats = true;
		const string BaseballBatsKey = "BaseballBats";
		
		static bool AncientOrb = true;
		const string AncientOrbKey = "AncientOrb";	

		static bool CellPhoneResprite = true;
		const string CellPhoneRespriteKey = "CellPhoneResprite";

		static bool ExtraDyes = true;
		const string ExtraDyesKey = "ExtraDyes";

		static bool Autofisher = true;
		const string AutofisherKey = "Autofisher";

		static bool MechanicsRodOften = true;
		const string MechanicsRodOftenKey = "MechanicsRodOften";

		static bool ChestVacuum = true;
		const string ChestVacuumKey = "ChestVacuum";
		
		static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania.json");
		
		static string OldConfigFolderPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania");
		static string OldConfigPath = Path.Combine(OldConfigFolderPath, "config.json");
		static string OldConfigVersionPath = Path.Combine(OldConfigFolderPath, "config.version");
		
		static readonly Preferences Configuration = new Preferences(ConfigPath);

        //We might not be able to use the mod's Logger as Config.Load() is called before Mod.Load()
        //So we make our own
        static ILog Logger = LogManager.GetLogger("MiscellaniaConfig");

        public static void Load()
		{
			if(Directory.Exists(OldConfigFolderPath))
			{
				if(File.Exists(OldConfigPath))
				{
					if(!File.Exists(ConfigPath))
					{
						Logger.Warn("Found config file in old folder! Moving config...");
						File.Move(OldConfigPath, ConfigPath);
					}
					else
					{
						Logger.Warn("Found config file in old folder! Deleting...");
						File.Delete(OldConfigPath);
					}
				}
				if(File.Exists(OldConfigVersionPath))
					File.Delete(OldConfigVersionPath);
				if(Directory.GetFiles(OldConfigFolderPath).Length == 0 && Directory.GetDirectories(OldConfigFolderPath).Length == 0)
					Directory.Delete(OldConfigFolderPath);
				else
                    Logger.Warn("Old config folder still cotains some files/directories. They will not get deleted.");
			}

            if (!File.Exists(ConfigPath))
                return;

            SetDefaults();
			if(!ReadConfig())
			{
                Logger.Error("Failed to read legacy config file!");
			}
            MoveToNewFormat();
			//SaveConfig();
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
				Configuration.Get(AncientOrbKey, ref AncientOrb);
				Configuration.Get(MechanicsRodOftenKey, ref MechanicsRodOften);
				Configuration.Get(ChestVacuumKey, ref ChestVacuum);
				return true;
			}
			return false;
		}

        private static void MoveToNewFormat()
        {
            Logger.Warn("Migrating to new config format...");

            string newConfigPathClient = Path.Combine(ConfigManager.ModConfigPath,
                    nameof(GoldensMisc) + '_' + ClientConfig.ConfigName + ".json");
            var newConfigClient = new Preferences(newConfigPathClient);

            newConfigClient.Put(CellPhoneRespriteKey, CellPhoneResprite);
            newConfigClient.Save();

            string newConfigPathServer = Path.Combine(ConfigManager.ModConfigPath,
                    nameof(GoldensMisc) + '_' + ServerConfig.ConfigName + ".json");

	    if(!File.Exists(newConfigPathServer))
	            File.Move(ConfigPath, newConfigPathServer);
	    else
		    File.Delete(ConfigPath);
        }
    }
}

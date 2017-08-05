
using System;
using System.IO;
using Terraria;
using Terraria.IO;

namespace GoldensMisc
{
	public static class Config
	{
		public static bool AltStaffs = true;
		public static bool MagicStones = true;
		public static bool GodStone = true;
		public static bool DemonCrown = true;
		public static bool HeartLocket = true;
		public static bool Magnets = true;
		public static bool NinjaGear = true;
		public static bool ReinforcedVest = true;
		public static bool AncientForges = true;
		public static bool RedBrickFurniture = true;
		public static bool AncientMuramasa = true;
		public static bool GasterBlaster = true;
		public static bool SpearofJustice = true;
		public static bool WormholeMirror = true;
		public static bool BuildingMaterials = true;
		public static bool BaseballBats = true;
		
		static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania.json");
		
		static readonly Preferences Configuration = new Preferences(ConfigPath);
		
		public static void Load()
		{
			if(!ReadConfig())
			{
				GoldensMisc.Log("Failed to read config file! Recreating config...");
				SaveConfig();
			}
		}
		
		static bool ReadConfig()
		{
			if(Configuration.Load())
			{
				Configuration.Get("AltStaffs", ref AltStaffs);
				Configuration.Get("MagicStones", ref MagicStones);
				Configuration.Get("GodStone", ref GodStone);
				Configuration.Get("DemonCrown", ref DemonCrown);
				Configuration.Get("HeartLocket", ref HeartLocket);
				Configuration.Get("Magnets", ref Magnets);
				Configuration.Get("NinjaGear", ref NinjaGear);
				Configuration.Get("ReinforcedVest", ref ReinforcedVest);
				Configuration.Get("AncientForges", ref AncientForges);
				Configuration.Get("RedBrickFurniture", ref RedBrickFurniture);
				Configuration.Get("AncientMuramasa", ref AncientMuramasa);
				Configuration.Get("GasterBlaster", ref GasterBlaster);
				Configuration.Get("SpearofJustice", ref SpearofJustice);
				Configuration.Get("WormholeMirror", ref WormholeMirror);
				Configuration.Get("BuildingMaterials", ref BuildingMaterials);
				Configuration.Get("BaseballBats", ref BaseballBats);
				return true;
			}
			return false;
		}
		
		static void SaveConfig()
		{
			Configuration.Clear();
			Configuration.Put("AltStaffs", AltStaffs);
			Configuration.Put("MagicStones", MagicStones);
			Configuration.Put("GodStone", GodStone);
			Configuration.Put("DemonCrown", DemonCrown);
			Configuration.Put("HeartLocket", HeartLocket);
			Configuration.Put("Magnets", Magnets);
			Configuration.Put("NinjaGear", NinjaGear);
			Configuration.Put("ReinforcedVest", ReinforcedVest);
			Configuration.Put("AncientForges", AncientForges);
			Configuration.Put("RedBrickFurniture", RedBrickFurniture);
			Configuration.Put("AncientMuramasa", AncientMuramasa);
			Configuration.Put("GasterBlaster", GasterBlaster);
			Configuration.Put("SpearofJustice", SpearofJustice);
			Configuration.Put("WormholeMirror", WormholeMirror);
			Configuration.Put("BuildingMaterials", BuildingMaterials);
			Configuration.Put("BaseballBats", BaseballBats);
			Configuration.Save();
		}
	}
}

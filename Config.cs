
using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public static class Config
	{
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
		
		public static bool EmblemofDeath = true;
		const string EmblemofDeathKey = "EmblemofDeath";
		
		public static bool BuildingMaterials = true;
		const string BuildingMaterialsKey = "BuildingMaterials";
		
		public static bool BaseballBats = true;
		const string BaseballBatsKey = "BaseballBats";
		
		public static bool AncientOrb = true;
		const string AncientOrbKey = "BaseballBats";		
		
		static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania.json");
		
		static string OldConfigFolderPath = Path.Combine(Main.SavePath, "Mod Configs", "Miscellania");
		static string OldConfigPath = Path.Combine(OldConfigFolderPath, "config.json");
		static string OldConfigVersionPath = Path.Combine(OldConfigFolderPath, "config.version");
		
		static readonly Preferences setting = new Preferences(ConfigPath);
		
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
			if(!ReadConfig())
				GoldensMisc.Log("Failed to read config file! Recreating config...");
			SaveConfig();
		}
		
		public static bool ReadConfig()
		{
			if(setting.Load())
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
				setting.Get(RodofWarpingKey, ref RodofWarping);
				setting.Get(EmblemofDeathKey, ref EmblemofDeath);
				setting.Get(BuildingMaterialsKey, ref BuildingMaterials);
				setting.Get(BaseballBatsKey, ref BaseballBats);
				return true;
			}
			return false;
		}
		
		public static void SaveConfig()
		{
			setting.Clear();
			setting.Put(AltStaffsKey, AltStaffs);
			setting.Put(MagicStonesKey, MagicStones);
			setting.Put(GodStoneKey, GodStone);
			setting.Put(DemonCrownKey, DemonCrown);
			setting.Put(HeartLocketKey, HeartLocket);
			setting.Put(MagnetsKey, Magnets);
			setting.Put(NinjaGearKey, NinjaGear);
			setting.Put(ReinforcedVestKey, ReinforcedVest);
			setting.Put(AncientForgesKey, AncientForges);				
			setting.Put(RedBrickFurnitureKey, RedBrickFurniture);
			setting.Put(AncientMuramasaKey, AncientMuramasa);
			setting.Put(GasterBlasterKey, GasterBlaster);
			setting.Put(SpearofJusticeKey, SpearofJustice);
			setting.Put(WormholeMirrorKey, WormholeMirror);
			setting.Put(CellPhoneUpgradeKey, CellPhoneUpgrade);
			setting.Put(RodofWarpingKey, RodofWarping);
			setting.Put(EmblemofDeathKey, EmblemofDeath);
			setting.Put(BuildingMaterialsKey, BuildingMaterials);
			setting.Put(BaseballBatsKey, BaseballBats);
			setting.Put(AncientOrbKey, AncientOrb);
			setting.Save();
		}
		
		//TODO: Transmations
		public static void LoadFKConfig(Mod mod)
		{
			var setting = FKTModSettings.ModSettingsAPI.CreateModSettingConfig(mod);

			setting.AddComment("Features marked with an asterisk (*) require an item reset to update properly.\n" +
			                   "An item can be reset by either re-entering the world or by placing it into an Item Frame, Weapon Rack or Mannequin.");
			setting.AddComment("Features marked with two asterisks (**) require a mod reload to update properly.");
			setting.AddComment("Features marked with three asterisks (***) require a new world or /miscworldgen command to work properly.");
			setting.AddComment("Features marked with Ancient in the name denote different versions of items with old art style(like prexisting items in terraria) (***)");
			setting.AddComment("Most values are only modifiable in singleplayer.");
			
			const float commentScale = 0.8f;
			
			setting.AddComment("Stat changes and new early game staffs", commentScale);
			setting.AddBool(AltStaffsKey, "Alternate Staffs", false);
			setting.AddComment("Magic stones that replinish health and mana for a number of uses.", commentScale);
			setting.AddBool(MagicStonesKey, "Adds Magic Stones**", false);
			setting.AddComment("the God stone is unobtainable normally(use cheat sheet or another mod.)", commentScale);
			setting.AddBool(GodStoneKey, "Adds the God Stone", false);
			setting.AddComment("A rare drop from Demons in Hardmode. Boosts your magical abilities and summons a Red Crystal to protect you.", commentScale);
			setting.AddBool(DemonCrownKey, "Adds the Demon Crown", false);
			setting.AddComment("Heart Locket = Panic Necklace + Cross Necklace.", commentScale);
			setting.AddBool(HeartLocketKey, "Adds the Heart Locket**", false);
			setting.AddComment("sold by Goblin Tinkerer in Hardmode at night. Increases item pickup range by 20 tiles.\n" + 
							   "Magnetism Ring = Universal Magnet + Celestial Magnet + Gold Ring. \n" + 
							   "Increases item, mana and coin pickup range.", commentScale);
			setting.AddBool(MagnetsKey, "Adds Magnetic stuff**", false);
			setting.AddComment("Ninja Gear = Tabi + Black Belt. Based on my old suggestion.\n" +
			                   "If you want to, you can add Tiger Climbing Gear to this accessory to get Master Ninja Gear.", commentScale);
			setting.AddBool(NinjaGearKey, "Adds Ninja Gear**", true);
			setting.AddComment("sold by Demolitionist in Hardmode. Armor item that protects you from your own explosives (bombs, rockets etc, just go nuts!)\n" + 
							   "Reinforced Horseshoe = Reinforced Vest + Obsidian Horseshoe.\n" + 
							   "Because a Reinforced Shield would be too cheaty.", commentScale);
			setting.AddBool(ReinforcedVestKey, "If Vanilla Tweaks is installed, equipping a SWAT Helmet and a Reinforced Vest grants a set bonus.", true);
			setting.AddComment("adds old style forges to use and find.", commentScale);
			setting.AddBool(AncientForgesKey, "Adds Ancient Forges***", false);
			setting.AddComment("adds the Red Brick Fireplace and Chimney.", commentScale);
			setting.AddBool(RedBrickFurnitureKey, "Adds more Red Brick stuff**", true);
			setting.AddComment("A rare drop from pre-Hardmode Dungeon monsters. Gives off light just like the pre-1.2 Muramasa, but the stats are the same as the current version. Based on my old suggestion.", commentScale);
			setting.AddBool(AncientMuramasaKey, "Adds Ancient Muramasa", false);
			setting.AddComment("mech-tier summoner weapon. Summons a Gaster Blaster sentry minion.", commentScale);
			setting.AddBool(GasterBlasterKey, "Adds the Karma Staff", true);
			setting.AddComment("mech-tier magic weapon. Rapidly throws piercing magical spears which also spawn mini-spears upon hitting an enemy.\n" + 
							   "Spear of Undying Justice = Spear of Justice + Spectre Bars. Direct upgrade.", commentScale);
			setting.AddBool(SpearofJusticeKey, "Adds the Spear of Justice", true);
			setting.AddComment("Wormhole Crystal is a drop from mages in the Hardmode Dungeon. Crafting material", commentScale);
			setting.AddBool(WormholeMirrorKey, "Adds a Wormhole mirror made of Wormhole Crystal", false);
			setting.AddComment("Upgraded Cell Phone with Wormhole functionality. " + 
							   "Has two recipes: " +
							   "Normal Cell Phone + Wormhole Mirror\n" +
							   "PDA + Wormhole Recall/Ice Mirror\n" +
							   "Note that its screen is one pixel taller than the old Cell Phone and it has a tiny front-facing camera. It lets you take magical selfies that teleport you to your friends", commentScale);
			setting.AddBool(CellPhoneUpgradeKey, "Makes the Cell Phone Upgradable with Wormhole power", false);
			setting.AddComment("a post-Lunar upgrade for the Rod of Discord, requires Luminite and Wormhole Crystals.\n" +
							   "Chaos State lasts for just 1 second and takes 1/10th of your HP instead of 1/7th.", commentScale);
			setting.AddBool(RodofWarpingKey, "Adds the Rod of Warping", true);
			setting.AddComment("a consumable item that teleports you to your last death position at a cost", commentScale);
			setting.AddBool(EmblemofDeathKey, "Adds the Emblem of Death", false);
			setting.AddBool(BuildingMaterialsKey, "Adds extra building materials", true);
			setting.AddComment("adds a chance for bats to be wearing a baseball cap which has a chance\n" +
							   " for dropping baseball themed gear.", commentScale);
			setting.AddBool(BaseballBatsKey, "Adds Baseball themed gear", true);
			setting.AddComment(" a pre-nerf Orb of shadow(then light) to Hardmode", commentScale);
			setting.AddBool(AncientOrbKey, "Adds the Ancient Orb of light", true);
		}
		
		public static void UpdateFKConfig(Mod mod)
		{
			FKTModSettings.ModSetting setting;
			if(FKTModSettings.ModSettingsAPI.TryGetModSetting(mod, out setting))
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
				setting.Get(RodofWarpingKey, ref RodofWarping);
				setting.Get(EmblemofDeathKey, ref EmblemofDeath);
				setting.Get(BuildingMaterialsKey, ref BuildingMaterials);
				setting.Get(BaseballBatsKey, ref BaseballBats);
				setting.Get(AncientOrbKey, ref AncientOrb);
			}
		}
		
		class MultiplayerSyncWorld : ModWorld
		{
			public override void NetSend(BinaryWriter writer)
			{
				var data = new BitsByte();
				data[0] = AltStaffs;
				data[1] = MagicStones;
				data[2] = GodStone;
				data[3] = DemonCrown;
				data[4] = HeartLocket;
				data[5] = Magnets;
				data[6] = NinjaGear;
				data[7] = ReinforcedVest;
				writer.Write((byte)data);
				data.ClearAll();
				data[0] = AncientForges;
				data[1] = RedBrickFurniture;
				data[2] = AncientMuramasa;
				data[3] = GasterBlaster;
				data[4] = SpearofJustice;
				data[5] = WormholeMirror;
				data[6] = CellPhoneUpgrade;
				data[7] = RodofWarping;
				writer.Write((byte)data);
				data.ClearAll();
				data[0] = EmblemofDeath;
				data[1] = BuildingMaterials;
				data[2] = BaseballBats;
				data[3] = AncientOrb;
			}
			
			public override void NetReceive(BinaryReader reader)
			{
				SaveConfig();
				var data = (BitsByte)reader.ReadByte();
				AltStaffs = data[0];
				MagicStones = data[1];
				GodStone = data[2];
				DemonCrown = data[3];
				HeartLocket = data[4];
				Magnets = data[5];
				NinjaGear = data[6];
				ReinforcedVest = data[7];
				data = (BitsByte)reader.ReadByte();
				AncientForges = data[0];
				RedBrickFurniture = data[1];
				AncientMuramasa = data[2];
				GasterBlaster = data[3];
				SpearofJustice = data[4];
				WormholeMirror = data[5];
				CellPhoneUpgrade = data[6];
				RodofWarping = data[7];
				data = (BitsByte)reader.ReadByte();
				EmblemofDeath = data[0];
				BuildingMaterials = data[1];
				BaseballBats = data[2];
				AncientOrb = data[3];
			}
		}
		
		class MultiplayerSyncPlayer : ModPlayer
		{
			public override void PlayerDisconnect(Player player)
			{
				ReadConfig();
			}
		}
		
	}
}

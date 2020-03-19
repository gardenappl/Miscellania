using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace GoldensMisc
{
	[Label("$Mods.GoldensMisc.Config.Server")]
	class ServerConfig : ModConfig
	{
		[JsonIgnore]
		public const string ConfigName = "Miscellania";

		public override bool Autoload(ref string name)
		{
			name = ConfigName;
			return base.Autoload(ref name);
		}

		public override ConfigScope Mode => ConfigScope.ServerSide;

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			message = Language.GetTextValue("Mods.GoldensMisc.Config.ServerBlocked");
			return false;
		}

		[Label("$Mods.GoldensMisc.Config.Autofisher")]
		[Tooltip("$Mods.GoldensMisc.Config.Autofisher.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool Autofisher;

		[Label("$Mods.GoldensMisc.Config.MechanicsRodOften")]
		[Tooltip("$Mods.GoldensMisc.Config.MechanicsRodOften.Desc")]
		[DefaultValue(true)]
		public bool MechanicsRodOften;

		[Label("$Mods.GoldensMisc.Config.ChestVacuum")]
		[Tooltip("$Mods.GoldensMisc.Config.ChestVacuum.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ChestVacuum;

		[Label("$Mods.GoldensMisc.Config.AltStaffs")]
		[Tooltip("$Mods.GoldensMisc.Config.AltStaffs.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool AltStaffs;

		[Label("$Mods.GoldensMisc.Config.MagicStones")]
		[Tooltip("$Mods.GoldensMisc.Config.MagicStones.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool MagicStones;

		[Label("$Mods.GoldensMisc.Config.GodStone")]
		[Tooltip("$Mods.GoldensMisc.Config.GodStone.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool GodStone;

		[Label("$Mods.GoldensMisc.Config.DemonCrown")]
		[Tooltip("$Mods.GoldensMisc.Config.DemonCrown.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool DemonCrown;

		[Label("$Mods.GoldensMisc.Config.HeartLocket")]
		[Tooltip("$Mods.GoldensMisc.Config.HeartLocket.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool HeartLocket;

		[Label("$Mods.GoldensMisc.Config.Magnet")]
		[Tooltip("$Mods.GoldensMisc.Config.Magnet.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool Magnet;

		[Label("$Mods.GoldensMisc.Config.NinjaGear")]
		[Tooltip("$Mods.GoldensMisc.Config.NinjaGear.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool NinjaGear;

		[Label("$Mods.GoldensMisc.Config.ReinforcedVest")]
		[Tooltip("$Mods.GoldensMisc.Config.ReinforcedVest.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ReinforcedVest;

		[Label("$Mods.GoldensMisc.Config.AncientForges")]
		[Tooltip("$Mods.GoldensMisc.Config.AncientForges.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientForges;

		[Label("$Mods.GoldensMisc.Config.RedBrickFurniture")]
		[Tooltip("$Mods.GoldensMisc.Config.RedBrickFurniture.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool RedBrickFurniture;

		[Label("$Mods.GoldensMisc.Config.AncientMuramasa")]
		[Tooltip("$Mods.GoldensMisc.Config.AncientMuramasa.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientMuramasa;

		[Label("$Mods.GoldensMisc.Config.GasterBlaster")]
		[Tooltip("$Mods.GoldensMisc.Config.GasterBlaster.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool GasterBlaster;

		[Label("$Mods.GoldensMisc.Config.SpearofJustice")]
		[Tooltip("$Mods.GoldensMisc.Config.SpearofJustice.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool SpearofJustice;

		[Label("$Mods.GoldensMisc.Config.WormholeMirror")]
		[Tooltip("$Mods.GoldensMisc.Config.WormholeMirror.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool WormholeMirror;

		[Label("$Mods.GoldensMisc.Config.WormholePhone")]
		[Tooltip("$Mods.GoldensMisc.Config.WormholePhone.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool WormholePhone;

		[Label("$Mods.GoldensMisc.Config.RodofWarping")]
		[Tooltip("$Mods.GoldensMisc.Config.RodofWarping.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool RodofWarping;

		[Label("$Mods.GoldensMisc.Config.RodofWarpingChaosState")]
		[Tooltip("$Mods.GoldensMisc.Config.RodofWarpingChaosState.Desc")]
		[Range(0f, 10f)]
		[Increment(0.25f)]
		[DefaultValue(1f)]
		public float RodofWarpingChaosState;

		[Label("$Mods.GoldensMisc.Config.EmblemofDeath")]
		[Tooltip("$Mods.GoldensMisc.Config.EmblemofDeath.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool EmblemofDeath;

		[Label("$Mods.GoldensMisc.Config.BuildingMaterials")]
		[Tooltip("$Mods.GoldensMisc.Config.BuildingMaterials.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool BuildingMaterials;

		[Label("$Mods.GoldensMisc.Config.BaseballBats")]
		[Tooltip("$Mods.GoldensMisc.Config.BaseballBats.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool BaseballBats;

		[Label("$Mods.GoldensMisc.Config.AncientOrb")]
		[Tooltip("$Mods.GoldensMisc.Config.AncientOrb.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientOrb;

		[Label("$Mods.GoldensMisc.Config.ExtraDyes")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool ExtraDyes;
	}

	[Label("$Mods.GoldensMisc.Config.Client")]
	class ClientConfig : ModConfig
	{
		[JsonIgnore]
		public const string ConfigName = "MiscellaniaClient";

		public override bool Autoload(ref string name)
		{
			name = ConfigName;
			return base.Autoload(ref name);
		}

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Label("$Mods.GoldensMisc.Config.CellPhoneResprite")]
		[Tooltip("$Mods.GoldensMisc.Config.CellPhoneResprite.Desc")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool CellPhoneResprite;
	}
}

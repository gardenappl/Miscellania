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
	public class ServerConfig : ModConfig
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
			message = Language.GetTextValue("Mods.GoldensMisc.Configs.ServerBlocked");
			return false;
		}

		[ReloadRequired]
		[DefaultValue(true)]
		public bool Autofisher;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool ChestVacuum;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool AltStaffs;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool MagicStones;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool GodStone;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool DemonCrown;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool HeartLocket;

		[ReloadRequired]
		[DefaultValue(false)]
		public bool Magnet;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool MagnetismRing;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool NinjaGear;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool ReinforcedVest;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientForges;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool RedBrickFurniture;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientMuramasa;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool GasterBlaster;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool SpearofJustice;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool WormholeMirror;

		[ReloadRequired]
		[DefaultValue(false)]
		public bool WormholePhone;

		[ReloadRequired]
		[DefaultValue(false)]
		public bool RodofWarping;

		[Range(0f, 10f)]
		[Increment(0.25f)]
		[DefaultValue(1f)]
		public float RodofWarpingChaosState;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool EmblemofDeath;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool BuildingMaterials;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool BaseballBats;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool AncientOrb;

		[ReloadRequired]
		[DefaultValue(true)]
		public bool ExtraDyes;
	}

	public class ClientConfig : ModConfig
	{
		[JsonIgnore]
		public const string ConfigName = "MiscellaniaClient";

        public override bool Autoload(ref string name)
		{
			name = ConfigName;
			return base.Autoload(ref name);
		}

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[ReloadRequired]
		[DefaultValue(false)]
		public bool CellPhoneResprite;
	}
}

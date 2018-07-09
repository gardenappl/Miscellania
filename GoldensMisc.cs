

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.World.Generation;
using GoldensMisc.Items;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Placeable;
using GoldensMisc.Items.Tools;
using GoldensMisc.Items.Weapons;
using GoldensMisc.Projectiles;
using GoldensMisc.UI;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Dyes;
using GoldensMisc.Items.Equipable.Vanity;

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public static GoldensMisc Instance;
		public static bool FKtModSettingsLoaded;
		public static bool VanillaTweaksLoaded;
		public static Texture2D CellPhoneTexture;
		internal UIWormhole WormholeUI;
		private UserInterface MiscUserInterface;
		
		public GoldensMisc()
		{
			Config.Load();
		}
		
		public override void Load()
		{
			Instance = this; //apparently you get some problems with Mod Reloading if you put this in the constructor
			FKtModSettingsLoaded = ModLoader.GetMod("FKTModSettings") != null;
			VanillaTweaksLoaded = ModLoader.GetMod("VanillaTweaks") != null;
			
			if(!Main.dedServ)
			{
				MiscLang.AddText();

				if(FKtModSettingsLoaded)
					Config.LoadFKConfig();
				
				MiscGlowMasks.Load();
				if(Config.CellPhoneResprite)
				{
					CellPhoneTexture = Main.itemTexture[ItemID.CellPhone];
					Main.itemTexture[ItemID.CellPhone] = GetTexture("Items/Tools/CellPhone_Resprite");
				}
				//				SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();

				GameShaders.Armor.BindShader<ArmorShaderData>(ItemType<MatrixDye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0f, 1.0f, 0.2f);
				GameShaders.Armor.BindShader<ArmorShaderData>(ItemType<VirtualDye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(1f, 0.1f, 0.1f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<CobaltDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.7f, 1.2f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<PalladiumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.2f, 0.5f, 0.3f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<MythrilDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.3f, 0.8f, 0.8f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<OrichalcumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.1f, 0.3f, 1.1f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<AdamantiteDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.1f, 0.4f, 0.6f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<TitaniumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.5f, 0.7f, 0.7f);
				GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(ItemType<ChlorophyteDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.5f, 1.1f, 0.1f);

				WormholeUI = new UIWormhole();
				WormholeUI.Activate();
				MiscUserInterface = new UserInterface();
				MiscUserInterface.SetState(WormholeUI);
			}
			AddProjectile("MagicSpearMiniAlt", new MagicSpearMini());
		}
		
		public override void PostSetupContent()
		{
			var hotkeysMod = ModLoader.GetMod("HelpfulHotkeys");
			if(hotkeysMod != null)
			{
				hotkeysMod.Call("RegisterRecallItem", ItemType<WormholeDoubleMirror>());
				hotkeysMod.Call("RegisterRecallItem", ItemType<WormholeIceMirror>());
				hotkeysMod.Call("RegisterRecallItem", ItemType<WormholeCellPhone>());
			}
		}
		
		public override void Unload()
		{
			Instance = null;

			MiscGlowMasks.Unload();
			if(CellPhoneTexture != null)
				Main.itemTexture[ItemID.CellPhone] = CellPhoneTexture;
		}
		
		public override void AddRecipeGroups()
		{
			MiscRecipes.AddRecipeGroups();
		}
		
		public override void PostAddRecipes()
		{
			MiscRecipes.PostAddRecipes();
		}
		
		public override void PostUpdateInput()
		{
			if(FKtModSettingsLoaded && !Main.dedServ && !Main.gameMenu)
				Config.UpdateFKConfig();
		}
		
		public override void PreSaveAndQuit()
		{
			if(FKtModSettingsLoaded && !Main.dedServ)
				Config.SaveConfig();

			UIWormhole.Close();
		}

		public override void UpdateUI(GameTime gameTime)
		{
			if(MiscUserInterface != null && UIWormhole.Visible)
				MiscUserInterface.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex(x => x.Name == "Vanilla: Mouse Text");
			if(index != -1)
			{
				layers.Insert(index, new LegacyGameInterfaceLayer("GoldensMisc: UI", delegate
				{
					if(UIWormhole.Visible)
					{
						WormholeUI.Draw(Main.spriteBatch);
					}
					return true;
				}, InterfaceScaleType.UI));
			}
		}

		public static void Log(object message)
		{
			ErrorLogger.Log(String.Format("[Miscellania][{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message));
		}
		
		public static void Log(string message, params object[] formatData)
		{
			ErrorLogger.Log(String.Format("[Miscellania][{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), String.Format(message, formatData)));
		}


		#region Hamstar's Mod Helpers integration

		public static string GithubUserName { get { return "goldenapple3"; } }
		public static string GithubProjectName { get { return "Miscellania"; } }

		public static string ConfigFileRelativePath { get { return "Mod Configs/Miscellania.json"; } }

		public static void ReloadConfigFromFile()
		{
			Config.ReadConfig();
		}

		public static void ResetConfigFromDefaults()
		{
			Config.SetDefaults();
			Config.SaveConfig();
		}

		#endregion
	}
}

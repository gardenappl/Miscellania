

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GoldensMisc.Items.Equipable.Vanity;
using GoldensMisc.Items.Tools;
using GoldensMisc.Projectiles;
using GoldensMisc.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public static bool VanillaTweaksLoaded;
		public static Texture2D CellPhoneTexture;
		
		public GoldensMisc()
		{
			LegacyConfig.Load();
		}
		
		public override void Load()
		{
			AutofisherHooks.Initialize();
			VanillaTweaksLoaded = ModLoader.GetMod("VanillaTweaks") != null;

			if (ModContent.GetInstance<ServerConfig>().WormholeMirror)
				WormholeHacks.Load();

			if(!Main.dedServ)
			{
				MiscGlowMasks.Load();
				if(ModContent.GetInstance<ClientConfig>().CellPhoneResprite)
				{
					CellPhoneTexture = Main.itemTexture[ItemID.CellPhone];
					Main.itemTexture[ItemID.CellPhone] = GetTexture("Items/Tools/CellPhone_Resprite");
				}
				//				SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();

				if(ModContent.GetInstance<ServerConfig>().ExtraDyes)
				{
					GameShaders.Armor.BindShader(ModContent.ItemType<MatrixDye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0f, 1.0f, 0.2f);
					GameShaders.Armor.BindShader(ModContent.ItemType<VirtualDye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(1f, 0.1f, 0.1f);
					GameShaders.Armor.BindShader(ModContent.ItemType<CobaltDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.7f, 1.2f);
					GameShaders.Armor.BindShader(ModContent.ItemType<PalladiumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.2f, 0.5f, 0.3f);
					GameShaders.Armor.BindShader(ModContent.ItemType<MythrilDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.3f, 0.8f, 0.8f);
					GameShaders.Armor.BindShader(ModContent.ItemType<OrichalcumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.1f, 0.3f, 1.1f);
					GameShaders.Armor.BindShader(ModContent.ItemType<AdamantiteDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(1.1f, 0.4f, 0.6f);
					GameShaders.Armor.BindShader(ModContent.ItemType<TitaniumDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.5f, 0.7f, 0.7f);
					GameShaders.Armor.BindShader(ModContent.ItemType<ChlorophyteDye>(), new ReflectiveArmorShaderData(Main.PixelShaderRef, "ArmorReflectiveColor")).UseColor(0.5f, 1.1f, 0.1f);
				}
			}
			if(ModContent.GetInstance<ServerConfig>().SpearofJustice)
			{
				AddProjectile("MagicSpearMiniAlt", new MagicSpearMini());
			}
		}
		
		public override void PostSetupContent()
		{
			var hotkeysMod = ModLoader.GetMod("HelpfulHotkeys");
			if(hotkeysMod != null && ModContent.GetInstance<ServerConfig>().WormholeMirror)
			{
				hotkeysMod.Call("RegisterRecallItem", ModContent.ItemType<WormholeDoubleMirror>());
				hotkeysMod.Call("RegisterRecallItem", ModContent.ItemType<WormholeIceMirror>());
				hotkeysMod.Call("RegisterRecallItem", ModContent.ItemType<WormholeCellPhone>());
			}
			if(ModContent.GetInstance<ServerConfig>().ChestVacuum)
			{
				ModContent.GetInstance<Tiles.ChestVacuum>().SetDefaultsPostContent();
			}
			//Call("RegisterHookGetFishingLevel", typeof(AutofisherHooks), "TestHookGetFishingLevel");
			//Call("RegisterHookCatchFish", typeof(AutofisherHooks), "TestHookCatchFish");
		}

		public override void Unload()
		{
			MiscGlowMasks.Unload();
			if(CellPhoneTexture != null)
			{
				Main.itemTexture[ItemID.CellPhone] = CellPhoneTexture;
				CellPhoneTexture = null;
			}
			AutofisherHooks.Unload();
		}
		
		public override void AddRecipeGroups()
		{
			MiscRecipes.AddRecipeGroups();
		}
		
		public override void PostAddRecipes()
		{
			MiscRecipes.PostAddRecipes();
		}

		public override object Call(params object[] args)
		{
			//don't catch exceptions, just blatantly show an error
			//it's probably the other modder's fault

			if((string)args[0] == "RegisterHookGetFishingLevel")
			{
				var methodInfo = ((Type)args[1]).GetMethod((string)args[2], BindingFlags.Static | BindingFlags.Public);
				if(methodInfo == null)
					throw new Exception("Method " + (string)args[2] + " not found. Make sure it's a public static method.");
				AutofisherHooks.RegisterHook(AutofisherHooks.HookType.GetFishingLevel, methodInfo);
				Log("Added GetFishingLevel hook");
				return true;
			}
			else if((string)args[0] == "RegisterHookCatchFish")
			{
				var methodInfo = ((Type)args[1]).GetMethod((string)args[2], BindingFlags.Static | BindingFlags.Public);
				if(methodInfo == null)
					throw new Exception("Method " + (string)args[2] + " not found. Make sure it's a public static method.");
				AutofisherHooks.RegisterHook(AutofisherHooks.HookType.CatchFish, methodInfo);
				Log("Added CatchFish hook");
				return true;
			}
			throw new Exception("Invalid mod.Call() message. Could be caused by an outdated version of Miscellania, or by a typo in someone else's mod.");
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			try
			{
				var messageType = (MiscMessageType)reader.ReadByte();
				switch(messageType)
				{
					case MiscMessageType.PrintAutofisherInfo:
						int id = reader.ReadInt32();
						var te = (AutofisherTE)TileEntity.ByID[id];
						NetMessage.SendChatMessageToClient(NetworkText.FromLiteral(te.DisplayedFishingInfo), Color.White, whoAmI);
						break;
                    case MiscMessageType.AddItemByWayOfVacuum:
                        id = reader.ReadInt32();
                        var vacuumTE = (ChestVacuumTE)TileEntity.ByID[id];
                        vacuumTE.FindAndAddItemToChest();
                        break;
                    case MiscMessageType.UpdateVacuumSmartStack:
                        id = reader.ReadInt32();
                        vacuumTE = (ChestVacuumTE)TileEntity.ByID[id];
                        vacuumTE.smartStack = reader.ReadBoolean();
                        vacuumTE.smartStackChanged = true;
                        break;
                }

			}
			catch(Exception e)
			{
				Main.NewText("Network error!");
				Error("Network error", e);
			}
		}

		public static void Log(object message)
		{
			ModContent.GetInstance<GoldensMisc>().Logger.Info(message);
		}

		public static void Error(object message, Exception e)
		{
			ModContent.GetInstance<GoldensMisc>().Logger.Error(message, e);
		}
		
		public static void Log(string message, params object[] formatData)
		{
			ModContent.GetInstance<GoldensMisc>().Logger.Info(string.Format(message, formatData));
		}
	}

	public enum MiscMessageType : byte
	{
		PrintAutofisherInfo = 0,
        AddItemByWayOfVacuum = 1,
        UpdateVacuumSmartStack = 2
	}
}

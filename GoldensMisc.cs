

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public static GoldensMisc Instance;
		
		public GoldensMisc()
		{
			Config.Load();
		}
		
		public override void Load()
		{
			Instance = this;
			if(!Main.dedServ)
			{
				MiscGlowMasks.Load();
//				SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();
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
			MiscGlowMasks.Unload();
		}
		
		public override void AddRecipeGroups()
		{
			MiscRecipes.AddRecipeGroups(this);
		}
		
		public override void PostAddRecipes()
		{
			MiscRecipes.PostAddRecipes(this);
		}
		
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int index = layers.FindIndex(x => x.Name == "Vanilla: Map / Minimap");
			if(index != -1)
			{
				layers.Insert(index, new LegacyGameInterfaceLayer("GoldensMisc: Horrible Wormhole Hack 1", delegate
				{
					PreDrawMap();
					return true;
				}));
				layers.Insert(index + 2, new LegacyGameInterfaceLayer("GoldensMisc: Horrible Wormhole Hack 2", delegate
				{
					PostDrawMap();
					return true;
				}));
			}
		}
		
		int WormholeReplaceSlot = -1;
		Item WormholeReplaceItem;
		
		void PreDrawMap()
		{
			for(int i = 0; i < 58; i++)
			{
				var item = Main.LocalPlayer.inventory[i];
				if(item.type == ItemType<WormholeMirror>() ||
				   item.type == ItemType<WormholeDoubleMirror>() ||
				   item.type == ItemType<WormholeIceMirror>() ||
				   item.type == ItemType<WormholeCellPhone>())
				{
					WormholeReplaceSlot = i;
					WormholeReplaceItem = item;
					Main.LocalPlayer.inventory[i] = new Item();
					Main.LocalPlayer.inventory[i].SetDefaults(ItemID.WormholePotion);
					Main.LocalPlayer.inventory[i].stack = 30;
					break;
				}
			}
		}
		
		void PostDrawMap()
		{
			if(WormholeReplaceSlot >= 0)
			{
				Main.LocalPlayer.inventory[WormholeReplaceSlot] = WormholeReplaceItem;
				WormholeReplaceSlot = -1;
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
	}
}

using GoldensMisc.Items.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace GoldensMisc.UI
{
	class UIWormhole : UIState
	{
		private UIPanel uiPanel;
		private UIList playerList;
		private UITextPanel<string> homeButton;

		private static Item WormholeItem = null;
		public static bool Visible { get; private set; }
		private static bool HomeButtonExists = false;

		public static void Open(Item item, bool homeButton = true)
		{
			WormholeItem = item;
			Visible = true;
			HomeButtonExists = homeButton;
		}

		public static void Close()
		{
			WormholeItem = null;
			Visible = false;
		}

		public override void OnInitialize()
		{
			//const float width = 400f;
			//const float height = 600f;

			uiPanel = new UIPanel();
			uiPanel.Width.Set(300f, 0f);
			uiPanel.Height.Set(400f, 0f);
			uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			//uiPanel.SetPadding(0);
			uiPanel.HAlign = 0.5f;
			uiPanel.VAlign = 0.5f;

			playerList = new UIList();
			playerList.Width.Set(-10f, 1f);
			playerList.Height.Set(-50f, 1f);
			playerList.Top.Set(50f, 0f);
			playerList.ListPadding = 5f;
			uiPanel.Append(playerList);

			var uiScrollbar = new UIScrollbar();
			uiScrollbar.SetView(100f, 1000f);
			uiScrollbar.Height.Set(-50f, 1f);
			uiScrollbar.Top.Set(50f, 0f);
			uiScrollbar.HAlign = 1f;
			uiPanel.Append(uiScrollbar);
			playerList.SetScrollbar(uiScrollbar);

			homeButton = new UITextPanel<string>(Language.GetTextValue("Mods.GoldensMisc.HomeButton"));
			homeButton.Width.Set(0, 0.4f);
			homeButton.OnMouseOver += MiscUtils.UI.FadedMouseOver;
			homeButton.OnMouseOut += MiscUtils.UI.FadedMouseOut;
			homeButton.OnClick += HomeButtonClicked;
			uiPanel.Append(homeButton);

			//var teleportButton = new UITextPanel<string>(Language.GetTextValue("Mods.GoldensMisc.TeleportButton"));
			//teleportButton.Width.Set(0f, 0.7f);
			////teleportButton.Height.Set(20f, 0f);
			////teleportButton.SetPadding(0);
			//teleportButton.OnMouseOver += MiscUtils.UI.FadedMouseOver;
			//teleportButton.OnMouseOut += MiscUtils.UI.FadedMouseOut;
			//teleportButton.OnClick += TeleportButtonClick;
			//uiPanel.Append(teleportButton);

			//var buttonDeleteTexture = ModLoader.GetTexture("Terraria/UI/ButtonDelete");
			//var closeButton = new UIImageButton(buttonDeleteTexture);
			var closeButton = new UITextPanel<string>(Language.GetTextValue("Mods.GoldensMisc.CloseButton"));
			closeButton.Left.Set(0, 0.6f);
			closeButton.Width.Set(0, 0.4f);
			//closeButton.SetPadding(0);
			closeButton.OnMouseOver += MiscUtils.UI.FadedMouseOver;
			closeButton.OnMouseOut += MiscUtils.UI.FadedMouseOut;
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			uiPanel.Append(closeButton);

			base.Append(uiPanel);
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Close();
		}

		private void HomeButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Close();
			Main.PlaySound(SoundID.Item6);
			Main.LocalPlayer.Spawn();
			for(int index = 0; index < 70; ++index)
				Dust.NewDust(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, 15, 0.0f, 0.0f, 150, Color.White, 1.5f);
		}

		public override void Update(GameTime gameTime)
		{
			var foundItem = false;
			foreach(var item in Main.LocalPlayer.inventory)
			{
				if(item == WormholeItem)
					foundItem = true;
			}
			if(!foundItem)
			{
				Main.PlaySound(SoundID.MenuClose);
				Close();
				return;
			}

			if(!HomeButtonExists)
			{
				uiPanel.RemoveChild(homeButton);
			}
			else
			{
				uiPanel.Append(homeButton);
			}

			base.Update(gameTime);

			UpdateTeammateList();
		}

		private void UpdateTeammateList()
		{
			var players = Main.player.Where(p => (p != null && p.active && !p.dead && p != Main.LocalPlayer && p.team != 0 && p.team == Main.LocalPlayer.team));
			var listedPlayers = playerList._items.Select(e => ((UITeammateListItem)e).Player);
			var oldPlayers = listedPlayers.Except(players);
			
			foreach(var teammateElement in playerList._items.ToArray())
			{
				if(oldPlayers.Contains(((UITeammateListItem)teammateElement).Player))
				{
					playerList.Remove(teammateElement);
				}
			}
			var newPlayers = players.Except(listedPlayers);
			foreach(var player in newPlayers)
			{
				playerList.Add(new UITeammateListItem(player));
			}
		}
		
		class UITeammateListItem : UITextPanel<string>
		{
			public readonly Player Player;

			public UITeammateListItem(Player player) : base(player.name)
			{
				this.Player = player;

				OnMouseOver += MiscUtils.UI.FadedMouseOver;
				OnMouseOut += MiscUtils.UI.FadedMouseOut;
				OnClick += TeammateButtonClicked;
				MarginBottom /= 2f;
				MarginTop /= 2f;
			}

			private void TeammateButtonClicked(UIMouseEvent evt, UIElement listeningElement)
			{
				Main.LocalPlayer.UnityTeleport(((UITeammateListItem)listeningElement).Player.position);
				Close();
			}
		}
	}
}

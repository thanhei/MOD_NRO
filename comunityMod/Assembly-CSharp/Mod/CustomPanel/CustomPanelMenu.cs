using System;
using Mod.ModMenu;
using Mod.R;
using UnityEngine;

namespace Mod.CustomPanel
{
	// Token: 0x02000163 RID: 355
	internal class CustomPanelMenu
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x000B69C4 File Offset: 0x000B4BC4
		internal static void Show(CustomPanelMenuConfig config, Panel panel = null)
		{
			((panel == null) ? CustomPanelMenu.customPanel : CustomPanelMenu.GetCustomPanel(panel)).ConfigAndShow(config);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000B69DC File Offset: 0x000B4BDC
		internal static CustomPanelMenu GetCustomPanel(Panel panel)
		{
			if (panel == GameCanvas.panel)
			{
				CustomPanelMenu.customPanel.panel = panel;
				return CustomPanelMenu.customPanel;
			}
			CustomPanelMenu.customPanel2.panel = panel;
			return CustomPanelMenu.customPanel2;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000B6A07 File Offset: 0x000B4C07
		internal void ConfigAndShow(CustomPanelMenuConfig customPanelMenuConfig)
		{
			this.panel.type = CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU;
			this.config = customPanelMenuConfig;
			this.SetType();
			this.panel.show();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000B6A34 File Offset: 0x000B4C34
		internal void SetType()
		{
			SoundMn.gI().getSoundOption();
			if (this.config.SetTabAction.Method == new Action<Panel>(ModMenuMain.SetTabModMenu).Method)
			{
				this.panel.mainTabName = (this.panel.tabName[CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU] = Strings.modMenuPanelTabName);
				this.panel.currentTabName = this.panel.tabName[CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU];
				this.panel.currentTabIndex = Mathf.Clamp(this.panel.currentTabIndex, 0, Strings.modMenuPanelTabName.Length - 1);
			}
			this.panel.setType((this.panel == GameCanvas.panel) ? 0 : 1);
			CustomPanelMenu.SetTab(this.panel);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000B6B00 File Offset: 0x000B4D00
		internal static bool PaintTabHeader(Panel panel, mGraphics g)
		{
			CustomPanelMenu customPanelMenu = CustomPanelMenu.GetCustomPanel(panel);
			if (customPanelMenu.config.PaintTabHeaderAction == null)
			{
				return false;
			}
			customPanelMenu.config.PaintTabHeaderAction(panel, g);
			return true;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000B6B36 File Offset: 0x000B4D36
		internal static void SetTab(Panel panel)
		{
			CustomPanelMenu.GetCustomPanel(panel).config.SetTabAction(panel);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000B6B4E File Offset: 0x000B4D4E
		internal static void DoFire(Panel panel)
		{
			CustomPanelMenu.GetCustomPanel(panel).config.DoFireItemAction(panel);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000B6B66 File Offset: 0x000B4D66
		internal static void Paint(Panel panel, mGraphics g)
		{
			CustomPanelMenu.GetCustomPanel(panel).config.PaintAction(panel, g);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000B6B80 File Offset: 0x000B4D80
		internal static void PaintTopInfo(Panel panel, mGraphics g)
		{
			CustomPanelMenu customPanelMenu = CustomPanelMenu.GetCustomPanel(panel);
			if (customPanelMenu.config.PaintTopInfoAction != null)
			{
				customPanelMenu.config.PaintTopInfoAction(panel, g);
				return;
			}
			SmallImage.drawSmallImage(g, (int)Utils.ID_NPC_MOD_FACE, panel.X + 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, Strings.communityMod, panel.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, Strings.gameVersion + ": v" + GameMidlet.VERSION, panel.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, panel.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.account,
				" ",
				mResources.account_server.ToLower(),
				" ",
				ServerListScreen.nameServer[ServerListScreen.ipSelect]
			}), panel.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x040017EF RID: 6127
		internal static readonly int TYPE_CUSTOM_PANEL_MENU = 27;

		// Token: 0x040017F0 RID: 6128
		internal Panel panel;

		// Token: 0x040017F1 RID: 6129
		private CustomPanelMenuConfig config;

		// Token: 0x040017F2 RID: 6130
		internal static CustomPanelMenu customPanel = new CustomPanelMenu
		{
			panel = GameCanvas.panel
		};

		// Token: 0x040017F3 RID: 6131
		internal static CustomPanelMenu customPanel2 = new CustomPanelMenu
		{
			panel = GameCanvas.panel2
		};
	}
}

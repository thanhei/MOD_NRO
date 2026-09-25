using System;
using System.Collections;
using System.Collections.Generic;
using Mod.CustomPanel;

namespace Mod.Xmap
{
	// Token: 0x02000107 RID: 263
	internal static class XmapPanel
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x000A423C File Offset: 0x000A243C
		internal static void Show(List<int> maps)
		{
			XmapPanel.currentMaps = maps;
			CustomPanelMenu.Show(new CustomPanelMenuConfig
			{
				SetTabAction = new Action<Panel>(XmapPanel.SetTab),
				DoFireItemAction = new Action<Panel>(XmapPanel.DoFire),
				PaintTabHeaderAction = new Action<Panel, mGraphics>(XmapPanel.PaintTabHeader),
				PaintAction = new Action<Panel, mGraphics>(XmapPanel.Paint)
			}, null);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000A42A4 File Offset: 0x000A24A4
		private static void Paint(Panel panel, mGraphics g)
		{
			PaintPanelTemplates.PaintCollectionCaptionAndDescriptionTemplate<int>(panel, g, XmapPanel.currentMaps, (int mapID) => TileMap.mapNames[mapID], (int mapID) => string.Format("ID: {0}", mapID), true);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000A42FC File Offset: 0x000A24FC
		private static void PaintTabHeader(Panel panel, mGraphics g)
		{
			PaintPanelTemplates.PaintTabHeaderTemplate(panel, g, "Xmap by Phucprotein");
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000A430A File Offset: 0x000A250A
		private static void SetTab(Panel panel)
		{
			SetTabPanelTemplates.setTabListTemplate(panel, new ICollection[] { XmapPanel.currentMaps });
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000A4320 File Offset: 0x000A2520
		private static void DoFire(Panel panel)
		{
			InfoDlg.hide();
			panel.hide();
			XmapController.start(XmapPanel.currentMaps[panel.selected]);
		}

		// Token: 0x04001519 RID: 5401
		private static List<int> currentMaps = new List<int>();
	}
}

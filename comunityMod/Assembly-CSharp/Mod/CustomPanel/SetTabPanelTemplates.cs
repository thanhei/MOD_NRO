using System;
using System.Collections;
using System.Linq;

namespace Mod.CustomPanel
{
	// Token: 0x02000166 RID: 358
	internal static class SetTabPanelTemplates
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x000B6ED4 File Offset: 0x000B50D4
		internal static void setTabListTemplate(Panel panel, params int[] lengths)
		{
			panel.ITEM_HEIGHT = 24;
			if (lengths.Length > 1)
			{
				panel.currentListLength = lengths[panel.currentTabIndex];
			}
			else
			{
				panel.currentListLength = lengths[0];
			}
			panel.selected = (GameCanvas.isTouch ? (-1) : 0);
			panel.cmyLim = panel.currentListLength * panel.ITEM_HEIGHT - panel.hScroll;
			if (panel.cmyLim < 0)
			{
				panel.cmyLim = 0;
			}
			panel.cmy = (panel.cmtoY = panel.cmyLast[panel.currentTabIndex]);
			if (panel.cmy < 0)
			{
				panel.cmy = (panel.cmtoY = 0);
			}
			if (panel.cmy > panel.cmyLim)
			{
				panel.cmy = (panel.cmtoY = panel.cmyLim);
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000B6F9C File Offset: 0x000B519C
		internal static void setTabListTemplate(Panel panel, params ICollection[] collections)
		{
			int[] array = collections.Select<ICollection, int>((ICollection x) => x.Count).ToArray<int>();
			SetTabPanelTemplates.setTabListTemplate(panel, array);
		}
	}
}

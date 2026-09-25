using System;
using System.Collections.Generic;
using System.Linq;

namespace Mod.CustomPanel
{
	// Token: 0x02000165 RID: 357
	internal static class PaintPanelTemplates
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x000B6D58 File Offset: 0x000B4F58
		internal static void PaintTabHeaderTemplate(Panel panel, mGraphics g, string header)
		{
			g.setColor(13524492);
			g.fillRect(panel.X + 1, 78, panel.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, header, panel.xScroll + panel.wScroll / 2, 59, mFont.CENTER);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000B6DAC File Offset: 0x000B4FAC
		internal static void PaintCollectionCaptionAndDescriptionTemplate<T>(Panel panel, mGraphics g, ICollection<T> collection, Func<T, string> getCaption, Func<T, string> getDescription, bool captionIndex = true)
		{
			g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
			g.translate(0, -panel.cmy);
			g.setColor(0);
			if (collection == null || collection.Count != panel.currentListLength)
			{
				return;
			}
			for (int i = 0; i < panel.currentListLength; i++)
			{
				int xScroll = panel.xScroll;
				int num = panel.yScroll + i * panel.ITEM_HEIGHT;
				int wScroll = panel.wScroll;
				int num2 = panel.ITEM_HEIGHT - 1;
				g.setColor((i != panel.selected) ? 15196114 : 16383818);
				g.fillRect(xScroll, num, wScroll, num2);
				string text = (captionIndex ? string.Format("{0}. ", i + 1) : "") + getCaption(collection.ElementAt<T>(i));
				string text2 = getDescription(collection.ElementAt<T>(i));
				mFont.tahoma_7_green2.drawString(g, text, xScroll + 5, num, 0);
				mFont.tahoma_7_blue.drawString(g, text2, xScroll + 5, num + 11, 0);
			}
			panel.paintScrollArrow(g);
		}
	}
}

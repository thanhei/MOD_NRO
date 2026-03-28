using System;
using Mod.DungPham.KoiOctiiu957;

// Token: 0x020000AE RID: 174
public class InfoDlg
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00007A64 File Offset: 0x00005C64
	public static void show(string title, string subtitle, int delay)
	{
		if (title == null)
		{
			return;
		}
		InfoDlg.isShow = true;
		InfoDlg.title = title;
		InfoDlg.subtitke = subtitle;
		InfoDlg.delay = delay;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00007A85 File Offset: 0x00005C85
	public static void showWait()
	{
		if (MainMod.isLockMap && !AutoMap.isAutoChangeMap)
		{
			return;
		}
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00007AAC File Offset: 0x00005CAC
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00071538 File Offset: 0x0006F738
	public static void paint(mGraphics g)
	{
		if (!InfoDlg.isShow)
		{
			return;
		}
		if (InfoDlg.isLock && InfoDlg.delay > 4990)
		{
			return;
		}
		if (GameScr.isPaintAlert)
		{
			return;
		}
		int num = 10;
		GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
		if (InfoDlg.isLock)
		{
			GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
		}
		else if (InfoDlg.subtitke != null)
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
			mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
		}
		else
		{
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00007AC0 File Offset: 0x00005CC0
	public static void update()
	{
		if (!InfoDlg.isShow)
		{
			return;
		}
		if (InfoDlg.delay > 0)
		{
			InfoDlg.delay--;
			if (InfoDlg.delay <= 0)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00007AF4 File Offset: 0x00005CF4
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x04000F0C RID: 3852
	public static bool isShow;

	// Token: 0x04000F0D RID: 3853
	private static string title;

	// Token: 0x04000F0E RID: 3854
	private static string subtitke;

	// Token: 0x04000F0F RID: 3855
	public static int delay;

	// Token: 0x04000F10 RID: 3856
	public static bool isLock;
}

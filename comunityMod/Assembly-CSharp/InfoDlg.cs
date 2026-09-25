using System;

// Token: 0x0200004E RID: 78
public class InfoDlg
{
	// Token: 0x06000471 RID: 1137 RVA: 0x0004BDC9 File Offset: 0x00049FC9
	public static void show(string title, string subtitle, int delay)
	{
		if (title != null)
		{
			InfoDlg.isShow = true;
			InfoDlg.title = title;
			InfoDlg.subtitke = subtitle;
			InfoDlg.delay = delay;
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0004BDE6 File Offset: 0x00049FE6
	public static void showWait()
	{
		InfoDlg.show(mResources.PLEASEWAIT, null, 1000);
		InfoDlg.isLock = true;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0004BDFE File Offset: 0x00049FFE
	public static void showWait(string str)
	{
		InfoDlg.show(str, null, 700);
		InfoDlg.isLock = true;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0004BE14 File Offset: 0x0004A014
	public static void paint(mGraphics g)
	{
		if (InfoDlg.isShow && (!InfoDlg.isLock || InfoDlg.delay <= 4990) && !GameScr.isPaintAlert)
		{
			int num = 10;
			GameCanvas.paintz.paintPopUp(GameCanvas.hw - 75, num, 150, 55, g);
			if (InfoDlg.isLock)
			{
				GameCanvas.paintShukiren(GameCanvas.hw - mFont.tahoma_8b.getWidth(InfoDlg.title) / 2 - 10, num + 28, g);
				mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw + 5, num + 21, 2);
				return;
			}
			if (InfoDlg.subtitke != null)
			{
				mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 13, 2);
				mFont.tahoma_7_green2.drawString(g, InfoDlg.subtitke, GameCanvas.hw, num + 30, 2);
				return;
			}
			mFont.tahoma_8b.drawString(g, InfoDlg.title, GameCanvas.hw, num + 21, 2);
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0004BF07 File Offset: 0x0004A107
	public static void update()
	{
		if (InfoDlg.delay > 0)
		{
			InfoDlg.delay--;
			if (InfoDlg.delay == 0)
			{
				InfoDlg.hide();
			}
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0004BF29 File Offset: 0x0004A129
	public static void hide()
	{
		InfoDlg.title = string.Empty;
		InfoDlg.subtitke = null;
		InfoDlg.isLock = false;
		InfoDlg.delay = 0;
		InfoDlg.isShow = false;
	}

	// Token: 0x04000926 RID: 2342
	public static bool isShow;

	// Token: 0x04000927 RID: 2343
	internal static string title;

	// Token: 0x04000928 RID: 2344
	internal static string subtitke;

	// Token: 0x04000929 RID: 2345
	public static int delay;

	// Token: 0x0400092A RID: 2346
	public static bool isLock;
}

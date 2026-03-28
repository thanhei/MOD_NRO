using System;

// Token: 0x02000095 RID: 149
public class TextInfo
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x00006BAE File Offset: 0x00004DAE
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x0002FB78 File Offset: 0x0002DD78
	public static void paint(mGraphics g, string str, int x, int y, int w, int h, mFont f)
	{
		if (TextInfo.wStr != f.getWidth(str) || !TextInfo.laststring.Equals(str))
		{
			TextInfo.laststring = str;
			TextInfo.dx = 0;
			TextInfo.wStr = f.getWidth(str);
			TextInfo.isBack = false;
			TextInfo.tx = 0;
		}
		g.setClip(x, y, w, h);
		if (TextInfo.wStr > w)
		{
			f.drawString(g, str, x - TextInfo.dx, y, 0);
		}
		else
		{
			f.drawString(g, str, x + w / 2, y, 2);
		}
		GameCanvas.resetTrans(g);
		if (TextInfo.wStr > w)
		{
			if (!TextInfo.isBack)
			{
				TextInfo.tx++;
				if (TextInfo.tx > 50)
				{
					TextInfo.dx++;
					if (TextInfo.dx >= TextInfo.wStr)
					{
						TextInfo.tx = 0;
						TextInfo.dx = -w + 30;
						TextInfo.isBack = true;
					}
				}
			}
			else
			{
				if (TextInfo.dx < 0)
				{
					int num = w + TextInfo.dx >> 1;
					TextInfo.dx += num;
				}
				if (TextInfo.dx > 0)
				{
					TextInfo.dx = 0;
				}
				if (TextInfo.dx == 0)
				{
					TextInfo.tx++;
					if (TextInfo.tx == 50)
					{
						TextInfo.tx = 0;
						TextInfo.isBack = false;
					}
				}
			}
		}
	}

	// Token: 0x0400084C RID: 2124
	public static int dx;

	// Token: 0x0400084D RID: 2125
	public static int tx;

	// Token: 0x0400084E RID: 2126
	public static int wStr;

	// Token: 0x0400084F RID: 2127
	public static bool isBack;

	// Token: 0x04000850 RID: 2128
	public static string laststring = string.Empty;
}

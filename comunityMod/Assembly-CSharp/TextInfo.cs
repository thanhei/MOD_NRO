using System;

// Token: 0x020000BA RID: 186
public class TextInfo
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x0008BBAA File Offset: 0x00089DAA
	public static void reset()
	{
		TextInfo.dx = 0;
		TextInfo.tx = 0;
		TextInfo.isBack = false;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0008BBC0 File Offset: 0x00089DC0
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
		if (TextInfo.wStr <= w)
		{
			return;
		}
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
			return;
		}
		if (TextInfo.dx < 0)
		{
			TextInfo.dx += w + TextInfo.dx >> 1;
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

	// Token: 0x040010E9 RID: 4329
	public static int dx;

	// Token: 0x040010EA RID: 4330
	public static int tx;

	// Token: 0x040010EB RID: 4331
	public static int wStr;

	// Token: 0x040010EC RID: 4332
	public static bool isBack;

	// Token: 0x040010ED RID: 4333
	public static string laststring = string.Empty;
}

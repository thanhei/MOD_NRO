using System;

// Token: 0x0200008C RID: 140
public class PopUp
{
	// Token: 0x06000752 RID: 1874 RVA: 0x00074584 File Offset: 0x00072784
	public PopUp(string info, int x, int y)
	{
		this.sayWidth = 100;
		if (info.Length < 10)
		{
			this.sayWidth = 60;
		}
		if (GameCanvas.w == 128)
		{
			this.sayWidth = 128;
		}
		this.says = mFont.tahoma_7b_dark.splitFontArray(info, this.sayWidth - 10);
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		if (x >= 0 && x <= 24)
		{
			this.cx += this.cw / 2 + 30;
		}
		if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00074710 File Offset: 0x00072910
	public static void loadBg()
	{
		if (PopUp.goc == null)
		{
			PopUp.goc = GameCanvas.loadImage("/mainImage/myTexture2dbd3.png");
		}
		if (PopUp.imgPopUp == null)
		{
			PopUp.imgPopUp = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup.png");
		}
		if (PopUp.imgPopUp2 == null)
		{
			PopUp.imgPopUp2 = GameCanvas.loadImage("/mainImage/myTexture2dimgPopup2.png");
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00074760 File Offset: 0x00072960
	public void updateXYWH(string[] info, int x, int y)
	{
		this.sayWidth = 0;
		for (int i = 0; i < info.Length; i++)
		{
			if (this.sayWidth < mFont.tahoma_7b_dark.getWidth(info[i]))
			{
				this.sayWidth = mFont.tahoma_7b_dark.getWidth(info[i]);
			}
		}
		this.sayWidth += 20;
		this.says = info;
		this.sayRun = 7;
		this.cx = x - this.sayWidth / 2 - 1;
		this.cy = y - 15 + this.sayRun - this.says.Length * 12 - 10;
		this.cw = this.sayWidth + 2;
		this.ch = (this.says.Length + 1) * 12 + 1;
		while (this.cw % 10 != 0)
		{
			this.cw++;
		}
		while (this.ch % 10 != 0)
		{
			this.ch++;
		}
		if (x >= 0 && x <= 24)
		{
			this.cx += this.cw / 2 + 30;
		}
		if (x <= TileMap.tmw * 24 && x >= TileMap.tmw * 24 - 24)
		{
			this.cx -= this.cw / 2 + 6;
		}
		while (this.cx <= 30)
		{
			this.cx += 2;
		}
		while (this.cx + this.cw >= TileMap.tmw * 24 - 30)
		{
			this.cx -= 2;
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x000748E7 File Offset: 0x00072AE7
	public static void addPopUp(int x, int y, string info)
	{
		PopUp.vPopups.addElement(new PopUp(info, x, y));
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x000748FB File Offset: 0x00072AFB
	public static void addPopUp(PopUp p)
	{
		PopUp.vPopups.addElement(p);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00074908 File Offset: 0x00072B08
	public static void removePopUp(PopUp p)
	{
		PopUp.vPopups.removeElement(p);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00074915 File Offset: 0x00072B15
	public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
	{
		if (color == 1)
		{
			g.fillRect(x, y, w, h, 16777215, 90);
			return;
		}
		g.fillRect(x, y, w, h, 0, 77);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00074940 File Offset: 0x00072B40
	public static void paintPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isButton)
	{
		if (!isButton)
		{
			g.setColor(0);
			g.fillRect(x + 6, y, w - 14 + 1, h);
			g.fillRect(x, y + 6, w, h - 12 + 1);
			g.setColor(color);
			g.fillRect(x + 6, y + 1, w - 12, h - 2);
			g.fillRect(x + 1, y + 6, w - 2, h - 12);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 0, x, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 2, x + w - 7, y, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 1, x, y + h - 6, 0);
			g.drawRegion(PopUp.goc, 0, 0, 7, 6, 3, x + w - 7, y + h - 6, 0);
			return;
		}
		Image image = ((color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2);
		g.drawRegion(image, 0, 0, 10, 10, 0, x, y, 0);
		g.drawRegion(image, 0, 20, 10, 10, 0, x + w - 10, y, 0);
		g.drawRegion(image, 0, 50, 10, 10, 0, x, y + h - 10, 0);
		g.drawRegion(image, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
		int num = (((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10));
		int num2 = (((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10));
		for (int i = 0; i < num; i++)
		{
			g.drawRegion(image, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
		}
		for (int j = 0; j < num2; j++)
		{
			g.drawRegion(image, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
		}
		for (int k = 0; k < num; k++)
		{
			g.drawRegion(image, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
		}
		for (int l = 0; l < num2; l++)
		{
			g.drawRegion(image, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
		}
		g.setColor((color != 1) ? 16770503 : 12052656);
		g.fillRect(x + 10, y + 10, w - 20, h - 20);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00074B98 File Offset: 0x00072D98
	public void paint(mGraphics g)
	{
		if (this.isPaint && this.says != null && ChatPopup.currChatPopup == null && !this.isHide)
		{
			this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
			for (int i = 0; i < this.says.Length; i++)
			{
				((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
			}
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00074C70 File Offset: 0x00072E70
	internal void update()
	{
		if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0)
		{
			if (this.cx + this.cw >= GameScr.cmx && this.cx <= GameCanvas.w + GameScr.cmx && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		if (global::Char.myCharz().taskMaint == null || (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId != 0))
		{
			if (this.cx + this.cw / 2 >= global::Char.myCharz().cx - 100 && this.cx + this.cw / 2 <= global::Char.myCharz().cx + 100 && this.cy + this.ch >= GameScr.cmy && this.cy <= GameCanvas.h + GameScr.cmy)
			{
				this.isHide = false;
			}
			else
			{
				this.isHide = true;
			}
		}
		if (this.timeDelay > 0)
		{
			this.timeDelay--;
			if (this.timeDelay == 0 && this.command != null)
			{
				this.command.performAction();
			}
		}
		if (!this.isWayPoint)
		{
			return;
		}
		if (global::Char.myCharz().taskMaint != null)
		{
			if (global::Char.myCharz().taskMaint.taskId == 0)
			{
				if (global::Char.myCharz().taskMaint.index == 0)
				{
					this.isPaint = false;
				}
				if (global::Char.myCharz().taskMaint.index == 1)
				{
					this.isPaint = true;
				}
				if (global::Char.myCharz().taskMaint.index > 1 && global::Char.myCharz().taskMaint.index < 6)
				{
					this.isPaint = false;
					return;
				}
			}
			else if (!this.isPaint)
			{
				this.tDelay++;
				if (this.tDelay == 50)
				{
					this.isPaint = true;
					return;
				}
			}
		}
		else if (!this.isPaint)
		{
			Hint.isPaint = false;
			this.tDelay++;
			if (this.tDelay == 50)
			{
				this.isPaint = true;
				Hint.isPaint = true;
			}
		}
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00074EAA File Offset: 0x000730AA
	public void doClick(int timeDelay)
	{
		this.timeDelay = timeDelay;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00074EB4 File Offset: 0x000730B4
	public static void paintAll(mGraphics g)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00074EEC File Offset: 0x000730EC
	public static void updateAll()
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).update();
		}
	}

	// Token: 0x04000E22 RID: 3618
	public static MyVector vPopups = new MyVector();

	// Token: 0x04000E23 RID: 3619
	public int sayWidth;

	// Token: 0x04000E24 RID: 3620
	public int sayRun;

	// Token: 0x04000E25 RID: 3621
	public string[] says;

	// Token: 0x04000E26 RID: 3622
	public int cx;

	// Token: 0x04000E27 RID: 3623
	public int cy;

	// Token: 0x04000E28 RID: 3624
	public int cw;

	// Token: 0x04000E29 RID: 3625
	public int ch;

	// Token: 0x04000E2A RID: 3626
	public static int f;

	// Token: 0x04000E2B RID: 3627
	public static int tF;

	// Token: 0x04000E2C RID: 3628
	public static int dir;

	// Token: 0x04000E2D RID: 3629
	public bool isWayPoint;

	// Token: 0x04000E2E RID: 3630
	public int tDelay;

	// Token: 0x04000E2F RID: 3631
	internal int timeDelay;

	// Token: 0x04000E30 RID: 3632
	public Command command;

	// Token: 0x04000E31 RID: 3633
	public bool isPaint = true;

	// Token: 0x04000E32 RID: 3634
	public bool isHide;

	// Token: 0x04000E33 RID: 3635
	public static Image goc;

	// Token: 0x04000E34 RID: 3636
	public static Image imgPopUp;

	// Token: 0x04000E35 RID: 3637
	public static Image imgPopUp2;

	// Token: 0x04000E36 RID: 3638
	public Image imgFocus;

	// Token: 0x04000E37 RID: 3639
	public Image imgUnFocus;
}

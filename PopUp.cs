using System;

// Token: 0x0200007B RID: 123
public class PopUp
{
	// Token: 0x0600042A RID: 1066 RVA: 0x00027B60 File Offset: 0x00025D60
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

	// Token: 0x0600042B RID: 1067 RVA: 0x00027D18 File Offset: 0x00025F18
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

	// Token: 0x0600042C RID: 1068 RVA: 0x00027D70 File Offset: 0x00025F70
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

	// Token: 0x0600042D RID: 1069 RVA: 0x000063CA File Offset: 0x000045CA
	public static void addPopUp(int x, int y, string info)
	{
		PopUp.vPopups.addElement(new PopUp(info, x, y));
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000063DE File Offset: 0x000045DE
	public static void addPopUp(PopUp p)
	{
		PopUp.vPopups.addElement(p);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x000063EB File Offset: 0x000045EB
	public static void removePopUp(PopUp p)
	{
		PopUp.vPopups.removeElement(p);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000063F8 File Offset: 0x000045F8
	public void paintClipPopUp(mGraphics g, int x, int y, int w, int h, int color, bool isFocus)
	{
		if (color == 1)
		{
			g.fillRect(x, y, w, h, 16777215, 90);
		}
		else
		{
			g.fillRect(x, y, w, h, 0, 77);
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00027F24 File Offset: 0x00026124
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
		Image arg = (color != 1) ? PopUp.imgPopUp : PopUp.imgPopUp2;
		g.drawRegion(arg, 0, 0, 10, 10, 0, x, y, 0);
		g.drawRegion(arg, 0, 20, 10, 10, 0, x + w - 10, y, 0);
		g.drawRegion(arg, 0, 50, 10, 10, 0, x, y + h - 10, 0);
		g.drawRegion(arg, 0, 70, 10, 10, 0, x + w - 10, y + h - 10, 0);
		int num = ((w - 20) % 10 != 0) ? ((w - 20) / 10 + 1) : ((w - 20) / 10);
		int num2 = ((h - 20) % 10 != 0) ? ((h - 20) / 10 + 1) : ((h - 20) / 10);
		for (int i = 0; i < num; i++)
		{
			g.drawRegion(arg, 0, 10, 10, 10, 0, x + 10 + i * 10, y, 0);
		}
		for (int j = 0; j < num2; j++)
		{
			g.drawRegion(arg, 0, 30, 10, 10, 0, x, y + 10 + j * 10, 0);
		}
		for (int k = 0; k < num; k++)
		{
			g.drawRegion(arg, 0, 60, 10, 10, 0, x + 10 + k * 10, y + h - 10, 0);
		}
		for (int l = 0; l < num2; l++)
		{
			g.drawRegion(arg, 0, 40, 10, 10, 0, x + w - 10, y + 10 + l * 10, 0);
		}
		g.setColor((color != 1) ? 16770503 : 12052656);
		g.fillRect(x + 10, y + 10, w - 20, h - 20);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0002817C File Offset: 0x0002637C
	public void paint(mGraphics g)
	{
		if (!this.isPaint)
		{
			return;
		}
		if (this.says == null)
		{
			return;
		}
		if (ChatPopup.currChatPopup != null)
		{
			return;
		}
		if (!this.isHide)
		{
			this.paintClipPopUp(g, this.cx, this.cy - GameCanvas.transY, this.cw, this.ch, (this.timeDelay != 0) ? 1 : 0, true);
			for (int i = 0; i < this.says.Length; i++)
			{
				((this.timeDelay != 0) ? mFont.tahoma_7b_green2 : mFont.tahoma_7b_white).drawString(g, this.says[i], this.cx + this.cw / 2, this.cy + (this.ch / 2 - this.says.Length * 12 / 2) + i * 12 - GameCanvas.transY, 2);
			}
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00028268 File Offset: 0x00026468
	private void update()
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
		if (this.isWayPoint)
		{
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
					}
				}
				else if (!this.isPaint)
				{
					this.tDelay++;
					if (this.tDelay == 50)
					{
						this.isPaint = true;
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
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00006429 File Offset: 0x00004629
	public void doClick(int timeDelay)
	{
		this.timeDelay = timeDelay;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x000284FC File Offset: 0x000266FC
	public static void paintAll(mGraphics g)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).paint(g);
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0002853C File Offset: 0x0002673C
	public static void updateAll()
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			((PopUp)PopUp.vPopups.elementAt(i)).update();
		}
	}

	// Token: 0x04000757 RID: 1879
	public static MyVector vPopups = new MyVector();

	// Token: 0x04000758 RID: 1880
	public int sayWidth;

	// Token: 0x04000759 RID: 1881
	public int sayRun;

	// Token: 0x0400075A RID: 1882
	public string[] says;

	// Token: 0x0400075B RID: 1883
	public int cx;

	// Token: 0x0400075C RID: 1884
	public int cy;

	// Token: 0x0400075D RID: 1885
	public int cw;

	// Token: 0x0400075E RID: 1886
	public int ch;

	// Token: 0x0400075F RID: 1887
	public static int f;

	// Token: 0x04000760 RID: 1888
	public static int tF;

	// Token: 0x04000761 RID: 1889
	public static int dir;

	// Token: 0x04000762 RID: 1890
	public bool isWayPoint;

	// Token: 0x04000763 RID: 1891
	public int tDelay;

	// Token: 0x04000764 RID: 1892
	private int timeDelay;

	// Token: 0x04000765 RID: 1893
	public Command command;

	// Token: 0x04000766 RID: 1894
	public bool isPaint = true;

	// Token: 0x04000767 RID: 1895
	public bool isHide;

	// Token: 0x04000768 RID: 1896
	public static Image goc;

	// Token: 0x04000769 RID: 1897
	public static Image imgPopUp;

	// Token: 0x0400076A RID: 1898
	public static Image imgPopUp2;

	// Token: 0x0400076B RID: 1899
	public Image imgFocus;

	// Token: 0x0400076C RID: 1900
	public Image imgUnFocus;
}

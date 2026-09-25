using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class RadarScr : mScreen
{
	// Token: 0x0600076C RID: 1900 RVA: 0x00075460 File Offset: 0x00073660
	public RadarScr()
	{
		RadarScr.TYPE_UI = true;
		Image image = mSystem.loadImage("/radar/17.png");
		Image image2 = mSystem.loadImage("/radar/3.png");
		Image image3 = mSystem.loadImage("/radar/23.png");
		RadarScr.fraImgFocus = new FrameImage(image, 28, 28);
		RadarScr.fraImgFocusNone = new FrameImage(image2, 30, 30);
		RadarScr.fraEff = new FrameImage(image3, 11, 11);
		RadarScr.imgUI = mSystem.loadImage("/radar/0.png");
		RadarScr.imgArrow_Left = mSystem.loadImage("/radar/1.png");
		RadarScr.imgArrow_Right = mSystem.loadImage("/radar/2.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/17.png");
		RadarScr.imgArrow_Down = mSystem.loadImage("/radar/4.png");
		RadarScr.imgLock = mSystem.loadImage("/radar/5.png");
		RadarScr.imgUse_0 = mSystem.loadImage("/radar/6.png");
		RadarScr.imgRank = new Image[7];
		for (int i = 0; i < 7; i++)
		{
			RadarScr.imgRank[i] = mSystem.loadImage("/radar/" + (i + 7).ToString() + ".png");
		}
		RadarScr.imgUse = mSystem.loadImage("/radar/14.png");
		RadarScr.imgBack = mSystem.loadImage("/radar/15.png");
		RadarScr.imgChange = mSystem.loadImage("/radar/16.png");
		RadarScr.imgUIText = mSystem.loadImage("/radar/18.png");
		RadarScr.imgBar_1 = mSystem.loadImage("/radar/19.png");
		RadarScr.imgPro_0 = mSystem.loadImage("/radar/20.png");
		RadarScr.imgPro_1 = mSystem.loadImage("/radar/21.png");
		RadarScr.imgBar_0 = mSystem.loadImage("/radar/22.png");
		RadarScr.wUi = 200;
		RadarScr.hUi = 219;
		RadarScr.xUi = GameCanvas.hw - (RadarScr.wUi + 40) / 2;
		RadarScr.yUi = GameCanvas.hh - RadarScr.hUi / 2;
		RadarScr.xText = RadarScr.xUi + RadarScr.wUi - 81;
		RadarScr.yText = RadarScr.yUi + 29;
		RadarScr.wText = 120;
		RadarScr.hText = 80;
		RadarScr.xyArrow = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 34,
				RadarScr.yUi + RadarScr.hUi - 42
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgArrow_Down.getWidth() / 2,
				RadarScr.yUi + RadarScr.hUi / 2 + 33
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 41,
				RadarScr.yUi + RadarScr.hUi - 42
			}
		};
		RadarScr.xyItem = new int[][]
		{
			new int[]
			{
				RadarScr.xUi + 25,
				RadarScr.yUi + RadarScr.hUi - 82
			},
			new int[]
			{
				RadarScr.xUi + 57,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi / 2 - 14,
				RadarScr.yUi + RadarScr.hUi - 102
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 57 - 28,
				RadarScr.yUi + RadarScr.hUi - 62
			},
			new int[]
			{
				RadarScr.xUi + RadarScr.wUi - 25 - 28,
				RadarScr.yUi + RadarScr.hUi - 82
			}
		};
		this.dxArrow = new int[2];
		this.dyArrow = 0;
		RadarScr.xMon = RadarScr.xUi + 73;
		RadarScr.yMon = RadarScr.yUi + RadarScr.hUi / 2 + 5;
		RadarScr.yCmd = RadarScr.yUi + RadarScr.hUi - 22;
		RadarScr.xCmd = new int[]
		{
			RadarScr.xUi + RadarScr.wUi / 2 - 8 - 80,
			RadarScr.xUi + RadarScr.wUi / 2 - 8,
			RadarScr.xUi + RadarScr.wUi / 2 - 8 + 80
		};
		RadarScr.dxCmd = new int[3];
		this.yClip = RadarScr.yText + 10 + 70;
		this.hClip = 0;
		RadarScr.list = new MyVector();
		RadarScr.listUse = new MyVector();
		this.page = 1;
		this.maxpage = 2;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00075891 File Offset: 0x00073A91
	public static RadarScr gI()
	{
		if (RadarScr.instance == null)
		{
			RadarScr.instance = new RadarScr();
		}
		return RadarScr.instance;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x000758AC File Offset: 0x00073AAC
	public void SetRadarScr(MyVector list, int num, int numMax)
	{
		RadarScr.list = list;
		RadarScr.SetNum(num, numMax);
		this.page = 1;
		this.indexFocus = 2;
		this.listIndex();
		RadarScr.TYPE_UI = true;
		RadarScr.SetListUse();
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = list.size() / 5 + ((list.size() % 5 > 0) ? 1 : 0);
			return;
		}
		this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00075931 File Offset: 0x00073B31
	public static void SetNum(int num, int numMax)
	{
		RadarScr.num = num;
		RadarScr.numMax = numMax;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00075940 File Offset: 0x00073B40
	public static void SetListUse()
	{
		RadarScr.listUse = new MyVector(string.Empty);
		for (int i = 0; i < RadarScr.list.size(); i++)
		{
			Info_RadaScr info_RadaScr = (Info_RadaScr)RadarScr.list.elementAt(i);
			if (info_RadaScr != null && info_RadaScr.isUse == 1)
			{
				RadarScr.listUse.addElement(info_RadaScr);
			}
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0007599C File Offset: 0x00073B9C
	public void listIndex()
	{
		MyVector myVector = RadarScr.listUse;
		if (RadarScr.TYPE_UI)
		{
			myVector = RadarScr.list;
		}
		int num = (this.page - 1) * 5;
		int num2 = num + 5;
		for (int i = num; i < num2; i++)
		{
			if (i >= myVector.size())
			{
				RadarScr.index[i - num] = -1;
			}
			else
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)myVector.elementAt(i);
				if (info_RadaScr != null)
				{
					RadarScr.index[i - num] = info_RadaScr.id;
				}
			}
		}
		RadarScr.cmyText = 0;
		RadarScr.hText = 0;
		SoundMn.gI().radarItem();
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00075A24 File Offset: 0x00073C24
	public override void update()
	{
		try
		{
			if (RadarScr.hText < 80)
			{
				RadarScr.hText += 4;
				if (RadarScr.hText > 80)
				{
					RadarScr.hText = 80;
				}
			}
			this.focus_card = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[this.indexFocus]);
			if (RadarScr.TYPE_UI)
			{
				this.focus_card = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[this.indexFocus]);
			}
			GameScr.gI().update();
			if (GameCanvas.gameTick % 10 < 6)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.dyArrow--;
				}
			}
			else
			{
				this.dyArrow = 0;
			}
			if (this.focus_card != null)
			{
				this.hClip = (int)(this.focus_card.amount * 100 / this.focus_card.max_amount) * RadarScr.imgBar_1.getHeight() / 100;
				this.wClip = RadarScr.num * 100 / RadarScr.list.size() * RadarScr.imgPro_1.getWidth() / 100;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-upd-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00075B54 File Offset: 0x00073D54
	public override void updateKey()
	{
		if (!InfoDlg.isLock)
		{
			if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
			{
				this.updateKeyTouchControl();
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.doKeyText(1);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.doKeyText(-1);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = false;
				this.doKeyItem(1);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false;
				this.doKeyItem(0);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doClickUse(1);
			}
			if (GameCanvas.keyPressed[13])
			{
				this.doClickUse(2);
			}
			if (GameCanvas.keyPressed[12])
			{
				GameCanvas.keyPressed[12] = false;
				this.doClickUse(0);
			}
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00075CA8 File Offset: 0x00073EA8
	internal void doChangeUI()
	{
		RadarScr.TYPE_UI = !RadarScr.TYPE_UI;
		this.page = 1;
		this.indexFocus = 0;
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}
		this.listIndex();
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00075D2C File Offset: 0x00073F2C
	internal void updateKeyTouchControl()
	{
		if (GameCanvas.isPointerClick)
		{
			for (int i = 0; i < 5; i++)
			{
				if (GameCanvas.isPointerHoldIn(RadarScr.xyItem[i][0], RadarScr.xyItem[i][1], 30, 30) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i != this.indexFocus)
				{
					this.doClickItem(i);
				}
			}
			if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[0][0] - 5, RadarScr.xyArrow[0][1] - 5, 20, 20))
			{
				if (GameCanvas.isPointerDown)
				{
					this.dxArrow[0] = 1;
				}
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickArrow(0);
					this.dxArrow[0] = 0;
				}
			}
			if (GameCanvas.isPointerHoldIn(RadarScr.xyArrow[2][0] - 5, RadarScr.xyArrow[2][1] - 5, 20, 20))
			{
				if (GameCanvas.isPointerDown)
				{
					this.dxArrow[1] = 1;
				}
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.doClickArrow(1);
					this.dxArrow[1] = 0;
				}
			}
			for (int j = 0; j < RadarScr.xCmd.Length; j++)
			{
				if (GameCanvas.isPointerHoldIn(RadarScr.xCmd[j] - 5, RadarScr.yCmd - 5, 20, 20))
				{
					if (GameCanvas.isPointerDown)
					{
						RadarScr.dxCmd[j] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						this.doClickUse(j);
						RadarScr.dxCmd[j] = 0;
					}
				}
			}
		}
		else
		{
			RadarScr.dxCmd[0] = 0;
			RadarScr.dxCmd[1] = 0;
			RadarScr.dxCmd[2] = 0;
			this.dxArrow[0] = 0;
			this.dxArrow[1] = 0;
		}
		if (!GameCanvas.isPointerHoldIn(RadarScr.xText, 0, RadarScr.wText, RadarScr.yText + RadarScr.hText))
		{
			return;
		}
		if (GameCanvas.isPointerMove)
		{
			if (this.pyy == 0)
			{
				this.pyy = GameCanvas.py;
			}
			this.pxx = this.pyy - GameCanvas.py;
			if (this.pxx != 0)
			{
				RadarScr.cmyText += this.pxx;
				this.pyy = GameCanvas.py;
			}
			if (RadarScr.cmyText < 0)
			{
				RadarScr.cmyText = 0;
			}
			if (RadarScr.cmyText > this.focus_card.cp.lim)
			{
				RadarScr.cmyText = this.focus_card.cp.lim;
				return;
			}
		}
		else
		{
			this.pyy = 0;
			this.pyy = 0;
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00075F68 File Offset: 0x00074168
	internal void doClickUse(int i)
	{
		if (i == 0)
		{
			this.doChangeUI();
		}
		else if (i == 1)
		{
			if (this.focus_card != null)
			{
				Service.gI().SendRada(1, this.focus_card.id);
			}
		}
		else if (i == 2)
		{
			GameScr.gI().switchToMe();
		}
		SoundMn.gI().radarClick();
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00075FBC File Offset: 0x000741BC
	internal void doClickArrow(int dir)
	{
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 > 0) ? 1 : 0);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 > 0) ? 1 : 0);
		}
		int num = this.page;
		if (dir == 0)
		{
			if (this.page == 1)
			{
				return;
			}
			num--;
			if (num < 1)
			{
				num = 1;
			}
		}
		else
		{
			if (this.page == this.maxpage)
			{
				return;
			}
			num++;
			if (num > this.maxpage)
			{
				num = this.maxpage;
			}
		}
		if (num != this.page)
		{
			this.page = num;
			this.listIndex();
		}
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00076077 File Offset: 0x00074277
	internal void doClickItem(int focus)
	{
		this.indexFocus = focus;
		this.listIndex();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00076088 File Offset: 0x00074288
	internal void doKeyText(int type)
	{
		RadarScr.cmyText += 12 * type;
		if (RadarScr.cmyText < 0)
		{
			RadarScr.cmyText = 0;
		}
		if (RadarScr.cmyText > this.focus_card.cp.lim)
		{
			RadarScr.cmyText = this.focus_card.cp.lim;
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x000760E0 File Offset: 0x000742E0
	internal void doKeyItem(int type)
	{
		int num = this.indexFocus;
		int num2 = this.page;
		num = ((type != 0) ? (num - 1) : (num + 1));
		if (num >= RadarScr.index.Length)
		{
			if (this.page < this.maxpage)
			{
				num = 0;
				num2++;
			}
			else
			{
				num = RadarScr.index.Length - 1;
			}
		}
		if (num < 0)
		{
			if (this.page > 1)
			{
				num = RadarScr.index.Length - 1;
				num2--;
			}
			else
			{
				num = 0;
			}
		}
		if (num != this.indexFocus)
		{
			this.indexFocus = num;
			RadarScr.cmyText = 0;
			RadarScr.hText = 0;
		}
		if (num2 != this.page)
		{
			this.page = num2;
			this.listIndex();
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00076184 File Offset: 0x00074384
	public override void paint(mGraphics g)
	{
		try
		{
			GameScr.gI().paint(g);
			g.translate(-GameScr.cmx, -GameScr.cmy);
			g.translate(0, GameCanvas.transY);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgUI, RadarScr.xUi, RadarScr.yUi, 0);
			g.drawImage(RadarScr.imgPro_0, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 - 2, 0);
			g.setClip(RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, this.wClip, RadarScr.imgPro_0.getHeight());
			g.drawImage(RadarScr.imgPro_1, RadarScr.xUi + RadarScr.wUi / 2 - RadarScr.imgPro_0.getWidth() / 2 + 13, RadarScr.yUi - RadarScr.imgPro_0.getHeight() / 2 + 3, 0);
			GameScr.resetTranslate(g);
			g.drawImage(RadarScr.imgChange, RadarScr.xCmd[0], RadarScr.yCmd + RadarScr.dxCmd[0], 0);
			g.drawImage(RadarScr.imgUse_0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			g.drawImage(RadarScr.imgBack, RadarScr.xCmd[2], RadarScr.yCmd + RadarScr.dxCmd[2], 0);
			if (RadarScr.TYPE_UI)
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 0, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			else
			{
				g.drawRegion(RadarScr.imgUse, 0, 0, 17, 17, 1, RadarScr.xCmd[1], RadarScr.yCmd + RadarScr.dxCmd[1], 0);
			}
			if (this.focus_card != null)
			{
				g.setClip(RadarScr.xUi + 30, RadarScr.yUi + 13, RadarScr.wUi - 60, RadarScr.hUi / 2);
				this.focus_card.paintInfo(g, RadarScr.xMon, RadarScr.yMon);
				GameScr.resetTranslate(g);
				mFont.tahoma_7b_yellow.drawString(g, ((this.focus_card.level <= 0) ? " " : ("Lv." + this.focus_card.level.ToString() + " ")) + this.focus_card.name, RadarScr.xUi + RadarScr.wUi / 2, RadarScr.yUi + 15, 2);
				mFont.tahoma_7_white.drawString(g, "no." + this.focus_card.no.ToString(), RadarScr.xUi + 30, RadarScr.yText - 2, 0);
				g.drawImage(RadarScr.imgBar_0, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				g.setClip(RadarScr.xUi + 36, this.yClip - this.hClip, 7, this.hClip);
				g.drawImage(RadarScr.imgBar_1, RadarScr.xUi + 36, RadarScr.yText + 10, 0);
				GameScr.resetTranslate(g);
				g.drawImage(RadarScr.imgRank[(int)this.focus_card.rank], RadarScr.xUi + 39 - 5 + 14, RadarScr.yText + 12, 0);
			}
			g.setClip(RadarScr.xText, RadarScr.yText, RadarScr.wText + 5, RadarScr.hText + 8);
			if (this.focus_card != null)
			{
				g.drawImage(RadarScr.imgUIText, RadarScr.xText, RadarScr.yText, 0);
			}
			GameScr.resetTranslate(g);
			g.setClip(RadarScr.xText, RadarScr.yText + 1, RadarScr.wText, RadarScr.hText + 5);
			if (this.focus_card != null && this.focus_card.cp != null)
			{
				if (this.focus_card.cp.says == null)
				{
					return;
				}
				this.focus_card.cp.paintRada(g, RadarScr.cmyText);
			}
			GameScr.resetTranslate(g);
			if ((!RadarScr.TYPE_UI && RadarScr.listUse.size() > 5) || RadarScr.TYPE_UI)
			{
				if (this.page > 1)
				{
					g.drawImage(RadarScr.imgArrow_Left, RadarScr.xyArrow[0][0], RadarScr.xyArrow[0][1] + this.dxArrow[0], 0);
				}
				if (this.page < this.maxpage)
				{
					g.drawImage(RadarScr.imgArrow_Right, RadarScr.xyArrow[2][0], RadarScr.xyArrow[2][1] + this.dxArrow[1], 0);
				}
			}
			for (int i = 0; i < RadarScr.index.Length; i++)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (i == this.indexFocus)
				{
					num = this.dyArrow;
					num2 = -10;
					num3 = 1;
					g.drawImage(RadarScr.imgArrow_Down, RadarScr.xyItem[i][0] + 10, RadarScr.xyItem[i][1] + this.dyArrow + 29 + num2, 0);
				}
				Info_RadaScr info_RadaScr = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[i]);
				if (RadarScr.TYPE_UI)
				{
					info_RadaScr = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[i]);
				}
				if (info_RadaScr != null)
				{
					RadarScr.fraImgFocus.drawFrame((int)info_RadaScr.rank, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					SmallImage.drawSmallImage(g, info_RadaScr.idIcon, RadarScr.xyItem[i][0] + 14, RadarScr.xyItem[i][1] + 14 + num + num2, 0, StaticObj.VCENTER_HCENTER);
					info_RadaScr.paintEff(g, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2);
					if (info_RadaScr.level == 0)
					{
						g.drawImage(RadarScr.imgLock, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0);
					}
					if (i == this.indexFocus)
					{
						RadarScr.fraImgFocus.drawFrame(7, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
					if (info_RadaScr.isUse == 1)
					{
						RadarScr.fraImgFocus.drawFrame(8, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
				}
				else
				{
					RadarScr.fraImgFocusNone.drawFrame(num3, RadarScr.xyItem[i][0] - 1, RadarScr.xyItem[i][1] - 1 + num + num2, 0, 0, g);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-pnt-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x000767E0 File Offset: 0x000749E0
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		base.switchToMe();
	}

	// Token: 0x04000E4F RID: 3663
	public const sbyte SUBCMD_ALL = 0;

	// Token: 0x04000E50 RID: 3664
	public const sbyte SUBCMD_USE = 1;

	// Token: 0x04000E51 RID: 3665
	public const sbyte SUBCMD_LEVEL = 2;

	// Token: 0x04000E52 RID: 3666
	public const sbyte SUBCMD_AMOUNT = 3;

	// Token: 0x04000E53 RID: 3667
	public const sbyte SUBCMD_AURA = 4;

	// Token: 0x04000E54 RID: 3668
	public static RadarScr instance;

	// Token: 0x04000E55 RID: 3669
	public static bool TYPE_UI;

	// Token: 0x04000E56 RID: 3670
	public static FrameImage fraImgFocus;

	// Token: 0x04000E57 RID: 3671
	public static FrameImage fraImgFocusNone;

	// Token: 0x04000E58 RID: 3672
	public static FrameImage fraEff;

	// Token: 0x04000E59 RID: 3673
	internal static Image imgUI;

	// Token: 0x04000E5A RID: 3674
	internal static Image imgUIText;

	// Token: 0x04000E5B RID: 3675
	internal static Image imgArrow_Left;

	// Token: 0x04000E5C RID: 3676
	internal static Image imgArrow_Right;

	// Token: 0x04000E5D RID: 3677
	internal static Image imgArrow_Down;

	// Token: 0x04000E5E RID: 3678
	internal static Image imgLock;

	// Token: 0x04000E5F RID: 3679
	internal static Image imgUse_0;

	// Token: 0x04000E60 RID: 3680
	internal static Image imgUse;

	// Token: 0x04000E61 RID: 3681
	internal static Image imgBack;

	// Token: 0x04000E62 RID: 3682
	internal static Image imgChange;

	// Token: 0x04000E63 RID: 3683
	internal static Image imgBar_0;

	// Token: 0x04000E64 RID: 3684
	internal static Image imgBar_1;

	// Token: 0x04000E65 RID: 3685
	internal static Image imgPro_0;

	// Token: 0x04000E66 RID: 3686
	internal static Image imgPro_1;

	// Token: 0x04000E67 RID: 3687
	internal static Image[] imgRank;

	// Token: 0x04000E68 RID: 3688
	public static int xUi;

	// Token: 0x04000E69 RID: 3689
	public static int yUi;

	// Token: 0x04000E6A RID: 3690
	public static int wUi;

	// Token: 0x04000E6B RID: 3691
	public static int hUi;

	// Token: 0x04000E6C RID: 3692
	public static int xMon;

	// Token: 0x04000E6D RID: 3693
	public static int yMon;

	// Token: 0x04000E6E RID: 3694
	public static int xText;

	// Token: 0x04000E6F RID: 3695
	public static int yText;

	// Token: 0x04000E70 RID: 3696
	public static int wText;

	// Token: 0x04000E71 RID: 3697
	public static int cmyText;

	// Token: 0x04000E72 RID: 3698
	public static int hText;

	// Token: 0x04000E73 RID: 3699
	public static int yCmd;

	// Token: 0x04000E74 RID: 3700
	public static int[] xCmd = new int[0];

	// Token: 0x04000E75 RID: 3701
	public static int[] dxCmd = new int[0];

	// Token: 0x04000E76 RID: 3702
	internal static int[][] xyArrow;

	// Token: 0x04000E77 RID: 3703
	internal static int[][] xyItem;

	// Token: 0x04000E78 RID: 3704
	internal static int[] index = new int[] { -2, -1, 0, 1, 2 };

	// Token: 0x04000E79 RID: 3705
	internal int dyArrow;

	// Token: 0x04000E7A RID: 3706
	internal int[] dxArrow;

	// Token: 0x04000E7B RID: 3707
	internal int page;

	// Token: 0x04000E7C RID: 3708
	internal int maxpage;

	// Token: 0x04000E7D RID: 3709
	internal int indexFocus;

	// Token: 0x04000E7E RID: 3710
	public static MyVector list;

	// Token: 0x04000E7F RID: 3711
	public static MyVector listUse;

	// Token: 0x04000E80 RID: 3712
	internal static int num;

	// Token: 0x04000E81 RID: 3713
	internal static int numMax;

	// Token: 0x04000E82 RID: 3714
	internal Info_RadaScr focus_card;

	// Token: 0x04000E83 RID: 3715
	internal int pxx;

	// Token: 0x04000E84 RID: 3716
	internal int pyy;

	// Token: 0x04000E85 RID: 3717
	internal int xClip;

	// Token: 0x04000E86 RID: 3718
	internal int wClip;

	// Token: 0x04000E87 RID: 3719
	internal int yClip;

	// Token: 0x04000E88 RID: 3720
	internal int hClip;
}

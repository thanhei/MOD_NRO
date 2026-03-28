using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class RadarScr : mScreen
{
	// Token: 0x0600099F RID: 2463 RVA: 0x000920BC File Offset: 0x000902BC
	public RadarScr()
	{
		RadarScr.TYPE_UI = true;
		Image img = mSystem.loadImage("/radar/17.png");
		Image img2 = mSystem.loadImage("/radar/3.png");
		Image img3 = mSystem.loadImage("/radar/23.png");
		RadarScr.fraImgFocus = new FrameImage(img, 28, 28);
		RadarScr.fraImgFocusNone = new FrameImage(img2, 30, 30);
		RadarScr.fraEff = new FrameImage(img3, 11, 11);
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
			RadarScr.imgRank[i] = mSystem.loadImage("/radar/" + (i + 7) + ".png");
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

	// Token: 0x060009A0 RID: 2464 RVA: 0x000085DB File Offset: 0x000067DB
	public static RadarScr gI()
	{
		if (RadarScr.instance == null)
		{
			RadarScr.instance = new RadarScr();
		}
		return RadarScr.instance;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x000924F4 File Offset: 0x000906F4
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
			this.maxpage = list.size() / 5 + ((list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
		}
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x000085F6 File Offset: 0x000067F6
	public static void SetNum(int num, int numMax)
	{
		RadarScr.num = num;
		RadarScr.numMax = numMax;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0009258C File Offset: 0x0009078C
	public static void SetListUse()
	{
		RadarScr.listUse = new MyVector(string.Empty);
		for (int i = 0; i < RadarScr.list.size(); i++)
		{
			Info_RadaScr info_RadaScr = (Info_RadaScr)RadarScr.list.elementAt(i);
			if (info_RadaScr != null && (int)info_RadaScr.isUse == 1)
			{
				RadarScr.listUse.addElement(info_RadaScr);
			}
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x000925F4 File Offset: 0x000907F4
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

	// Token: 0x060009A5 RID: 2469 RVA: 0x00092690 File Offset: 0x00090890
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
				int num = (int)this.focus_card.amount * 100 / (int)this.focus_card.max_amount;
				this.hClip = num * RadarScr.imgBar_1.getHeight() / 100;
				int num2 = RadarScr.num * 100 / RadarScr.list.size();
				this.wClip = num2 * RadarScr.imgPro_1.getWidth() / 100;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-upd-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x000927EC File Offset: 0x000909EC
	public override void updateKey()
	{
		if (InfoDlg.isLock)
		{
			return;
		}
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

	// Token: 0x060009A7 RID: 2471 RVA: 0x0009299C File Offset: 0x00090B9C
	private void doChangeUI()
	{
		RadarScr.TYPE_UI = !RadarScr.TYPE_UI;
		this.page = 1;
		this.indexFocus = 0;
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
		}
		this.listIndex();
		RadarScr.hText = 0;
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00092A38 File Offset: 0x00090C38
	private void updateKeyTouchControl()
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
		if (GameCanvas.isPointerHoldIn(RadarScr.xText, 0, RadarScr.wText, RadarScr.yText + RadarScr.hText))
		{
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
				}
			}
			else
			{
				this.pyy = 0;
				this.pyy = 0;
			}
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00092CC4 File Offset: 0x00090EC4
	private void doClickUse(int i)
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

	// Token: 0x060009AA RID: 2474 RVA: 0x00092D2C File Offset: 0x00090F2C
	private void doClickArrow(int dir)
	{
		if (RadarScr.TYPE_UI)
		{
			this.maxpage = RadarScr.list.size() / 5 + ((RadarScr.list.size() % 5 <= 0) ? 0 : 1);
		}
		else
		{
			this.maxpage = RadarScr.listUse.size() / 5 + ((RadarScr.listUse.size() % 5 <= 0) ? 0 : 1);
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

	// Token: 0x060009AB RID: 2475 RVA: 0x00008604 File Offset: 0x00006804
	private void doClickItem(int focus)
	{
		this.indexFocus = focus;
		this.listIndex();
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00092E10 File Offset: 0x00091010
	private void doKeyText(int type)
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

	// Token: 0x060009AD RID: 2477 RVA: 0x00092E6C File Offset: 0x0009106C
	private void doKeyItem(int type)
	{
		int num = this.indexFocus;
		int num2 = this.page;
		if (type == 0)
		{
			num++;
		}
		else
		{
			num--;
		}
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

	// Token: 0x060009AE RID: 2478 RVA: 0x00092F30 File Offset: 0x00091130
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
				mFont.tahoma_7b_yellow.drawString(g, (((int)this.focus_card.level <= 0) ? " " : ("Lv." + this.focus_card.level + " ")) + this.focus_card.name, RadarScr.xUi + RadarScr.wUi / 2, RadarScr.yUi + 15, 2);
				mFont.tahoma_7_white.drawString(g, "no." + this.focus_card.no, RadarScr.xUi + 30, RadarScr.yText - 2, 0);
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
				int idx = 0;
				if (i == this.indexFocus)
				{
					num = this.dyArrow;
					num2 = -10;
					idx = 1;
					g.drawImage(RadarScr.imgArrow_Down, RadarScr.xyItem[i][0] + 10, RadarScr.xyItem[i][1] + this.dyArrow + 29 + num2, 0);
				}
				Info_RadaScr info = Info_RadaScr.GetInfo(RadarScr.listUse, RadarScr.index[i]);
				if (RadarScr.TYPE_UI)
				{
					info = Info_RadaScr.GetInfo(RadarScr.list, RadarScr.index[i]);
				}
				if (info != null)
				{
					RadarScr.fraImgFocus.drawFrame((int)info.rank, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					SmallImage.drawSmallImage(g, info.idIcon, RadarScr.xyItem[i][0] + 14, RadarScr.xyItem[i][1] + 14 + num + num2, 0, StaticObj.VCENTER_HCENTER);
					info.paintEff(g, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2);
					if ((int)info.level == 0)
					{
						g.drawImage(RadarScr.imgLock, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0);
					}
					if (i == this.indexFocus)
					{
						RadarScr.fraImgFocus.drawFrame(7, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
					if ((int)info.isUse == 1)
					{
						RadarScr.fraImgFocus.drawFrame(8, RadarScr.xyItem[i][0], RadarScr.xyItem[i][1] + num + num2, 0, 0, g);
					}
				}
				else
				{
					RadarScr.fraImgFocusNone.drawFrame(idx, RadarScr.xyItem[i][0] - 1, RadarScr.xyItem[i][1] - 1 + num + num2, 0, 0, g);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("-pnt-radaScr-null: " + ex.ToString());
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00008613 File Offset: 0x00006813
	public override void switchToMe()
	{
		GameScr.isPaintOther = true;
		base.switchToMe();
	}

	// Token: 0x040011F3 RID: 4595
	public const sbyte SUBCMD_ALL = 0;

	// Token: 0x040011F4 RID: 4596
	public const sbyte SUBCMD_USE = 1;

	// Token: 0x040011F5 RID: 4597
	public const sbyte SUBCMD_LEVEL = 2;

	// Token: 0x040011F6 RID: 4598
	public const sbyte SUBCMD_AMOUNT = 3;

	// Token: 0x040011F7 RID: 4599
	public const sbyte SUBCMD_AURA = 4;

	// Token: 0x040011F8 RID: 4600
	public static RadarScr instance;

	// Token: 0x040011F9 RID: 4601
	public static bool TYPE_UI;

	// Token: 0x040011FA RID: 4602
	public static FrameImage fraImgFocus;

	// Token: 0x040011FB RID: 4603
	public static FrameImage fraImgFocusNone;

	// Token: 0x040011FC RID: 4604
	public static FrameImage fraEff;

	// Token: 0x040011FD RID: 4605
	private static Image imgUI;

	// Token: 0x040011FE RID: 4606
	private static Image imgUIText;

	// Token: 0x040011FF RID: 4607
	private static Image imgArrow_Left;

	// Token: 0x04001200 RID: 4608
	private static Image imgArrow_Right;

	// Token: 0x04001201 RID: 4609
	private static Image imgArrow_Down;

	// Token: 0x04001202 RID: 4610
	private static Image imgLock;

	// Token: 0x04001203 RID: 4611
	private static Image imgUse_0;

	// Token: 0x04001204 RID: 4612
	private static Image imgUse;

	// Token: 0x04001205 RID: 4613
	private static Image imgBack;

	// Token: 0x04001206 RID: 4614
	private static Image imgChange;

	// Token: 0x04001207 RID: 4615
	private static Image imgBar_0;

	// Token: 0x04001208 RID: 4616
	private static Image imgBar_1;

	// Token: 0x04001209 RID: 4617
	private static Image imgPro_0;

	// Token: 0x0400120A RID: 4618
	private static Image imgPro_1;

	// Token: 0x0400120B RID: 4619
	private static Image[] imgRank;

	// Token: 0x0400120C RID: 4620
	public static int xUi;

	// Token: 0x0400120D RID: 4621
	public static int yUi;

	// Token: 0x0400120E RID: 4622
	public static int wUi;

	// Token: 0x0400120F RID: 4623
	public static int hUi;

	// Token: 0x04001210 RID: 4624
	public static int xMon;

	// Token: 0x04001211 RID: 4625
	public static int yMon;

	// Token: 0x04001212 RID: 4626
	public static int xText;

	// Token: 0x04001213 RID: 4627
	public static int yText;

	// Token: 0x04001214 RID: 4628
	public static int wText;

	// Token: 0x04001215 RID: 4629
	public static int cmyText;

	// Token: 0x04001216 RID: 4630
	public static int hText;

	// Token: 0x04001217 RID: 4631
	public static int yCmd;

	// Token: 0x04001218 RID: 4632
	public static int[] xCmd = new int[0];

	// Token: 0x04001219 RID: 4633
	public static int[] dxCmd = new int[0];

	// Token: 0x0400121A RID: 4634
	private static int[][] xyArrow;

	// Token: 0x0400121B RID: 4635
	private static int[][] xyItem;

	// Token: 0x0400121C RID: 4636
	private static int[] index = new int[]
	{
		-2,
		-1,
		0,
		1,
		2
	};

	// Token: 0x0400121D RID: 4637
	private int dyArrow;

	// Token: 0x0400121E RID: 4638
	private int[] dxArrow;

	// Token: 0x0400121F RID: 4639
	private int page;

	// Token: 0x04001220 RID: 4640
	private int maxpage;

	// Token: 0x04001221 RID: 4641
	private int indexFocus;

	// Token: 0x04001222 RID: 4642
	public static MyVector list;

	// Token: 0x04001223 RID: 4643
	public static MyVector listUse;

	// Token: 0x04001224 RID: 4644
	private static int num;

	// Token: 0x04001225 RID: 4645
	private static int numMax;

	// Token: 0x04001226 RID: 4646
	private Info_RadaScr focus_card;

	// Token: 0x04001227 RID: 4647
	private int pxx;

	// Token: 0x04001228 RID: 4648
	private int pyy;

	// Token: 0x04001229 RID: 4649
	private int xClip;

	// Token: 0x0400122A RID: 4650
	private int wClip;

	// Token: 0x0400122B RID: 4651
	private int yClip;

	// Token: 0x0400122C RID: 4652
	private int hClip;
}

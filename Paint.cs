using System;

// Token: 0x02000075 RID: 117
public class Paint
{
	// Token: 0x060003E8 RID: 1000 RVA: 0x00026450 File Offset: 0x00024650
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1) + ".png");
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00026498 File Offset: 0x00024698
	public void paintDefaultBg(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgBg, GameCanvas.w / 2, GameCanvas.h / 2 - Paint.hTab / 2 - 1, 3);
		g.drawImage(Paint.imgLT, 0, 0, 0);
		g.drawImage(Paint.imgRT, GameCanvas.w, 0, mGraphics.TOP | mGraphics.RIGHT);
		g.drawImage(Paint.imgLB, 0, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.LEFT);
		g.drawImage(Paint.imgRB, GameCanvas.w, GameCanvas.h - Paint.hTab - 2, mGraphics.BOTTOM | mGraphics.RIGHT);
		g.setColor(16774843);
		g.drawRect(0, 0, GameCanvas.w, 0);
		g.drawRect(0, GameCanvas.h - Paint.hTab - 2, GameCanvas.w, 0);
		g.drawRect(0, 0, 0, GameCanvas.h - Paint.hTab);
		g.drawRect(GameCanvas.w - 1, 0, 0, GameCanvas.h - Paint.hTab);
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00006032 File Offset: 0x00004232
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000045ED File Offset: 0x000027ED
	public void repaintCircleBg()
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00006051 File Offset: 0x00004251
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00006081 File Offset: 0x00004281
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000265C0 File Offset: 0x000247C0
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0002661C File Offset: 0x0002481C
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = (!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark;
		int num = 3;
		if (left != null)
		{
			Paint.lenCaption = mFont.getWidth(left.caption);
			if (Paint.lenCaption > 0)
			{
				if (left.x >= 0 && left.y > 0)
				{
					left.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 0) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, 1, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, left.caption, 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (center != null)
		{
			Paint.lenCaption = mFont.getWidth(center.caption);
			if (Paint.lenCaption > 0)
			{
				if (center.x > 0 && center.y > 0)
				{
					center.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.hw - 35, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, center.caption, GameCanvas.hw, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
		if (right != null)
		{
			Paint.lenCaption = mFont.getWidth(right.caption);
			if (Paint.lenCaption > 0)
			{
				if (right.x > 0 && right.y > 0)
				{
					right.paint(g);
				}
				else
				{
					g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
					mFont.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
				}
			}
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000060B5 File Offset: 0x000042B5
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000060CE File Offset: 0x000042CE
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00026820 File Offset: 0x00024A20
	public void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(16646144);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16770612);
		}
		else
		{
			g.setColor(16775097);
			g.fillRoundRect(x, y, w, h, 10, 10);
			g.setColor(16775097);
		}
		g.fillRoundRect(x + 3, y + 3, w - 6, h - 6, 10, 10);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x000060DE File Offset: 0x000042DE
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		if (index == 1)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0000610C File Offset: 0x0000430C
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0000611F File Offset: 0x0000431F
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000268A0 File Offset: 0x00024AA0
	public void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus)
	{
		if (focus)
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 1 : 3) * 18, 20, 18, 0, x, y, 0);
		}
		else
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 0 : 2) * 18, 20, 18, 0, x, y, 0);
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00026904 File Offset: 0x00024B04
	public void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str)
	{
		this.paintFrame(x, y, w, h, g);
		int num = y + 20 - mFont.tahoma_8b.getHeight();
		int i = 0;
		int num2 = num;
		while (i < str.Length)
		{
			mFont.tahoma_8b.drawString(g, str[i], x + w / 2, num2, 2);
			i++;
			num2 += mFont.tahoma_8b.getHeight();
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000045ED File Offset: 0x000027ED
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00006128 File Offset: 0x00004328
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00006141 File Offset: 0x00004341
	public void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss)
	{
		if (iss)
		{
			g.setColor(13132288);
			g.fillRect(x + 2, y + 2, w - 3, w - 3);
		}
		g.setColor(3502080);
		g.drawRect(x, y, w, w);
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00006180 File Offset: 0x00004380
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00006198 File Offset: 0x00004398
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x000061A0 File Offset: 0x000043A0
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0002696C File Offset: 0x00024B6C
	public void paintTextLogin(mGraphics g, bool isRes)
	{
		int num = 0;
		if (!isRes && GameCanvas.h <= 240)
		{
			num = 15;
		}
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[0], GameCanvas.hw, GameCanvas.hh + 60 - num, 2);
		mFont.tahoma_7b_green2.drawString(g, mResources.LOGINLABELS[1], GameCanvas.hw, GameCanvas.hh + 73 - num, 2);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000061D9 File Offset: 0x000043D9
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00004381 File Offset: 0x00002581
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000061EB File Offset: 0x000043EB
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x000061F2 File Offset: 0x000043F2
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x0000621B File Offset: 0x0000441B
	public string getUrlUpdateGame()
	{
		return string.Concat(new object[]
		{
			"http://wap.teamobi.com?info=checkupdate&game=3&version=",
			GameMidlet.VERSION,
			"&provider=",
			GameMidlet.PROVIDER
		});
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000045ED File Offset: 0x000027ED
	public void doSelect(int focus)
	{
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000269D8 File Offset: 0x00024BD8
	public void paintPopUp(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(9340251);
		g.drawRect(x + 18, y, (w - 36) / 2 - 32, h);
		g.drawRect(x + 18 + (w - 36) / 2 + 32, y, (w - 36) / 2 - 22, h);
		g.drawRect(x, y + 8, w, h - 17);
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x + 18, y + 3, (w - 36) / 2 - 32, h - 4);
		g.fillRect(x + 18 + (w - 36) / 2 + 31, y + 3, (w - 38) / 2 - 22, h - 4);
		g.fillRect(x + 1, y + 6, w - 1, h - 11);
		g.setColor(14667919);
		g.fillRect(x + 18, y + 1, (w - 36) / 2 - 32, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + 1, (w - 36) / 2 - 12, 2);
		g.fillRect(x + 18, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 18 + (w - 36) / 2 + 32, y + h - 2, (w - 36) / 2 - 31, 2);
		g.fillRect(x + 1, y + 11, 2, h - 18);
		g.fillRect(x + w - 2, y + 11, 2, h - 18);
		g.drawImage(Paint.goc[0], x - 3, y - 2, mGraphics.TOP | mGraphics.LEFT);
		g.drawImage(Paint.goc[2], x + w + 3, y - 2, StaticObj.TOP_RIGHT);
		g.drawImage(Paint.goc[1], x - 3, y + h + 3, StaticObj.BOTTOM_LEFT);
		g.drawImage(Paint.goc[3], x + w + 4, y + h + 2, StaticObj.BOTTOM_RIGHT);
		g.drawImage(Paint.goc[4], x + w / 2, y, StaticObj.TOP_CENTER);
		g.drawImage(Paint.goc[5], x + w / 2, y + h + 1, StaticObj.BOTTOM_HCENTER);
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00026BF8 File Offset: 0x00024DF8
	public void paintFrame(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(13524492);
		g.drawRect(x + 6, y, w - 12, h);
		g.drawRect(x, y + 6, w, h - 12);
		g.drawRect(x + 7, y + 1, w - 14, h - 2);
		g.drawRect(x + 1, y + 7, w - 2, h - 14);
		g.setColor(14338484);
		g.fillRect(x + 8, y + 2, w - 16, h - 3);
		g.fillRect(x + 2, y + 8, w - 3, h - 14);
		g.drawImage(GameCanvas.imgBorder[2], x, y, mGraphics.TOP | mGraphics.LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 2, x + w + 1, y, StaticObj.TOP_RIGHT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 1, x, y + h + 1, StaticObj.BOTTOM_LEFT);
		g.drawRegion(GameCanvas.imgBorder[2], 0, 0, 16, 16, 3, x + w + 1, y + h + 1, StaticObj.BOTTOM_RIGHT);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0000624D File Offset: 0x0000444D
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00006287 File Offset: 0x00004487
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00006296 File Offset: 0x00004496
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x000062B0 File Offset: 0x000044B0
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x040006E3 RID: 1763
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x040006E4 RID: 1764
	public static int COLORLIGHT = 16383818;

	// Token: 0x040006E5 RID: 1765
	public static int COLORDARK = 3937280;

	// Token: 0x040006E6 RID: 1766
	public static int COLORBORDER = 15224576;

	// Token: 0x040006E7 RID: 1767
	public static int COLORFOCUS = 16777215;

	// Token: 0x040006E8 RID: 1768
	public static Image imgBg;

	// Token: 0x040006E9 RID: 1769
	public static Image imgLogo;

	// Token: 0x040006EA RID: 1770
	public static Image imgLB;

	// Token: 0x040006EB RID: 1771
	public static Image imgLT;

	// Token: 0x040006EC RID: 1772
	public static Image imgRB;

	// Token: 0x040006ED RID: 1773
	public static Image imgRT;

	// Token: 0x040006EE RID: 1774
	public static Image imgChuong;

	// Token: 0x040006EF RID: 1775
	public static Image imgSelectBoard;

	// Token: 0x040006F0 RID: 1776
	public static Image imgtoiSmall;

	// Token: 0x040006F1 RID: 1777
	public static Image imgTayTren;

	// Token: 0x040006F2 RID: 1778
	public static Image imgTayDuoi;

	// Token: 0x040006F3 RID: 1779
	public static Image[] imgTick = new Image[2];

	// Token: 0x040006F4 RID: 1780
	public static Image[] imgMsg = new Image[2];

	// Token: 0x040006F5 RID: 1781
	public static Image[] goc = new Image[6];

	// Token: 0x040006F6 RID: 1782
	public static int hTab = 24;

	// Token: 0x040006F7 RID: 1783
	public static int lenCaption = 0;

	// Token: 0x040006F8 RID: 1784
	public int[] color = new int[]
	{
		15970400,
		13479911,
		2250052,
		16374659,
		15906669,
		12931125,
		3108954
	};

	// Token: 0x040006F9 RID: 1785
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}

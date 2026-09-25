using System;

// Token: 0x02000083 RID: 131
public class Paint
{
	// Token: 0x0600061E RID: 1566 RVA: 0x0005A540 File Offset: 0x00058740
	public static void loadbg()
	{
		for (int i = 0; i < Paint.goc.Length; i++)
		{
			Paint.goc[i] = GameCanvas.loadImage("/mainImage/myTexture2dgoc" + (i + 1).ToString() + ".png");
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0005A588 File Offset: 0x00058788
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

	// Token: 0x06000620 RID: 1568 RVA: 0x0005A6AD File Offset: 0x000588AD
	public void paintfillDefaultBg(mGraphics g)
	{
		g.setColor(205314);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00004887 File Offset: 0x00002A87
	public void repaintCircleBg()
	{
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintSolidBg(mGraphics g)
	{
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0005A6CC File Offset: 0x000588CC
	public void paintDefaultPopup(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(8411138);
		g.fillRect(x, y, w, h);
		g.setColor(13606712);
		g.drawRect(x, y, w, h);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0005A6FC File Offset: 0x000588FC
	public void paintWhitePopup(mGraphics g, int y, int x, int width, int height)
	{
		g.setColor(16776363);
		g.fillRect(x, y, width, height);
		g.setColor(0);
		g.drawRect(x - 1, y - 1, width + 1, height + 1);
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0005A730 File Offset: 0x00058930
	public void paintDefaultPopupH(mGraphics g, int h)
	{
		g.setColor(14279153);
		g.fillRect(8, GameCanvas.h - (h + 37), GameCanvas.w - 16, h + 4);
		g.setColor(4682453);
		g.fillRect(10, GameCanvas.h - (h + 35), GameCanvas.w - 20, h);
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0005A78C File Offset: 0x0005898C
	public void paintCmdBar(mGraphics g, Command left, Command center, Command right)
	{
		mFont mFont = ((!GameCanvas.isTouch) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_dark);
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
		if (right == null)
		{
			return;
		}
		Paint.lenCaption = mFont.getWidth(right.caption);
		if (Paint.lenCaption > 0)
		{
			if (right.x > 0 && right.y > 0)
			{
				right.paint(g);
				return;
			}
			g.drawImage((mScreen.keyTouch != 2) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, GameCanvas.w - 71, GameCanvas.h - mScreen.cmdH - 1, 0);
			mFont.drawString(g, right.caption, GameCanvas.w - 35, GameCanvas.h - mScreen.cmdH + 3 + num, 2);
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintTabSoft(mGraphics g)
	{
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0005A94B File Offset: 0x00058B4B
	public void paintSelect(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16774843);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0005A964 File Offset: 0x00058B64
	public void paintLogo(mGraphics g, int x, int y)
	{
		g.drawImage(Paint.imgLogo, x, y, 3);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintHotline(mGraphics g, string number)
	{
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0005A974 File Offset: 0x00058B74
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

	// Token: 0x0600062C RID: 1580 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check)
	{
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintDefaultScrList(mGraphics g, string title, string subTitle, string check)
	{
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0005A9EB File Offset: 0x00058BEB
	public void paintCheck(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgTick[1], x, y, 3);
		if (index == 1)
		{
			g.drawImage(Paint.imgTick[0], x + 1, y - 3, 3);
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0005AA16 File Offset: 0x00058C16
	public void paintImgMsg(mGraphics g, int x, int y, int index)
	{
		g.drawImage(Paint.imgMsg[index], x, y, 0);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0005AA29 File Offset: 0x00058C29
	public void paintTitleBoard(mGraphics g, int roomId)
	{
		this.paintDefaultBg(g);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0005AA34 File Offset: 0x00058C34
	public void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus)
	{
		if (focus)
		{
			g.drawRegion(Paint.imgCheck, 0, ((!check) ? 1 : 3) * 18, 20, 18, 0, x, y, 0);
			return;
		}
		g.drawRegion(Paint.imgCheck, 0, (check ? 2 : 0) * 18, 20, 18, 0, x, y, 0);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0005AA84 File Offset: 0x00058C84
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

	// Token: 0x06000633 RID: 1587 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool isSe, int i, int wStr)
	{
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0005AAE1 File Offset: 0x00058CE1
	public void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo)
	{
		g.setColor(16774843);
		g.drawLine(x, y, xTo, yTo);
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0005AAFA File Offset: 0x00058CFA
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

	// Token: 0x06000636 RID: 1590 RVA: 0x0005AB36 File Offset: 0x00058D36
	public void paintScroll(mGraphics g, int x, int y, int h)
	{
		g.setColor(3847752);
		g.fillRect(x, y, 4, h);
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0005AB4E File Offset: 0x00058D4E
	public int[] getColorMsg()
	{
		return this.color;
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0005AB56 File Offset: 0x00058D56
	public void paintLogo(mGraphics g)
	{
		g.setColor(8916494);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		g.drawImage(Paint.imgLogo, GameCanvas.h >> 1, GameCanvas.w >> 1, 3);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0005AB90 File Offset: 0x00058D90
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

	// Token: 0x0600063A RID: 1594 RVA: 0x0005ABF5 File Offset: 0x00058DF5
	public void paintSellectBoard(mGraphics g, int x, int y, int w, int h)
	{
		g.drawImage(Paint.imgSelectBoard, x - 7, y, 0);
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x000151BF File Offset: 0x000133BF
	public int isRegisterUsingWAP()
	{
		return 0;
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0005AC07 File Offset: 0x00058E07
	public string getCard()
	{
		return "/vmg/card.on";
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0005AC0E File Offset: 0x00058E0E
	public void paintSellectedShop(mGraphics g, int x, int y, int w, int h)
	{
		g.setColor(16777215);
		g.drawRect(x, y, 40, 40);
		g.drawRect(x + 1, y + 1, 38, 38);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0005AC37 File Offset: 0x00058E37
	public string getUrlUpdateGame()
	{
		return "http://wap.teamobi.com?info=checkupdate&game=3&version=" + GameMidlet.VERSION + "&provider=" + GameMidlet.PROVIDER.ToString();
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00004887 File Offset: 0x00002A87
	public void doSelect(int focus)
	{
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0005AC58 File Offset: 0x00058E58
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

	// Token: 0x06000641 RID: 1601 RVA: 0x0005AE78 File Offset: 0x00059078
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

	// Token: 0x06000642 RID: 1602 RVA: 0x0005AF94 File Offset: 0x00059194
	public void paintFrameSimple(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(6702080);
		g.fillRect(x, y, w, h);
		g.setColor(14338484);
		g.fillRect(x + 1, y + 1, w - 2, h - 2);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0005AFCE File Offset: 0x000591CE
	public void paintFrameBorder(int x, int y, int w, int h, mGraphics g)
	{
		this.paintFrame(x, y, w, h, g);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0005AFDD File Offset: 0x000591DD
	public void paintFrameInside(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORBACKGROUND);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0005AFF7 File Offset: 0x000591F7
	public void paintFrameInsideSelected(int x, int y, int w, int h, mGraphics g)
	{
		g.setColor(Paint.COLORLIGHT);
		g.fillRect(x, y, w, h);
	}

	// Token: 0x04000C68 RID: 3176
	public static int COLORBACKGROUND = 15787715;

	// Token: 0x04000C69 RID: 3177
	public static int COLORLIGHT = 16383818;

	// Token: 0x04000C6A RID: 3178
	public static int COLORDARK = 3937280;

	// Token: 0x04000C6B RID: 3179
	public static int COLORBORDER = 15224576;

	// Token: 0x04000C6C RID: 3180
	public static int COLORFOCUS = 16777215;

	// Token: 0x04000C6D RID: 3181
	public static Image imgBg;

	// Token: 0x04000C6E RID: 3182
	public static Image imgLogo;

	// Token: 0x04000C6F RID: 3183
	public static Image imgLB;

	// Token: 0x04000C70 RID: 3184
	public static Image imgLT;

	// Token: 0x04000C71 RID: 3185
	public static Image imgRB;

	// Token: 0x04000C72 RID: 3186
	public static Image imgRT;

	// Token: 0x04000C73 RID: 3187
	public static Image imgChuong;

	// Token: 0x04000C74 RID: 3188
	public static Image imgSelectBoard;

	// Token: 0x04000C75 RID: 3189
	public static Image imgtoiSmall;

	// Token: 0x04000C76 RID: 3190
	public static Image imgTayTren;

	// Token: 0x04000C77 RID: 3191
	public static Image imgTayDuoi;

	// Token: 0x04000C78 RID: 3192
	public static Image[] imgTick = new Image[2];

	// Token: 0x04000C79 RID: 3193
	public static Image[] imgMsg = new Image[2];

	// Token: 0x04000C7A RID: 3194
	public static Image[] goc = new Image[6];

	// Token: 0x04000C7B RID: 3195
	public static int hTab = 24;

	// Token: 0x04000C7C RID: 3196
	public static int lenCaption = 0;

	// Token: 0x04000C7D RID: 3197
	public int[] color = new int[] { 15970400, 13479911, 2250052, 16374659, 15906669, 12931125, 3108954 };

	// Token: 0x04000C7E RID: 3198
	public static Image imgCheck = GameCanvas.loadImage("/mainImage/myTexture2dcheck.png");
}

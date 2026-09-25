using System;

// Token: 0x02000020 RID: 32
public class Command
{
	// Token: 0x060001CF RID: 463 RVA: 0x0001A620 File Offset: 0x00018820
	public Command(string caption, IActionListener actionListener, int action, object p, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0001A688 File Offset: 0x00018888
	public Command()
	{
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0001A6B8 File Offset: 0x000188B8
	public Command(string caption, IActionListener actionListener, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.actionListener = actionListener;
		this.p = p;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0001A710 File Offset: 0x00018910
	public Command(string caption, int action, object p)
	{
		this.caption = caption;
		this.idAction = action;
		this.p = p;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0001A760 File Offset: 0x00018960
	public Command(string caption, int action)
	{
		this.caption = caption;
		this.idAction = action;
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0001A7A0 File Offset: 0x000189A0
	public Command(string caption, int action, int x, int y)
	{
		this.caption = caption;
		this.idAction = action;
		this.x = x;
		this.y = y;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0001A7F8 File Offset: 0x000189F8
	public void perform(string str)
	{
		if (this.actionChat != null)
		{
			this.actionChat(str);
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0001A810 File Offset: 0x00018A10
	public void performAction()
	{
		GameCanvas.clearAllPointerEvent();
		if (this.isPlaySoundButton && ((this.caption != null && !this.caption.Equals(string.Empty) && !this.caption.Equals(mResources.saying)) || this.img != null))
		{
			SoundMn.gI().buttonClick();
		}
		if (this.idAction > 0)
		{
			if (this.actionListener != null)
			{
				this.actionListener.perform(this.idAction, this.p);
				return;
			}
			GameScr.gI().actionPerform(this.idAction, this.p);
		}
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0001A8A7 File Offset: 0x00018AA7
	public void setType()
	{
		this.type = 1;
		this.w = 160;
		this.hw = 80;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0001A8C4 File Offset: 0x00018AC4
	public void paint(mGraphics g)
	{
		if (this.img != null)
		{
			g.drawImage(this.img, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
			if (this.isFocus)
			{
				if (this.imgFocus == null)
				{
					if (this.cmdClosePanel)
					{
						g.drawImage(ItemMap.imageFlare, this.x + 8, this.y + mGraphics.addYWhenOpenKeyBoard + 8, 3);
					}
					else
					{
						g.drawImage(ItemMap.imageFlare, this.x - (this.img.Equals(GameScr.imgMenu) ? 10 : 0), this.y + mGraphics.addYWhenOpenKeyBoard, 0);
					}
				}
				else
				{
					g.drawImage(this.imgFocus, this.x, this.y + mGraphics.addYWhenOpenKeyBoard, 0);
				}
			}
			if (this.caption != "menu" && this.caption != null)
			{
				if (!this.isFocus)
				{
					mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
					return;
				}
				mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + mGraphics.getImageWidth(this.img) / 2, this.y + mGraphics.getImageHeight(this.img) / 2 - 5, 2);
			}
			return;
		}
		if (this.caption != string.Empty)
		{
			if (!this.isFocus)
			{
				Command.paintOngMau(Command.btn0left, Command.btn0mid, Command.btn0right, this.x, this.y, this.w, g);
			}
			else
			{
				Command.paintOngMau(Command.btn1left, Command.btn1mid, Command.btn1right, this.x, this.y, this.w, g);
			}
		}
		if (!this.isFocus)
		{
			mFont.tahoma_7b_dark.drawString(g, this.caption, this.x + this.w / 2, this.y + 7, 2);
			return;
		}
		mFont.tahoma_7b_green2.drawString(g, this.caption, this.x + this.w / 2, this.y + 7, 2);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0001AAF8 File Offset: 0x00018CF8
	public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
	{
		for (int i = 10; i <= size - 20; i += 10)
		{
			g.drawImage(img1, x + i, y, 0);
		}
		int num = size % 10;
		if (num > 0)
		{
			g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img2, x + size - 10, y, 0);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0001AB68 File Offset: 0x00018D68
	public bool isPointerPressInside()
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x, this.y, this.w, this.h))
		{
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0001ABBC File Offset: 0x00018DBC
	public bool isPointerPressInsideCamera(int cmx, int cmy)
	{
		this.isFocus = false;
		if (GameCanvas.isPointerHoldIn(this.x - cmx, this.y - cmy, this.w, this.h))
		{
			Res.outz("w= " + this.w.ToString());
			if (GameCanvas.isPointerDown)
			{
				this.isFocus = true;
			}
			if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000475 RID: 1141
	public bool isDisplay;

	// Token: 0x04000476 RID: 1142
	public ActionChat actionChat;

	// Token: 0x04000477 RID: 1143
	public string caption;

	// Token: 0x04000478 RID: 1144
	public string[] subCaption;

	// Token: 0x04000479 RID: 1145
	public IActionListener actionListener;

	// Token: 0x0400047A RID: 1146
	public int idAction;

	// Token: 0x0400047B RID: 1147
	public bool isPlaySoundButton = true;

	// Token: 0x0400047C RID: 1148
	public Image img;

	// Token: 0x0400047D RID: 1149
	public Image imgFocus;

	// Token: 0x0400047E RID: 1150
	public int x;

	// Token: 0x0400047F RID: 1151
	public int y;

	// Token: 0x04000480 RID: 1152
	public int w = mScreen.cmdW;

	// Token: 0x04000481 RID: 1153
	public int h = mScreen.cmdH;

	// Token: 0x04000482 RID: 1154
	public int hw;

	// Token: 0x04000483 RID: 1155
	internal int lenCaption;

	// Token: 0x04000484 RID: 1156
	public bool isFocus;

	// Token: 0x04000485 RID: 1157
	public object p;

	// Token: 0x04000486 RID: 1158
	public int type;

	// Token: 0x04000487 RID: 1159
	public string caption2 = string.Empty;

	// Token: 0x04000488 RID: 1160
	public static Image btn0left;

	// Token: 0x04000489 RID: 1161
	public static Image btn0mid;

	// Token: 0x0400048A RID: 1162
	public static Image btn0right;

	// Token: 0x0400048B RID: 1163
	public static Image btn1left;

	// Token: 0x0400048C RID: 1164
	public static Image btn1mid;

	// Token: 0x0400048D RID: 1165
	public static Image btn1right;

	// Token: 0x0400048E RID: 1166
	public bool cmdClosePanel;

	// Token: 0x0400048F RID: 1167
	public bool isPaintNew;
}

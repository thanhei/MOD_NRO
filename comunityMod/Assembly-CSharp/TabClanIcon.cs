using System;

// Token: 0x020000B5 RID: 181
public class TabClanIcon : IActionListener
{
	// Token: 0x0600098F RID: 2447 RVA: 0x0008A71C File Offset: 0x0008891C
	public TabClanIcon()
	{
		this.left = new Command(mResources.SELECT, this, 1, null);
		this.right = new Command(mResources.CLOSE, this, 2, null);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0008A770 File Offset: 0x00088970
	public void init()
	{
		if (this.isGetName)
		{
			this.w = 170;
			this.h = 118;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
		}
		else
		{
			this.w = 170;
			this.h = 170;
			this.x = GameCanvas.w / 2 - this.w / 2;
			this.y = GameCanvas.h / 2 - this.h / 2;
			if (GameCanvas.h < 240)
			{
				this.y -= 10;
			}
		}
		this.cmx = this.x;
		this.cmtoX = 0;
		if (!this.isRequest)
		{
			this.nItem = ClanImage.vClanImage.size();
		}
		else
		{
			this.nItem = this.vItems.size();
		}
		if (GameCanvas.isTouch)
		{
			this.left.x = this.x;
			this.left.y = this.y + this.h + 5;
			this.right.x = this.x + this.w - 68;
			this.right.y = this.y + this.h + 5;
		}
		TabClanIcon.scrMain = new Scroll();
		TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0008A910 File Offset: 0x00088B10
	public void show(bool isGetName)
	{
		if (global::Char.myCharz().clan != null)
		{
			this.isUpdate = true;
		}
		this.isShow = true;
		this.isGetName = isGetName;
		this.init();
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0008A939 File Offset: 0x00088B39
	public void showRequest(int msgID)
	{
		this.isShow = true;
		this.isRequest = true;
		this.msgID = msgID;
		this.init();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0008A956 File Offset: 0x00088B56
	public void hide()
	{
		this.cmtoX = this.x + this.w;
		SmallImage.clearHastable();
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00004887 File Offset: 0x00002A87
	public void paintPeans(mGraphics g)
	{
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0008A970 File Offset: 0x00088B70
	public void paintIcon(mGraphics g)
	{
		g.translate(-this.cmx, 0);
		PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
		if (this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
			if (clanImage.idImage != null)
			{
				global::Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
			}
		}
		global::Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, global::Char.myCharz().cf, false);
		g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
		if (TabClanIcon.scrMain != null)
		{
			g.translate(0, -TabClanIcon.scrMain.cmy);
		}
		for (int i = 0; i < this.nItem; i++)
		{
			int num = this.x + 10;
			int num2 = this.y + i * this.WIDTH + this.disStart;
			if (num2 + this.WIDTH - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) <= this.y + this.disStart + this.h)
			{
				ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
				mFont mFont = mFont.tahoma_7_grey;
				if (i == this.lastSelect)
				{
					mFont = mFont.tahoma_7_blue;
				}
				if (clanImage2.name != null)
				{
					mFont.drawString(g, clanImage2.name, num + 20, num2, 0);
				}
				if (clanImage2.xu > 0)
				{
					mFont.drawString(g, clanImage2.xu.ToString() + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
				}
				else if (clanImage2.luong > 0)
				{
					mFont.drawString(g, clanImage2.luong.ToString() + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
				}
				if (clanImage2.idImage != null)
				{
					SmallImage.drawSmallImage(g, (int)clanImage2.idImage[0], num, num2, 0, 0);
				}
			}
		}
		g.translate(0, -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0008AC5A File Offset: 0x00088E5A
	public void paint(mGraphics g)
	{
		if (!this.isRequest)
		{
			this.paintIcon(g);
			return;
		}
		this.paintPeans(g);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0008AC74 File Offset: 0x00088E74
	public void update()
	{
		if (TabClanIcon.scrMain != null)
		{
			TabClanIcon.scrMain.updatecm();
		}
		if (this.cmx != this.cmtoX)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		if (Math2.abs(this.cmtoX - this.cmx) < 10)
		{
			this.cmx = this.cmtoX;
		}
		if (this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10)
		{
			this.isShow = false;
		}
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0008AD48 File Offset: 0x00088F48
	public void updateKey()
	{
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			this.left.performAction();
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			this.right.performAction();
		}
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			this.center.performAction();
		}
		if (!this.isGetName)
		{
			if (TabClanIcon.scrMain == null)
			{
				return;
			}
			if (GameCanvas.isTouch)
			{
				TabClanIcon.scrMain.updateKey();
				this.select = TabClanIcon.scrMain.selectedItem;
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
				this.select--;
				if (this.select < 0)
				{
					this.select = this.nItem - 1;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				this.select++;
				if (this.select > this.nItem - 1)
				{
					this.select = 0;
				}
				TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
			}
			if (this.select != -1)
			{
				this.lastSelect = this.select;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0008AF00 File Offset: 0x00089100
	public void perform(int idAction, object p)
	{
		if (idAction == 2)
		{
			this.hide();
		}
		if (idAction != 1 || this.isGetName)
		{
			return;
		}
		if (!this.isRequest)
		{
			if (this.lastSelect >= 0)
			{
				this.hide();
				if (global::Char.myCharz().clan == null)
				{
					Service.gI().getClan(2, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
					return;
				}
				Service.gI().getClan(4, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
				return;
			}
		}
		else if (this.lastSelect >= 0)
		{
			Item item = (Item)this.vItems.elementAt(this.select);
		}
	}

	// Token: 0x040010A4 RID: 4260
	internal int x;

	// Token: 0x040010A5 RID: 4261
	internal int y;

	// Token: 0x040010A6 RID: 4262
	internal int w;

	// Token: 0x040010A7 RID: 4263
	internal int h;

	// Token: 0x040010A8 RID: 4264
	internal Command left;

	// Token: 0x040010A9 RID: 4265
	internal Command right;

	// Token: 0x040010AA RID: 4266
	internal Command center;

	// Token: 0x040010AB RID: 4267
	internal int WIDTH = 24;

	// Token: 0x040010AC RID: 4268
	public int nItem;

	// Token: 0x040010AD RID: 4269
	internal int disStart = 50;

	// Token: 0x040010AE RID: 4270
	public static Scroll scrMain;

	// Token: 0x040010AF RID: 4271
	public int cmtoX;

	// Token: 0x040010B0 RID: 4272
	public int cmx;

	// Token: 0x040010B1 RID: 4273
	public int cmvx;

	// Token: 0x040010B2 RID: 4274
	public int cmdx;

	// Token: 0x040010B3 RID: 4275
	public bool isShow;

	// Token: 0x040010B4 RID: 4276
	public bool isGetName;

	// Token: 0x040010B5 RID: 4277
	public string text;

	// Token: 0x040010B6 RID: 4278
	internal bool isRequest;

	// Token: 0x040010B7 RID: 4279
	internal bool isUpdate;

	// Token: 0x040010B8 RID: 4280
	public MyVector vItems = new MyVector();

	// Token: 0x040010B9 RID: 4281
	internal int msgID;

	// Token: 0x040010BA RID: 4282
	internal int select;

	// Token: 0x040010BB RID: 4283
	internal int lastSelect;

	// Token: 0x040010BC RID: 4284
	internal ScrollResult sr;
}

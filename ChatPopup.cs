using System;

// Token: 0x02000037 RID: 55
public class ChatPopup : Effect2, IActionListener
{
	// Token: 0x06000248 RID: 584 RVA: 0x0000547B File Offset: 0x0000367B
	public static void addNextPopUpMultiLine(string strNext, Npc next)
	{
		ChatPopup.nextMultiChatPopUp = strNext;
		ChatPopup.nextChar = next;
		if (ChatPopup.currChatPopup == null)
		{
			ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
			ChatPopup.nextMultiChatPopUp = null;
			ChatPopup.nextChar = null;
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x000045ED File Offset: 0x000027ED
	public static void addBigMessage(string chat, int howLong, Npc c)
	{
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000054B3 File Offset: 0x000036B3
	public static void addChatPopupMultiLine(string chat, int howLong, Npc c)
	{
		GameScr.info1.addInfo(chat.Substring(32), 0);
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00016044 File Offset: 0x00014244
	public static ChatPopup addChatPopupWithIcon(string chat, int howLong, Npc c, int idIcon)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - ((!GameCanvas.menu.showMenu) ? 0 : GameCanvas.menu.menuX);
		if (chatPopup.sayWidth > 320)
		{
			chatPopup.sayWidth = 320;
		}
		if (chat.Length < 10)
		{
			chatPopup.sayWidth = 64;
		}
		if (GameCanvas.w == 128)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		chatPopup.iconID = idIcon;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		if (chatPopup.ch > GameCanvas.h - 80)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		if (GameCanvas.menu.showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		if (c != null && c.charID == 5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		chatPopup.ch += 15;
		return chatPopup;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x000161C4 File Offset: 0x000143C4
	public static ChatPopup addChatPopup(string chat, int howLong, Npc c)
	{
		ChatPopup.performDelay = 10;
		ChatPopup chatPopup = new ChatPopup();
		chatPopup.sayWidth = GameCanvas.w - 30 - ((!GameCanvas.menu.showMenu) ? 0 : GameCanvas.menu.menuX);
		if (chatPopup.sayWidth > 320)
		{
			chatPopup.sayWidth = 320;
		}
		if (chat.Length < 10)
		{
			chatPopup.sayWidth = 64;
		}
		if (GameCanvas.w == 128)
		{
			chatPopup.sayWidth = 128;
		}
		chatPopup.says = mFont.tahoma_7_red.splitFontArray(chat, chatPopup.sayWidth - 10);
		chatPopup.delay = howLong;
		chatPopup.c = c;
		global::Char.chatPopup = chatPopup;
		chatPopup.ch = 15 - chatPopup.sayRun + chatPopup.says.Length * 12 + 10;
		if (chatPopup.ch > GameCanvas.h - 80)
		{
			chatPopup.ch = GameCanvas.h - 80;
		}
		chatPopup.mH = 10;
		if (GameCanvas.menu.showMenu)
		{
			chatPopup.mH = 0;
		}
		Effect2.vEffect2.addElement(chatPopup);
		ChatPopup.isHavePetNpc = false;
		if (c != null && c.charID == 5)
		{
			ChatPopup.isHavePetNpc = true;
			GameScr.info1.addInfo(string.Empty, 1);
		}
		ChatPopup.curr = (ChatPopup.last = mSystem.currentTimeMillis());
		return chatPopup;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0001632C File Offset: 0x0001452C
	public override void update()
	{
		if (ChatPopup.scr != null)
		{
			GameScr.info1.isUpdate = false;
			ChatPopup.scr.updatecm();
		}
		else
		{
			GameScr.info1.isUpdate = true;
		}
		if (GameCanvas.menu.showMenu)
		{
			this.strY = 0;
			this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
			this.cy = GameCanvas.menu.menuY - this.ch;
		}
		else
		{
			this.strY = 0;
			if (GameScr.gI().right != null || GameScr.gI().left != null || GameScr.gI().center != null || this.cmdNextLine != null || this.cmdMsg1 != null)
			{
				this.strY = 5;
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 20 - this.ch;
			}
			else
			{
				this.cx = GameCanvas.w / 2 - this.sayWidth / 2 - 1;
				this.cy = GameCanvas.h - 5 - this.ch;
			}
		}
		if (this.delay > 0)
		{
			this.delay--;
		}
		if (ChatPopup.performDelay > 0)
		{
			ChatPopup.performDelay--;
		}
		else
		{
			GameScr.info1.info.time = 0;
			for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
			{
				if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed != 70)
				{
					((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
				}
			}
		}
		if (this.sayRun > 1)
		{
			this.sayRun--;
		}
		if ((this.c != null && global::Char.chatPopup != null && global::Char.chatPopup != this) || (this.c != null && global::Char.chatPopup == null) || this.delay <= 0)
		{
			Effect2.vEffect2Outside.removeElement(this);
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00016584 File Offset: 0x00014784
	public override void paint(mGraphics g)
	{
		if (GameScr.gI().activeRongThan && GameScr.gI().isUseFreez)
		{
			return;
		}
		GameCanvas.resetTrans(g);
		int num = this.cx;
		int num2 = this.cy;
		int num3 = this.sayWidth + 2;
		int num4 = this.ch;
		if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
		{
			return;
		}
		if (this.c != null)
		{
			int num5;
			if (GameCanvas.gameTick % 10 > 2)
			{
				num5 = 0;
			}
			else
			{
				num5 = 1;
			}
			SmallImage.drawSmallImage(g, this.c.avatar, this.cx + 14, this.cy + num5, 0, StaticObj.BOTTOM_LEFT);
		}
		if (this.iconID != 0)
		{
			int num5;
			if (GameCanvas.gameTick % 10 > 2)
			{
				num5 = 0;
			}
			else
			{
				num5 = 1;
			}
			SmallImage.drawSmallImage(g, this.iconID, this.cx + num3 / 2, this.cy + this.ch - 15 + num5, 0, StaticObj.VCENTER_HCENTER);
		}
		PopUp.paintPopUp(g, num, num2, num3, num4, 16777215, false);
		if (ChatPopup.scr != null)
		{
			g.setClip(num, num2, num3, num4 - 16);
			g.translate(0, -ChatPopup.scr.cmy);
		}
		int tx = 0;
		int ty = 0;
		if (this.isClip)
		{
			tx = g.getTranslateX();
			ty = g.getTranslateY();
			g.setClip(num, num2 + 1, num3, num4 - 17);
			g.translate(0, -ChatPopup.cmyText);
		}
		int num6 = -1;
		for (int i = 0; i < this.says.Length; i++)
		{
			if (!this.says[i].StartsWith("--"))
			{
				mFont mFont = mFont.tahoma_7;
				int num7 = 2;
				string st = this.says[i];
				int num8;
				if (this.says[i].StartsWith("|"))
				{
					string[] array = Res.split(this.says[i], "|", 0);
					if (array.Length == 3)
					{
						st = array[2];
					}
					if (array.Length == 4)
					{
						st = array[3];
						num7 = int.Parse(array[2]);
					}
					num8 = int.Parse(array[1]);
					num6 = num8;
				}
				else
				{
					num8 = num6;
				}
				switch (num8 + 1)
				{
				case 0:
					mFont = mFont.tahoma_7;
					break;
				case 1:
					mFont = mFont.tahoma_7b_dark;
					break;
				case 2:
					mFont = mFont.tahoma_7b_green;
					break;
				case 3:
					mFont = mFont.tahoma_7b_blue;
					break;
				case 4:
					mFont = mFont.tahoma_7_red;
					break;
				case 5:
					mFont = mFont.tahoma_7_green;
					break;
				case 6:
					mFont = mFont.tahoma_7_blue;
					break;
				case 8:
					mFont = mFont.tahoma_7b_red;
					break;
				case 9:
					mFont = mFont.tahoma_7b_yellow;
					break;
				}
				IL_2FD:
				if (this.says[i].StartsWith("<"))
				{
					string[] array2 = Res.split(this.says[i], "<", 0);
					string[] array3 = Res.split(array2[1], ">", 1);
					if (this.second == 0)
					{
						this.second = int.Parse(array3[1]);
					}
					else
					{
						ChatPopup.curr = mSystem.currentTimeMillis();
						if (ChatPopup.curr - ChatPopup.last >= 1000L)
						{
							ChatPopup.last = ChatPopup.curr;
							this.second--;
						}
					}
					st = this.second + " " + array3[2];
					mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
					goto IL_46B;
				}
				if (num7 == 2)
				{
					mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
				}
				if (num7 == 1)
				{
					mFont.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY + 12, num7);
					goto IL_46B;
				}
				goto IL_46B;
				goto IL_2FD;
			}
			g.setColor(0);
			g.fillRect(num + 10, this.cy + this.sayRun + i * 12 + 6, num3 - 20, 1);
			IL_46B:;
		}
		if (this.isClip)
		{
			GameCanvas.resetTrans(g);
			g.translate(tx, ty);
		}
		if ((int)this.maxStarSlot > 4)
		{
			this.nMaxslot_tren = ((int)this.maxStarSlot + 1) / 2;
			this.nMaxslot_duoi = (int)this.maxStarSlot - this.nMaxslot_tren;
			int[] array4 = new int[(int)this.maxStarSlot];
			int[] array5 = new int[(int)this.maxStarSlot];
			for (int j = 0; j < this.nMaxslot_tren; j++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 17, 3);
				array4[j] = num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + j * 20 + mGraphics.getImageWidth(Panel.imgMaxStar);
				array5[j] = num2 + num4 - 17;
			}
			for (int k = 0; k < this.nMaxslot_duoi; k++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 8, 3);
				array4[this.nMaxslot_tren + k] = num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + k * 20 + mGraphics.getImageWidth(Panel.imgMaxStar);
				array5[this.nMaxslot_tren + k] = num2 + num4 - 8;
			}
			if ((int)this.maxStarSlot >= 7)
			{
				for (int l = 7; l < (int)this.maxStarSlot; l++)
				{
					if (this.starCuongHoa[l])
					{
						g.drawImage(Panel.imgStarCuongHoa, array4[l], array5[l], 3);
					}
				}
			}
			if ((int)this.starSlot > 0)
			{
				this.imgStar = Panel.imgStar;
				if ((int)this.starSlot >= this.nMaxslot_tren)
				{
					this.nslot_duoi = (int)this.starSlot - this.nMaxslot_tren;
					for (int m = 0; m < this.nMaxslot_tren; m++)
					{
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + m * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
					}
					for (int n = 0; n < this.nslot_duoi; n++)
					{
						if (n + this.nMaxslot_tren >= ChatPopup.numSlot)
						{
							this.imgStar = Panel.imgStar8;
						}
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_duoi * 20 / 2 + n * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 8, 3);
					}
				}
				else
				{
					for (int num9 = 0; num9 < (int)this.starSlot; num9++)
					{
						g.drawImage(this.imgStar, num + num3 / 2 - this.nMaxslot_tren * 20 / 2 + num9 * 20 + mGraphics.getImageWidth(this.imgStar), num2 + num4 - 17, 3);
					}
				}
			}
		}
		else
		{
			for (int num10 = 0; num10 < (int)this.maxStarSlot; num10++)
			{
				g.drawImage(Panel.imgMaxStar, num + num3 / 2 - (int)this.maxStarSlot * 20 / 2 + num10 * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), num2 + num4 - 13, 3);
			}
			if ((int)this.starSlot > 0)
			{
				for (int num11 = 0; num11 < (int)this.starSlot; num11++)
				{
					g.drawImage(Panel.imgStar, num + num3 / 2 - (int)this.maxStarSlot * 20 / 2 + num11 * 20 + mGraphics.getImageWidth(Panel.imgStar), num2 + num4 - 13, 3);
				}
			}
		}
		this.paintCmd(g);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00016DE8 File Offset: 0x00014FE8
	public void paintRada(mGraphics g, int cmyText)
	{
		int num = this.cx;
		int num2 = this.cy;
		int num3 = this.sayWidth;
		int num4 = this.ch;
		int translateX = g.getTranslateX();
		int translateY = g.getTranslateY();
		g.translate(0, -cmyText);
		if ((num <= 0 || num2 <= 0) && !GameCanvas.panel.isShow)
		{
			return;
		}
		int num5 = -1;
		for (int i = 0; i < this.says.Length; i++)
		{
			if (this.says[i].StartsWith("--"))
			{
				g.setColor(16777215);
				g.fillRect(num + 10, this.cy + this.sayRun + i * 12 - 6, num3 - 20, 1);
			}
			else
			{
				mFont mFont = mFont.tahoma_7_white;
				int num6 = 2;
				string st = this.says[i];
				int num7;
				if (this.says[i].StartsWith("|"))
				{
					string[] array = Res.split(this.says[i], "|", 0);
					if (array.Length == 3)
					{
						st = array[2];
					}
					if (array.Length == 4)
					{
						st = array[3];
						num6 = int.Parse(array[2]);
					}
					num7 = int.Parse(array[1]);
					num5 = num7;
				}
				else
				{
					num7 = num5;
				}
				switch (num7 + 1)
				{
				case 0:
					mFont = mFont.tahoma_7_white;
					break;
				case 1:
					mFont = mFont.tahoma_7b_white;
					break;
				case 2:
					mFont = mFont.tahoma_7b_green;
					break;
				case 3:
					mFont = mFont.tahoma_7b_red;
					break;
				}
				if (this.says[i].StartsWith("<"))
				{
					string[] array2 = Res.split(this.says[i], "<", 0);
					string[] array3 = Res.split(array2[1], ">", 1);
					if (this.second == 0)
					{
						this.second = int.Parse(array3[1]);
					}
					else
					{
						ChatPopup.curr = mSystem.currentTimeMillis();
						if (ChatPopup.curr - ChatPopup.last >= 1000L)
						{
							ChatPopup.last = ChatPopup.curr;
							this.second--;
						}
					}
					st = this.second + " " + array3[2];
					mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
				}
				else
				{
					if (num6 == 2)
					{
						mFont.drawString(g, st, this.cx + this.sayWidth / 2, this.cy + this.sayRun + i * 12 - this.strY, num6);
					}
					if (num6 == 1)
					{
						mFont.drawString(g, st, this.cx + this.sayWidth - 5, this.cy + this.sayRun + i * 12 - this.strY, num6);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		g.translate(translateX, translateY);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000054C8 File Offset: 0x000036C8
	private void doKeyText(int type)
	{
		ChatPopup.cmyText += 12 * type;
		if (ChatPopup.cmyText < 0)
		{
			ChatPopup.cmyText = 0;
		}
		if (ChatPopup.cmyText > this.lim)
		{
			ChatPopup.cmyText = this.lim;
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0001710C File Offset: 0x0001530C
	public void updateKey()
	{
		if (this.isClip)
		{
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
			if (GameCanvas.isPointerHoldIn(this.cx, 0, this.sayWidth + 2, this.ch))
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
						ChatPopup.cmyText += this.pxx;
						this.pyy = GameCanvas.py;
					}
					if (ChatPopup.cmyText < 0)
					{
						ChatPopup.cmyText = 0;
					}
					if (ChatPopup.cmyText > this.lim)
					{
						ChatPopup.cmyText = this.lim;
					}
				}
				else
				{
					this.pyy = 0;
					this.pyy = 0;
				}
			}
		}
		if (ChatPopup.scr != null)
		{
			if (GameCanvas.isTouch)
			{
				ChatPopup.scr.updateKey();
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				ChatPopup.scr.cmtoY -= 12;
				if (ChatPopup.scr.cmtoY < 0)
				{
					ChatPopup.scr.cmtoY = 0;
				}
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
				ChatPopup.scr.cmtoY += 12;
				if (ChatPopup.scr.cmtoY > ChatPopup.scr.cmyLim)
				{
					ChatPopup.scr.cmtoY = ChatPopup.scr.cmyLim;
				}
			}
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			if (this.cmdNextLine != null)
			{
				this.cmdNextLine.performAction();
			}
			else if (this.cmdMsg1 != null)
			{
				this.cmdMsg1.performAction();
			}
			else if (this.cmdMsg2 != null)
			{
				this.cmdMsg2.performAction();
			}
		}
		if (ChatPopup.scr != null && ChatPopup.scr.pointerIsDowning)
		{
			return;
		}
		if (this.cmdMsg1 != null && (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.cmdMsg1)))
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			this.cmdMsg1.performAction();
			mScreen.keyTouch = -1;
		}
		if (this.cmdMsg2 != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.cmdMsg2)))
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			this.cmdMsg2.performAction();
			mScreen.keyTouch = -1;
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x000174C0 File Offset: 0x000156C0
	public void paintCmd(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		if (this.cmdNextLine != null)
		{
			GameCanvas.paintz.paintCmdBar(g, null, this.cmdNextLine, null);
		}
		if (this.cmdMsg1 != null)
		{
			GameCanvas.paintz.paintCmdBar(g, this.cmdMsg1, null, this.cmdMsg2);
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00017540 File Offset: 0x00015740
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			try
			{
				GameMidlet.instance.platformRequest((string)p);
			}
			catch (Exception ex)
			{
			}
			if (!Main.isPC)
			{
				GameMidlet.instance.notifyDestroyed();
			}
			else
			{
				idAction = 1001;
			}
			GameCanvas.endDlg();
		}
		if (idAction == 1001)
		{
			ChatPopup.scr = null;
			global::Char.chatPopup = null;
			ChatPopup.serverChatPopUp = null;
			GameScr.info1.isUpdate = true;
			global::Char.isLockKey = false;
			if (ChatPopup.isHavePetNpc)
			{
				GameScr.info1.info.time = 0;
				GameScr.info1.info.info.speed = 10;
			}
		}
		if (idAction == 8000)
		{
			if (ChatPopup.performDelay > 0)
			{
				return;
			}
			int num = ChatPopup.currChatPopup.currentLine;
			num++;
			if (num >= ChatPopup.currChatPopup.lines.Length)
			{
				global::Char.chatPopup = null;
				ChatPopup.currChatPopup = null;
				GameScr.info1.isUpdate = true;
				global::Char.isLockKey = false;
				if (ChatPopup.nextMultiChatPopUp != null)
				{
					ChatPopup.addChatPopupMultiLine(ChatPopup.nextMultiChatPopUp, 100000, ChatPopup.nextChar);
					ChatPopup.nextMultiChatPopUp = null;
					ChatPopup.nextChar = null;
				}
				else if (ChatPopup.isHavePetNpc)
				{
					GameScr.info1.info.time = 0;
					for (int i = 0; i < GameScr.info1.info.infoWaitToShow.size(); i++)
					{
						if (((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed == 10000000)
						{
							((InfoItem)GameScr.info1.info.infoWaitToShow.elementAt(i)).speed = 10;
						}
					}
				}
				return;
			}
			ChatPopup chatPopup = ChatPopup.addChatPopup(ChatPopup.currChatPopup.lines[num], ChatPopup.currChatPopup.delay, ChatPopup.currChatPopup.c);
			chatPopup.currentLine = num;
			chatPopup.lines = ChatPopup.currChatPopup.lines;
			chatPopup.cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
			ChatPopup.currChatPopup = chatPopup;
		}
	}

	// Token: 0x04000269 RID: 617
	public int sayWidth = 100;

	// Token: 0x0400026A RID: 618
	public int delay;

	// Token: 0x0400026B RID: 619
	public int sayRun;

	// Token: 0x0400026C RID: 620
	public string[] says;

	// Token: 0x0400026D RID: 621
	public int cx;

	// Token: 0x0400026E RID: 622
	public int cy;

	// Token: 0x0400026F RID: 623
	public int ch;

	// Token: 0x04000270 RID: 624
	public int cmx;

	// Token: 0x04000271 RID: 625
	public int cmy;

	// Token: 0x04000272 RID: 626
	public int lim;

	// Token: 0x04000273 RID: 627
	public Npc c;

	// Token: 0x04000274 RID: 628
	private bool outSide;

	// Token: 0x04000275 RID: 629
	public static long curr;

	// Token: 0x04000276 RID: 630
	public static long last;

	// Token: 0x04000277 RID: 631
	private int currentLine;

	// Token: 0x04000278 RID: 632
	private string[] lines;

	// Token: 0x04000279 RID: 633
	public Command cmdNextLine;

	// Token: 0x0400027A RID: 634
	public Command cmdMsg1;

	// Token: 0x0400027B RID: 635
	public Command cmdMsg2;

	// Token: 0x0400027C RID: 636
	public static ChatPopup currChatPopup;

	// Token: 0x0400027D RID: 637
	public static ChatPopup serverChatPopUp;

	// Token: 0x0400027E RID: 638
	public static string nextMultiChatPopUp;

	// Token: 0x0400027F RID: 639
	public static Npc nextChar;

	// Token: 0x04000280 RID: 640
	public bool isShopDetail;

	// Token: 0x04000281 RID: 641
	public sbyte starSlot;

	// Token: 0x04000282 RID: 642
	public sbyte maxStarSlot;

	// Token: 0x04000283 RID: 643
	public static Scroll scr;

	// Token: 0x04000284 RID: 644
	public static bool isHavePetNpc;

	// Token: 0x04000285 RID: 645
	public int mH;

	// Token: 0x04000286 RID: 646
	public static int performDelay;

	// Token: 0x04000287 RID: 647
	public int dx;

	// Token: 0x04000288 RID: 648
	public int dy;

	// Token: 0x04000289 RID: 649
	public int second;

	// Token: 0x0400028A RID: 650
	private Point[] saoPoint = new Point[14];

	// Token: 0x0400028B RID: 651
	private int indexStar;

	// Token: 0x0400028C RID: 652
	private int indexStar2;

	// Token: 0x0400028D RID: 653
	public bool[] starCuongHoa = new bool[20];

	// Token: 0x0400028E RID: 654
	public static int numSlot = 7;

	// Token: 0x0400028F RID: 655
	private int nMaxslot_duoi;

	// Token: 0x04000290 RID: 656
	private int nMaxslot_tren;

	// Token: 0x04000291 RID: 657
	private int nslot_duoi;

	// Token: 0x04000292 RID: 658
	private Image imgStar;

	// Token: 0x04000293 RID: 659
	public int strY;

	// Token: 0x04000294 RID: 660
	private int iconID;

	// Token: 0x04000295 RID: 661
	public bool isClip;

	// Token: 0x04000296 RID: 662
	public static int cmyText;

	// Token: 0x04000297 RID: 663
	private int pxx;

	// Token: 0x04000298 RID: 664
	private int pyy;
}

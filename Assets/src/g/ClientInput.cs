using System;

namespace Assets.src.g
{
	// Token: 0x020000A5 RID: 165
	public class ClientInput : mScreen, IActionListener
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0005D678 File Offset: 0x0005B878
		private void init(string t)
		{
			this.w = GameCanvas.w - 20;
			if (this.w > 320)
			{
				this.w = 320;
			}
			Res.outz("title= " + t);
			this.strPaint = mFont.tahoma_7b_dark.splitFontArray(t, this.w - 20);
			this.x = (GameCanvas.w - this.w) / 2;
			this.tf = new TField[this.nTf];
			this.h = this.tf.Length * 35 + (this.strPaint.Length - 1) * 20 + 40;
			this.y = GameCanvas.h - this.h - 40 - (this.strPaint.Length - 1) * 20;
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i] = new TField();
				this.tf[i].name = string.Empty;
				this.tf[i].x = this.x + 10;
				this.tf[i].y = this.y + 35 + (this.strPaint.Length - 1) * 20 + i * 35;
				this.tf[i].width = this.w - 20;
				this.tf[i].height = mScreen.ITEM_HEIGHT + 2;
				if (GameCanvas.isTouch)
				{
					this.tf[0].isFocus = false;
				}
				else
				{
					this.tf[0].isFocus = true;
				}
				if (!GameCanvas.isTouch)
				{
					this.right = this.tf[0].cmdClear;
				}
			}
			this.left = new Command(mResources.CLOSE, this, 1, null);
			this.center = new Command(mResources.OK, this, 2, null);
			if (GameCanvas.isTouch)
			{
				this.center.x = GameCanvas.w / 2 + 18;
				this.left.x = GameCanvas.w / 2 - 85;
				this.center.y = (this.left.y = this.y + this.h + 5);
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00007609 File Offset: 0x00005809
		public static ClientInput gI()
		{
			if (ClientInput.instance == null)
			{
				ClientInput.instance = new ClientInput();
			}
			return ClientInput.instance;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00007624 File Offset: 0x00005824
		public override void switchToMe()
		{
			this.focus = 0;
			base.switchToMe();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00007633 File Offset: 0x00005833
		public void setInput(int type, string title)
		{
			this.nTf = type;
			this.init(title);
			this.switchToMe();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0005D8B0 File Offset: 0x0005BAB0
		public override void paint(mGraphics g)
		{
			GameScr.gI().paint(g);
			PopUp.paintPopUp(g, this.x, this.y, this.w, this.h, -1, true);
			for (int i = 0; i < this.strPaint.Length; i++)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strPaint[i], GameCanvas.w / 2, this.y + 15 + i * 20, mFont.CENTER);
			}
			for (int j = 0; j < this.tf.Length; j++)
			{
				this.tf[j].paint(g);
			}
			base.paint(g);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0005D95C File Offset: 0x0005BB5C
		public override void update()
		{
			GameScr.gI().update();
			for (int i = 0; i < this.tf.Length; i++)
			{
				this.tf[i].update();
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0005D99C File Offset: 0x0005BB9C
		public override void keyPress(int keyCode)
		{
			for (int i = 0; i < this.tf.Length; i++)
			{
				if (this.tf[i].isFocus)
				{
					this.tf[i].keyPressed(keyCode);
					break;
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
		public override void updateKey()
		{
			if (GameCanvas.keyPressed[2])
			{
				this.focus--;
				if (this.focus < 0)
				{
					this.focus = this.tf.Length - 1;
				}
			}
			else if (GameCanvas.keyPressed[8])
			{
				this.focus++;
				if (this.focus > this.tf.Length - 1)
				{
					this.focus = 0;
				}
			}
			if (GameCanvas.keyPressed[2] || GameCanvas.keyPressed[8])
			{
				GameCanvas.clearKeyPressed();
				for (int i = 0; i < this.tf.Length; i++)
				{
					if (this.focus == i)
					{
						this.tf[i].isFocus = true;
						if (!GameCanvas.isTouch)
						{
							this.right = this.tf[i].cmdClear;
						}
					}
					else
					{
						this.tf[i].isFocus = false;
					}
					if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(this.tf[i].x, this.tf[i].y, this.tf[i].width, this.tf[i].height))
					{
						this.focus = i;
						break;
					}
				}
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00007649 File Offset: 0x00005849
		public void clearScreen()
		{
			ClientInput.instance = null;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0005DB50 File Offset: 0x0005BD50
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				GameScr.instance.switchToMe();
				this.clearScreen();
			}
			if (idAction == 2)
			{
				for (int i = 0; i < this.tf.Length; i++)
				{
					if (this.tf[i].getText() == null || this.tf[i].getText().Equals(string.Empty))
					{
						GameCanvas.startOKDlg(mResources.vuilongnhapduthongtin);
						return;
					}
				}
				Service.gI().sendClientInput(this.tf);
				GameScr.instance.switchToMe();
			}
		}

		// Token: 0x04000CC4 RID: 3268
		public static ClientInput instance;

		// Token: 0x04000CC5 RID: 3269
		public TField[] tf;

		// Token: 0x04000CC6 RID: 3270
		private int x;

		// Token: 0x04000CC7 RID: 3271
		private int y;

		// Token: 0x04000CC8 RID: 3272
		private int w;

		// Token: 0x04000CC9 RID: 3273
		private int h;

		// Token: 0x04000CCA RID: 3274
		private string[] strPaint;

		// Token: 0x04000CCB RID: 3275
		private int focus;

		// Token: 0x04000CCC RID: 3276
		private int nTf;
	}
}

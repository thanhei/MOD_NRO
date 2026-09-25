using System;

// Token: 0x02000018 RID: 24
public class ChatTextField : IActionListener
{
	// Token: 0x060001AB RID: 427 RVA: 0x00019280 File Offset: 0x00017480
	public ChatTextField()
	{
		this.tfChat = new TField();
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.name = "chat";
		if (Main.isWindowsPhone)
		{
			this.tfChat.strInfo = this.tfChat.name;
		}
		this.tfChat.width = GameCanvas.w - 6;
		if (Main.isPC && this.tfChat.width > 250)
		{
			this.tfChat.width = 250;
		}
		this.tfChat.height = mScreen.ITEM_HEIGHT + 2;
		this.tfChat.x = GameCanvas.w / 2 - this.tfChat.width / 2;
		this.tfChat.isFocus = true;
		this.tfChat.setMaxTextLenght(80);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00019384 File Offset: 0x00017584
	public void initChatTextField()
	{
		this.left = new Command(mResources.OK, this, 8000, null, 1, GameCanvas.h - mScreen.cmdH + 1);
		this.right = new Command(mResources.DELETE, this, 8001, null, GameCanvas.w - 70, GameCanvas.h - mScreen.cmdH + 1);
		this.center = null;
		this.w = this.tfChat.width + 20;
		this.h = this.tfChat.height + 26;
		this.x = GameCanvas.w / 2 - this.w / 2;
		this.y = this.tfChat.y - 18;
		if (Main.isPC && this.w > 320)
		{
			this.w = 320;
		}
		this.left.x = this.x;
		this.right.x = this.x + this.w - 68;
		if (GameCanvas.isTouch)
		{
			this.tfChat.y -= 5;
			this.y -= 20;
			this.h += 30;
			this.left.x = GameCanvas.w / 2 - 68 - 5;
			this.right.x = GameCanvas.w / 2 + 5;
			this.left.y = GameCanvas.h - 30;
			this.right.y = GameCanvas.h - 30;
		}
		this.cmdChat = new Command();
		ActionChat actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.tfChat.setText(str);
			this.parentScreen.onChatFromMe(str, this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		};
		this.cmdChat.actionChat = actionChat;
		this.cmdChat2 = new Command();
		this.cmdChat2.actionChat = delegate(string str)
		{
			this.tfChat.justReturnFromTextBox = false;
			if (this.parentScreen != null)
			{
				this.tfChat.setText(str);
				this.parentScreen.onChatFromMe(str, this.to);
				this.tfChat.setText(string.Empty);
				this.tfChat.clearKb();
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			this.isShow = false;
		};
		this.yBegin = this.tfChat.y;
		this.yUp = GameCanvas.h / 2 - 2 * this.tfChat.height;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateWhenKeyBoardVisible()
	{
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000195AC File Offset: 0x000177AC
	public void keyPressed(int keyCode)
	{
		if (this.isShow)
		{
			this.tfChat.keyPressed(keyCode);
		}
		if (this.tfChat.getText().Equals(string.Empty))
		{
			this.right.caption = mResources.CLOSE;
			return;
		}
		this.right.caption = mResources.DELETE;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00019606 File Offset: 0x00017806
	public static ChatTextField gI()
	{
		if (ChatTextField.instance == null)
		{
			return ChatTextField.instance = new ChatTextField();
		}
		return ChatTextField.instance;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00019620 File Offset: 0x00017820
	public void startChat(int firstCharacter, IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		this.tfChat.keyPressed(firstCharacter);
		if (!this.tfChat.getText().Equals(string.Empty) && GameCanvas.currentDialog == null)
		{
			this.parentScreen = parentScreen;
			this.isShow = true;
		}
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000196A4 File Offset: 0x000178A4
	public void startChat(IChatable parentScreen, string to)
	{
		this.right.caption = mResources.CLOSE;
		this.to = to;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			this.tfChat.isFocus = true;
			if (!Main.isPC)
			{
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00019740 File Offset: 0x00017940
	public void startChat2(IChatable parentScreen, string to)
	{
		this.tfChat.setFocusWithKb(true);
		this.to = to;
		this.parentScreen = parentScreen;
		if (Main.isWindowsPhone)
		{
			this.tfChat.showSubTextField = false;
		}
		if (Main.isIPhone)
		{
			this.tfChat.isPaintMouse = false;
		}
		if (GameCanvas.currentDialog == null)
		{
			this.isShow = true;
			if (!Main.isPC)
			{
				this.tfChat.setFocusWithKb(true);
			}
		}
		this.tfChat.setText(string.Empty);
		this.tfChat.clearAll();
		this.isPublic = false;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateKey()
	{
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000197D0 File Offset: 0x000179D0
	public void update()
	{
		if (!this.isShow)
		{
			return;
		}
		this.tfChat.update();
		if (Main.isWindowsPhone)
		{
			this.updateWhenKeyBoardVisible();
		}
		if (this.tfChat.justReturnFromTextBox)
		{
			this.tfChat.justReturnFromTextBox = false;
			this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
			this.tfChat.setText(string.Empty);
			this.right.caption = mResources.CLOSE;
		}
		if (!Main.isPC)
		{
			return;
		}
		if (GameCanvas.keyPressed[15])
		{
			if (this.left != null && this.tfChat.getText() != string.Empty)
			{
				this.left.performAction();
			}
			GameCanvas.keyPressed[15] = false;
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
		}
		if (GameCanvas.keyPressed[14])
		{
			if (this.right != null)
			{
				this.right.performAction();
			}
			GameCanvas.keyPressed[14] = false;
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x000198D1 File Offset: 0x00017AD1
	public void close()
	{
		this.tfChat.setText(string.Empty);
		this.isShow = false;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000198EC File Offset: 0x00017AEC
	public void paint(mGraphics g)
	{
		if (this.isShow && !Main.isIPhone)
		{
			int num = ((!Main.isWindowsPhone) ? (this.y - this.KC) : (this.tfChat.y - 5));
			PopUp.paintPopUp(g, (!Main.isWindowsPhone) ? this.x : 0, num, (!Main.isWindowsPhone) ? this.w : GameCanvas.w, this.h, -1, true);
			if (Main.isPC)
			{
				mFont.tahoma_7b_green2.drawString(g, this.strChat + this.to, this.tfChat.x, this.tfChat.y - ((!GameCanvas.isTouch) ? 12 : 17), 0);
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
			this.tfChat.paint(g);
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000199D8 File Offset: 0x00017BD8
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
		case 8000:
			Cout.LogError("perform chat 8000");
			if (this.parentScreen != null)
			{
				long num = mSystem.currentTimeMillis();
				if (num - this.lastChatTime >= 1000L)
				{
					this.lastChatTime = num;
					this.parentScreen.onChatFromMe(this.tfChat.getText(), this.to);
					this.tfChat.setText(string.Empty);
					this.right.caption = mResources.CLOSE;
					this.tfChat.clearKb();
					return;
				}
			}
			break;
		case 8001:
			Cout.LogError("perform chat 8001");
			if (this.tfChat.getText().Equals(string.Empty))
			{
				this.isShow = false;
				this.parentScreen.onCancelChat();
			}
			this.tfChat.clear();
			break;
		case 8002:
			break;
		default:
			return;
		}
	}

	// Token: 0x04000314 RID: 788
	internal static ChatTextField instance;

	// Token: 0x04000315 RID: 789
	public TField tfChat;

	// Token: 0x04000316 RID: 790
	public bool isShow;

	// Token: 0x04000317 RID: 791
	public IChatable parentScreen;

	// Token: 0x04000318 RID: 792
	internal long lastChatTime;

	// Token: 0x04000319 RID: 793
	public Command left;

	// Token: 0x0400031A RID: 794
	public Command cmdChat;

	// Token: 0x0400031B RID: 795
	public Command right;

	// Token: 0x0400031C RID: 796
	public Command center;

	// Token: 0x0400031D RID: 797
	internal int x;

	// Token: 0x0400031E RID: 798
	internal int y;

	// Token: 0x0400031F RID: 799
	internal int w;

	// Token: 0x04000320 RID: 800
	internal int h;

	// Token: 0x04000321 RID: 801
	internal bool isPublic;

	// Token: 0x04000322 RID: 802
	public Command cmdChat2;

	// Token: 0x04000323 RID: 803
	public int yBegin;

	// Token: 0x04000324 RID: 804
	public int yUp;

	// Token: 0x04000325 RID: 805
	public int KC;

	// Token: 0x04000326 RID: 806
	public string to;

	// Token: 0x04000327 RID: 807
	public string strChat = "Chat ";
}

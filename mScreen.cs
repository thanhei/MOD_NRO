using System;

// Token: 0x020000C4 RID: 196
public class mScreen
{
	// Token: 0x060009EC RID: 2540 RVA: 0x0000880D File Offset: 0x00006A0D
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		if (GameCanvas.currentScreen != null)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		Cout.LogError3(">>>>>>>>>>cur Screen: " + GameCanvas.currentScreen);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000045ED File Offset: 0x000027ED
	public virtual void unLoad()
	{
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x000045ED File Offset: 0x000027ED
	public static void initPos()
	{
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000045ED File Offset: 0x000027ED
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x000045ED File Offset: 0x000027ED
	public virtual void update()
	{
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x000959CC File Offset: 0x00093BCC
	public virtual void updateKey()
	{
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
		}
		if (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left))
		{
			GameCanvas.keyPressed[12] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().left != null)
				{
					ChatTextField.gI().left.performAction();
				}
			}
			else if (this.left != null)
			{
				this.left.performAction();
			}
		}
		if (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right))
		{
			GameCanvas.keyPressed[13] = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (ChatTextField.gI().isShow)
			{
				if (ChatTextField.gI().right != null)
				{
					ChatTextField.gI().right.performAction();
				}
			}
			else if (this.right != null)
			{
				this.right.performAction();
			}
		}
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00095B44 File Offset: 0x00093D44
	public static bool getCmdPointerLast(Command cmd)
	{
		if (cmd == null)
		{
			return false;
		}
		if (cmd.x >= 0 && cmd.y != 0)
		{
			return cmd.isPointerPressInside();
		}
		if (GameCanvas.currentDialog != null)
		{
			if (GameCanvas.currentDialog.center != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (cmd == GameCanvas.currentDialog.center && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.left != null && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (cmd == GameCanvas.currentDialog.left && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (GameCanvas.currentDialog.right != null && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if ((cmd == GameCanvas.currentDialog.right || cmd == ChatTextField.gI().right) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		else
		{
			if (cmd == GameCanvas.currentScreen.left && GameCanvas.isPointerHoldIn(0, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 0;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if (cmd == GameCanvas.currentScreen.right && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 2;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
			if ((cmd == GameCanvas.currentScreen.center || ChatPopup.currChatPopup != null) && GameCanvas.isPointerHoldIn(GameCanvas.w - mScreen.cmdW >> 1, GameCanvas.h - mScreen.cmdH - 5, mScreen.cmdW, mScreen.cmdH + 10))
			{
				mScreen.keyTouch = 1;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x00095DE0 File Offset: 0x00093FE0
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		if (!ChatTextField.gI().isShow || !Main.isPC)
		{
			if (GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
			{
				GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
			}
		}
	}

	// Token: 0x04001276 RID: 4726
	public Command left;

	// Token: 0x04001277 RID: 4727
	public Command center;

	// Token: 0x04001278 RID: 4728
	public Command right;

	// Token: 0x04001279 RID: 4729
	public Command cmdClose;

	// Token: 0x0400127A RID: 4730
	public static int ITEM_HEIGHT;

	// Token: 0x0400127B RID: 4731
	public static int yOpenKeyBoard = 100;

	// Token: 0x0400127C RID: 4732
	public static int cmdW = 68;

	// Token: 0x0400127D RID: 4733
	public static int cmdH = 26;

	// Token: 0x0400127E RID: 4734
	public static int keyTouch = -1;

	// Token: 0x0400127F RID: 4735
	public static int keyMouse = -1;
}

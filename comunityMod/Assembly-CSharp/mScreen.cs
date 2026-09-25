using System;

// Token: 0x020000C6 RID: 198
public class mScreen
{
	// Token: 0x06000A39 RID: 2617 RVA: 0x00092218 File Offset: 0x00090418
	public virtual void switchToMe()
	{
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
		if (GameCanvas.currentScreen != null)
		{
			GameCanvas.currentScreen.unLoad();
		}
		GameCanvas.currentScreen = this;
		string text = "cur Screen: ";
		mScreen currentScreen = GameCanvas.currentScreen;
		Cout.LogError3(text + ((currentScreen != null) ? currentScreen.ToString() : null));
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void unLoad()
	{
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00004887 File Offset: 0x00002A87
	public static void initPos()
	{
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void keyPress(int keyCode)
	{
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void update()
	{
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00092268 File Offset: 0x00090468
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
		if (!GameCanvas.keyPressed[13] && !mScreen.getCmdPointerLast(GameCanvas.currentScreen.right))
		{
			return;
		}
		GameCanvas.keyPressed[13] = false;
		mScreen.keyTouch = -1;
		GameCanvas.isPointerJustRelease = false;
		if (ChatTextField.gI().isShow)
		{
			if (ChatTextField.gI().right != null)
			{
				ChatTextField.gI().right.performAction();
				return;
			}
		}
		else if (this.right != null)
		{
			this.right.performAction();
		}
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x000923A4 File Offset: 0x000905A4
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

	// Token: 0x06000A40 RID: 2624 RVA: 0x000925EC File Offset: 0x000907EC
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h + 1);
		if ((!ChatTextField.gI().isShow || !Main.isPC) && GameCanvas.currentDialog == null && !GameCanvas.menu.showMenu)
		{
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}
	}

	// Token: 0x040013DB RID: 5083
	public Command left;

	// Token: 0x040013DC RID: 5084
	public Command center;

	// Token: 0x040013DD RID: 5085
	public Command right;

	// Token: 0x040013DE RID: 5086
	public Command cmdClose;

	// Token: 0x040013DF RID: 5087
	public static int ITEM_HEIGHT;

	// Token: 0x040013E0 RID: 5088
	public static int yOpenKeyBoard = 100;

	// Token: 0x040013E1 RID: 5089
	public static int cmdW = 68;

	// Token: 0x040013E2 RID: 5090
	public static int cmdH = 26;

	// Token: 0x040013E3 RID: 5091
	public static int keyTouch = -1;

	// Token: 0x040013E4 RID: 5092
	public static int keyMouse = -1;
}

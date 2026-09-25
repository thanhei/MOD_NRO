using System;

// Token: 0x02000028 RID: 40
public abstract class Dialog
{
	// Token: 0x06000248 RID: 584 RVA: 0x0002E274 File Offset: 0x0002C474
	public virtual void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		GameCanvas.paintz.paintTabSoft(g);
		GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
	public virtual void keyPress(int keyCode)
	{
		switch (keyCode)
		{
		case -7:
			goto IL_00A8;
		case -6:
			goto IL_0095;
		case -5:
			goto IL_00BB;
		default:
			if (keyCode == -39)
			{
				goto IL_006E;
			}
			if (keyCode != -38)
			{
				if (keyCode == -22)
				{
					goto IL_00A8;
				}
				if (keyCode == -21)
				{
					goto IL_0095;
				}
				if (keyCode == -27)
				{
					return;
				}
				if (keyCode != 10)
				{
					return;
				}
				goto IL_00BB;
			}
			break;
		case -2:
			goto IL_006E;
		case -1:
			break;
		}
		GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
		return;
		IL_006E:
		GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
		return;
		IL_0095:
		GameCanvas.keyHold[12] = true;
		GameCanvas.keyPressed[12] = true;
		return;
		IL_00A8:
		GameCanvas.keyHold[13] = true;
		GameCanvas.keyPressed[13] = true;
		return;
		IL_00BB:
		GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = true;
		GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0002E3C0 File Offset: 0x0002C5C0
	public virtual void update()
	{
		if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.center != null)
			{
				this.center.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
		{
			GameCanvas.keyPressed[12] = false;
			GameCanvas.isPointerClick = false;
			mScreen.keyTouch = -1;
			GameCanvas.isPointerJustRelease = false;
			if (this.left != null)
			{
				this.left.performAction();
			}
			mScreen.keyTouch = -1;
		}
		if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
		{
			GameCanvas.keyPressed[13] = false;
			GameCanvas.isPointerClick = false;
			GameCanvas.isPointerJustRelease = false;
			mScreen.keyTouch = -1;
			if (this.right != null)
			{
				this.right.performAction();
			}
			mScreen.keyTouch = -1;
		}
		GameCanvas.clearKeyPressed();
		GameCanvas.clearKeyHold();
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00004887 File Offset: 0x00002A87
	public virtual void show()
	{
	}

	// Token: 0x040004F9 RID: 1273
	public Command left;

	// Token: 0x040004FA RID: 1274
	public Command center;

	// Token: 0x040004FB RID: 1275
	public Command right;

	// Token: 0x040004FC RID: 1276
	internal int lenCaption;
}

using System;

// Token: 0x020000BA RID: 186
public class MsgDlg : Dialog
{
	// Token: 0x06000876 RID: 2166 RVA: 0x00079254 File Offset: 0x00077454
	public MsgDlg()
	{
		this.padLeft = 35;
		if (GameCanvas.w <= 176)
		{
			this.padLeft = 10;
		}
		if (GameCanvas.w > 320)
		{
			this.padLeft = 80;
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00007F7A File Offset: 0x0000617A
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
		this.time = mSystem.currentTimeMillis() + 5000L;
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00007FA2 File Offset: 0x000061A2
	public override void show()
	{
		GameCanvas.currentDialog = this;
		this.time = -1L;
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000792A8 File Offset: 0x000774A8
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0007930C File Offset: 0x0007750C
	public void setInfo(string info, Command left, Command center, Command right)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.left = left;
		this.center = center;
		this.right = right;
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
		if (GameCanvas.isTouch)
		{
			if (left != null)
			{
				this.left.x = GameCanvas.w / 2 - 68 - 5;
				this.left.y = GameCanvas.h - 50;
			}
			if (right != null)
			{
				this.right.x = GameCanvas.w / 2 + 5;
				this.right.y = GameCanvas.h - 50;
			}
			if (center != null)
			{
				this.center.x = GameCanvas.w / 2 - 35;
				this.center.y = GameCanvas.h - 50;
			}
		}
		this.isWait = false;
		this.time = -1L;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00079428 File Offset: 0x00077628
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (LoginScr.isContinueToLogin)
		{
			return;
		}
		int num = GameCanvas.h - this.h - 38;
		int w = GameCanvas.w - this.padLeft * 2;
		GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
		int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
		if (this.isWait)
		{
			num2 += 8;
			GameCanvas.paintShukiren(GameCanvas.hw, num2 - 12, g);
		}
		int i = 0;
		int num3 = num2;
		while (i < this.info.Length)
		{
			mFont.tahoma_7b_dark.drawString(g, this.info[i], GameCanvas.hw, num3, 2);
			i++;
			num3 += mFont.tahoma_8b.getHeight();
		}
		base.paint(g);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00007FB2 File Offset: 0x000061B2
	public override void update()
	{
		base.update();
		if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x0400105A RID: 4186
	public string[] info;

	// Token: 0x0400105B RID: 4187
	public bool isWait;

	// Token: 0x0400105C RID: 4188
	private int h;

	// Token: 0x0400105D RID: 4189
	private int padLeft;

	// Token: 0x0400105E RID: 4190
	private long time = -1L;
}

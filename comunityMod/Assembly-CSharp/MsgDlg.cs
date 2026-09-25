using System;

// Token: 0x02000074 RID: 116
public class MsgDlg : Dialog
{
	// Token: 0x060005A8 RID: 1448 RVA: 0x0005711A File Offset: 0x0005531A
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

	// Token: 0x060005A9 RID: 1449 RVA: 0x0005715A File Offset: 0x0005535A
	public void pleasewait()
	{
		this.setInfo(mResources.PLEASEWAIT, null, null, null);
		GameCanvas.currentDialog = this;
		this.time = mSystem.currentTimeMillis() + 5000L;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00057182 File Offset: 0x00055382
	public override void show()
	{
		GameCanvas.currentDialog = this;
		this.time = -1L;
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00057194 File Offset: 0x00055394
	public void setInfo(string info)
	{
		this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
		this.h = 80;
		if (this.info.Length >= 5)
		{
			this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000571F4 File Offset: 0x000553F4
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

	// Token: 0x060005AD RID: 1453 RVA: 0x00057304 File Offset: 0x00055504
	public override void paint(mGraphics g)
	{
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (!LoginScr.isContinueToLogin)
		{
			int num = GameCanvas.h - this.h - 38;
			int num2 = GameCanvas.w - this.padLeft * 2;
			GameCanvas.paintz.paintPopUp(this.padLeft, num, num2, this.h, g);
			int num3 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
			if (this.isWait)
			{
				num3 += 8;
				GameCanvas.paintShukiren(GameCanvas.hw, num3 - 12, g);
			}
			int i = 0;
			int num4 = num3;
			while (i < this.info.Length)
			{
				mFont.tahoma_7b_dark.drawString(g, this.info[i], GameCanvas.hw, num4, 2);
				i++;
				num4 += mFont.tahoma_8b.getHeight();
			}
			base.paint(g);
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000573E8 File Offset: 0x000555E8
	public override void update()
	{
		base.update();
		if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04000BFC RID: 3068
	public string[] info;

	// Token: 0x04000BFD RID: 3069
	public bool isWait;

	// Token: 0x04000BFE RID: 3070
	internal int h;

	// Token: 0x04000BFF RID: 3071
	internal int padLeft;

	// Token: 0x04000C00 RID: 3072
	internal long time = -1L;
}

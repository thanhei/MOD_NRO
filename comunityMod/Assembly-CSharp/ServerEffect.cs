using System;

// Token: 0x02000095 RID: 149
public class ServerEffect : Effect2
{
	// Token: 0x060007C9 RID: 1993 RVA: 0x00078158 File Offset: 0x00076358
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0007819C File Offset: 0x0007639C
	public static void addServerEffect(int id, int cx, int cy, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x000781E8 File Offset: 0x000763E8
	public static void addServerEffect(int id, Mob m, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.m = m;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00078224 File Offset: 0x00076424
	public static void addServerEffect(int id, global::Char c, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00078260 File Offset: 0x00076460
	public static void addServerEffect(int id, global::Char c, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x000782A4 File Offset: 0x000764A4
	public static void addServerEffectWithTime(int id, int cx, int cy, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x000782F4 File Offset: 0x000764F4
	public static void addServerEffectWithTime(int id, global::Char c, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0007833C File Offset: 0x0007653C
	public override void paint(mGraphics g)
	{
		if (mGraphics.zoomLevel == 1)
		{
			GameScr.countEff++;
		}
		if (GameScr.countEff < 8)
		{
			if (this.c != null)
			{
				this.x = this.c.cx;
				this.y = this.c.cy + GameCanvas.transY;
			}
			if (this.m != null)
			{
				this.x = this.m.x;
				this.y = this.m.y + GameCanvas.transY;
			}
			int num = this.x + this.dx0 + this.eff.arrEfInfo[this.i0].dx;
			int num2 = this.y + this.dy0 + this.eff.arrEfInfo[this.i0].dy;
			if (GameCanvas.isPaint(num, num2))
			{
				SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00078450 File Offset: 0x00076650
	public override void update()
	{
		if (this.endTime != 0L)
		{
			this.i0++;
			if (this.i0 >= this.eff.arrEfInfo.Length)
			{
				this.i0 = 0;
			}
			if (mSystem.currentTimeMillis() - this.endTime > 0L)
			{
				Effect2.vEffect2.removeElement(this);
			}
		}
		else
		{
			this.i0++;
			if (this.i0 >= this.eff.arrEfInfo.Length)
			{
				this.loopCount -= 1;
				if (this.loopCount <= 0)
				{
					Effect2.vEffect2.removeElement(this);
				}
				else
				{
					this.i0 = 0;
				}
			}
		}
		if (GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != global::Char.myCharz() && !GameScr.vCharInMap.contains(this.c))
		{
			Effect2.vEffect2.removeElement(this);
		}
	}

	// Token: 0x04000EB8 RID: 3768
	public EffectCharPaint eff;

	// Token: 0x04000EB9 RID: 3769
	internal int i0;

	// Token: 0x04000EBA RID: 3770
	internal int dx0;

	// Token: 0x04000EBB RID: 3771
	internal int dy0;

	// Token: 0x04000EBC RID: 3772
	internal int x;

	// Token: 0x04000EBD RID: 3773
	internal int y;

	// Token: 0x04000EBE RID: 3774
	internal global::Char c;

	// Token: 0x04000EBF RID: 3775
	internal Mob m;

	// Token: 0x04000EC0 RID: 3776
	internal short loopCount;

	// Token: 0x04000EC1 RID: 3777
	internal long endTime;

	// Token: 0x04000EC2 RID: 3778
	internal int trans;
}

using System;

// Token: 0x02000048 RID: 72
public class ServerEffect : Effect2
{
	// Token: 0x060002BA RID: 698 RVA: 0x0001B5B0 File Offset: 0x000197B0
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0001B5F4 File Offset: 0x000197F4
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

	// Token: 0x060002BC RID: 700 RVA: 0x0001B640 File Offset: 0x00019840
	public static void addServerEffect(int id, Mob m, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.m = m;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0001B67C File Offset: 0x0001987C
	public static void addServerEffect(int id, global::Char c, int loopCount)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0001B6B8 File Offset: 0x000198B8
	public static void addServerEffect(int id, global::Char c, int loopCount, int trans)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.loopCount = (short)loopCount;
		serverEffect.trans = trans;
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0001B6FC File Offset: 0x000198FC
	public static void addServerEffectWithTime(int id, int cx, int cy, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.x = cx;
		serverEffect.y = cy;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0001B74C File Offset: 0x0001994C
	public static void addServerEffectWithTime(int id, global::Char c, int timeLengthInSecond)
	{
		ServerEffect serverEffect = new ServerEffect();
		serverEffect.eff = GameScr.efs[id - 1];
		serverEffect.c = c;
		serverEffect.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffect2.addElement(serverEffect);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0001B794 File Offset: 0x00019994
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

	// Token: 0x060002C2 RID: 706 RVA: 0x0001B8B4 File Offset: 0x00019AB4
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

	// Token: 0x0400036E RID: 878
	public EffectCharPaint eff;

	// Token: 0x0400036F RID: 879
	private int i0;

	// Token: 0x04000370 RID: 880
	private int dx0;

	// Token: 0x04000371 RID: 881
	private int dy0;

	// Token: 0x04000372 RID: 882
	private int x;

	// Token: 0x04000373 RID: 883
	private int y;

	// Token: 0x04000374 RID: 884
	private global::Char c;

	// Token: 0x04000375 RID: 885
	private Mob m;

	// Token: 0x04000376 RID: 886
	private short loopCount;

	// Token: 0x04000377 RID: 887
	private long endTime;

	// Token: 0x04000378 RID: 888
	private int trans;
}

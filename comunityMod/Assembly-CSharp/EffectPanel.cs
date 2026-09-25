using System;

// Token: 0x02000034 RID: 52
public class EffectPanel : Effect2
{
	// Token: 0x0600028F RID: 655 RVA: 0x000305E0 File Offset: 0x0002E7E0
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		EffectPanel effectPanel = new EffectPanel();
		effectPanel.eff = GameScr.efs[id - 1];
		effectPanel.x = cx;
		effectPanel.y = cy;
		effectPanel.loopCount = (short)loopCount;
		Effect2.vEffect3.addElement(effectPanel);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00030624 File Offset: 0x0002E824
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
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.i0].idImg, num, num2, this.trans, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0003072C File Offset: 0x0002E92C
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
				Effect2.vEffect3.removeElement(this);
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
					Effect2.vEffect3.removeElement(this);
				}
				else
				{
					this.i0 = 0;
				}
			}
		}
		if (GameCanvas.gameTick % 11 == 0 && this.c != null && this.c != global::Char.myCharz() && !GameScr.vCharInMap.contains(this.c))
		{
			Effect2.vEffect3.removeElement(this);
		}
	}

	// Token: 0x04000572 RID: 1394
	public EffectCharPaint eff;

	// Token: 0x04000573 RID: 1395
	internal int i0;

	// Token: 0x04000574 RID: 1396
	internal int dx0;

	// Token: 0x04000575 RID: 1397
	internal int dy0;

	// Token: 0x04000576 RID: 1398
	internal int x;

	// Token: 0x04000577 RID: 1399
	internal int y;

	// Token: 0x04000578 RID: 1400
	internal global::Char c;

	// Token: 0x04000579 RID: 1401
	internal Mob m;

	// Token: 0x0400057A RID: 1402
	internal short loopCount;

	// Token: 0x0400057B RID: 1403
	internal long endTime;

	// Token: 0x0400057C RID: 1404
	internal int trans;
}

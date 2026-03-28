using System;

// Token: 0x0200003E RID: 62
public class EffectPanel : Effect2
{
	// Token: 0x06000287 RID: 647 RVA: 0x00019764 File Offset: 0x00017964
	public static void addServerEffect(int id, int cx, int cy, int loopCount)
	{
		EffectPanel effectPanel = new EffectPanel();
		effectPanel.eff = GameScr.efs[id - 1];
		effectPanel.x = cx;
		effectPanel.y = cy;
		effectPanel.loopCount = (short)loopCount;
		Effect2.vEffect3.addElement(effectPanel);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x000197A8 File Offset: 0x000179A8
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

	// Token: 0x06000289 RID: 649 RVA: 0x000198BC File Offset: 0x00017ABC
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

	// Token: 0x040002F9 RID: 761
	public EffectCharPaint eff;

	// Token: 0x040002FA RID: 762
	private int i0;

	// Token: 0x040002FB RID: 763
	private int dx0;

	// Token: 0x040002FC RID: 764
	private int dy0;

	// Token: 0x040002FD RID: 765
	private int x;

	// Token: 0x040002FE RID: 766
	private int y;

	// Token: 0x040002FF RID: 767
	private global::Char c;

	// Token: 0x04000300 RID: 768
	private Mob m;

	// Token: 0x04000301 RID: 769
	private short loopCount;

	// Token: 0x04000302 RID: 770
	private long endTime;

	// Token: 0x04000303 RID: 771
	private int trans;
}

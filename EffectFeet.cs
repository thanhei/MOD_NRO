using System;

// Token: 0x0200003D RID: 61
public class EffectFeet : Effect2
{
	// Token: 0x06000282 RID: 642 RVA: 0x00019578 File Offset: 0x00017778
	public static void addFeet(int cx, int cy, int ctrans, int timeLengthInSecond, bool isCF)
	{
		EffectFeet effectFeet = new EffectFeet();
		effectFeet.x = cx;
		effectFeet.y = cy;
		effectFeet.trans = ctrans;
		effectFeet.isF = isCF;
		effectFeet.endTime = mSystem.currentTimeMillis() + (long)(timeLengthInSecond * 1000);
		Effect2.vEffectFeet.addElement(effectFeet);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000568C File Offset: 0x0000388C
	public override void update()
	{
		if (mSystem.currentTimeMillis() - this.endTime > 0L)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x000195C8 File Offset: 0x000177C8
	public override void paint(mGraphics g)
	{
		int num = (int)TileMap.size;
		if (TileMap.tileTypeAt(this.x + num / 2, this.y + 1, 4))
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt((this.x - num / 2) / num, (this.y + 1) / num) == 0)
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, 100, 100);
		}
		else if (TileMap.tileTypeAt((this.x + num / 2) / num, (this.y + 1) / num) == 0)
		{
			g.setClip(this.x / num * num, (this.y - 30) / num * num, num, 100);
		}
		else if (TileMap.tileTypeAt(this.x - num / 2, this.y + 1, 8))
		{
			g.setClip(this.x / 24 * num, (this.y - 30) / num * num, num, 100);
		}
		g.drawRegion((!this.isF) ? EffectFeet.imgFeet3 : EffectFeet.imgFeet1, 0, 0, EffectFeet.imgFeet1.getWidth(), EffectFeet.imgFeet1.getHeight(), this.trans, this.x, this.y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x040002F2 RID: 754
	private int x;

	// Token: 0x040002F3 RID: 755
	private int y;

	// Token: 0x040002F4 RID: 756
	private int trans;

	// Token: 0x040002F5 RID: 757
	private long endTime;

	// Token: 0x040002F6 RID: 758
	private bool isF;

	// Token: 0x040002F7 RID: 759
	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	// Token: 0x040002F8 RID: 760
	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");
}

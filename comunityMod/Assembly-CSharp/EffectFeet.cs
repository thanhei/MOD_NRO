using System;

// Token: 0x02000030 RID: 48
public class EffectFeet : Effect2
{
	// Token: 0x0600027C RID: 636 RVA: 0x00030228 File Offset: 0x0002E428
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

	// Token: 0x0600027D RID: 637 RVA: 0x00030277 File Offset: 0x0002E477
	public override void update()
	{
		if (mSystem.currentTimeMillis() - this.endTime > 0L)
		{
			Effect2.vEffectFeet.removeElement(this);
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00030294 File Offset: 0x0002E494
	public override void paint(mGraphics g)
	{
		int size = (int)TileMap.size;
		if (TileMap.tileTypeAt(this.x + size / 2, this.y + 1, 4))
		{
			g.setClip(this.x / size * size, (this.y - 30) / size * size, size, 100);
		}
		else if (TileMap.tileTypeAt((this.x - size / 2) / size, (this.y + 1) / size) == 0)
		{
			g.setClip(this.x / size * size, (this.y - 30) / size * size, 100, 100);
		}
		else if (TileMap.tileTypeAt((this.x + size / 2) / size, (this.y + 1) / size) == 0)
		{
			g.setClip(this.x / size * size, (this.y - 30) / size * size, size, 100);
		}
		else if (TileMap.tileTypeAt(this.x - size / 2, this.y + 1, 8))
		{
			g.setClip(this.x / 24 * size, (this.y - 30) / size * size, size, 100);
		}
		g.drawRegion((!this.isF) ? EffectFeet.imgFeet3 : EffectFeet.imgFeet1, 0, 0, EffectFeet.imgFeet1.getWidth(), EffectFeet.imgFeet1.getHeight(), this.trans, this.x, this.y, mGraphics.BOTTOM | mGraphics.HCENTER);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x0400055F RID: 1375
	internal int x;

	// Token: 0x04000560 RID: 1376
	internal int y;

	// Token: 0x04000561 RID: 1377
	internal int trans;

	// Token: 0x04000562 RID: 1378
	internal long endTime;

	// Token: 0x04000563 RID: 1379
	internal bool isF;

	// Token: 0x04000564 RID: 1380
	public static Image imgFeet1 = GameCanvas.loadImage("/mainImage/myTexture2dmove-1.png");

	// Token: 0x04000565 RID: 1381
	public static Image imgFeet3 = GameCanvas.loadImage("/mainImage/myTexture2dmove-3.png");
}

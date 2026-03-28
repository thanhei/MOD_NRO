using System;

namespace Assets.src.e
{
	// Token: 0x02000087 RID: 135
	public class Small
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x000065EE File Offset: 0x000047EE
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0002987C File Offset: 0x00027A7C
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000298E0 File Offset: 0x00027AE0
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00029904 File Offset: 0x00027B04
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (mGraphics.getImageWidth(this.img) == 1)
			{
				return;
			}
			g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0002996C File Offset: 0x00027B6C
		public void update()
		{
			this.timeUpdate++;
			if (this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id))
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x040007DA RID: 2010
		public Image img;

		// Token: 0x040007DB RID: 2011
		public int id;

		// Token: 0x040007DC RID: 2012
		public int timePaint;

		// Token: 0x040007DD RID: 2013
		public int timeUpdate;
	}
}

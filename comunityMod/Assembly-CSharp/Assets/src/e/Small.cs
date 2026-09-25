using System;

namespace Assets.src.e
{
	// Token: 0x020001BC RID: 444
	public class Small
	{
		// Token: 0x060012F8 RID: 4856 RVA: 0x000CA314 File Offset: 0x000C8514
		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			this.timePaint = 0;
			this.timeUpdate = 0;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000CA338 File Offset: 0x000C8538
		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			g.drawRegion(this.img, 0, 0, mGraphics.getImageWidth(this.img), mGraphics.getImageHeight(this.img), transform, x, y, anchor);
			if (GameCanvas.gameTick % 1000 == 0)
			{
				this.timePaint++;
				this.timeUpdate = this.timePaint;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000CA398 File Offset: 0x000C8598
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			this.paint(g, transform, f, x, y, w, h, anchor, false);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000CA3BC File Offset: 0x000C85BC
		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (mGraphics.getImageWidth(this.img) != 1)
			{
				g.drawRegion(this.img, 0, f * w, w, h, transform, x, y, anchor, isClip);
				if (GameCanvas.gameTick % 1000 == 0)
				{
					this.timePaint++;
					this.timeUpdate = this.timePaint;
				}
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000CA41C File Offset: 0x000C861C
		public void update()
		{
			this.timeUpdate++;
			if (this.timeUpdate - this.timePaint > 1 && !global::Char.myCharz().isCharBodyImageID(this.id))
			{
				SmallImage.imgNew[this.id] = null;
			}
		}

		// Token: 0x04001A4F RID: 6735
		public Image img;

		// Token: 0x04001A50 RID: 6736
		public int id;

		// Token: 0x04001A51 RID: 6737
		public int timePaint;

		// Token: 0x04001A52 RID: 6738
		public int timeUpdate;
	}
}

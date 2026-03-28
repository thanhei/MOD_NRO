using System;

namespace Assets.src.g
{
	// Token: 0x020000BF RID: 191
	public class PetFollow
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x000084D7 File Offset: 0x000066D7
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00008515 File Offset: 0x00006715
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			if (fimg < 1)
			{
				return;
			}
			this.fimg = fimg;
			this.frame = frameNew;
			this.wimg = wimg;
			this.himg = himg;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00091F14 File Offset: 0x00090114
		public void paint(mGraphics g)
		{
			int w = 32;
			int h = 32;
			int num = (GameCanvas.gameTick % 10 <= 5) ? 0 : 1;
			if (this.fimg > 0)
			{
				w = this.wimg;
				h = this.himg;
				num = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num, w, h, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00091F98 File Offset: 0x00090198
		public void update()
		{
			this.moveCamera();
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.f = this.frame[this.count];
				this.count++;
			}
			if (this.count >= this.frame.Length)
			{
				this.count = 0;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000853C File Offset: 0x0000673C
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 1);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00091FF4 File Offset: 0x000901F4
		public void moveCamera()
		{
			if (this.cmy != this.cmtoY)
			{
				this.cmvy = this.cmtoY - this.cmy << 2;
				this.cmdy += this.cmvy;
				this.cmy += this.cmdy >> 4;
				this.cmdy &= 15;
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 4;
				this.cmdx &= 15;
			}
		}

		// Token: 0x040011D9 RID: 4569
		public short smallID;

		// Token: 0x040011DA RID: 4570
		public Info info = new Info();

		// Token: 0x040011DB RID: 4571
		public int dir;

		// Token: 0x040011DC RID: 4572
		public int f;

		// Token: 0x040011DD RID: 4573
		public int tF;

		// Token: 0x040011DE RID: 4574
		public int cmtoY;

		// Token: 0x040011DF RID: 4575
		public int cmy;

		// Token: 0x040011E0 RID: 4576
		public int cmdy;

		// Token: 0x040011E1 RID: 4577
		public int cmvy;

		// Token: 0x040011E2 RID: 4578
		public int cmyLim;

		// Token: 0x040011E3 RID: 4579
		public int cmtoX;

		// Token: 0x040011E4 RID: 4580
		public int cmx;

		// Token: 0x040011E5 RID: 4581
		public int cmdx;

		// Token: 0x040011E6 RID: 4582
		public int cmvx;

		// Token: 0x040011E7 RID: 4583
		public int cmxLim;

		// Token: 0x040011E8 RID: 4584
		public int fimg = -1;

		// Token: 0x040011E9 RID: 4585
		public int wimg;

		// Token: 0x040011EA RID: 4586
		public int himg;

		// Token: 0x040011EB RID: 4587
		private int[] frame = new int[]
		{
			0,
			1,
			2,
			1
		};

		// Token: 0x040011EC RID: 4588
		private int count;
	}
}

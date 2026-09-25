using System;

namespace Assets.src.g
{
	// Token: 0x020001B8 RID: 440
	public class PetFollow
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x000C5C03 File Offset: 0x000C3E03
		public PetFollow()
		{
			this.f = Res.random(0, 3);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000C5C41 File Offset: 0x000C3E41
		public void SetImg(int fimg, int[] frameNew, int wimg, int himg)
		{
			if (fimg >= 1)
			{
				this.fimg = fimg;
				this.frame = frameNew;
				this.wimg = wimg;
				this.himg = himg;
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000C5C64 File Offset: 0x000C3E64
		public void paint(mGraphics g)
		{
			int num = 32;
			int num2 = 32;
			int num3 = ((GameCanvas.gameTick % 10 > 5) ? 1 : 0);
			if (this.fimg > 0)
			{
				num = this.wimg;
				num2 = this.himg;
				num3 = 0;
			}
			SmallImage.drawSmallImage(g, (int)this.smallID, this.f, this.cmx, this.cmy + 3 + num3, num, num2, (this.dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000C5CD8 File Offset: 0x000C3ED8
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

		// Token: 0x060012D5 RID: 4821 RVA: 0x000C5D2C File Offset: 0x000C3F2C
		public void remove()
		{
			ServerEffect.addServerEffect(60, this.cmx, this.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 1);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000C5D54 File Offset: 0x000C3F54
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

		// Token: 0x040019F6 RID: 6646
		public short smallID;

		// Token: 0x040019F7 RID: 6647
		public Info info = new Info();

		// Token: 0x040019F8 RID: 6648
		public int dir;

		// Token: 0x040019F9 RID: 6649
		public int f;

		// Token: 0x040019FA RID: 6650
		public int tF;

		// Token: 0x040019FB RID: 6651
		public int cmtoY;

		// Token: 0x040019FC RID: 6652
		public int cmy;

		// Token: 0x040019FD RID: 6653
		public int cmdy;

		// Token: 0x040019FE RID: 6654
		public int cmvy;

		// Token: 0x040019FF RID: 6655
		public int cmyLim;

		// Token: 0x04001A00 RID: 6656
		public int cmtoX;

		// Token: 0x04001A01 RID: 6657
		public int cmx;

		// Token: 0x04001A02 RID: 6658
		public int cmdx;

		// Token: 0x04001A03 RID: 6659
		public int cmvx;

		// Token: 0x04001A04 RID: 6660
		public int cmxLim;

		// Token: 0x04001A05 RID: 6661
		public int fimg = -1;

		// Token: 0x04001A06 RID: 6662
		public int wimg;

		// Token: 0x04001A07 RID: 6663
		public int himg;

		// Token: 0x04001A08 RID: 6664
		internal int[] frame = new int[] { 0, 1, 2, 1 };

		// Token: 0x04001A09 RID: 6665
		internal int count;
	}
}

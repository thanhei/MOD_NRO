using System;

// Token: 0x0200008B RID: 139
public class Point
{
	// Token: 0x06000746 RID: 1862 RVA: 0x00074050 File Offset: 0x00072250
	public Point()
	{
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x000740B4 File Offset: 0x000722B4
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00074124 File Offset: 0x00072324
	public Point(int x, int y, int goc)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0007419B File Offset: 0x0007239B
	public void update()
	{
		this.f++;
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x000741D1 File Offset: 0x000723D1
	public void update_not_f()
	{
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x000741FC File Offset: 0x000723FC
	public void paint(mGraphics g)
	{
		if (!this.isRemove)
		{
			int num = 0;
			if (this.isSmall && this.f >= this.fSmall)
			{
				num = 1;
			}
			Point.FraEffInMap[this.color].drawFrame(this.frame / 2 + num, this.x, this.y, this.dis, 3, g);
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0007425C File Offset: 0x0007245C
	public void updateInMap()
	{
		this.f++;
		if (this.maxframe > 1)
		{
			this.frame++;
			if (this.frame / 2 >= this.maxframe)
			{
				this.frame = 0;
			}
		}
		if (this.f >= this.fRe)
		{
			this.isRemove = true;
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x000742BC File Offset: 0x000724BC
	public int setFrameAngle(int goc)
	{
		if (goc <= 15 || goc > 345)
		{
			return 12;
		}
		int num = (goc - 15) / 15 + 1;
		if (num > 24)
		{
			num = 24;
		}
		return (int)this.mpaintone_Arrow[num];
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000742F4 File Offset: 0x000724F4
	public void create_Arrow(int vMax)
	{
		this.vMax = vMax;
		int num = this.toX - this.x;
		int num2 = this.toY - this.y;
		if (this.x > this.toX)
		{
			this.dir = 2;
			this.dir_nguoc = 0;
		}
		else
		{
			this.dir = 0;
			this.dir_nguoc = 2;
		}
		this.create_Speed(num, num2);
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0007435C File Offset: 0x0007255C
	public void create_Speed(int dx, int dy)
	{
		this.frame = this.setFrameAngle(Res.angle(dx, dy));
		int num = Res.getDistance(dx, dy) / this.vMax;
		if (num == 0)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		if (num2 == 0 && dx < num)
		{
			num2 = ((dx >= 0) ? 1 : (-1));
		}
		if (num3 == 0 && dy < num)
		{
			num3 = ((dy >= 0) ? 1 : (-1));
		}
		if (Res.abs(num2) > Res.abs(dx))
		{
			num2 = dx;
		}
		if (Res.abs(num3) > Res.abs(dy))
		{
			num3 = dy;
		}
		this.vx = num2;
		this.vy = num3;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x000743EC File Offset: 0x000725EC
	public void moveTo_xy(int toX, int toY)
	{
		int num = toX - this.x;
		int num2 = toY - this.y;
		if (num > 1)
		{
			this.frame = this.setFrameAngle(Res.angle(num, num2));
		}
		if (Res.abs(this.vx) > 0)
		{
			if (Res.abs(this.x - toX) < Res.abs(this.vx))
			{
				this.x = toX;
				this.vx = 0;
			}
			else
			{
				this.x += this.vx;
			}
		}
		else
		{
			this.x = toX;
			this.vx = 0;
		}
		if (Res.abs(this.vy) <= 0)
		{
			this.y = toY;
			this.vy = 0;
			return;
		}
		if (Res.abs(this.y - toY) < Res.abs(this.vy))
		{
			this.y = toY;
			this.vy = 0;
			return;
		}
		this.y += this.vy;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000744D8 File Offset: 0x000726D8
	public void paint_Arrow(mGraphics g, FrameImage frm, int anchor, bool isCountFr)
	{
		if (frm != null)
		{
			int num = frm.nFrame / 3;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = 3;
			int num3 = ((frm.nFrame <= 3) ? (this.f % num) : ((this.f / num2 % 2 != 0) ? 3 : 0));
			int num4 = num * (int)this.mImageArrow[this.frame] + num3;
			if (frm.nFrame < 3)
			{
				num4 = this.f / num2 % frm.nFrame;
			}
			if (isCountFr)
			{
				num4 = this.f / num2 % frm.nFrame;
			}
			frm.drawFrame(num4, this.x, this.y, (int)this.mXoayArrow[this.frame], anchor, g);
		}
	}

	// Token: 0x04000DE8 RID: 3560
	public sbyte type;

	// Token: 0x04000DE9 RID: 3561
	public int x;

	// Token: 0x04000DEA RID: 3562
	public int y;

	// Token: 0x04000DEB RID: 3563
	public int g;

	// Token: 0x04000DEC RID: 3564
	public int v;

	// Token: 0x04000DED RID: 3565
	public int vMax;

	// Token: 0x04000DEE RID: 3566
	public int w;

	// Token: 0x04000DEF RID: 3567
	public int h;

	// Token: 0x04000DF0 RID: 3568
	public int color;

	// Token: 0x04000DF1 RID: 3569
	public int limitY;

	// Token: 0x04000DF2 RID: 3570
	public int vx;

	// Token: 0x04000DF3 RID: 3571
	public int vy;

	// Token: 0x04000DF4 RID: 3572
	public int x2;

	// Token: 0x04000DF5 RID: 3573
	public int y2;

	// Token: 0x04000DF6 RID: 3574
	public int toX;

	// Token: 0x04000DF7 RID: 3575
	public int toY;

	// Token: 0x04000DF8 RID: 3576
	public int dis;

	// Token: 0x04000DF9 RID: 3577
	public int f;

	// Token: 0x04000DFA RID: 3578
	public int ftam;

	// Token: 0x04000DFB RID: 3579
	public int fRe;

	// Token: 0x04000DFC RID: 3580
	public int frame;

	// Token: 0x04000DFD RID: 3581
	public int maxframe;

	// Token: 0x04000DFE RID: 3582
	public int fSmall;

	// Token: 0x04000DFF RID: 3583
	public int goc;

	// Token: 0x04000E00 RID: 3584
	public int gocT_Arc;

	// Token: 0x04000E01 RID: 3585
	public int idir;

	// Token: 0x04000E02 RID: 3586
	public int dirThrow;

	// Token: 0x04000E03 RID: 3587
	public int dir;

	// Token: 0x04000E04 RID: 3588
	public int dir_nguoc;

	// Token: 0x04000E05 RID: 3589
	public int idSkill;

	// Token: 0x04000E06 RID: 3590
	public int id;

	// Token: 0x04000E07 RID: 3591
	public int levelPaint;

	// Token: 0x04000E08 RID: 3592
	public int num_per_frame = 1;

	// Token: 0x04000E09 RID: 3593
	public int life;

	// Token: 0x04000E0A RID: 3594
	public int goc_Arc;

	// Token: 0x04000E0B RID: 3595
	public int vx1000;

	// Token: 0x04000E0C RID: 3596
	public int vy1000;

	// Token: 0x04000E0D RID: 3597
	public int va;

	// Token: 0x04000E0E RID: 3598
	public int x1000;

	// Token: 0x04000E0F RID: 3599
	public int y1000;

	// Token: 0x04000E10 RID: 3600
	public int vX1000;

	// Token: 0x04000E11 RID: 3601
	public int vY1000;

	// Token: 0x04000E12 RID: 3602
	public long time;

	// Token: 0x04000E13 RID: 3603
	public long timecount;

	// Token: 0x04000E14 RID: 3604
	public MyVector vecEffPoint;

	// Token: 0x04000E15 RID: 3605
	public string name;

	// Token: 0x04000E16 RID: 3606
	public string info;

	// Token: 0x04000E17 RID: 3607
	public bool isRemove;

	// Token: 0x04000E18 RID: 3608
	public bool isSmall;

	// Token: 0x04000E19 RID: 3609
	public bool isPaint;

	// Token: 0x04000E1A RID: 3610
	public bool isChange;

	// Token: 0x04000E1B RID: 3611
	public static FrameImage[] FraEffInMap;

	// Token: 0x04000E1C RID: 3612
	public FrameImage fraImgEff;

	// Token: 0x04000E1D RID: 3613
	public FrameImage fraImgEff_2;

	// Token: 0x04000E1E RID: 3614
	public short index;

	// Token: 0x04000E1F RID: 3615
	public byte[] mpaintone_Arrow = new byte[]
	{
		12, 11, 10, 9, 8, 7, 6, 5, 4, 3,
		2, 1, 0, 23, 22, 21, 20, 19, 18, 17,
		16, 15, 14, 13
	};

	// Token: 0x04000E20 RID: 3616
	public byte[] mImageArrow = new byte[]
	{
		0, 0, 2, 1, 1, 2, 0, 0, 2, 1,
		1, 2, 0, 0, 2, 1, 1, 2, 0, 0,
		2, 1, 1, 2
	};

	// Token: 0x04000E21 RID: 3617
	public byte[] mXoayArrow = new byte[]
	{
		2, 2, 3, 3, 3, 4, 5, 5, 5, 5,
		5, 1, 0, 0, 0, 0, 0, 7, 6, 6,
		6, 6, 6, 2
	};
}

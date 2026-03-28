using System;

// Token: 0x0200007A RID: 122
public class Point
{
	// Token: 0x0600041E RID: 1054 RVA: 0x00027608 File Offset: 0x00025808
	public Point()
	{
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0002766C File Offset: 0x0002586C
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000276DC File Offset: 0x000258DC
	public Point(int x, int y, int goc)
	{
		this.x = x;
		this.y = y;
		this.goc = goc;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0000636C File Offset: 0x0000456C
	public void update()
	{
		this.f++;
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x000063A2 File Offset: 0x000045A2
	public void update_not_f()
	{
		this.x += this.vx;
		this.y += this.vy;
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00027754 File Offset: 0x00025954
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

	// Token: 0x06000424 RID: 1060 RVA: 0x000277BC File Offset: 0x000259BC
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

	// Token: 0x06000425 RID: 1061 RVA: 0x00027824 File Offset: 0x00025A24
	public int setFrameAngle(int goc)
	{
		int result;
		if (goc <= 15 || goc > 345)
		{
			result = 12;
		}
		else
		{
			int num = (goc - 15) / 15 + 1;
			if (num > 24)
			{
				num = 24;
			}
			result = (int)this.mpaintone_Arrow[num];
		}
		return result;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0002786C File Offset: 0x00025A6C
	public void create_Arrow(int vMax)
	{
		this.vMax = vMax;
		int dx = this.toX - this.x;
		int dy = this.toY - this.y;
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
		this.create_Speed(dx, dy);
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x000278DC File Offset: 0x00025ADC
	public void create_Speed(int dx, int dy)
	{
		int frameAngle = Res.angle(dx, dy);
		this.frame = this.setFrameAngle(frameAngle);
		int num = Res.getDistance(dx, dy) / this.vMax;
		if (num == 0)
		{
			num = 1;
		}
		int num2 = dx / num;
		int num3 = dy / num;
		if (num2 == 0 && dx < num)
		{
			num2 = ((dx >= 0) ? 1 : -1);
		}
		if (num3 == 0 && dy < num)
		{
			num3 = ((dy >= 0) ? 1 : -1);
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

	// Token: 0x06000428 RID: 1064 RVA: 0x00027990 File Offset: 0x00025B90
	public void moveTo_xy(int toX, int toY)
	{
		int num = toX - this.x;
		int dy = toY - this.y;
		if (num > 1)
		{
			int frameAngle = Res.angle(num, dy);
			this.frame = this.setFrameAngle(frameAngle);
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
		if (Res.abs(this.vy) > 0)
		{
			if (Res.abs(this.y - toY) < Res.abs(this.vy))
			{
				this.y = toY;
				this.vy = 0;
			}
			else
			{
				this.y += this.vy;
			}
		}
		else
		{
			this.y = toY;
			this.vy = 0;
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00027A9C File Offset: 0x00025C9C
	public void paint_Arrow(mGraphics g, FrameImage frm, int anchor, bool isCountFr)
	{
		if (frm == null)
		{
			return;
		}
		int num = frm.nFrame / 3;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = 3;
		int num3;
		if (frm.nFrame > 3)
		{
			num3 = ((this.f / num2 % 2 != 0) ? 3 : 0);
		}
		else
		{
			num3 = this.f % num;
		}
		int idx = num * (int)this.mImageArrow[this.frame] + num3;
		if (frm.nFrame < 3)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		if (isCountFr)
		{
			idx = this.f / num2 % frm.nFrame;
		}
		frm.drawFrame(idx, this.x, this.y, (int)this.mXoayArrow[this.frame], anchor, g);
	}

	// Token: 0x0400071D RID: 1821
	public sbyte type;

	// Token: 0x0400071E RID: 1822
	public int x;

	// Token: 0x0400071F RID: 1823
	public int y;

	// Token: 0x04000720 RID: 1824
	public int g;

	// Token: 0x04000721 RID: 1825
	public int v;

	// Token: 0x04000722 RID: 1826
	public int vMax;

	// Token: 0x04000723 RID: 1827
	public int w;

	// Token: 0x04000724 RID: 1828
	public int h;

	// Token: 0x04000725 RID: 1829
	public int color;

	// Token: 0x04000726 RID: 1830
	public int limitY;

	// Token: 0x04000727 RID: 1831
	public int vx;

	// Token: 0x04000728 RID: 1832
	public int vy;

	// Token: 0x04000729 RID: 1833
	public int x2;

	// Token: 0x0400072A RID: 1834
	public int y2;

	// Token: 0x0400072B RID: 1835
	public int toX;

	// Token: 0x0400072C RID: 1836
	public int toY;

	// Token: 0x0400072D RID: 1837
	public int dis;

	// Token: 0x0400072E RID: 1838
	public int f;

	// Token: 0x0400072F RID: 1839
	public int ftam;

	// Token: 0x04000730 RID: 1840
	public int fRe;

	// Token: 0x04000731 RID: 1841
	public int frame;

	// Token: 0x04000732 RID: 1842
	public int maxframe;

	// Token: 0x04000733 RID: 1843
	public int fSmall;

	// Token: 0x04000734 RID: 1844
	public int goc;

	// Token: 0x04000735 RID: 1845
	public int gocT_Arc;

	// Token: 0x04000736 RID: 1846
	public int idir;

	// Token: 0x04000737 RID: 1847
	public int dirThrow;

	// Token: 0x04000738 RID: 1848
	public int dir;

	// Token: 0x04000739 RID: 1849
	public int dir_nguoc;

	// Token: 0x0400073A RID: 1850
	public int idSkill;

	// Token: 0x0400073B RID: 1851
	public int id;

	// Token: 0x0400073C RID: 1852
	public int levelPaint;

	// Token: 0x0400073D RID: 1853
	public int num_per_frame = 1;

	// Token: 0x0400073E RID: 1854
	public int life;

	// Token: 0x0400073F RID: 1855
	public int goc_Arc;

	// Token: 0x04000740 RID: 1856
	public int vx1000;

	// Token: 0x04000741 RID: 1857
	public int vy1000;

	// Token: 0x04000742 RID: 1858
	public int va;

	// Token: 0x04000743 RID: 1859
	public int x1000;

	// Token: 0x04000744 RID: 1860
	public int y1000;

	// Token: 0x04000745 RID: 1861
	public int vX1000;

	// Token: 0x04000746 RID: 1862
	public int vY1000;

	// Token: 0x04000747 RID: 1863
	public long time;

	// Token: 0x04000748 RID: 1864
	public long timecount;

	// Token: 0x04000749 RID: 1865
	public MyVector vecEffPoint;

	// Token: 0x0400074A RID: 1866
	public string name;

	// Token: 0x0400074B RID: 1867
	public string info;

	// Token: 0x0400074C RID: 1868
	public bool isRemove;

	// Token: 0x0400074D RID: 1869
	public bool isSmall;

	// Token: 0x0400074E RID: 1870
	public bool isPaint;

	// Token: 0x0400074F RID: 1871
	public bool isChange;

	// Token: 0x04000750 RID: 1872
	public static FrameImage[] FraEffInMap;

	// Token: 0x04000751 RID: 1873
	public FrameImage fraImgEff;

	// Token: 0x04000752 RID: 1874
	public FrameImage fraImgEff_2;

	// Token: 0x04000753 RID: 1875
	public short index;

	// Token: 0x04000754 RID: 1876
	public byte[] mpaintone_Arrow = new byte[]
	{
		12,
		11,
		10,
		9,
		8,
		7,
		6,
		5,
		4,
		3,
		2,
		1,
		0,
		23,
		22,
		21,
		20,
		19,
		18,
		17,
		16,
		15,
		14,
		13
	};

	// Token: 0x04000755 RID: 1877
	public byte[] mImageArrow = new byte[]
	{
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2,
		0,
		0,
		2,
		1,
		1,
		2
	};

	// Token: 0x04000756 RID: 1878
	public byte[] mXoayArrow = new byte[]
	{
		2,
		2,
		3,
		3,
		3,
		4,
		5,
		5,
		5,
		5,
		5,
		1,
		0,
		0,
		0,
		0,
		0,
		7,
		6,
		6,
		6,
		6,
		6,
		2
	};
}

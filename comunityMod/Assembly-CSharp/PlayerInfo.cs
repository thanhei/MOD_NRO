using System;

// Token: 0x0200008A RID: 138
public class PlayerInfo
{
	// Token: 0x06000740 RID: 1856 RVA: 0x00073FEA File Offset: 0x000721EA
	public string getName()
	{
		return this.name;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00073FF2 File Offset: 0x000721F2
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0007400C File Offset: 0x0007220C
	public void setName(string name)
	{
		this.name = name;
		if (name.Length > 9)
		{
			this.showName = name.Substring(0, 8);
			return;
		}
		this.showName = name;
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x00004887 File Offset: 0x00002A87
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00074035 File Offset: 0x00072235
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x04000DDA RID: 3546
	public string name;

	// Token: 0x04000DDB RID: 3547
	public string showName;

	// Token: 0x04000DDC RID: 3548
	public string status;

	// Token: 0x04000DDD RID: 3549
	public int IDDB;

	// Token: 0x04000DDE RID: 3550
	internal int exp;

	// Token: 0x04000DDF RID: 3551
	public bool isReady;

	// Token: 0x04000DE0 RID: 3552
	public int xu;

	// Token: 0x04000DE1 RID: 3553
	public int gold;

	// Token: 0x04000DE2 RID: 3554
	public string strMoney = string.Empty;

	// Token: 0x04000DE3 RID: 3555
	public sbyte finishPosition;

	// Token: 0x04000DE4 RID: 3556
	public bool isMaster;

	// Token: 0x04000DE5 RID: 3557
	public static Image[] imgStart;

	// Token: 0x04000DE6 RID: 3558
	public sbyte[] indexLv;

	// Token: 0x04000DE7 RID: 3559
	public int onlineTime;
}

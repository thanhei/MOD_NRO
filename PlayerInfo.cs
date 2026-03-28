using System;

// Token: 0x02000079 RID: 121
public class PlayerInfo
{
	// Token: 0x06000419 RID: 1049 RVA: 0x00006312 File Offset: 0x00004512
	public string getName()
	{
		return this.name;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0000631A File Offset: 0x0000451A
	public void setMoney(int m)
	{
		this.xu = m;
		this.strMoney = GameCanvas.getMoneys(this.xu);
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00006334 File Offset: 0x00004534
	public void setName(string name)
	{
		this.name = name;
		if (name.Length > 9)
		{
			this.showName = name.Substring(0, 8);
		}
		else
		{
			this.showName = name;
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x000045ED File Offset: 0x000027ED
	public void paint(mGraphics g, int x, int y)
	{
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00006364 File Offset: 0x00004564
	public int getExp()
	{
		return this.exp;
	}

	// Token: 0x0400070F RID: 1807
	public string name;

	// Token: 0x04000710 RID: 1808
	public string showName;

	// Token: 0x04000711 RID: 1809
	public string status;

	// Token: 0x04000712 RID: 1810
	public int IDDB;

	// Token: 0x04000713 RID: 1811
	private int exp;

	// Token: 0x04000714 RID: 1812
	public bool isReady;

	// Token: 0x04000715 RID: 1813
	public int xu;

	// Token: 0x04000716 RID: 1814
	public int gold;

	// Token: 0x04000717 RID: 1815
	public string strMoney = string.Empty;

	// Token: 0x04000718 RID: 1816
	public sbyte finishPosition;

	// Token: 0x04000719 RID: 1817
	public bool isMaster;

	// Token: 0x0400071A RID: 1818
	public static Image[] imgStart;

	// Token: 0x0400071B RID: 1819
	public sbyte[] indexLv;

	// Token: 0x0400071C RID: 1820
	public int onlineTime;
}

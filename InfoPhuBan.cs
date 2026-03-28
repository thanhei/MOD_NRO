using System;

// Token: 0x02000061 RID: 97
public class InfoPhuBan
{
	// Token: 0x06000380 RID: 896 RVA: 0x00022A90 File Offset: 0x00020C90
	public InfoPhuBan(int type_PB, short idmapPaint, string nameTeam1, string nameTeam2, int maxPoint, short timeSecond)
	{
		this.type_PB = type_PB;
		this.idmapPaint = idmapPaint;
		this.nameTeam1 = nameTeam1;
		this.nameTeam2 = nameTeam2;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
		this.maxPoint = maxPoint;
		if (this.maxPoint <= 0)
		{
			this.maxPoint = 1;
		}
		this.pointTeam1 = 0;
		this.pointTeam2 = 0;
		this.owner = 0;
		this.color_1 = 4;
		this.color_2 = 6;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00005C0F File Offset: 0x00003E0F
	public void updateTime(int type_PB, short timeSecond)
	{
		this.type_PB = type_PB;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00005C2A File Offset: 0x00003E2A
	public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
	{
		this.type_PB = type_PB;
		this.pointTeam1 = pointTeam1;
		this.pointTeam2 = pointTeam2;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00005C41 File Offset: 0x00003E41
	public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
	{
		this.type_PB = type_PB;
		this.lifeTeam1 = lifeTeam1;
		this.lifeTeam2 = lifeTeam2;
	}

	// Token: 0x0400059E RID: 1438
	public int type_PB;

	// Token: 0x0400059F RID: 1439
	public int maxPoint;

	// Token: 0x040005A0 RID: 1440
	public int pointTeam1;

	// Token: 0x040005A1 RID: 1441
	public int pointTeam2;

	// Token: 0x040005A2 RID: 1442
	public int color_1;

	// Token: 0x040005A3 RID: 1443
	public int color_2;

	// Token: 0x040005A4 RID: 1444
	public int maxLife = 1;

	// Token: 0x040005A5 RID: 1445
	public int lifeTeam1;

	// Token: 0x040005A6 RID: 1446
	public int lifeTeam2;

	// Token: 0x040005A7 RID: 1447
	public string nameTeam1;

	// Token: 0x040005A8 RID: 1448
	public string nameTeam2;

	// Token: 0x040005A9 RID: 1449
	public short idmapPaint;

	// Token: 0x040005AA RID: 1450
	public short timeSecond;

	// Token: 0x040005AB RID: 1451
	public short timepaintSecond;

	// Token: 0x040005AC RID: 1452
	public short maxtimeSecond = 1;

	// Token: 0x040005AD RID: 1453
	public byte owner;

	// Token: 0x040005AE RID: 1454
	public long timeStart;

	// Token: 0x040005AF RID: 1455
	public MyVector vecInfo = new MyVector("vecInfo chientruong");
}

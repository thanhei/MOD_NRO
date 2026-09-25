using System;

// Token: 0x02000051 RID: 81
public class InfoPhuBan
{
	// Token: 0x06000485 RID: 1157 RVA: 0x0004CA94 File Offset: 0x0004AC94
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

	// Token: 0x06000486 RID: 1158 RVA: 0x0004CB30 File Offset: 0x0004AD30
	public void updateTime(int type_PB, short timeSecond)
	{
		this.type_PB = type_PB;
		this.timeSecond = timeSecond;
		this.timeStart = GameCanvas.timeNow;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0004CB4B File Offset: 0x0004AD4B
	public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
	{
		this.type_PB = type_PB;
		this.pointTeam1 = pointTeam1;
		this.pointTeam2 = pointTeam2;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x0004CB62 File Offset: 0x0004AD62
	public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
	{
		this.type_PB = type_PB;
		this.lifeTeam1 = lifeTeam1;
		this.lifeTeam2 = lifeTeam2;
	}

	// Token: 0x0400094C RID: 2380
	public int type_PB;

	// Token: 0x0400094D RID: 2381
	public int maxPoint;

	// Token: 0x0400094E RID: 2382
	public int pointTeam1;

	// Token: 0x0400094F RID: 2383
	public int pointTeam2;

	// Token: 0x04000950 RID: 2384
	public int color_1;

	// Token: 0x04000951 RID: 2385
	public int color_2;

	// Token: 0x04000952 RID: 2386
	public int maxLife = 1;

	// Token: 0x04000953 RID: 2387
	public int lifeTeam1;

	// Token: 0x04000954 RID: 2388
	public int lifeTeam2;

	// Token: 0x04000955 RID: 2389
	public string nameTeam1;

	// Token: 0x04000956 RID: 2390
	public string nameTeam2;

	// Token: 0x04000957 RID: 2391
	public short idmapPaint;

	// Token: 0x04000958 RID: 2392
	public short timeSecond;

	// Token: 0x04000959 RID: 2393
	public short timepaintSecond;

	// Token: 0x0400095A RID: 2394
	public short maxtimeSecond = 1;

	// Token: 0x0400095B RID: 2395
	public byte owner;

	// Token: 0x0400095C RID: 2396
	public long timeStart;

	// Token: 0x0400095D RID: 2397
	public MyVector vecInfo = new MyVector("vecInfo chientruong");
}

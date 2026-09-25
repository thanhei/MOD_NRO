using System;

// Token: 0x0200002D RID: 45
public class EffectChar
{
	// Token: 0x0600026D RID: 621 RVA: 0x0002F462 File Offset: 0x0002D662
	public EffectChar(sbyte templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x0400054D RID: 1357
	public static EffectTemplate[] effTemplates;

	// Token: 0x0400054E RID: 1358
	public static sbyte EFF_ME;

	// Token: 0x0400054F RID: 1359
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x04000550 RID: 1360
	public int timeStart;

	// Token: 0x04000551 RID: 1361
	public int timeLenght;

	// Token: 0x04000552 RID: 1362
	public short param;

	// Token: 0x04000553 RID: 1363
	public EffectTemplate template;
}

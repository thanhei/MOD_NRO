using System;

// Token: 0x02000055 RID: 85
public class EffectChar
{
	// Token: 0x060002F8 RID: 760 RVA: 0x0000592F File Offset: 0x00003B2F
	public EffectChar(short templateId, int timeStart, int timeLenght, short param)
	{
		this.template = EffectChar.effTemplates[(int)templateId];
		this.timeStart = timeStart;
		this.timeLenght = timeLenght / 1000;
		this.param = param;
	}

	// Token: 0x040004FD RID: 1277
	public static EffectTemplate[] effTemplates;

	// Token: 0x040004FE RID: 1278
	public static sbyte EFF_ME;

	// Token: 0x040004FF RID: 1279
	public static sbyte EFF_FRIEND = 1;

	// Token: 0x04000500 RID: 1280
	public int timeStart;

	// Token: 0x04000501 RID: 1281
	public int timeLenght;

	// Token: 0x04000502 RID: 1282
	public short param;

	// Token: 0x04000503 RID: 1283
	public EffectTemplate template;
}

using System;

// Token: 0x020000B6 RID: 182
public class Task
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0008AFC4 File Offset: 0x000891C4
	public Task(short taskId, sbyte index, string name, string detail, string[] subNames, short[] counts, short count, string[] contentInfo)
	{
		this.taskId = taskId;
		this.index = (int)index;
		this.names = mFont.tahoma_7b_green2.splitFontArray(name, Panel.WIDTH_PANEL - 20);
		this.details = mFont.tahoma_7.splitFontArray(detail, Panel.WIDTH_PANEL - 20);
		this.subNames = subNames;
		this.counts = counts;
		this.count = count;
		this.contentInfo = contentInfo;
	}

	// Token: 0x040010BD RID: 4285
	public int index;

	// Token: 0x040010BE RID: 4286
	public int max;

	// Token: 0x040010BF RID: 4287
	public short[] counts;

	// Token: 0x040010C0 RID: 4288
	public short taskId;

	// Token: 0x040010C1 RID: 4289
	public string[] names;

	// Token: 0x040010C2 RID: 4290
	public string[] details;

	// Token: 0x040010C3 RID: 4291
	public string[] subNames;

	// Token: 0x040010C4 RID: 4292
	public string[] contentInfo;

	// Token: 0x040010C5 RID: 4293
	public short count;
}

using System;

// Token: 0x02000091 RID: 145
public class Task
{
	// Token: 0x060004BA RID: 1210 RVA: 0x0002EF48 File Offset: 0x0002D148
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

	// Token: 0x0400081F RID: 2079
	public int index;

	// Token: 0x04000820 RID: 2080
	public int max;

	// Token: 0x04000821 RID: 2081
	public short[] counts;

	// Token: 0x04000822 RID: 2082
	public short taskId;

	// Token: 0x04000823 RID: 2083
	public string[] names;

	// Token: 0x04000824 RID: 2084
	public string[] details;

	// Token: 0x04000825 RID: 2085
	public string[] subNames;

	// Token: 0x04000826 RID: 2086
	public string[] contentInfo;

	// Token: 0x04000827 RID: 2087
	public short count;
}

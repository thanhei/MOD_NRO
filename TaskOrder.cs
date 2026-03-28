using System;

// Token: 0x02000092 RID: 146
public class TaskOrder
{
	// Token: 0x060004BB RID: 1211 RVA: 0x00006B18 File Offset: 0x00004D18
	public TaskOrder(sbyte taskId, short count, short maxCount, string name, string description, sbyte killId, sbyte mapId)
	{
		this.count = (int)count;
		this.maxCount = maxCount;
		this.taskId = (int)taskId;
		this.name = name;
		this.description = description;
		this.killId = (int)killId;
		this.mapId = (int)mapId;
	}

	// Token: 0x04000828 RID: 2088
	public const sbyte TASK_DAY = 0;

	// Token: 0x04000829 RID: 2089
	public const sbyte TASK_BOSS = 1;

	// Token: 0x0400082A RID: 2090
	public int taskId;

	// Token: 0x0400082B RID: 2091
	public int count;

	// Token: 0x0400082C RID: 2092
	public short maxCount;

	// Token: 0x0400082D RID: 2093
	public string name;

	// Token: 0x0400082E RID: 2094
	public string description;

	// Token: 0x0400082F RID: 2095
	public int killId;

	// Token: 0x04000830 RID: 2096
	public int mapId;
}

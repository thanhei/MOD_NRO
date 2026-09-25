using System;

// Token: 0x020000B7 RID: 183
public class TaskOrder
{
	// Token: 0x0600099B RID: 2459 RVA: 0x0008B038 File Offset: 0x00089238
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

	// Token: 0x040010C6 RID: 4294
	public const sbyte TASK_DAY = 0;

	// Token: 0x040010C7 RID: 4295
	public const sbyte TASK_BOSS = 1;

	// Token: 0x040010C8 RID: 4296
	public int taskId;

	// Token: 0x040010C9 RID: 4297
	public int count;

	// Token: 0x040010CA RID: 4298
	public short maxCount;

	// Token: 0x040010CB RID: 4299
	public string name;

	// Token: 0x040010CC RID: 4300
	public string description;

	// Token: 0x040010CD RID: 4301
	public int killId;

	// Token: 0x040010CE RID: 4302
	public int mapId;
}

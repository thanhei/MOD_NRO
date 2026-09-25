using System;
using System.Collections.Generic;

namespace Mod.ModHelper
{
	// Token: 0x02000139 RID: 313
	public class MainThreadDispatcher
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x000B0A85 File Offset: 0x000AEC85
		public static void Dispatch(Action action)
		{
			MainThreadDispatcher.Queue.Enqueue(action);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000B0A92 File Offset: 0x000AEC92
		public static void update()
		{
			while (MainThreadDispatcher.Queue.Count > 0)
			{
				MainThreadDispatcher.Queue.Dequeue()();
			}
		}

		// Token: 0x04001764 RID: 5988
		private static readonly Queue<Action> Queue = new Queue<Action>();
	}
}

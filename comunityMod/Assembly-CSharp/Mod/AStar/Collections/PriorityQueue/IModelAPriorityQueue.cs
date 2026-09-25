using System;

namespace Mod.AStar.Collections.PriorityQueue
{
	// Token: 0x020001A7 RID: 423
	internal interface IModelAPriorityQueue<T>
	{
		// Token: 0x06001238 RID: 4664
		int Push(T item);

		// Token: 0x06001239 RID: 4665
		T Pop();

		// Token: 0x0600123A RID: 4666
		T Peek();

		// Token: 0x0600123B RID: 4667
		void Clear();

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600123C RID: 4668
		int Count { get; }
	}
}

using System;
using System.Collections.Generic;

namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001AA RID: 426
	internal interface IModelAGraph<T>
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600124A RID: 4682
		bool HasOpenNodes { get; }

		// Token: 0x0600124B RID: 4683
		IEnumerable<T> GetSuccessors(T node);

		// Token: 0x0600124C RID: 4684
		T GetParent(T node);

		// Token: 0x0600124D RID: 4685
		void OpenNode(T node);

		// Token: 0x0600124E RID: 4686
		T GetOpenNodeWithSmallestF();
	}
}

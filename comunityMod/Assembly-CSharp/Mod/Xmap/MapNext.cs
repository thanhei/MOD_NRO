using System;

namespace Mod.Xmap
{
	// Token: 0x02000103 RID: 259
	internal struct MapNext
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x000A41F5 File Offset: 0x000A23F5
		internal MapNext(int mapStart, int to, TypeMapNext type, int[] info)
		{
			this.mapStart = mapStart;
			this.to = to;
			this.type = type;
			this.info = info;
		}

		// Token: 0x0400150A RID: 5386
		internal int mapStart;

		// Token: 0x0400150B RID: 5387
		internal int to;

		// Token: 0x0400150C RID: 5388
		internal TypeMapNext type;

		// Token: 0x0400150D RID: 5389
		internal int[] info;
	}
}

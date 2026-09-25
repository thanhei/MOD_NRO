using System;
using System.Collections.Generic;

namespace Mod.Xmap
{
	// Token: 0x02000104 RID: 260
	internal struct GroupMap
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x000A4214 File Offset: 0x000A2414
		internal GroupMap(string[] nameGroup, List<int> maps)
		{
			this.names = nameGroup;
			this.maps = maps;
		}

		// Token: 0x0400150E RID: 5390
		internal string[] names;

		// Token: 0x0400150F RID: 5391
		internal List<int> maps;
	}
}

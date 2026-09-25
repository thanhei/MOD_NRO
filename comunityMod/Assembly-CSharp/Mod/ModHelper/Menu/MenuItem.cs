using System;

namespace Mod.ModHelper.Menu
{
	// Token: 0x02000146 RID: 326
	public class MenuItem
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x000B15BB File Offset: 0x000AF7BB
		public MenuItem(string caption, MenuAction action)
		{
			this.caption = caption;
			this.action = action;
		}

		// Token: 0x0400177F RID: 6015
		public string caption;

		// Token: 0x04001780 RID: 6016
		public MenuAction action;
	}
}

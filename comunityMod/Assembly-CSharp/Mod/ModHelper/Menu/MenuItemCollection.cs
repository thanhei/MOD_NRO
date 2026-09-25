using System;
using System.Collections.Generic;

namespace Mod.ModHelper.Menu
{
	// Token: 0x02000147 RID: 327
	public class MenuItemCollection
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x000B15D1 File Offset: 0x000AF7D1
		public MenuItemCollection(Action<List<MenuItem>> action)
		{
			this.menuItems = new List<MenuItem>();
			action(this.menuItems);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000B15F0 File Offset: 0x000AF7F0
		public int Count
		{
			get
			{
				return this.menuItems.Count;
			}
		}

		// Token: 0x170000B0 RID: 176
		public MenuItem this[int index]
		{
			get
			{
				return this.menuItems[index];
			}
		}

		// Token: 0x04001781 RID: 6017
		public List<MenuItem> menuItems;
	}
}

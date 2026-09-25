using System;

namespace Mod.ModMenu
{
	// Token: 0x0200012E RID: 302
	internal class ModMenuItemBooleanConfig : ModMenuItemConfig
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000AD2A9 File Offset: 0x000AB4A9
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000AD2B1 File Offset: 0x000AB4B1
		internal string RMSName { get; set; } = "";

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000AD2BA File Offset: 0x000AB4BA
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x000AD2C2 File Offset: 0x000AB4C2
		internal Func<bool> GetValueFunc { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000AD2CB File Offset: 0x000AB4CB
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x000AD2D3 File Offset: 0x000AB4D3
		internal Action<bool> SetValueAction { get; set; }
	}
}

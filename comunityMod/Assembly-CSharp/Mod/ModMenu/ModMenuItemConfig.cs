using System;

namespace Mod.ModMenu
{
	// Token: 0x0200012F RID: 303
	internal class ModMenuItemConfig
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x000AD2EF File Offset: 0x000AB4EF
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x000AD2F7 File Offset: 0x000AB4F7
		internal string ID { get; set; } = "";

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x000AD300 File Offset: 0x000AB500
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x000AD308 File Offset: 0x000AB508
		internal string Title { get; set; } = "";

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000AD311 File Offset: 0x000AB511
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000AD319 File Offset: 0x000AB519
		internal string Description { get; set; } = "";

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x000AD322 File Offset: 0x000AB522
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x000AD32A File Offset: 0x000AB52A
		internal Func<bool> GetIsDisabled { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000AD333 File Offset: 0x000AB533
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x000AD33B File Offset: 0x000AB53B
		internal Func<string> GetDisabledReason { get; set; }
	}
}

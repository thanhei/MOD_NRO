using System;

namespace Mod.ModMenu
{
	// Token: 0x02000130 RID: 304
	internal class ModMenuItemFunction : ModMenuItem
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000AD36D File Offset: 0x000AB56D
		internal Action Action
		{
			get
			{
				return this._config.Action;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000AD37A File Offset: 0x000AB57A
		internal ModMenuItemFunction(ModMenuItemFunctionConfig config)
			: base(config)
		{
			this._config = config;
		}

		// Token: 0x040016EE RID: 5870
		private ModMenuItemFunctionConfig _config;
	}
}

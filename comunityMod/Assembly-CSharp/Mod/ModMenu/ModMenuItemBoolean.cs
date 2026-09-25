using System;

namespace Mod.ModMenu
{
	// Token: 0x0200012D RID: 301
	internal class ModMenuItemBoolean : ModMenuItem
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000AD23C File Offset: 0x000AB43C
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x000AD249 File Offset: 0x000AB449
		internal bool Value
		{
			get
			{
				return this.GetValueFunc();
			}
			set
			{
				this.SetValueAction(value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000AD257 File Offset: 0x000AB457
		internal string RMSName
		{
			get
			{
				return this._config.RMSName;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000AD264 File Offset: 0x000AB464
		internal Func<bool> GetValueFunc
		{
			get
			{
				return this._config.GetValueFunc;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000AD271 File Offset: 0x000AB471
		internal Action<bool> SetValueAction
		{
			get
			{
				return this._config.SetValueAction;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000AD27E File Offset: 0x000AB47E
		internal ModMenuItemBoolean(ModMenuItemBooleanConfig config)
			: base(config)
		{
			this._config = config;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000AD28E File Offset: 0x000AB48E
		internal void SwitchSelection()
		{
			this.SetValueAction(!this.GetValueFunc());
		}

		// Token: 0x040016E5 RID: 5861
		private ModMenuItemBooleanConfig _config;
	}
}

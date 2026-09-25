using System;

namespace Mod.ModMenu
{
	// Token: 0x0200012C RID: 300
	internal class ModMenuItem
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000AD1BA File Offset: 0x000AB3BA
		internal string ID
		{
			get
			{
				return this._config.ID;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000AD1C7 File Offset: 0x000AB3C7
		internal string Title
		{
			get
			{
				return this._config.Title;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000AD1D4 File Offset: 0x000AB3D4
		internal string Description
		{
			get
			{
				return this._config.Description;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000AD1E1 File Offset: 0x000AB3E1
		internal Func<bool> GetIsDisabled
		{
			get
			{
				return this._config.GetIsDisabled;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000AD1EE File Offset: 0x000AB3EE
		internal Func<string> GetDisabledReason
		{
			get
			{
				return this._config.GetDisabledReason;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000AD1FB File Offset: 0x000AB3FB
		internal bool IsDisabled
		{
			get
			{
				return this.GetIsDisabled != null && this.GetIsDisabled();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x000AD212 File Offset: 0x000AB412
		internal string DisabledReason
		{
			get
			{
				if (this.GetDisabledReason != null)
				{
					return this.GetDisabledReason();
				}
				return "";
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000AD22D File Offset: 0x000AB42D
		internal ModMenuItem(ModMenuItemConfig config)
		{
			this._config = config;
		}

		// Token: 0x040016E4 RID: 5860
		private ModMenuItemConfig _config;
	}
}

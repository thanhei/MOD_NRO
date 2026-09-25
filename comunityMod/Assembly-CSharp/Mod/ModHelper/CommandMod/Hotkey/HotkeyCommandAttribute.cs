using System;

namespace Mod.ModHelper.CommandMod.Hotkey
{
	// Token: 0x0200014F RID: 335
	public class HotkeyCommandAttribute : BaseCommandAttribute
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x000B1995 File Offset: 0x000AFB95
		public HotkeyCommandAttribute(char key)
		{
			this.key = key;
		}

		// Token: 0x04001791 RID: 6033
		public char key;

		// Token: 0x04001792 RID: 6034
		public string agrs = "";
	}
}

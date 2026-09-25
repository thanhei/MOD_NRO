using System;
using Newtonsoft.Json;

namespace Mod.ModHelper.CommandMod.Hotkey
{
	// Token: 0x0200014E RID: 334
	public class HotkeyCommand : BaseCommand
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x000B1978 File Offset: 0x000AFB78
		public void execute()
		{
			this.method.Invoke(null, this.parameters);
		}

		// Token: 0x0400178E RID: 6030
		public char key;

		// Token: 0x0400178F RID: 6031
		public string fullCommand;

		// Token: 0x04001790 RID: 6032
		[JsonIgnore]
		public object[] parameters;
	}
}

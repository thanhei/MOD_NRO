using System;

namespace Mod.ModHelper.CommandMod.Chat
{
	// Token: 0x02000152 RID: 338
	public class ChatCommandAttribute : BaseCommandAttribute
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x000B1B18 File Offset: 0x000AFD18
		public ChatCommandAttribute(string command)
		{
			this.command = command;
		}

		// Token: 0x04001796 RID: 6038
		public string command;
	}
}

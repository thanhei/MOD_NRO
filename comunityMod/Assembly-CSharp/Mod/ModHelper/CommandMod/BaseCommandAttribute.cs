using System;

namespace Mod.ModHelper.CommandMod
{
	// Token: 0x0200014A RID: 330
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public abstract class BaseCommandAttribute : Attribute
	{
		// Token: 0x04001788 RID: 6024
		public char delimiter = ' ';
	}
}

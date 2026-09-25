using System;

namespace Mod.ModHelper.Menu
{
	// Token: 0x02000140 RID: 320
	public class MenuAction
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x000B1164 File Offset: 0x000AF364
		public MenuAction(Action<int, string, string[]> action)
		{
			this.action = action;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000B1174 File Offset: 0x000AF374
		public MenuAction(Action<int, string> action)
		{
			this.action = delegate(int selected, string caption, string[] _)
			{
				action(selected, caption);
			};
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		public MenuAction(Action<int> action)
		{
			this.action = delegate(int selected, string _, string[] _)
			{
				action(selected);
			};
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000B11DC File Offset: 0x000AF3DC
		public MenuAction(Action action)
		{
			this.action = delegate(int _, string _, string[] _)
			{
				action();
			};
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000B120E File Offset: 0x000AF40E
		public void Invoke(int selected, string caption, string[] captions)
		{
			this.action(selected, caption, captions);
		}

		// Token: 0x04001771 RID: 6001
		public Action<int, string, string[]> action;
	}
}

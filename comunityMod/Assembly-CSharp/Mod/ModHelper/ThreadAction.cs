using System;
using System.Threading;

namespace Mod.ModHelper
{
	// Token: 0x0200013C RID: 316
	public abstract class ThreadAction<T> where T : ThreadAction<T>, new()
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x000B0FBC File Offset: 0x000AF1BC
		public static T gI { get; } = new T();

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x000B0FC3 File Offset: 0x000AF1C3
		public bool IsActing
		{
			get
			{
				Thread thread = this.threadAction;
				return thread != null && thread.IsAlive;
			}
		}

		// Token: 0x06000FA0 RID: 4000
		protected abstract void action();

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000B0FD6 File Offset: 0x000AF1D6
		public void performAction()
		{
			if (this.IsActing)
			{
				this.threadAction.Abort();
			}
			this.executeAction();
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000B0FF1 File Offset: 0x000AF1F1
		protected void executeAction()
		{
			if (Thread.CurrentThread != this.threadAction)
			{
				this.threadAction = new Thread(new ThreadStart(this.executeAction))
				{
					IsBackground = true
				};
				this.threadAction.Start();
				return;
			}
			this.action();
		}

		// Token: 0x0400176C RID: 5996
		protected Thread threadAction;
	}
}

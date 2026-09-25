using System;
using System.Threading;

namespace Mod.ModHelper
{
	// Token: 0x0200013D RID: 317
	internal abstract class ThreadActionUpdate<T> : ThreadAction<T> where T : ThreadActionUpdate<T>, new()
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x000B103C File Offset: 0x000AF23C
		internal new bool IsActing
		{
			get
			{
				return this.isActing;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000FA6 RID: 4006
		internal abstract int Interval { get; }

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000B1044 File Offset: 0x000AF244
		protected override void action()
		{
			while (this.isActing)
			{
				this.update();
				Thread.Sleep(this.Interval);
			}
		}

		// Token: 0x06000FA8 RID: 4008
		protected abstract void update();

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000B1064 File Offset: 0x000AF264
		internal void toggle(bool? isActing = null)
		{
			if (isActing == null)
			{
				isActing = new bool?(!this.isActing);
			}
			if (this.isActing = isActing.Value)
			{
				base.performAction();
				return;
			}
			if (base.IsActing)
			{
				this.threadAction.Abort();
			}
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000B10B6 File Offset: 0x000AF2B6
		internal static void toggle(bool value)
		{
			ThreadAction<T>.gI.toggle(new bool?(value));
		}

		// Token: 0x0400176D RID: 5997
		private bool isActing;
	}
}

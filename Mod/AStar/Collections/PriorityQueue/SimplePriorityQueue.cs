using System;
using System.Collections.Generic;

namespace Mod.AStar.Collections.PriorityQueue
{
	// Token: 0x020001A8 RID: 424
	internal class SimplePriorityQueue<T> : IModelAPriorityQueue<T>
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x000C312A File Offset: 0x000C132A
		public SimplePriorityQueue(IComparer<T> comparer = null)
		{
			this._comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000C3150 File Offset: 0x000C1350
		public T Peek()
		{
			if (this._innerList.Count <= 0)
			{
				return default(T);
			}
			return this._innerList[0];
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000C3181 File Offset: 0x000C1381
		public void Clear()
		{
			this._innerList.Clear();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000C318E File Offset: 0x000C138E
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000C319C File Offset: 0x000C139C
		public int Push(T item)
		{
			int num = this._innerList.Count;
			this._innerList.Add(item);
			while (num != 0)
			{
				int num2 = (num - 1) / 2;
				if (this.OnCompare(num, num2) >= 0)
				{
					break;
				}
				this.SwitchElements(num, num2);
				num = num2;
			}
			return num;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000C31E4 File Offset: 0x000C13E4
		public T Pop()
		{
			T t = this._innerList[0];
			int num = 0;
			this._innerList[0] = this._innerList[this._innerList.Count - 1];
			this._innerList.RemoveAt(this._innerList.Count - 1);
			for (;;)
			{
				int num2 = num;
				int num3 = 2 * num + 1;
				int num4 = 2 * num + 2;
				if (this._innerList.Count > num3 && this.OnCompare(num, num3) > 0)
				{
					num = num3;
				}
				if (this._innerList.Count > num4 && this.OnCompare(num, num4) > 0)
				{
					num = num4;
				}
				if (num == num2)
				{
					break;
				}
				this.SwitchElements(num, num2);
			}
			return t;
		}

		// Token: 0x1700010D RID: 269
		public T this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				this._innerList[index] = value;
				this.Update(index);
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000C32B8 File Offset: 0x000C14B8
		private void Update(int i)
		{
			int num;
			int num2;
			for (num = i; num != 0; num = num2)
			{
				num2 = (num - 1) / 2;
				if (this.OnCompare(num, num2) >= 0)
				{
					break;
				}
				this.SwitchElements(num, num2);
			}
			if (num < i)
			{
				return;
			}
			for (;;)
			{
				int num3 = num;
				int num4 = 2 * num + 1;
				num2 = 2 * num + 2;
				if (this._innerList.Count > num4 && this.OnCompare(num, num4) > 0)
				{
					num = num4;
				}
				if (this._innerList.Count > num2 && this.OnCompare(num, num2) > 0)
				{
					num = num2;
				}
				if (num == num3)
				{
					break;
				}
				this.SwitchElements(num, num3);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000C3340 File Offset: 0x000C1540
		private void SwitchElements(int i, int j)
		{
			T t = this._innerList[i];
			this._innerList[i] = this._innerList[j];
			this._innerList[j] = t;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000C337F File Offset: 0x000C157F
		private int OnCompare(int i, int j)
		{
			return this._comparer.Compare(this._innerList[i], this._innerList[j]);
		}

		// Token: 0x04001983 RID: 6531
		private readonly List<T> _innerList = new List<T>();

		// Token: 0x04001984 RID: 6532
		private readonly IComparer<T> _comparer;
	}
}

using System;
using System.Collections;

// Token: 0x02000076 RID: 118
public class MyHashTable
{
	// Token: 0x060005B2 RID: 1458 RVA: 0x0005744E File Offset: 0x0005564E
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0005745C File Offset: 0x0005565C
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00057469 File Offset: 0x00055669
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x00057476 File Offset: 0x00055676
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00057483 File Offset: 0x00055683
	public void put(object k, object v)
	{
		if (this.h.ContainsKey(k))
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000574AC File Offset: 0x000556AC
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x000574AC File Offset: 0x000556AC
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x000574BA File Offset: 0x000556BA
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x04000C04 RID: 3076
	public Hashtable h = new Hashtable();
}

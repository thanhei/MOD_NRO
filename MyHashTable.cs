using System;
using System.Collections;

// Token: 0x0200000D RID: 13
public class MyHashTable
{
	// Token: 0x0600005F RID: 95 RVA: 0x00004397 File Offset: 0x00002597
	public object get(object k)
	{
		return this.h[k];
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000043A5 File Offset: 0x000025A5
	public void clear()
	{
		this.h.Clear();
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000043B2 File Offset: 0x000025B2
	public IDictionaryEnumerator GetEnumerator()
	{
		return this.h.GetEnumerator();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000043BF File Offset: 0x000025BF
	public int size()
	{
		return this.h.Count;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000043CC File Offset: 0x000025CC
	public void put(object k, object v)
	{
		if (this.h.ContainsKey(k))
		{
			this.h.Remove(k);
		}
		this.h.Add(k, v);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000043F8 File Offset: 0x000025F8
	public void remove(object k)
	{
		this.h.Remove(k);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000043F8 File Offset: 0x000025F8
	public void Remove(string key)
	{
		this.h.Remove(key);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004406 File Offset: 0x00002606
	public bool containsKey(object key)
	{
		return this.h.ContainsKey(key);
	}

	// Token: 0x04000024 RID: 36
	public Hashtable h = new Hashtable();
}

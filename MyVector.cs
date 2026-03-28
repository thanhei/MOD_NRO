using System;
using System.Collections;

// Token: 0x02000011 RID: 17
public class MyVector
{
	// Token: 0x06000070 RID: 112 RVA: 0x00004451 File Offset: 0x00002651
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00004451 File Offset: 0x00002651
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004464 File Offset: 0x00002664
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004473 File Offset: 0x00002673
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004482 File Offset: 0x00002682
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00004498 File Offset: 0x00002698
	public int size()
	{
		if (this.a == null)
		{
			return 0;
		}
		return this.a.Count;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000044B2 File Offset: 0x000026B2
	public object elementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			return this.a[index];
		}
		return null;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000044DA File Offset: 0x000026DA
	public void set(int index, object obj)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004501 File Offset: 0x00002701
	public void setElementAt(object obj, int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00004528 File Offset: 0x00002728
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00004536 File Offset: 0x00002736
	public void removeElementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x0000455C File Offset: 0x0000275C
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000456A File Offset: 0x0000276A
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004577 File Offset: 0x00002777
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004586 File Offset: 0x00002786
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004594 File Offset: 0x00002794
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000027 RID: 39
	private ArrayList a;
}

using System;
using System.Collections;

// Token: 0x0200007A RID: 122
public class MyVector
{
	// Token: 0x060005C4 RID: 1476 RVA: 0x00057AD8 File Offset: 0x00055CD8
	public MyVector()
	{
		this.a = new ArrayList();
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x00057AD8 File Offset: 0x00055CD8
	public MyVector(string s)
	{
		this.a = new ArrayList();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00057AEB File Offset: 0x00055CEB
	public MyVector(ArrayList a)
	{
		this.a = a;
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00057AFA File Offset: 0x00055CFA
	public void addElement(object o)
	{
		this.a.Add(o);
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00057B09 File Offset: 0x00055D09
	public bool contains(object o)
	{
		return this.a.Contains(o);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00057B1C File Offset: 0x00055D1C
	public int size()
	{
		if (this.a == null)
		{
			return 0;
		}
		return this.a.Count;
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00057B33 File Offset: 0x00055D33
	public object elementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			return this.a[index];
		}
		return null;
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00057B55 File Offset: 0x00055D55
	public void set(int index, object obj)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00057B76 File Offset: 0x00055D76
	public void setElementAt(object obj, int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a[index] = obj;
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00057B97 File Offset: 0x00055D97
	public int indexOf(object o)
	{
		return this.a.IndexOf(o);
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00057BA5 File Offset: 0x00055DA5
	public void removeElementAt(int index)
	{
		if (index > -1 && index < this.a.Count)
		{
			this.a.RemoveAt(index);
		}
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x00057BC5 File Offset: 0x00055DC5
	public void removeElement(object o)
	{
		this.a.Remove(o);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00057BD3 File Offset: 0x00055DD3
	public void removeAllElements()
	{
		this.a.Clear();
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x00057BE0 File Offset: 0x00055DE0
	public void insertElementAt(object o, int i)
	{
		this.a.Insert(i, o);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00057BEF File Offset: 0x00055DEF
	public object firstElement()
	{
		return this.a[0];
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00057BFD File Offset: 0x00055DFD
	public object lastElement()
	{
		return this.a[this.a.Count - 1];
	}

	// Token: 0x04000C07 RID: 3079
	internal ArrayList a;
}

using System;

// Token: 0x02000005 RID: 5
public class DataOutputStream
{
	// Token: 0x06000021 RID: 33 RVA: 0x0000416A File Offset: 0x0000236A
	public DataOutputStream()
	{
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000417D File Offset: 0x0000237D
	public DataOutputStream(int len)
	{
		this.w = new myWriter(len);
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000419C File Offset: 0x0000239C
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000041AA File Offset: 0x000023AA
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000041B8 File Offset: 0x000023B8
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000041C6 File Offset: 0x000023C6
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000041D3 File Offset: 0x000023D3
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000041E0 File Offset: 0x000023E0
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000041EE File Offset: 0x000023EE
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000041FC File Offset: 0x000023FC
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x04000008 RID: 8
	private myWriter w = new myWriter();
}

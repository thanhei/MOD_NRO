using System;

// Token: 0x02000027 RID: 39
public class DataOutputStream
{
	// Token: 0x0600023E RID: 574 RVA: 0x0002E1D2 File Offset: 0x0002C3D2
	public DataOutputStream()
	{
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0002E1E5 File Offset: 0x0002C3E5
	public DataOutputStream(int len)
	{
		this.w = new myWriter(len);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0002E204 File Offset: 0x0002C404
	public void writeShort(short i)
	{
		this.w.writeShort(i);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0002E212 File Offset: 0x0002C412
	public void writeInt(int i)
	{
		this.w.writeInt(i);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0002E220 File Offset: 0x0002C420
	public void write(sbyte[] data)
	{
		this.w.writeSByte(data);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0002E22E File Offset: 0x0002C42E
	public sbyte[] toByteArray()
	{
		return this.w.getData();
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0002E23B File Offset: 0x0002C43B
	public void close()
	{
		this.w.Close();
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0002E248 File Offset: 0x0002C448
	public void writeByte(sbyte b)
	{
		this.w.writeByte(b);
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0002E256 File Offset: 0x0002C456
	public void writeUTF(string name)
	{
		this.w.writeUTF(name);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0002E264 File Offset: 0x0002C464
	public void writeBoolean(bool b)
	{
		this.w.writeBoolean(b);
	}

	// Token: 0x040004F8 RID: 1272
	internal myWriter w = new myWriter();
}

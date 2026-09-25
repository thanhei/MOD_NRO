using System;

// Token: 0x0200006B RID: 107
public class Message
{
	// Token: 0x06000547 RID: 1351 RVA: 0x0005334A File Offset: 0x0005154A
	public Message(int command)
	{
		this.command = (sbyte)command;
		this.dos = new myWriter();
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00053365 File Offset: 0x00051565
	public Message()
	{
		this.dos = new myWriter();
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00053378 File Offset: 0x00051578
	public Message(sbyte command)
	{
		this.command = command;
		this.dos = new myWriter();
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00053392 File Offset: 0x00051592
	public Message(sbyte command, sbyte[] data)
	{
		this.command = command;
		this.dis = new myReader(data);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x000533AD File Offset: 0x000515AD
	public sbyte[] getData()
	{
		return this.dos.getData();
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x000533BA File Offset: 0x000515BA
	public myReader reader()
	{
		return this.dis;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000533C2 File Offset: 0x000515C2
	public myWriter writer()
	{
		return this.dos;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000533CA File Offset: 0x000515CA
	public int readInt3Byte()
	{
		return this.dis.readInt();
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00004887 File Offset: 0x00002A87
	public void cleanup()
	{
	}

	// Token: 0x04000B37 RID: 2871
	public sbyte command;

	// Token: 0x04000B38 RID: 2872
	internal myReader dis;

	// Token: 0x04000B39 RID: 2873
	internal myWriter dos;
}

using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class DataInputStream
{
	// Token: 0x06000229 RID: 553 RVA: 0x0002DFE5 File Offset: 0x0002C1E5
	public DataInputStream(string filename)
	{
		this.r = new myReader(ArrayCast.cast(((TextAsset)Resources.Load(filename, typeof(TextAsset))).bytes));
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0002E017 File Offset: 0x0002C217
	public DataInputStream(sbyte[] data)
	{
		this.r = new myReader(data);
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0002E02B File Offset: 0x0002C22B
	public static void update()
	{
		if (DataInputStream.status == 2)
		{
			DataInputStream.status = 1;
			DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
			DataInputStream.status = 0;
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0002E050 File Offset: 0x0002C250
	public static DataInputStream getResourceAsStream(string filename)
	{
		return DataInputStream.__getResourceAsStream(filename);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0002E058 File Offset: 0x0002C258
	internal static DataInputStream _getResourceAsStream(string filename)
	{
		if (DataInputStream.status != 0)
		{
			for (int i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (DataInputStream.status == 0)
				{
					break;
				}
			}
			if (DataInputStream.status != 0)
			{
				Debug.LogError("CANNOT GET INPUTSTREAM " + filename + " WHEN GETTING " + DataInputStream.filenametemp);
				return null;
			}
		}
		DataInputStream.istemp = null;
		DataInputStream.filenametemp = filename;
		DataInputStream.status = 2;
		int j;
		for (j = 0; j < 500; j++)
		{
			Thread.Sleep(5);
			if (DataInputStream.status == 0)
			{
				break;
			}
		}
		if (j == 500)
		{
			Debug.LogError("TOO LONG FOR CREATE INPUTSTREAM " + filename);
			DataInputStream.status = 0;
			return null;
		}
		return DataInputStream.istemp;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0002E100 File Offset: 0x0002C300
	internal static DataInputStream __getResourceAsStream(string filename)
	{
		DataInputStream dataInputStream;
		try
		{
			dataInputStream = new DataInputStream(filename);
		}
		catch (Exception)
		{
			dataInputStream = null;
		}
		return dataInputStream;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x0002E12C File Offset: 0x0002C32C
	public short readShort()
	{
		return this.r.readShort();
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0002E139 File Offset: 0x0002C339
	public int readInt()
	{
		return this.r.readInt();
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0002E146 File Offset: 0x0002C346
	public int read()
	{
		return (int)this.r.readUnsignedByte();
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0002E153 File Offset: 0x0002C353
	public void read(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0002E162 File Offset: 0x0002C362
	public void close()
	{
		this.r.Close();
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0002E162 File Offset: 0x0002C362
	public void Close()
	{
		this.r.Close();
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0002E16F File Offset: 0x0002C36F
	public string readUTF()
	{
		return this.r.readUTF();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0002E17C File Offset: 0x0002C37C
	public sbyte readByte()
	{
		return this.r.readByte();
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0002E189 File Offset: 0x0002C389
	public long readLong()
	{
		return this.r.readLong();
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0002E196 File Offset: 0x0002C396
	public bool readBoolean()
	{
		return this.r.readBoolean();
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0002E1A3 File Offset: 0x0002C3A3
	public int readUnsignedByte()
	{
		return (int)((byte)this.r.readByte());
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0002E1B1 File Offset: 0x0002C3B1
	public int readUnsignedShort()
	{
		return (int)this.r.readUnsignedShort();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0002E153 File Offset: 0x0002C353
	public void readFully(ref sbyte[] data)
	{
		this.r.read(ref data);
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0002E1BE File Offset: 0x0002C3BE
	public int available()
	{
		return this.r.available();
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0002E1CB File Offset: 0x0002C3CB
	internal void read(ref sbyte[] byteData, int p, int size)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040004F2 RID: 1266
	public myReader r;

	// Token: 0x040004F3 RID: 1267
	internal const int INTERVAL = 5;

	// Token: 0x040004F4 RID: 1268
	internal const int MAXTIME = 500;

	// Token: 0x040004F5 RID: 1269
	public static DataInputStream istemp;

	// Token: 0x040004F6 RID: 1270
	internal static int status;

	// Token: 0x040004F7 RID: 1271
	internal static string filenametemp;
}

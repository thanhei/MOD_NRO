using System;
using System.Text;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class myReader
{
	// Token: 0x06000A69 RID: 2665 RVA: 0x00003A00 File Offset: 0x00001C00
	public myReader()
	{
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00093220 File Offset: 0x00091420
	public myReader(sbyte[] data)
	{
		this.buffer = data;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0009322F File Offset: 0x0009142F
	public myReader(string filename)
	{
		this.buffer = mSystem.convertToSbyte(((TextAsset)Resources.Load(filename, typeof(TextAsset))).bytes);
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0009325C File Offset: 0x0009145C
	public sbyte readSByte()
	{
		if (this.posRead < this.buffer.Length)
		{
			sbyte[] array = this.buffer;
			int num = this.posRead;
			this.posRead = num + 1;
			return array[num];
		}
		this.posRead = this.buffer.Length;
		throw new Exception(" loi doc sbyte eof ");
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x000932AA File Offset: 0x000914AA
	public sbyte readsbyte()
	{
		return this.readSByte();
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x000932AA File Offset: 0x000914AA
	public sbyte readByte()
	{
		return this.readSByte();
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x000932B2 File Offset: 0x000914B2
	public void mark(int readlimit)
	{
		this.posMark = this.posRead;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x000932C0 File Offset: 0x000914C0
	public void reset()
	{
		this.posRead = this.posMark;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x000932CE File Offset: 0x000914CE
	public byte readUnsignedByte()
	{
		return myReader.convertSbyteToByte(this.readSByte());
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x000932DC File Offset: 0x000914DC
	public short readShort()
	{
		short num = 0;
		for (int i = 0; i < 2; i++)
		{
			short num2 = (short)(num << 8);
			short num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = num2 | (num3 & array[num4]);
		}
		return num;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00093320 File Offset: 0x00091520
	public ushort readUnsignedShort()
	{
		ushort num = 0;
		for (int i = 0; i < 2; i++)
		{
			ushort num2 = (ushort)(num << 8);
			ushort num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = num2 | (num3 & array[num4]);
		}
		return num;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00093364 File Offset: 0x00091564
	public int readInt()
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			int num2 = num << 8;
			int num3 = 255;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = num2 | (num3 & array[num4]);
		}
		return num;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x000933A4 File Offset: 0x000915A4
	public long readLong()
	{
		long num = 0L;
		for (int i = 0; i < 8; i++)
		{
			long num2 = num << 8;
			long num3 = 255L;
			sbyte[] array = this.buffer;
			int num4 = this.posRead;
			this.posRead = num4 + 1;
			num = num2 | (num3 & array[num4]);
		}
		return num;
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x000933E5 File Offset: 0x000915E5
	public bool readBool()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x000933E5 File Offset: 0x000915E5
	public bool readBoolean()
	{
		return this.readSByte() > 0;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x000933F0 File Offset: 0x000915F0
	public string readString()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		return new UTF8Encoding().GetString(array);
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x00093430 File Offset: 0x00091630
	public string readStringUTF()
	{
		short num = this.readShort();
		byte[] array = new byte[(int)num];
		for (int i = 0; i < (int)num; i++)
		{
			array[i] = myReader.convertSbyteToByte(this.readSByte());
		}
		return new UTF8Encoding().GetString(array);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00093470 File Offset: 0x00091670
	public string readUTF()
	{
		return this.readStringUTF();
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00093478 File Offset: 0x00091678
	public int read()
	{
		if (this.posRead < this.buffer.Length)
		{
			return (int)this.readSByte();
		}
		return -1;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00093494 File Offset: 0x00091694
	public int read(ref sbyte[] data)
	{
		if (data == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				return -1;
			}
			num++;
		}
		return num;
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x000934DC File Offset: 0x000916DC
	public void readFully(ref sbyte[] data)
	{
		if (data != null && data.Length + this.posRead <= this.buffer.Length)
		{
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
			}
		}
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0009351B File Offset: 0x0009171B
	public int available()
	{
		return this.buffer.Length - this.posRead;
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0004AAED File Offset: 0x00048CED
	public static byte convertSbyteToByte(sbyte var)
	{
		if (var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0009352C File Offset: 0x0009172C
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if (var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0009356D File Offset: 0x0009176D
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0009356D File Offset: 0x0009176D
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00093578 File Offset: 0x00091778
	public void read(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			data[i + arg1] = this.readSByte();
			if (this.posRead > this.buffer.Length)
			{
				break;
			}
		}
	}

	// Token: 0x040013F7 RID: 5111
	public sbyte[] buffer;

	// Token: 0x040013F8 RID: 5112
	internal int posRead;

	// Token: 0x040013F9 RID: 5113
	internal int posMark;

	// Token: 0x040013FA RID: 5114
	internal static string fileName;

	// Token: 0x040013FB RID: 5115
	internal static int status;
}

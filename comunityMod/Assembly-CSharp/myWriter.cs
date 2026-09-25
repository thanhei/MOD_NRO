using System;
using System.Text;

// Token: 0x020000C9 RID: 201
public class myWriter
{
	// Token: 0x06000A84 RID: 2692 RVA: 0x000935B2 File Offset: 0x000917B2
	public myWriter()
	{
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x000935D5 File Offset: 0x000917D5
	public myWriter(int len)
	{
		this.buffer = new sbyte[len];
		this.lenght = len;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0009360C File Offset: 0x0009180C
	public void writeSByte(sbyte value)
	{
		this.checkLenght(0);
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0009363C File Offset: 0x0009183C
	public void writeSByteUncheck(sbyte value)
	{
		sbyte[] array = this.buffer;
		int num = this.posWrite;
		this.posWrite = num + 1;
		array[num] = value;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00093662 File Offset: 0x00091862
	public void writeByte(sbyte value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0009366B File Offset: 0x0009186B
	public void writeByte(int value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00093675 File Offset: 0x00091875
	public void writeChar(char value)
	{
		this.writeSByte(0);
		this.writeSByte((sbyte)value);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0009366B File Offset: 0x0009186B
	public void writeUnsignedByte(byte value)
	{
		this.writeSByte((sbyte)value);
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x00093688 File Offset: 0x00091888
	public void writeUnsignedByte(byte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck((sbyte)value[i]);
		}
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x000936B8 File Offset: 0x000918B8
	public void writeSByte(sbyte[] value)
	{
		this.checkLenght(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			this.writeSByteUncheck(value[i]);
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x000936E8 File Offset: 0x000918E8
	public void writeShort(short value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x00093718 File Offset: 0x00091918
	public void writeShort(int value)
	{
		this.checkLenght(2);
		short num = (short)value;
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(num >> i * 8));
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0009374C File Offset: 0x0009194C
	public void writeUnsignedShort(ushort value)
	{
		this.checkLenght(2);
		for (int i = 1; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0009377C File Offset: 0x0009197C
	public void writeInt(int value)
	{
		this.checkLenght(4);
		for (int i = 3; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x000937AC File Offset: 0x000919AC
	public void writeLong(long value)
	{
		this.checkLenght(8);
		for (int i = 7; i >= 0; i--)
		{
			this.writeSByteUncheck((sbyte)(value >> i * 8));
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x000937DB File Offset: 0x000919DB
	public void writeBoolean(bool value)
	{
		this.writeSByte(value ? 1 : 0);
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x000937DB File Offset: 0x000919DB
	public void writeBool(bool value)
	{
		this.writeSByte(value ? 1 : 0);
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x000937EC File Offset: 0x000919EC
	public void writeString(string value)
	{
		char[] array = value.ToCharArray();
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0009382C File Offset: 0x00091A2C
	public void writeUTF(string value)
	{
		Encoding unicode = Encoding.Unicode;
		byte[] array = Encoding.Convert(unicode, Encoding.GetEncoding(65001), unicode.GetBytes(value));
		this.writeShort((short)array.Length);
		this.checkLenght(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			this.writeSByteUncheck((sbyte)array[i]);
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00093884 File Offset: 0x00091A84
	public void write(ref sbyte[] data, int arg1, int arg2)
	{
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < arg2; i++)
		{
			this.writeSByte(data[i + arg1]);
			if (this.posWrite > this.buffer.Length)
			{
				break;
			}
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x000938BE File Offset: 0x00091ABE
	public void write(sbyte[] value)
	{
		this.writeSByte(value);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x000938C8 File Offset: 0x00091AC8
	public sbyte[] getData()
	{
		if (this.posWrite <= 0)
		{
			return null;
		}
		sbyte[] array = new sbyte[this.posWrite];
		for (int i = 0; i < this.posWrite; i++)
		{
			array[i] = this.buffer[i];
		}
		return array;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0009390C File Offset: 0x00091B0C
	public void checkLenght(int ltemp)
	{
		if (this.posWrite + ltemp > this.lenght)
		{
			sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
			for (int i = 0; i < this.lenght; i++)
			{
				array[i] = this.buffer[i];
			}
			this.buffer = null;
			this.buffer = array;
			this.lenght += 1024 + ltemp;
		}
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0009397B File Offset: 0x00091B7B
	public void Close()
	{
		this.buffer = null;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0009397B File Offset: 0x00091B7B
	public void close()
	{
		this.buffer = null;
	}

	// Token: 0x040013FC RID: 5116
	public sbyte[] buffer = new sbyte[2048];

	// Token: 0x040013FD RID: 5117
	internal int posWrite;

	// Token: 0x040013FE RID: 5118
	internal int lenght = 2048;
}

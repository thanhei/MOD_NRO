using System;

// Token: 0x0200000C RID: 12
public class ArrayCast
{
	// Token: 0x0600005F RID: 95 RVA: 0x00003A08 File Offset: 0x00001C08
	public static sbyte[] cast(byte[] data)
	{
		sbyte[] array = new sbyte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (sbyte)data[i];
		}
		return array;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003A34 File Offset: 0x00001C34
	public static byte[] cast(sbyte[] data)
	{
		byte[] array = new byte[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)data[i];
		}
		return array;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003A60 File Offset: 0x00001C60
	public static char[] ToCharArray(sbyte[] data)
	{
		char[] array = new char[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (char)data[i];
		}
		return array;
	}
}

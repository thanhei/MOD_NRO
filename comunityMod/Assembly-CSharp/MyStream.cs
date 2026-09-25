using System;

// Token: 0x02000079 RID: 121
public class MyStream
{
	// Token: 0x060005C2 RID: 1474 RVA: 0x00057A9C File Offset: 0x00055C9C
	public static DataInputStream readFile(string path)
	{
		path = Main.res + path;
		DataInputStream dataInputStream;
		try
		{
			dataInputStream = DataInputStream.getResourceAsStream(path);
		}
		catch (Exception)
		{
			dataInputStream = null;
		}
		return dataInputStream;
	}
}

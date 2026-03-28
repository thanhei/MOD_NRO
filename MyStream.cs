using System;

// Token: 0x02000010 RID: 16
public class MyStream
{
	// Token: 0x0600006F RID: 111 RVA: 0x0000A700 File Offset: 0x00008900
	public static DataInputStream readFile(string path)
	{
		path = Main.res + path;
		DataInputStream result;
		try
		{
			result = DataInputStream.getResourceAsStream(path);
		}
		catch (Exception ex)
		{
			result = null;
		}
		return result;
	}
}

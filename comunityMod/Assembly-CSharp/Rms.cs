using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class Rms
{
	// Token: 0x060007A1 RID: 1953 RVA: 0x00076F43 File Offset: 0x00075143
	public static void saveRMS(string filename, sbyte[] data)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Rms.__saveRMS(filename, data);
			return;
		}
		Rms._saveRMS(filename, data);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00076F6A File Offset: 0x0007516A
	public static sbyte[] loadRMS(string filename)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return Rms.__loadRMS(filename);
		}
		return Rms._loadRMS(filename);
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00076F90 File Offset: 0x00075190
	public static string loadRMSString(string fileName)
	{
		sbyte[] array = Rms.loadRMS(fileName);
		if (array == null)
		{
			return null;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		try
		{
			string text = dataInputStream.readUTF();
			dataInputStream.close();
			return text;
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
		return null;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x00076FE0 File Offset: 0x000751E0
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

	// Token: 0x060007A5 RID: 1957 RVA: 0x00077024 File Offset: 0x00075224
	public static void saveRMSString(string filename, string data)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(data);
			Rms.saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x00077070 File Offset: 0x00075270
	internal static void _saveRMS(string filename, sbyte[] data)
	{
		if (Rms.status != 0)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
			return;
		}
		Rms.filename = filename;
		Rms.data = data;
		Rms.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Rms.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO SAVE RMS " + filename);
		}
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x000770E8 File Offset: 0x000752E8
	internal static sbyte[] _loadRMS(string filename)
	{
		if (Rms.status != 0)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
			return null;
		}
		Rms.filename = filename;
		Rms.data = null;
		Rms.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Rms.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO LOAD RMS " + filename);
		}
		return Rms.data;
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00077164 File Offset: 0x00075364
	public static void update()
	{
		if (Rms.status == 2)
		{
			Rms.status = 1;
			Rms.__saveRMS(Rms.filename, Rms.data);
			Rms.status = 0;
			return;
		}
		if (Rms.status == 3)
		{
			Rms.status = 1;
			Rms.data = Rms.__loadRMS(Rms.filename);
			Rms.status = 0;
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000771B8 File Offset: 0x000753B8
	public static int loadRMSInt(string file)
	{
		sbyte[] array = Rms.loadRMS(file);
		if (array == null)
		{
			return -1;
		}
		return (int)array[0];
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x000771D4 File Offset: 0x000753D4
	public static void saveRMSInt(string file, int x)
	{
		try
		{
			Rms.saveRMS(file, new sbyte[] { (sbyte)x });
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00077208 File Offset: 0x00075408
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public static string GetiPhoneDocumentsPath()
	{
		return Application.persistentDataPath;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0007720F File Offset: 0x0007540F
	internal static void __saveRMS(string filename, sbyte[] data)
	{
		string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
		FileStream fileStream = new FileStream(text, FileMode.Create);
		fileStream.Write(ArrayCast.cast(data), 0, data.Length);
		fileStream.Flush();
		fileStream.Close();
		Main.setBackupIcloud(text);
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00077248 File Offset: 0x00075448
	internal static sbyte[] __loadRMS(string filename)
	{
		sbyte[] array2;
		try
		{
			FileStream fileStream = new FileStream(Rms.GetiPhoneDocumentsPath() + "/" + filename, FileMode.Open);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			ArrayCast.cast(array);
			array2 = ArrayCast.cast(array);
		}
		catch (Exception)
		{
			array2 = null;
		}
		return array2;
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x000772B0 File Offset: 0x000754B0
	public static void clearAll()
	{
		Cout.LogError3("clean rms");
		FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
		for (int i = 0; i < files.Length; i++)
		{
			files[i].Delete();
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x000772F8 File Offset: 0x000754F8
	public static void DeleteStorage(string path)
	{
		try
		{
			File.Delete(Rms.GetiPhoneDocumentsPath() + "/" + path);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00077330 File Offset: 0x00075530
	public static string ByteArrayToString(byte[] ba)
	{
		return BitConverter.ToString(ba).Replace("-", string.Empty);
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00077348 File Offset: 0x00075548
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00077388 File Offset: 0x00075588
	public static void deleteRecord(string name)
	{
		try
		{
			PlayerPrefs.DeleteKey(name);
		}
		catch (Exception ex)
		{
			Cout.println("loi xoa RMS --------------------------" + ex.ToString());
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x000773C8 File Offset: 0x000755C8
	public static void clearRMS()
	{
		Rms.deleteRecord("data");
		Rms.deleteRecord("dataVersion");
		Rms.deleteRecord("map");
		Rms.deleteRecord("mapVersion");
		Rms.deleteRecord("skill");
		Rms.deleteRecord("killVersion");
		Rms.deleteRecord("item");
		Rms.deleteRecord("itemVersion");
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00077425 File Offset: 0x00075625
	public static void saveIP(string strID)
	{
		Rms.saveRMSString("NRIPlink", strID);
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00077434 File Offset: 0x00075634
	public static string loadIP()
	{
		string text = Rms.loadRMSString("NRIPlink");
		if (text == null)
		{
			return null;
		}
		return text;
	}

	// Token: 0x04000E92 RID: 3730
	public static int status;

	// Token: 0x04000E93 RID: 3731
	public static sbyte[] data;

	// Token: 0x04000E94 RID: 3732
	public static string filename;

	// Token: 0x04000E95 RID: 3733
	internal const int INTERVAL = 5;

	// Token: 0x04000E96 RID: 3734
	internal const int MAXTIME = 500;
}

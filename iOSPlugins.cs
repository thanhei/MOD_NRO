using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class iOSPlugins
{
	// Token: 0x060000F6 RID: 246
	[DllImport("__Internal")]
	private static extern void _SMSsend(string tophone, string withtext, int n);

	// Token: 0x060000F7 RID: 247
	[DllImport("__Internal")]
	private static extern int _unpause();

	// Token: 0x060000F8 RID: 248
	[DllImport("__Internal")]
	private static extern int _checkRotation();

	// Token: 0x060000F9 RID: 249
	[DllImport("__Internal")]
	private static extern int _back();

	// Token: 0x060000FA RID: 250
	[DllImport("__Internal")]
	private static extern int _Send();

	// Token: 0x060000FB RID: 251
	[DllImport("__Internal")]
	private static extern void _purchaseItem(string itemID, string userName, string gameID);

	// Token: 0x060000FC RID: 252 RVA: 0x0000D344 File Offset: 0x0000B544
	public static int Check()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return iOSPlugins.checkCanSendSMS();
		}
		iOSPlugins.devide = iPhoneSettings.generation.ToString();
		string a = string.Empty + iOSPlugins.devide[2];
		if (a == "h" && iOSPlugins.devide.Length > 6)
		{
			iOSPlugins.Myname = SystemInfo.operatingSystem.ToString();
			string a2 = string.Empty + iOSPlugins.Myname[10];
			if (a2 != "2" && a2 != "3")
			{
				return 0;
			}
			return 1;
		}
		else
		{
			Cout.println(iOSPlugins.devide + "  loai");
			if (iOSPlugins.devide == "Unknown" && ScaleGUI.WIDTH * ScaleGUI.HEIGHT < 786432f)
			{
				return 0;
			}
			return -1;
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00004A23 File Offset: 0x00002C23
	public static int checkCanSendSMS()
	{
		if (iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation == iPhoneGeneration.iPhone4S || iPhoneSettings.generation == iPhoneGeneration.iPhone5)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00004A56 File Offset: 0x00002C56
	public static void SMSsend(string phonenumber, string bodytext, int n)
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._SMSsend(phonenumber, bodytext, n);
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00004A6A File Offset: 0x00002C6A
	public static void back()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._back();
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00004A7C File Offset: 0x00002C7C
	public static void Send()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._Send();
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00004A8E File Offset: 0x00002C8E
	public static int unpause()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			return iOSPlugins._unpause();
		}
		return 0;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00004AA1 File Offset: 0x00002CA1
	public static int checkRotation()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			return iOSPlugins._checkRotation();
		}
		return 0;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00004AB4 File Offset: 0x00002CB4
	public static void purchaseItem(string itemID, string userName, string gameID)
	{
		if (Application.platform != RuntimePlatform.OSXEditor)
		{
			iOSPlugins._purchaseItem(itemID, userName, gameID);
		}
	}

	// Token: 0x040000E5 RID: 229
	public static string devide;

	// Token: 0x040000E6 RID: 230
	public static string Myname;
}

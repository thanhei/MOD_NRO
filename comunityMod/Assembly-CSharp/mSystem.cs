using System;
using System.Text;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class mSystem
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x00004887 File Offset: 0x00002A87
	public static void AddIpTest()
	{
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00092687 File Offset: 0x00090887
	public static void resetCurInapp()
	{
		mSystem.curINAPP = 0;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x0009268F File Offset: 0x0009088F
	public static int getWidth(Image img)
	{
		int num = mSystem.clientType;
		return img.getWidth();
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0009269F File Offset: 0x0009089F
	public static int getHeight(Image img)
	{
		if (mSystem.clientType == 5)
		{
			return img.getHeight();
		}
		return img.getWidth();
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x000926B8 File Offset: 0x000908B8
	public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
	{
		string text = string.Empty;
		long num = (timeStart + (long)(secondCount * 1000) - mSystem.currentTimeMillis()) / 1000L;
		if (num <= 0L)
		{
			return string.Empty;
		}
		long num2 = 0L;
		long num3 = 0L;
		long num4 = num / 60L;
		long num5 = num;
		if (isOnlySecond)
		{
			return num5.ToString() + string.Empty;
		}
		if (num >= 86400L)
		{
			num2 = num / 86400L;
			num3 = num % 86400L / 3600L;
		}
		else if (num >= 3600L)
		{
			num3 = num / 3600L;
			num4 = num % 3600L / 60L;
		}
		else if (num >= 60L)
		{
			num4 = num / 60L;
			num5 = num % 60L;
		}
		else
		{
			num5 = num;
		}
		if (isShortText)
		{
			if (num2 > 0L)
			{
				return num2.ToString() + "d";
			}
			if (num3 > 0L)
			{
				return num3.ToString() + "h";
			}
			if (num4 > 0L)
			{
				return num4.ToString() + "m";
			}
			if (num5 > 0L)
			{
				return num5.ToString() + "s";
			}
		}
		if (num2 > 0L)
		{
			if (num2 >= 10L)
			{
				text = ((num3 < 1L) ? (num2.ToString() + "d") : ((num3 >= 10L) ? (num2.ToString() + "d" + num3.ToString() + "h") : (num2.ToString() + "d0" + num3.ToString() + "h")));
			}
			else if (num2 < 10L)
			{
				text = ((num3 < 1L) ? (num2.ToString() + "d") : ((num3 >= 10L) ? (num2.ToString() + "d" + num3.ToString() + "h") : (num2.ToString() + "d0" + num3.ToString() + "h")));
			}
		}
		else if (num3 > 0L)
		{
			if (num3 >= 10L)
			{
				text = ((num4 < 1L) ? (num3.ToString() + "h") : ((num4 >= 10L) ? (num3.ToString() + "h" + num4.ToString() + "m") : (num3.ToString() + "h0" + num4.ToString() + "m")));
			}
			else if (num3 < 10L)
			{
				text = ((num4 < 1L) ? (num3.ToString() + "h") : ((num4 >= 10L) ? (num3.ToString() + "h" + num4.ToString() + "m") : (num3.ToString() + "h0" + num4.ToString() + "m")));
			}
		}
		else if (num4 > 0L)
		{
			if (num4 >= 10L)
			{
				if (num5 >= 10L)
				{
					text = num4.ToString() + "m" + num5.ToString() + string.Empty;
				}
				else if (num5 < 10L)
				{
					text = num4.ToString() + "m0" + num5.ToString() + string.Empty;
				}
			}
			else if (num4 < 10L)
			{
				if (num5 >= 10L)
				{
					text = num4.ToString() + "m" + num5.ToString() + string.Empty;
				}
				else if (num5 < 10L)
				{
					text = num4.ToString() + "m0" + num5.ToString() + string.Empty;
				}
			}
		}
		else
		{
			text = ((num5 >= 10L) ? (num5.ToString() + string.Empty) : ("0" + num5.ToString() + string.Empty));
		}
		return text;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x00092A74 File Offset: 0x00090C74
	public static string numberTostring2(int aa)
	{
		string text4;
		try
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = aa.ToString() + string.Empty;
			if (text3.Equals(string.Empty))
			{
				text4 = text;
			}
			else
			{
				if (text3[0] == '-')
				{
					text2 = "-";
					text3 = text3.Substring(1);
				}
				for (int i = text3.Length - 1; i >= 0; i--)
				{
					text = (((text3.Length - 1 - i) % 3 != 0 || text3.Length - 1 - i <= 0) ? (text3[i].ToString() + text) : (text3[i].ToString() + "." + text));
				}
				text4 = text2 + text;
			}
		}
		catch (Exception)
		{
			text4 = aa.ToString() + string.Empty;
		}
		return text4;
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00092B68 File Offset: 0x00090D68
	public static string numberTostring(long number)
	{
		string text = string.Empty + number.ToString();
		bool flag = false;
		try
		{
			string text2 = string.Empty;
			if (number < 0L)
			{
				flag = true;
				number = -number;
				text = string.Empty + number.ToString();
			}
			int num;
			if (number >= 1000000000L)
			{
				text2 = "b";
				number /= 1000000000L;
				num = (string.Empty + number.ToString()).Length;
			}
			else if (number >= 1000000L)
			{
				text2 = "m";
				number /= 1000000L;
				num = (string.Empty + number.ToString()).Length;
			}
			else if (number < 1000L)
			{
				if (flag)
				{
					return "-" + text;
				}
				return text;
			}
			else
			{
				text2 = "k";
				number /= 1000L;
				num = (string.Empty + number.ToString()).Length;
			}
			int num2 = int.Parse(text.Substring(num, 2));
			text = ((num2 == 0) ? (text.Substring(0, num) + text2) : ((num2 % 10 != 0) ? (text.Substring(0, num) + "," + text.Substring(num, 2) + text2) : (text.Substring(0, num) + "," + text.Substring(num, 1) + text2)));
		}
		catch (Exception)
		{
		}
		if (flag)
		{
			return "-" + text;
		}
		return text;
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00092CF8 File Offset: 0x00090EF8
	public static void callHotlinePC()
	{
		Application.OpenURL("http://ngocrongonline.com/");
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x00004887 File Offset: 0x00002A87
	public static void callHotlineJava()
	{
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00004887 File Offset: 0x00002A87
	public static void callHotlineIphone()
	{
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00004887 File Offset: 0x00002A87
	public static void callHotlineWindowsPhone()
	{
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00004887 File Offset: 0x00002A87
	public static void closeBanner()
	{
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00004887 File Offset: 0x00002A87
	public static void showBanner()
	{
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00004887 File Offset: 0x00002A87
	public static void createAdmob()
	{
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00004887 File Offset: 0x00002A87
	public static void checkAdComlete()
	{
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00092D04 File Offset: 0x00090F04
	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00092D17 File Offset: 0x00090F17
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00092D24 File Offset: 0x00090F24
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		if (scr != null && dest != null && scrPos + lenght <= scr.Length)
		{
			sbyte[] array = new sbyte[dest.Length + lenght];
			for (int i = 0; i < destPos; i++)
			{
				array[i] = dest[i];
			}
			for (int j = destPos; j < destPos + lenght; j++)
			{
				array[j] = scr[scrPos + j - destPos];
			}
			for (int k = destPos + lenght; k < array.Length; k++)
			{
				array[k] = dest[destPos + k - lenght];
			}
		}
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00092D98 File Offset: 0x00090F98
	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00092DD3 File Offset: 0x00090FD3
	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00092DE0 File Offset: 0x00090FE0
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00092E0C File Offset: 0x0009100C
	public static sbyte[] convertToSbyte(string scr)
	{
		return mSystem.convertToSbyte(new ASCIIEncoding().GetBytes(scr));
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00092E20 File Offset: 0x00091020
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			if (scr[i] > 0)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00092E64 File Offset: 0x00091064
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00092E90 File Offset: 0x00091090
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00092EAA File Offset: 0x000910AA
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00092DD3 File Offset: 0x00090FD3
	public static void gcc()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00092EB2 File Offset: 0x000910B2
	public static mSystem gI()
	{
		if (mSystem.instance == null)
		{
			mSystem.instance = new mSystem();
		}
		return mSystem.instance;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00092ECA File Offset: 0x000910CA
	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00092ED2 File Offset: 0x000910D2
	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
		Debug.LogError(">>>>>>>> Controller.isConnectionFail = true;");
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00092EE4 File Offset: 0x000910E4
	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00004887 File Offset: 0x00002A87
	public static void exitWP()
	{
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00092EEC File Offset: 0x000910EC
	public static void paintFlyText(mGraphics g)
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1 && GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]))
			{
				if (GameScr.flyTextColor[i] == mFont.RED)
				{
					mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (GameScr.flyTextColor[i] == mFont.YELLOW)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (GameScr.flyTextColor[i] == mFont.GREEN)
				{
					mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (GameScr.flyTextColor[i] == mFont.FATAL)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (GameScr.flyTextColor[i] == mFont.FATAL_ME)
				{
					mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (GameScr.flyTextColor[i] == mFont.MISS)
				{
					mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
				}
				else if (GameScr.flyTextColor[i] == mFont.ORANGE)
				{
					mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (GameScr.flyTextColor[i] == mFont.ADDMONEY)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (GameScr.flyTextColor[i] == mFont.MISS_ME)
				{
					mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (GameScr.flyTextColor[i] == mFont.HP)
				{
					mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (GameScr.flyTextColor[i] == mFont.MP)
				{
					mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
			}
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00004887 File Offset: 0x00002A87
	public static void endKey()
	{
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x000931B0 File Offset: 0x000913B0
	public static FrameImage getFraImage(string nameImg)
	{
		FrameImage frameImage = null;
		MainImage mainImage = null;
		if (mainImage == null)
		{
			mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
		}
		if (mainImage.img != null)
		{
			int num = mainImage.img.getHeight() / (int)mainImage.nFrame;
			if (num < 1)
			{
				num = 1;
			}
			frameImage = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
		}
		return frameImage;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0009320A File Offset: 0x0009140A
	public static Image loadImage(string path)
	{
		return GameCanvas.loadImage(path);
	}

	// Token: 0x040013E5 RID: 5093
	public static bool isTest;

	// Token: 0x040013E6 RID: 5094
	public static string strAdmob;

	// Token: 0x040013E7 RID: 5095
	public static bool loadAdOk;

	// Token: 0x040013E8 RID: 5096
	public static string publicID;

	// Token: 0x040013E9 RID: 5097
	public static string android_pack;

	// Token: 0x040013EA RID: 5098
	public static int clientType = 4;

	// Token: 0x040013EB RID: 5099
	public static sbyte LANGUAGE;

	// Token: 0x040013EC RID: 5100
	public static sbyte curINAPP;

	// Token: 0x040013ED RID: 5101
	public static sbyte maxINAPP = 5;

	// Token: 0x040013EE RID: 5102
	public const int JAVA = 1;

	// Token: 0x040013EF RID: 5103
	public const int ANDROID = 2;

	// Token: 0x040013F0 RID: 5104
	public const int IP_JB = 3;

	// Token: 0x040013F1 RID: 5105
	public const int PC = 4;

	// Token: 0x040013F2 RID: 5106
	public const int IP_APPSTORE = 5;

	// Token: 0x040013F3 RID: 5107
	public const int WINDOWS_PHONE = 6;

	// Token: 0x040013F4 RID: 5108
	public const int GOOGLE_PLAY = 7;

	// Token: 0x040013F5 RID: 5109
	public static mSystem instance;

	// Token: 0x040013F6 RID: 5110
	internal static bool isANDROID;
}

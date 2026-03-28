using System;

// Token: 0x0200009B RID: 155
public class NinjaUtil
{
	// Token: 0x060004FB RID: 1275 RVA: 0x00006C6E File Offset: 0x00004E6E
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x00006C75 File Offset: 0x00004E75
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x00044934 File Offset: 0x00042B34
	public static int randomNumber(int max)
	{
		MyRandom myRandom = new MyRandom();
		return myRandom.nextInt(max);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00044950 File Offset: 0x00042B50
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			if (num > 1)
			{
				sbyte[] result = new sbyte[num];
				msg.reader().read(ref result);
				return result;
			}
		}
		catch (Exception ex)
		{
		}
		return null;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000449AC File Offset: 0x00042BAC
	public static sbyte[] readByteArray(myReader dos)
	{
		try
		{
			int num = dos.readInt();
			sbyte[] result = new sbyte[num];
			dos.read(ref result);
			return result;
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00006C81 File Offset: 0x00004E81
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000449FC File Offset: 0x00042BFC
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string str = string.Empty;
		if (number.Equals(string.Empty))
		{
			return text;
		}
		if (number[0] == '-')
		{
			str = "-";
			number = number.Substring(1);
		}
		for (int i = number.Length - 1; i >= 0; i--)
		{
			if ((number.Length - 1 - i) % 3 == 0 && number.Length - 1 - i > 0)
			{
				text = number[i] + "." + text;
			}
			else
			{
				text = number[i] + text;
			}
		}
		return str + text;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00044AB8 File Offset: 0x00042CB8
	public static string getDate(int second)
	{
		long num = (long)second * 1000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		int day = dateTime2.Day;
		int month = dateTime2.Month;
		int year = dateTime2.Year;
		return string.Concat(new object[]
		{
			day,
			"/",
			month,
			"/",
			year,
			" ",
			hour,
			"h"
		});
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00044B80 File Offset: 0x00042D80
	public static string getDate2(long second)
	{
		long num = second + 25200000L;
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		DateTime dateTime2 = dateTime.Add(new TimeSpan(num * 10000L)).ToUniversalTime();
		int hour = dateTime2.Hour;
		int minute = dateTime2.Minute;
		return string.Concat(new object[]
		{
			hour,
			"h",
			minute,
			"m"
		});
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00044C08 File Offset: 0x00042E08
	public static string getTime(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num > 9)
			{
				text += num;
			}
			else
			{
				text = text + "0" + num;
			}
			text += ":";
			if (timeRemainS > 9)
			{
				text += timeRemainS;
			}
			else
			{
				text = text + "0" + timeRemainS;
			}
		}
		return text;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00044D2C File Offset: 0x00042F2C
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			if (m < 1000L)
			{
				text = m + text;
				break;
			}
			long num3 = m % 1000L;
			if (num3 == 0L)
			{
				text = ".000" + text;
			}
			else if (num3 < 10L)
			{
				text = ".00" + num3 + text;
			}
			else if (num3 < 100L)
			{
				text = ".0" + num3 + text;
			}
			else
			{
				text = "." + num3 + text;
			}
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00044E00 File Offset: 0x00043000
	public static string getTimeAgo(int timeRemainS)
	{
		int num = 0;
		if (timeRemainS > 60)
		{
			num = timeRemainS / 60;
			timeRemainS %= 60;
		}
		int num2 = 0;
		if (num > 60)
		{
			num2 = num / 60;
			num %= 60;
		}
		int num3 = 0;
		if (num2 > 24)
		{
			num3 = num2 / 24;
			num2 %= 24;
		}
		string text = string.Empty;
		if (num3 > 0)
		{
			text += num3;
			text += "d";
			text = text + num2 + "h";
		}
		else if (num2 > 0)
		{
			text += num2;
			text += "h";
			text = text + num + "'";
		}
		else
		{
			if (num == 0)
			{
				num = 1;
			}
			text += num;
			text += "ph";
		}
		return text;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00044EE0 File Offset: 0x000430E0
	public static string[] split(string original, string separator)
	{
		MyVector myVector = new MyVector();
		for (int i = original.IndexOf(separator); i >= 0; i = original.IndexOf(separator))
		{
			myVector.addElement(original.Substring(0, i));
			original = original.Substring(i + separator.Length);
		}
		myVector.addElement(original);
		string[] array = new string[myVector.size()];
		if (myVector.size() > 0)
		{
			for (int j = 0; j < myVector.size(); j++)
			{
				array[j] = (string)myVector.elementAt(j);
			}
		}
		return array;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00044F74 File Offset: 0x00043174
	public static bool checkNumber(string numberStr)
	{
		bool result;
		try
		{
			int.Parse(numberStr);
			result = true;
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}
}

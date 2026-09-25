using System;

// Token: 0x0200007F RID: 127
public class NinjaUtil
{
	// Token: 0x06000604 RID: 1540 RVA: 0x0004D332 File Offset: 0x0004B532
	public static void onLoadMapComplete()
	{
		GameCanvas.endDlg();
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00058D15 File Offset: 0x00056F15
	public void onLoading()
	{
		GameCanvas.startWaitDlg(mResources.downloading_data);
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x00058D21 File Offset: 0x00056F21
	public static int randomNumber(int max)
	{
		return new MyRandom().nextInt(max);
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00058D30 File Offset: 0x00056F30
	public static sbyte[] readByteArray(Message msg)
	{
		try
		{
			int num = msg.reader().readInt();
			if (num > 1)
			{
				sbyte[] array = new sbyte[num];
				msg.reader().read(ref array);
				return array;
			}
		}
		catch (Exception)
		{
		}
		return null;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00058D80 File Offset: 0x00056F80
	public static sbyte[] readByteArray(myReader dos)
	{
		try
		{
			sbyte[] array = new sbyte[dos.readInt()];
			dos.read(ref array);
			return array;
		}
		catch (Exception)
		{
			Cout.LogError("LOI DOC readByteArray dos  NINJAUTIL");
		}
		return null;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00058DC8 File Offset: 0x00056FC8
	public static string replace(string text, string regex, string replacement)
	{
		return text.Replace(regex, replacement);
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x00058DD4 File Offset: 0x00056FD4
	public static string numberTostring(string number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (number.Equals(string.Empty))
		{
			return text;
		}
		if (number[0] == '-')
		{
			text2 = "-";
			number = number.Substring(1);
		}
		for (int i = number.Length - 1; i >= 0; i--)
		{
			text = (((number.Length - 1 - i) % 3 != 0 || number.Length - 1 - i <= 0) ? (number[i].ToString() + text) : (number[i].ToString() + "." + text));
		}
		return text2 + text;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00058E80 File Offset: 0x00057080
	public static string getDate(int second)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan((long)second * 1000L * 10000L)).ToUniversalTime();
		int hour = dateTime.Hour;
		int minute = dateTime.Minute;
		int day = dateTime.Day;
		int month = dateTime.Month;
		int year = dateTime.Year;
		return string.Concat(new string[]
		{
			day.ToString(),
			"/",
			month.ToString(),
			"/",
			year.ToString(),
			" ",
			hour.ToString(),
			"h"
		});
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x00058F44 File Offset: 0x00057144
	public static string getDate2(long second)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(new TimeSpan((second + 25200000L) * 10000L)).ToUniversalTime();
		int hour = dateTime.Hour;
		int minute = dateTime.Minute;
		return hour.ToString() + "h" + minute.ToString() + "m";
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00058FB4 File Offset: 0x000571B4
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
			return text + num3.ToString() + "d" + num2 + "h";
		}
		if (num2 > 0)
		{
			return text + num2.ToString() + "h" + num + "'";
		}
		text = ((num <= 9) ? (text + "0" + num.ToString()) : (text + num.ToString())) + ":";
		if (timeRemainS > 9)
		{
			return text + timeRemainS.ToString();
		}
		return text + "0" + timeRemainS.ToString();
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x000590AC File Offset: 0x000572AC
	public static string getMoneys(long m)
	{
		string text = string.Empty;
		long num = m / 1000L + 1L;
		int num2 = 0;
		while ((long)num2 < num)
		{
			if (m < 1000L)
			{
				text = m.ToString() + text;
				break;
			}
			long num3 = m % 1000L;
			text = ((num3 != 0L) ? ((num3 >= 10L) ? ((num3 >= 100L) ? ("." + num3.ToString() + text) : (".0" + num3.ToString() + text)) : (".00" + num3.ToString() + text)) : (".000" + text));
			m /= 1000L;
			num2++;
		}
		return text;
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00059164 File Offset: 0x00057364
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
		string empty = string.Empty;
		if (num3 > 0)
		{
			return empty + num3.ToString() + "d" + num2 + "h";
		}
		if (num2 > 0)
		{
			return empty + num2.ToString() + "h" + num + "'";
		}
		if (num == 0)
		{
			num = 1;
		}
		return empty + num.ToString() + "ph";
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00059220 File Offset: 0x00057420
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

	// Token: 0x06000611 RID: 1553 RVA: 0x000592A4 File Offset: 0x000574A4
	public static bool checkNumber(string numberStr)
	{
		bool flag;
		try
		{
			int.Parse(numberStr);
			flag = true;
		}
		catch (Exception)
		{
			flag = false;
		}
		return flag;
	}
}

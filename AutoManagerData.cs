using System;
using System.Collections;
using System.Text;

public sealed class AutoManagerAccount
{
	public string Username = string.Empty;

	public string Password = string.Empty;

	public long LastUsed;
}

public static class AutoManagerData
{
	private const string AccountsKey = "auto_manager_accounts_v1";

	private const string AutoSaveKey = "auto_manager_auto_save";

	public static MyVector LoadAccounts()
	{
		MyVector myVector = new MyVector();
		string text = Rms.loadRMSString("auto_manager_accounts_v1");
		if (text == null || text.Length == 0)
		{
			return myVector;
		}
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			AutoManagerAccount autoManagerAccount = AutoManagerData.ParseLine(array[i]);
			if (autoManagerAccount != null && autoManagerAccount.Username.Length > 0)
			{
				myVector.addElement(autoManagerAccount);
			}
		}
		AutoManagerData.SortAccounts(myVector);
		return myVector;
	}

	public static void SaveAccounts(MyVector accounts)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < accounts.size(); i++)
		{
			AutoManagerAccount autoManagerAccount = (AutoManagerAccount)accounts.elementAt(i);
			if (autoManagerAccount != null && autoManagerAccount.Username.Length > 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(AutoManagerData.Encode(autoManagerAccount.Username));
				stringBuilder.Append('|');
				stringBuilder.Append(AutoManagerData.Encode(autoManagerAccount.Password));
				stringBuilder.Append('|');
				stringBuilder.Append(autoManagerAccount.LastUsed);
			}
		}
		Rms.saveRMSString("auto_manager_accounts_v1", stringBuilder.ToString());
	}

	public static void UpsertAccount(string username, string password)
	{
		string text = AutoManagerData.Normalize(username);
		if (text.Length == 0)
		{
			return;
		}
		MyVector myVector = AutoManagerData.LoadAccounts();
		AutoManagerAccount autoManagerAccount = null;
		for (int i = 0; i < myVector.size(); i++)
		{
			AutoManagerAccount autoManagerAccount2 = (AutoManagerAccount)myVector.elementAt(i);
			if (autoManagerAccount2 != null && AutoManagerData.Normalize(autoManagerAccount2.Username).Equals(text))
			{
				autoManagerAccount = autoManagerAccount2;
				break;
			}
		}
		if (autoManagerAccount == null)
		{
			autoManagerAccount = new AutoManagerAccount();
			myVector.addElement(autoManagerAccount);
		}
		autoManagerAccount.Username = username.Trim();
		autoManagerAccount.Password = ((password != null) ? password.Trim() : string.Empty);
		autoManagerAccount.LastUsed = mSystem.currentTimeMillis();
		AutoManagerData.SortAccounts(myVector);
		AutoManagerData.SaveAccounts(myVector);
	}

	public static void RemoveAt(int index)
	{
		MyVector myVector = AutoManagerData.LoadAccounts();
		if (index < 0 || index >= myVector.size())
		{
			return;
		}
		myVector.removeElementAt(index);
		AutoManagerData.SaveAccounts(myVector);
	}

	public static bool IsAutoSaveEnabled()
	{
		return Rms.loadRMSInt("auto_manager_auto_save") != 2;
	}

	public static void SetAutoSaveEnabled(bool enabled)
	{
		Rms.saveRMSInt("auto_manager_auto_save", enabled ? 1 : 2);
	}

	private static void SortAccounts(MyVector accounts)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < accounts.size(); i++)
		{
			arrayList.Add(accounts.elementAt(i));
		}
		arrayList.Sort(new AutoManagerComparer());
		accounts.removeAllElements();
		for (int j = 0; j < arrayList.Count; j++)
		{
			accounts.addElement(arrayList[j]);
		}
	}

	private static AutoManagerAccount ParseLine(string line)
	{
		if (line == null || line.Length == 0)
		{
			return null;
		}
		string[] array = line.Split(new char[]
		{
			'|'
		});
		if (array.Length < 2)
		{
			return null;
		}
		AutoManagerAccount autoManagerAccount = new AutoManagerAccount();
		autoManagerAccount.Username = AutoManagerData.Decode(array[0]);
		autoManagerAccount.Password = AutoManagerData.Decode(array[1]);
		if (array.Length > 2)
		{
			long.TryParse(array[2], out autoManagerAccount.LastUsed);
		}
		return autoManagerAccount;
	}

	private static string Normalize(string value)
	{
		return ((value != null) ? value.Trim().ToLower() : string.Empty);
	}

	private static string Encode(string value)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes((value != null) ? value : string.Empty));
	}

	private static string Decode(string value)
	{
		try
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(value));
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}

	private sealed class AutoManagerComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			AutoManagerAccount autoManagerAccount = x as AutoManagerAccount;
			AutoManagerAccount autoManagerAccount2 = y as AutoManagerAccount;
			long num = (autoManagerAccount != null) ? autoManagerAccount.LastUsed : 0L;
			long num2 = (autoManagerAccount2 != null) ? autoManagerAccount2.LastUsed : 0L;
			if (num == num2)
			{
				return 0;
			}
			return (num < num2) ? 1 : -1;
		}
	}
}

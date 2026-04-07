using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace AccountManagerExe
{
	[DataContract]
	public class AccountEntry
	{
		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string PasswordCipher { get; set; }

		[DataMember]
		public int Server { get; set; }
	}

	[DataContract]
	public class AccountFile
	{
		[DataMember]
		public List<AccountEntry> Accounts { get; set; }
	}

	[DataContract]
	public class ManagerSettings
	{
		[DataMember]
		public string GameFolderPath { get; set; }
	}

	public sealed class Storage
	{
		private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("MOD_NRO_ACCOUNT_MANAGER");
		private readonly string accountFilePath;
		private readonly string settingsFilePath;

		public Storage(string baseDirectory)
		{
			this.accountFilePath = Path.Combine(baseDirectory, "accounts.dat");
			this.settingsFilePath = Path.Combine(baseDirectory, "manager-settings.json");
		}

		public List<AccountEntry> LoadAccounts()
		{
			AccountFile data = this.ReadJson<AccountFile>(this.accountFilePath);
			if (data == null || data.Accounts == null)
			{
				return new List<AccountEntry>();
			}
			return data.Accounts;
		}

		public void SaveAccounts(List<AccountEntry> accounts)
		{
			AccountFile data = new AccountFile
			{
				Accounts = accounts ?? new List<AccountEntry>()
			};
			this.WriteJson(this.accountFilePath, data);
		}

		public ManagerSettings LoadSettings()
		{
			ManagerSettings data = this.ReadJson<ManagerSettings>(this.settingsFilePath);
			if (data != null)
			{
				return data;
			}
			return new ManagerSettings();
		}

		public void SaveSettings(ManagerSettings settings)
		{
			this.WriteJson(this.settingsFilePath, settings ?? new ManagerSettings());
		}

		public void SaveGameWindowSize(string gameFolderPath, int width, int height)
		{
			if (string.IsNullOrWhiteSpace(gameFolderPath))
			{
				throw new InvalidOperationException("Chưa chọn thư mục game.");
			}
			if (!Directory.Exists(gameFolderPath))
			{
				throw new DirectoryNotFoundException("Không tìm thấy thư mục game.");
			}
			File.WriteAllText(Path.Combine(gameFolderPath, "pc_window_size.txt"), string.Format("{0}x{1}", width, height));
		}

		public static string EncryptPassword(string plainText)
		{
			if (string.IsNullOrEmpty(plainText))
			{
				return string.Empty;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			byte[] cipher = ProtectedData.Protect(bytes, Entropy, DataProtectionScope.CurrentUser);
			return Convert.ToBase64String(cipher);
		}

		public static string DecryptPassword(string cipherText)
		{
			if (string.IsNullOrEmpty(cipherText))
			{
				return string.Empty;
			}
			try
			{
				byte[] cipher = Convert.FromBase64String(cipherText);
				byte[] plain = ProtectedData.Unprotect(cipher, Entropy, DataProtectionScope.CurrentUser);
				return Encoding.UTF8.GetString(plain);
			}
			catch
			{
			}
			return string.Empty;
		}

		private T ReadJson<T>(string filePath) where T : class
		{
			if (!File.Exists(filePath))
			{
				return null;
			}
			try
			{
				using (FileStream stream = File.OpenRead(filePath))
				{
					DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
					return serializer.ReadObject(stream) as T;
				}
			}
			catch
			{
			}
			return null;
		}

		private void WriteJson<T>(string filePath, T data)
		{
			using (FileStream stream = File.Create(filePath))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
				serializer.WriteObject(stream, data);
			}
		}
	}
}

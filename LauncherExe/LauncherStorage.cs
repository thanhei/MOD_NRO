using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace LauncherExe
{
	public sealed class LauncherStorage
	{
		private readonly string settingsFilePath;

		public LauncherStorage(string baseDirectory)
		{
			this.settingsFilePath = Path.Combine(baseDirectory, "launcher-settings.json");
		}

		public LauncherSettings LoadSettings()
		{
			LauncherSettings settings = this.ReadJson<LauncherSettings>(this.settingsFilePath);
			if (settings != null)
			{
				return settings;
			}
			return new LauncherSettings
			{
				InstallFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MOD_NRO"),
				GameRelativePath = @"Game\game.exe",
				AccountManagerRelativePath = @"Tools\AccountManagerExe.exe"
			};
		}

		public void SaveSettings(LauncherSettings settings)
		{
			this.WriteJson(this.settingsFilePath, settings);
		}

		public T ReadJsonFromString<T>(string json) where T : class
		{
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
				return serializer.ReadObject(stream) as T;
			}
		}

		public string ComputeSha256(string filePath)
		{
			using (FileStream stream = File.OpenRead(filePath))
			using (SHA256 sha = SHA256.Create())
			{
				byte[] hash = sha.ComputeHash(stream);
				StringBuilder builder = new StringBuilder(hash.Length * 2);
				for (int i = 0; i < hash.Length; i++)
				{
					builder.Append(hash[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		private T ReadJson<T>(string filePath) where T : class
		{
			if (!File.Exists(filePath))
			{
				return null;
			}
			using (FileStream stream = File.OpenRead(filePath))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
				return serializer.ReadObject(stream) as T;
			}
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

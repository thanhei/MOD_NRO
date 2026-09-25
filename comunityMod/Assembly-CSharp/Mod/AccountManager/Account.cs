using System;
using System.Text.RegularExpressions;
using Mod.R;
using Newtonsoft.Json;

namespace Mod.AccountManager
{
	// Token: 0x0200018A RID: 394
	internal class Account
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x000BE791 File Offset: 0x000BC991
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x000BE799 File Offset: 0x000BC999
		[JsonProperty("username")]
		internal string Username { get; set; } = "";

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000BE7A2 File Offset: 0x000BC9A2
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x000BE7AA File Offset: 0x000BC9AA
		[JsonProperty("password")]
		internal string Password { get; set; } = "";

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x000BE7B3 File Offset: 0x000BC9B3
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x000BE7BB File Offset: 0x000BC9BB
		[JsonProperty("server")]
		[JsonConverter(typeof(ServerConverter))]
		internal Server Server { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x000BE7CC File Offset: 0x000BC9CC
		[JsonProperty("last_time_login")]
		internal DateTime LastTimeLogin { get; set; } = DateTime.MinValue;

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x000BE7D5 File Offset: 0x000BC9D5
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x000BE7DD File Offset: 0x000BC9DD
		[JsonProperty("vang")]
		internal long Gold { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x000BE7E6 File Offset: 0x000BC9E6
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x000BE7EE File Offset: 0x000BC9EE
		[JsonProperty("ngoc_xanh")]
		internal long Gem { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x000BE7F7 File Offset: 0x000BC9F7
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x000BE7FF File Offset: 0x000BC9FF
		[JsonProperty("ruby")]
		internal long Ruby { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000BE808 File Offset: 0x000BCA08
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x000BE810 File Offset: 0x000BCA10
		[JsonProperty("info")]
		internal CharacterInfo Info { get; set; } = new CharacterInfo();

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x000BE819 File Offset: 0x000BCA19
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x000BE821 File Offset: 0x000BCA21
		[JsonProperty("info_pet")]
		internal CharacterInfo PetInfo { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x000BE82A File Offset: 0x000BCA2A
		[JsonIgnore]
		internal AccountType Type
		{
			get
			{
				if (string.IsNullOrEmpty(this.Password) && Account.regexMatchUserAo.IsMatch(this.Username) && !this.Server.IsCustomIP())
				{
					return AccountType.Unregistered;
				}
				return AccountType.Registered;
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000BE85C File Offset: 0x000BCA5C
		internal string GetLastTimeLogin()
		{
			if (this.LastTimeLogin == DateTime.MinValue)
			{
				return Strings.haventLoggedInYet;
			}
			string text = "";
			TimeSpan timeSpan = DateTime.Now - this.LastTimeLogin;
			if (timeSpan.TotalMinutes < 1.0)
			{
				text += Strings.justNow;
			}
			else if (timeSpan.TotalHours < 1.0)
			{
				text += string.Format(Strings.minutesAgo, timeSpan.Minutes);
			}
			else if (timeSpan.TotalDays < 1.0)
			{
				text += string.Format(Strings.hoursAgo, timeSpan.Hours);
			}
			else if (timeSpan.TotalDays < 2.0)
			{
				text += string.Format(Strings.yesterdayAt, this.LastTimeLogin.ToString("HH:mm"));
			}
			else
			{
				text += this.LastTimeLogin.ToString("dd/MM/yyyy HH:mm");
			}
			return text;
		}

		// Token: 0x040018F2 RID: 6386
		private static Regex regexMatchUserAo = new Regex("^User[0-9]{1,}$", RegexOptions.Compiled);
	}
}

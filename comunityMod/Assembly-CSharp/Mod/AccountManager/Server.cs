using System;

namespace Mod.AccountManager
{
	// Token: 0x02000193 RID: 403
	internal class Server
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x000C252C File Offset: 0x000C072C
		internal Server(int index)
		{
			this.index = index;
			this.name = (this.hostnameOrIPAddress = "");
			this.port = 0;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000C2561 File Offset: 0x000C0761
		internal Server(string name, string hostnameOrIPAddress, ushort port)
		{
			this.index = -1;
			this.name = name;
			this.hostnameOrIPAddress = hostnameOrIPAddress;
			this.port = port;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000C2588 File Offset: 0x000C0788
		internal Server(string serverInfo)
		{
			this.name = "";
			this.hostnameOrIPAddress = "";
			this.port = 0;
			if (int.TryParse(serverInfo, out this.index))
			{
				return;
			}
			this.index = -1;
			string[] array = serverInfo.Split(':', StringSplitOptions.None);
			if (array.Length == 2)
			{
				this.hostnameOrIPAddress = array[0];
				this.port = ushort.Parse(array[1]);
				return;
			}
			if (array.Length == 3)
			{
				this.name = array[0];
				this.hostnameOrIPAddress = array[1];
				this.port = ushort.Parse(array[2]);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000C2622 File Offset: 0x000C0822
		public override string ToString()
		{
			if (this.index > -1)
			{
				return this.index.ToString();
			}
			return string.Format("{0}:{1}:{2}", this.name, this.hostnameOrIPAddress, this.port).TrimStart(' ');
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000C2661 File Offset: 0x000C0861
		internal string GetIPPort()
		{
			return string.Format("{0}:{1}", this.hostnameOrIPAddress, this.port).TrimStart(' ');
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000C2685 File Offset: 0x000C0885
		internal bool IsCustomIP()
		{
			return this.index == -1;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000C2690 File Offset: 0x000C0890
		public override bool Equals(object obj)
		{
			Server server = obj as Server;
			if (server == null)
			{
				return base.Equals(obj);
			}
			if (server.IsCustomIP() != this.IsCustomIP())
			{
				return false;
			}
			if (this.IsCustomIP())
			{
				return server.hostnameOrIPAddress == this.hostnameOrIPAddress && server.port == this.port;
			}
			return server.index == this.index;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000C26F9 File Offset: 0x000C08F9
		public override int GetHashCode()
		{
			return HashCode.Combine<int, string, string, ushort>(this.index, this.name, this.hostnameOrIPAddress, this.port);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000C2718 File Offset: 0x000C0918
		public static bool operator ==(Server a, Server b)
		{
			if (a == null)
			{
				return b == null;
			}
			return a.Equals(b);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000C2729 File Offset: 0x000C0929
		public static bool operator !=(Server a, Server b)
		{
			if (a == null)
			{
				return b != null;
			}
			return !a.Equals(b);
		}

		// Token: 0x04001964 RID: 6500
		internal int index;

		// Token: 0x04001965 RID: 6501
		internal string name;

		// Token: 0x04001966 RID: 6502
		internal string hostnameOrIPAddress;

		// Token: 0x04001967 RID: 6503
		internal ushort port;
	}
}

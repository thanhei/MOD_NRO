using System;
using Newtonsoft.Json;

namespace Mod.AccountManager
{
	// Token: 0x02000194 RID: 404
	internal class ServerConverter : JsonConverter
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00039302 File Offset: 0x00037502
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00039302 File Offset: 0x00037502
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000C273D File Offset: 0x000C093D
		public override bool CanConvert(Type objectType)
		{
			return typeof(string) == objectType;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000C2750 File Offset: 0x000C0950
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			string text = reader.Value as string;
			if (text != null)
			{
				return new Server(text);
			}
			return null;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000C2774 File Offset: 0x000C0974
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Server server = value as Server;
			if (server != null)
			{
				writer.WriteValue(server.ToString());
			}
		}
	}
}

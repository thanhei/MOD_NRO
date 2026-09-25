using System;

// Token: 0x02000047 RID: 71
public interface IMessageHandler
{
	// Token: 0x0600040F RID: 1039
	void onMessage(Message message);

	// Token: 0x06000410 RID: 1040
	void onConnectionFail(bool isMain);

	// Token: 0x06000411 RID: 1041
	void onDisconnected(bool isMain);

	// Token: 0x06000412 RID: 1042
	void onConnectOK(bool isMain);
}

using System;

// Token: 0x0200004A RID: 74
public interface IMessageHandler
{
	// Token: 0x060002C4 RID: 708
	void onMessage(Message message);

	// Token: 0x060002C5 RID: 709
	void onConnectionFail(bool isMain);

	// Token: 0x060002C6 RID: 710
	void onDisconnected(bool isMain);

	// Token: 0x060002C7 RID: 711
	void onConnectOK(bool isMain);
}

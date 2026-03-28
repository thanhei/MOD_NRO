using System;

// Token: 0x0200004B RID: 75
public interface ISession
{
	// Token: 0x060002C8 RID: 712
	bool isConnected();

	// Token: 0x060002C9 RID: 713
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x060002CA RID: 714
	void connect(string host, int port);

	// Token: 0x060002CB RID: 715
	void sendMessage(Message message);

	// Token: 0x060002CC RID: 716
	void close();
}

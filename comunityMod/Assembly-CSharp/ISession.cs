using System;

// Token: 0x02000049 RID: 73
public interface ISession
{
	// Token: 0x06000437 RID: 1079
	bool isConnected();

	// Token: 0x06000438 RID: 1080
	void setHandler(IMessageHandler messageHandler);

	// Token: 0x06000439 RID: 1081
	void connect(string host, int port);

	// Token: 0x0600043A RID: 1082
	void sendMessage(Message message);

	// Token: 0x0600043B RID: 1083
	void close();
}

using System;

// Token: 0x02000044 RID: 68
public interface IChatable
{
	// Token: 0x06000406 RID: 1030
	void onChatFromMe(string text, string to);

	// Token: 0x06000407 RID: 1031
	void onCancelChat();
}

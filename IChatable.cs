using System;

// Token: 0x020000AB RID: 171
public interface IChatable
{
	// Token: 0x060007D0 RID: 2000
	void onChatFromMe(string text, string to);

	// Token: 0x060007D1 RID: 2001
	void onCancelChat();
}

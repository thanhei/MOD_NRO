using System;
using System.IO;
using Mod.ModHelper;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.Menu;
using Mod.R;

namespace Mod.Auto.AutoChat
{
	// Token: 0x02000187 RID: 391
	internal class AutoChat : ThreadActionUpdate<AutoChat>
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000BE27B File Offset: 0x000BC47B
		internal override int Interval
		{
			get
			{
				return Setup.delayAutoChat;
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000BE284 File Offset: 0x000BC484
		protected override void update()
		{
			string text = File.ReadAllLines(Utils.PathAutoChat)[0];
			Service.gI().chat("mcd" + Res.random(10, 100).ToString() + ": " + text);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000BE2CC File Offset: 0x000BC4CC
		[ChatCommand("openachat")]
		internal static void showMenu()
		{
			new MenuBuilder().addItem("Auto chat: \n" + (ThreadAction<AutoChat>.gI.IsActing ? mResources.ON : mResources.OFF), new MenuAction(delegate
			{
				ThreadAction<AutoChat>.gI.toggle(null);
				if (!ThreadAction<AutoChat>.gI.IsActing)
				{
					Setup.clearStringTrash();
					GameScr.info1.addInfo(Strings.autoChatDisabled + "!", 0);
				}
			})).addItem(Strings.inputContent, new MenuAction(delegate
			{
				ChatTextField.gI().strChat = Setup.inputTextAutoChat[0];
				ChatTextField.gI().tfChat.name = Setup.inputTextAutoChat[1];
				ChatTextField.gI().startChat2(Setup.gI, string.Empty);
			})).addItem(string.Format(Strings.delaySeconds, (float)Setup.delayAutoChat / 1000f), new MenuAction(delegate
			{
				ChatTextField.gI().strChat = Setup.inputDelayAutoChat[0];
				ChatTextField.gI().tfChat.name = Setup.inputDelayAutoChat[1];
				ChatTextField.gI().startChat2(Setup.gI, string.Empty);
			}))
				.addItem(Strings.viewContent, new MenuAction(delegate
				{
					using (StreamReader streamReader = new StreamReader(Utils.PathAutoChat))
					{
						string text = streamReader.ReadToEnd();
						GameCanvas.startOKDlg(Strings.autoChatContent + ":\n" + text);
					}
				}))
				.start();
		}
	}
}

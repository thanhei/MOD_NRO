using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000E3 RID: 227
	public class HistoryChat
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x0009E8EB File Offset: 0x0009CAEB
		private HistoryChat()
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0009E8FE File Offset: 0x0009CAFE
		public static HistoryChat gI { get; } = new HistoryChat();

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0009E905 File Offset: 0x0009CB05
		public int maxWidth
		{
			get
			{
				if (mGraphics.zoomLevel <= 1)
				{
					return 350;
				}
				return 280;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0009E91C File Offset: 0x0009CB1C
		public void append(string text)
		{
			List<string> list = new List<string>();
			try
			{
				list = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Utils.PathChatHistory));
			}
			catch
			{
			}
			if (list == null)
			{
				list = new List<string>();
			}
			list.Remove(text);
			list.Insert(0, text);
			try
			{
				File.WriteAllText(Utils.PathChatHistory, JsonConvert.SerializeObject(list, Formatting.Indented));
			}
			catch
			{
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0009E990 File Offset: 0x0009CB90
		public void paint(mGraphics g)
		{
			if (this.hints.Count > 0)
			{
				ChatTextField chatTextField = ChatTextField.gI();
				this.lenghtHintsShow = ((this.hints.Count < 10) ? this.hints.Count : 10);
				this.height = (this.lenghtHintsShow + 1) * 10;
				this.width = ((GameCanvas.w - 10 > this.maxWidth) ? this.maxWidth : (GameCanvas.w - 10));
				int num = this.lenghtHintsShow * (this.height - 10) / this.hints.Count;
				this.x = (GameCanvas.w - this.width) / 2;
				this.y = chatTextField.tfChat.y - 40 - this.height;
				g.setColor(0, 0.75f);
				g.fillRect(this.x, this.y, this.width, this.height);
				g.setColor(0, 1f);
				g.fillRect(this.x, this.y, this.width, 10);
				this.chatBack != this.hints[this.selectedIndex];
				g.setColor(16777215, 0.5f);
				g.fillRect(this.x, this.y + 10 - 1, this.width, 1);
				g.setColor(8618883, 0.75f);
				g.fillRect(this.x, this.y + 10 + 10 * (this.selectedIndex - this.scrollValue), this.width - 5, 10);
				g.setColor(16777215, 0.75f);
				g.fillRect(this.x, this.y + 10 + 10 * (this.selectedIndex - this.scrollValue), 2, 10);
				g.setColor(16777215, 0.75f);
				g.fillRect(this.x + this.width - 5, this.y + 10, 1, this.height - 10);
				g.setColor(16777215, 0.75f);
				g.fillRect(this.x + this.width - 3, this.y + 10 + this.scrollValue * (this.height - 10) / this.hints.Count, 2, num);
				for (int i = this.scrollValue; i < this.scrollValue + this.lenghtHintsShow; i++)
				{
				}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0009EC11 File Offset: 0x0009CE11
		public void show()
		{
			this.isShow = true;
			this.selectedIndex = 0;
			this.loadHints();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0009EC28 File Offset: 0x0009CE28
		public void update()
		{
			TField tfChat = ChatTextField.gI().tfChat;
			if (!ChatTextField.gI().isShow)
			{
				this.isShow = false;
			}
			if (!this.isShow)
			{
				return;
			}
			if (this.chatBack != tfChat.getText())
			{
				this.chatBack = tfChat.getText();
				this.selectedIndex = 0;
				this.scrollValue = 0;
				this.loadHints();
			}
			string text = "";
			string text2 = this.chatBack;
			int num = this.chatBack.LastIndexOf('/');
			if (num != -1)
			{
				text = this.chatBack.Substring(0, num);
				text2 = this.chatBack.Substring(num);
			}
			if (GameCanvas.keyPressed[22])
			{
				this.selectedIndex++;
				if (this.selectedIndex >= this.hints.Count)
				{
					this.selectedIndex = this.hints.Count - 1;
				}
				if (this.selectedIndex >= this.scrollValue + 10)
				{
					this.scrollValue = this.selectedIndex - 9;
				}
				tfChat.setText(text + this.hints[this.selectedIndex]);
				this.chatBack = tfChat.getText();
				GameCanvas.keyPressed[22] = false;
				GameCanvas.clearKeyPressed();
				GameCanvas.clearKeyHold();
			}
			if (GameCanvas.keyPressed[21])
			{
				this.selectedIndex--;
				if (this.selectedIndex <= 0)
				{
					this.selectedIndex = 0;
				}
				if (this.selectedIndex < this.scrollValue)
				{
					this.scrollValue = this.selectedIndex;
				}
				tfChat.setText(text + this.hints[this.selectedIndex]);
				this.chatBack = ChatTextField.gI().tfChat.getText();
				GameCanvas.keyPressed[21] = false;
				GameCanvas.clearKeyPressed();
				GameCanvas.clearKeyHold();
			}
			if (GameCanvas.keyPressed[16])
			{
				try
				{
					if (text2 != this.hints[this.selectedIndex])
					{
						tfChat.setText(text + this.hints[this.selectedIndex]);
					}
				}
				catch (Exception)
				{
				}
				GameCanvas.keyPressed[16] = false;
				GameCanvas.clearKeyPressed();
				GameCanvas.clearKeyHold();
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0009EE58 File Offset: 0x0009D058
		private void loadHints()
		{
			List<string> list = new List<string>();
			try
			{
				list = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Utils.PathChatHistory));
			}
			catch
			{
			}
			int num = list.IndexOf(GUIUtility.systemCopyBuffer);
			if (num == -1)
			{
				list.Insert(0, GUIUtility.systemCopyBuffer);
			}
			else if (num > 10)
			{
				list.RemoveAt(num);
				list.Insert(0, GUIUtility.systemCopyBuffer);
			}
			TField tfChat = ChatTextField.gI().tfChat;
			string endStr = tfChat.getText();
			int num2 = endStr.LastIndexOf('/');
			if (num2 != -1)
			{
				endStr = endStr.Substring(num2);
			}
			this.hints = list.FindAll((string x) => x.StartsWith(endStr));
		}

		// Token: 0x0400149F RID: 5279
		public const int HEIGHT_HINT_ITEM = 10;

		// Token: 0x040014A0 RID: 5280
		public const int MAX_HINTS_ITEM = 10;

		// Token: 0x040014A1 RID: 5281
		public List<string> hints = new List<string>();

		// Token: 0x040014A2 RID: 5282
		public int selectedIndex;

		// Token: 0x040014A3 RID: 5283
		public bool isShow;

		// Token: 0x040014A4 RID: 5284
		public int lenghtHintsShow;

		// Token: 0x040014A5 RID: 5285
		public int scrollValue;

		// Token: 0x040014A6 RID: 5286
		public int width;

		// Token: 0x040014A7 RID: 5287
		public int height;

		// Token: 0x040014A8 RID: 5288
		public int x;

		// Token: 0x040014A9 RID: 5289
		public int y;

		// Token: 0x040014AA RID: 5290
		private string chatBack;
	}
}

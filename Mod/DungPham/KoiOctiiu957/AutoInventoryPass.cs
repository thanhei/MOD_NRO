using System;

namespace Mod.DungPham.KoiOctiiu957
{
	// Tự bật ô nhập mã bảo vệ khi server báo "Chức năng bảo vệ đã được bật"
	public class AutoInventoryPass : IChatable
	{
		public static AutoInventoryPass getInstance()
		{
			if (AutoInventoryPass._Instance == null)
			{
				AutoInventoryPass._Instance = new AutoInventoryPass();
			}
			return AutoInventoryPass._Instance;
		}

		// Gọi từ InfoMe.addInfo (GameScr.info1 - thông báo hệ thống do chim đưa thư)
		public static void OnServerInfo(string text)
		{
			if (text == null || !AutoInventoryPass.IsTriggerText(text))
			{
				return;
			}
			// Con chim báo 2 lần liên tiếp -> không bật lại nếu ô đang mở hoặc vừa bật
			if (AutoInventoryPass.IsShowing())
			{
				return;
			}
			long now = mSystem.currentTimeMillis();
			if (now - AutoInventoryPass.lastShowTime < AutoInventoryPass.COOLDOWN_MS)
			{
				return;
			}
			AutoInventoryPass.lastShowTime = now;
			// Không mở ngay trong lúc xử lý message, để frame update kế tiếp mở
			AutoInventoryPass.isPending = true;
		}

		// Gọi mỗi frame từ MainMod.Update
		public static void Update()
		{
			if (!AutoInventoryPass.isPending)
			{
				return;
			}
			AutoInventoryPass.isPending = false;
			AutoInventoryPass.Show();
		}

		private static bool IsTriggerText(string text)
		{
			string lower = text.ToLower();
			try
			{
				lower = lower.Normalize(System.Text.NormalizationForm.FormC);
			}
			catch (Exception)
			{
			}
			// Bỏ hết khoảng trắng/xuống dòng để không phụ thuộc cách server ngắt dòng
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < lower.Length; i++)
			{
				if (!char.IsWhiteSpace(lower[i]))
				{
					sb.Append(lower[i]);
				}
			}
			return sb.ToString().Contains(AutoInventoryPass.TRIGGER_TEXT);
		}

		public static bool IsShowing()
		{
			if (ChatTextField.gI().isShow && mResources.input_Inventory_Pass.Equals(ChatTextField.gI().strChat))
			{
				return true;
			}
			Panel panel = GameCanvas.panel;
			return panel != null && panel.isShow && panel.chatTField != null && panel.chatTField.isShow && mResources.input_Inventory_Pass.Equals(panel.chatTField.strChat);
		}

		public static void Show()
		{
			// Đang mở Panel (hành trang, shop...) thì dùng ô gốc của Panel để nó nằm trên cùng và nhận phím.
			// Enter do Panel.onChatFromMe gốc xử lý
			Panel panel = GameCanvas.panel;
			if (panel != null && panel.isShow)
			{
				int oldSelected = panel.selected;
				panel.selected = 0;
				panel.doFireAccount();
				panel.selected = oldSelected;
				return;
			}
			// Không mở Panel thì dùng ô chat dùng chung của GameScr (cùng class ChatTextField), set giống Panel.doFireAccount case 0
			ChatTextField chat = ChatTextField.gI();
			chat.tfChat.setText(string.Empty);
			chat.strChat = mResources.input_Inventory_Pass;
			chat.tfChat.name = mResources.input_Inventory_Pass;
			chat.to = string.Empty;
			chat.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			chat.startChat2(AutoInventoryPass.getInstance(), string.Empty);
		}

		// Giống nhánh input_Inventory_Pass trong Panel.onChatFromMe
		public void onChatFromMe(string text, string to)
		{
			string pass = ChatTextField.gI().tfChat.getText();
			AutoInventoryPass.ResetChatTextField();
			if (pass == null || pass.Equals(string.Empty))
			{
				return;
			}
			try
			{
				int lockInventory = int.Parse(pass);
				if (pass.Length != 6)
				{
					GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
					return;
				}
				Service.gI().setLockInventory(lockInventory);
			}
			catch (Exception)
			{
				GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
			}
		}

		public void onCancelChat()
		{
			AutoInventoryPass.ResetChatTextField();
		}

		// Trả ô chat map về như cũ
		private static void ResetChatTextField()
		{
			ChatTextField chat = ChatTextField.gI();
			chat.strChat = "Chat";
			chat.tfChat.name = "chat";
			chat.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			chat.isShow = false;
		}

		private const string TRIGGER_TEXT = "chứcnăngbảovệđãđượcbật";

		private const long COOLDOWN_MS = 5000L;

		private static AutoInventoryPass _Instance;

		private static long lastShowTime;

		private static bool isPending;
	}
}

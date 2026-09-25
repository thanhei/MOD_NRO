using System;
using Mod.R;

namespace Mod.ModMenu
{
	// Token: 0x02000132 RID: 306
	internal class ModMenuItemValues : ModMenuItem, IChatable
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000AD3A3 File Offset: 0x000AB5A3
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x000AD3B0 File Offset: 0x000AB5B0
		internal double SelectedValue
		{
			get
			{
				return this.GetValueFunc();
			}
			set
			{
				if (value < this.MinValue || value > this.MaxValue)
				{
					return;
				}
				this.SetValueAction(value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000AD3D1 File Offset: 0x000AB5D1
		internal string[] Values
		{
			get
			{
				return this._config.Values;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000AD3DE File Offset: 0x000AB5DE
		internal string RMSName
		{
			get
			{
				return this._config.RMSName;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x000AD3EB File Offset: 0x000AB5EB
		internal Action<double> SetValueAction
		{
			get
			{
				return this._config.SetValueAction;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000AD3F8 File Offset: 0x000AB5F8
		internal Func<double> GetValueFunc
		{
			get
			{
				return this._config.GetValueFunc;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000AD405 File Offset: 0x000AB605
		internal string TextFieldName
		{
			get
			{
				return this._config.TextFieldTitle;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000AD412 File Offset: 0x000AB612
		internal string TextFieldHint
		{
			get
			{
				return this._config.TextFieldHint;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000AD41F File Offset: 0x000AB61F
		internal double MinValue
		{
			get
			{
				return this._config.MinValue;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x000AD42C File Offset: 0x000AB62C
		internal double MaxValue
		{
			get
			{
				return this._config.MaxValue;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000AD439 File Offset: 0x000AB639
		internal bool IsFloatingPoint
		{
			get
			{
				return this._config.IsFloatingPoint;
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000AD446 File Offset: 0x000AB646
		internal ModMenuItemValues(ModMenuItemValuesConfig config)
			: base(config)
		{
			if ((config.Values == null || config.Values.Length == 0) && string.IsNullOrEmpty(config.Description))
			{
				throw new ArgumentException("Values and description cannot be null at the same time");
			}
			this._config = config;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000AD47F File Offset: 0x000AB67F
		internal string getSelectedValue()
		{
			return this.Values[(int)this.SelectedValue];
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000AD490 File Offset: 0x000AB690
		internal void SwitchSelection()
		{
			if (this.Values != null)
			{
				if (this.SelectedValue < (double)(this.Values.Length - 1))
				{
					double selectedValue = this.SelectedValue;
					this.SelectedValue = selectedValue + 1.0;
					return;
				}
				this.SelectedValue = 0.0;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000AD4E0 File Offset: 0x000AB6E0
		internal void StartChat(ChatTextField textField)
		{
			this.currentCTF = textField;
			textField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			textField.initChatTextField();
			textField.strChat = string.Empty;
			textField.tfChat.name = this.TextFieldHint;
			textField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			textField.startChat2(this, this.TextFieldName);
			textField.tfChat.setText(this.SelectedValue.ToString());
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000AD570 File Offset: 0x000AB770
		public void onChatFromMe(string text, string to)
		{
			if (string.IsNullOrEmpty(text) || to != this.TextFieldName)
			{
				this.onCancelChat();
				return;
			}
			text = text.Replace('.', ',');
			double num = 0.0;
			bool flag = double.TryParse(text, out num);
			if (flag)
			{
				int num2 = 0;
				flag = this.IsFloatingPoint || int.TryParse(text, out num2);
				if (!this.IsFloatingPoint && flag)
				{
					num = (double)num2;
				}
			}
			if (flag)
			{
				if (this.MinValue != this.MaxValue && (num < this.MinValue || num > this.MaxValue))
				{
					GameCanvas.startOKDlg(string.Format(Strings.inputNumberOutOfRange, this.MinValue, this.MaxValue) + "!");
				}
				else
				{
					this.SelectedValue = num;
					GameScr.info1.addInfo(string.Format(Strings.valueChanged, base.Title, this.SelectedValue) + "!", 0);
				}
			}
			else
			{
				GameCanvas.startOKDlg(Strings.invalidValue + "!");
			}
			this.onCancelChat();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000AD68C File Offset: 0x000AB88C
		public void onCancelChat()
		{
			this.currentCTF.ResetTF();
		}

		// Token: 0x040016F0 RID: 5872
		private ModMenuItemValuesConfig _config;

		// Token: 0x040016F1 RID: 5873
		private ChatTextField currentCTF;
	}
}

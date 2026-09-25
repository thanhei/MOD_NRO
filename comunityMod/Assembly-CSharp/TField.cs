using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class TField : IActionListener
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000966 RID: 2406 RVA: 0x000880BA File Offset: 0x000862BA
	// (set) Token: 0x06000967 RID: 2407 RVA: 0x000880C2 File Offset: 0x000862C2
	public bool isFocus
	{
		get
		{
			return this._isFocus;
		}
		set
		{
			if (!value)
			{
				this.selectStartIndex = -1;
				this.undoQueue.Clear();
				this.undoIndex = -1;
			}
			else
			{
				TField.currentTField = this;
			}
			this._isFocus = value;
		}
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x000880F0 File Offset: 0x000862F0
	public TField(mScreen parentScr)
	{
		this.text = string.Empty;
		this.parentScr = parentScr;
		this.init();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x000881A8 File Offset: 0x000863A8
	public TField()
	{
		this.text = string.Empty;
		this.init();
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0008825C File Offset: 0x0008645C
	public TField(int x, int y, int w, int h)
	{
		this.text = string.Empty;
		this.init();
		this.x = x;
		this.y = y;
		this.width = w;
		this.height = h;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0008832C File Offset: 0x0008652C
	public TField(string text, int maxLen, int inputType)
	{
		this.text = text;
		this.maxTextLenght = maxLen;
		this.inputType = inputType;
		this.init();
		this.isTfield = true;
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x000883EE File Offset: 0x000865EE
	public static bool setNormal(char ch)
	{
		return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00004887 File Offset: 0x00002A87
	public void doChangeToTextBox()
	{
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00088414 File Offset: 0x00086614
	public static void setVendorTypeMode(int mode)
	{
		if (mode == TField.MOTO)
		{
			TField.print[0] = "0";
			TField.print[10] = " *";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
			return;
		}
		if (mode == TField.NOKIA)
		{
			TField.print[0] = " 0";
			TField.print[10] = "*";
			TField.print[11] = "#";
			TField.changeModeKey = 35;
			return;
		}
		if (mode == TField.ORTHER)
		{
			TField.print[0] = "0";
			TField.print[10] = "*";
			TField.print[11] = " #";
			TField.changeModeKey = 42;
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x000884C4 File Offset: 0x000866C4
	public void init()
	{
		TField.CARET_HEIGHT = mScreen.ITEM_HEIGHT + 1;
		this.cmdClear = new Command(mResources.DELETE, this, 1000, null);
		if (Main.isPC)
		{
			TField.typeXpeed = 0;
		}
		if (TField.imgTf == null)
		{
			TField.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00088517 File Offset: 0x00086717
	public void clearKeyWhenPutText(int keyCode)
	{
		if (keyCode == -8 && this.timeDelayKyCode <= 0)
		{
			if (this.timeDelayKyCode <= 0)
			{
				this.timeDelayKyCode = 1;
			}
			this.clear();
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0008853D File Offset: 0x0008673D
	public void clearAllText()
	{
		this.text = string.Empty;
		if (TField.kb != null)
		{
			TField.kb.text = string.Empty;
		}
		this.selectStartIndex = -1;
		this.caretPos = 0;
		this.setOffset(0);
		this.setPasswordTest();
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0008857C File Offset: 0x0008677C
	public void clear()
	{
		if (this.caretPos > 0 && this.text.Length > 0)
		{
			this.text = this.text.Substring(0, this.caretPos - 1);
			this.caretPos--;
			this.setOffset(0);
			this.setPasswordTest();
			if (TField.kb != null)
			{
				TField.kb.text = this.text;
			}
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000885EC File Offset: 0x000867EC
	public void clearAll()
	{
		if (this.caretPos > 0 && this.text.Length > 0)
		{
			this.text = this.text.Substring(0, this.text.Length - 1);
			this.caretPos--;
			this.setOffset();
			this.setPasswordTest();
			this.setFocusWithKb(true);
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00088668 File Offset: 0x00086868
	public void setOffset()
	{
		if (this.paintedText != null && mFont.tahoma_8b != null)
		{
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				this.paintedText = this.passwordText;
			}
			else
			{
				this.paintedText = this.text;
			}
			if (this.multiline)
			{
				this.paintedText = this.paintedText.Replace('\r', '\t').Replace('\n', '\t');
			}
			if (this.offsetX < 0 && mFont.tahoma_8b.getWidth(this.paintedText) + this.offsetX < this.width - TField.TEXT_GAP_X - 13 - TField.typingModeAreaWidth)
			{
				this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText);
			}
			if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) <= 0)
			{
				this.offsetX = -mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
				this.offsetX += 40;
			}
			else if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) >= this.width - 12 - TField.typingModeAreaWidth)
			{
				this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) - 2 * TField.TEXT_GAP_X;
			}
			if (this.offsetX > 0)
			{
				this.offsetX = 0;
			}
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0008880C File Offset: 0x00086A0C
	internal void keyPressedAny(int keyCode)
	{
		string[] array = ((this.inputType != TField.INPUT_TYPE_PASSWORD && this.inputType != TField.INPUT_ALPHA_NUMBER_ONLY) ? TField.print : TField.printA);
		if (keyCode == TField.lastKey)
		{
			this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
			char c = array[keyCode - 48][this.indexOfActiveChar];
			c = ((TField.mode == 0) ? char.ToLower(c) : ((TField.mode == 1) ? char.ToUpper(c) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c))));
			string text = this.text.Substring(0, this.caretPos - 1) + c.ToString();
			if (this.caretPos < this.text.Length)
			{
				text += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = text;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.setPasswordTest();
		}
		else if (this.text.Length < this.maxTextLenght)
		{
			if (TField.mode == 1 && TField.lastKey != -1984)
			{
				TField.mode = 0;
			}
			this.indexOfActiveChar = 0;
			char c2 = array[keyCode - 48][this.indexOfActiveChar];
			c2 = ((TField.mode == 0) ? char.ToLower(c2) : ((TField.mode == 1) ? char.ToUpper(c2) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c2))));
			string text2 = this.text.Substring(0, this.caretPos) + c2.ToString();
			if (this.caretPos < this.text.Length)
			{
				text2 += this.text.Substring(this.caretPos, this.text.Length);
			}
			this.text = text2;
			this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset();
		}
		TField.lastKey = keyCode;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00088A5C File Offset: 0x00086C5C
	internal void keyPressedAscii(int keyCode)
	{
		if ((this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122))
		{
			return;
		}
		if (this.text.Length < this.maxTextLenght)
		{
			string text = this.text.Substring(0, this.caretPos) + ((char)keyCode).ToString();
			if (this.caretPos < this.text.Length)
			{
				text += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
			}
			this.text = text;
			this.caretPos++;
			this.setPasswordTest();
			this.setOffset(0);
		}
		if (TField.kb != null)
		{
			TField.kb.text = this.text;
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00088B50 File Offset: 0x00086D50
	public static void setMode()
	{
		TField.mode++;
		if (TField.mode > 3)
		{
			TField.mode = 0;
		}
		TField.lastKey = TField.changeModeKey;
		TField.timeChangeMode = (long)(Environment.TickCount / 1000);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00088B88 File Offset: 0x00086D88
	internal void setDau()
	{
		this.timeDau = (long)(Environment.TickCount / 100);
		if (this.indexDau == -1)
		{
			for (int i = this.caretPos; i > 0; i--)
			{
				char c = this.text[i - 1];
				for (int j = 0; j < TField.printDau.Length; j++)
				{
					if (c == TField.printDau[j])
					{
						this.indexTemplate = j;
						this.indexCong = 0;
						this.indexDau = i - 1;
						return;
					}
				}
			}
			this.indexDau = -1;
			return;
		}
		this.indexCong++;
		if (this.indexCong >= 6)
		{
			this.indexCong = 0;
		}
		string text = this.text.Substring(0, this.indexDau);
		string text2 = this.text.Substring(this.indexDau + 1);
		this.text = text + TField.printDau.Substring(this.indexTemplate + this.indexCong, 1) + text2;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x000151BF File Offset: 0x000133BF
	public bool keyPressed(int keyCode)
	{
		return false;
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00088C7C File Offset: 0x00086E7C
	public void setOffset(int index)
	{
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		if (this.multiline)
		{
			this.paintedText = this.paintedText.Replace('\r', '\t').Replace('\n', '\t');
		}
		int num = mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
		if (index == -1)
		{
			if (num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length)
			{
				this.offsetX += mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
		}
		else if (index == 1)
		{
			if (num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0)
			{
				this.offsetX -= mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
			}
		}
		else
		{
			this.offsetX = -(num - (this.width - 12));
		}
		if (this.offsetX > 0)
		{
			this.offsetX = 0;
			return;
		}
		if (this.offsetX < 0)
		{
			int num2 = mFont.tahoma_8b.getWidth(this.paintedText) - (this.width - 12);
			if (this.offsetX < -num2)
			{
				this.offsetX = -num2;
			}
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00088E0C File Offset: 0x0008700C
	public void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text, string info)
	{
		g.setColor(0);
		if (iss)
		{
			g.drawRegion(TField.imgTf, 0, 81, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 135, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + w - 58, y, 0);
			for (int i = 0; i < (w - 58) / 29; i++)
			{
				g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + 29 + i * 29, y, 0);
			}
		}
		else
		{
			g.drawRegion(TField.imgTf, 0, 0, 29, 27, 0, x, y, 0);
			g.drawRegion(TField.imgTf, 0, 54, 29, 27, 0, x + w - 29, y, 0);
			g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + w - 58, y, 0);
			for (int j = 0; j < (w - 58) / 29; j++)
			{
				g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + 29 + j * 29, y, 0);
			}
		}
		g.setClip(x + 3, y + 1, w - 4, h);
		if (Utils.IsPC() && this.selectStartIndex > -1)
		{
			g.setColor(new Color(0f, 0f, 0f, 0.4f));
			string text2 = text.Substring(Math.Min(this.selectStartIndex, this.caretPos), Math.Abs(this.selectStartIndex - this.caretPos));
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				text2 = new string('*', text2.Length);
			}
			g.fillRect(xText + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, Math.Min(this.selectStartIndex, this.caretPos))) - 1, yText, mFont.tahoma_8b.getWidth(text2), mFont.tahoma_8b.getHeight());
		}
		if (text != null && !text.Equals(string.Empty))
		{
			mFont.tahoma_8b.drawString(g, text, xText, yText, 0);
			return;
		}
		if (info != null)
		{
			if (iss)
			{
				mFont.tahoma_7b_focus.drawString(g, info, xText, yText, 0);
				return;
			}
			mFont.tahoma_7b_unfocus.drawString(g, info, xText, yText, 0);
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0008905C File Offset: 0x0008725C
	public void paint(mGraphics g)
	{
		this.lastTimeCheckVisible = mSystem.currentTimeMillis();
		this.isVisible = true;
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		bool flag = this.isFocused();
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.paintedText = this.passwordText;
		}
		else
		{
			this.paintedText = this.text;
		}
		if (this.multiline)
		{
			this.paintedText = this.paintedText.Replace('\r', '\t').Replace('\n', '\t');
		}
		this.paintInputTf(g, flag, this.x, this.y - 1, this.width, this.height + 5, TField.TEXT_GAP_X + this.offsetX + this.x + 1, this.y + (this.height - mFont.tahoma_8b.getHeight()) / 2 + 2, this.paintedText, this.name);
		g.setClip(this.x + 3, this.y + 1, this.width - 4, this.height - 2);
		g.setColor(0);
		if (flag && this.isPaintMouse && this.isPaintCarret)
		{
			if (this.keyInActiveState == 0 && (this.showCaretCounter > 0 || this.counter / TField.CARET_SHOWING_TIME % 4 == 0))
			{
				g.setColor(7999781);
				g.fillRect(TField.TEXT_GAP_X + 1 + this.offsetX + this.x + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos) + "a") - TField.CARET_WIDTH - mFont.tahoma_8b.getWidth("a"), this.y + (this.height - TField.CARET_HEIGHT) / 2 + 5, TField.CARET_WIDTH, TField.CARET_HEIGHT);
			}
			GameCanvas.resetTrans(g);
			if (this.text != null && this.text.Length > 0 && GameCanvas.isTouch)
			{
				g.drawImage(GameCanvas.imgClear, this.x + this.width - 13, this.y + this.height / 2 + 3, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00089290 File Offset: 0x00087490
	internal bool isFocused()
	{
		return this.isFocus;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00089298 File Offset: 0x00087498
	public string subString(string str, int index, int indexTo)
	{
		if (index >= 0 && indexTo > str.Length - 1)
		{
			return str.Substring(index);
		}
		if (index < 0 || index > str.Length - 1 || indexTo < 0 || indexTo > str.Length - 1)
		{
			return string.Empty;
		}
		string text = string.Empty;
		for (int i = index; i < indexTo; i++)
		{
			text += str[i].ToString();
		}
		return text;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0008930C File Offset: 0x0008750C
	internal void setPasswordTest()
	{
		if (this.inputType == TField.INPUT_TYPE_PASSWORD)
		{
			this.passwordText = string.Empty;
			for (int i = 0; i < this.text.Length; i++)
			{
				this.passwordText += "*";
			}
			if (this.keyInActiveState > 0 && this.caretPos > 0)
			{
				this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1].ToString() + this.passwordText.Substring(this.caretPos, this.passwordText.Length);
			}
		}
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x000893C8 File Offset: 0x000875C8
	public void update()
	{
		this.isPaintCarret = true;
		if (Main.isPC)
		{
			if (this.timeDelayKyCode > 0)
			{
				this.timeDelayKyCode--;
			}
			if (this.timeDelayKyCode <= 0)
			{
				this.timeDelayKyCode = 0;
			}
		}
		if (TField.kb != null && TField.currentTField == this)
		{
			if (TField.kb.text.Length < 40 && this.isFocus)
			{
				this.setText(TField.kb.text);
			}
			if (TField.kb.status == TouchScreenKeyboard.Status.Done && this.cmdDoneAction != null)
			{
				this.cmdDoneAction.performAction();
			}
		}
		this.counter++;
		if (this.keyInActiveState > 0)
		{
			this.keyInActiveState--;
			if (this.keyInActiveState == 0)
			{
				this.indexOfActiveChar = 0;
				if (TField.mode == 1 && TField.lastKey != TField.changeModeKey && this.isFocus)
				{
					TField.mode = 0;
				}
				TField.lastKey = -1984;
				this.setPasswordTest();
			}
		}
		if (this.showCaretCounter > 0)
		{
			this.showCaretCounter--;
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.setTextBox();
		}
		if (this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L)
		{
			this.indexDau = -1;
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00089514 File Offset: 0x00087714
	public void setTextBox()
	{
		if (GameCanvas.isPointerHoldIn(this.x + this.width - 20, this.y, 40, this.height))
		{
			this.clearAllText();
			this.isFocus = true;
			return;
		}
		if (GameCanvas.isPointerHoldIn(this.x, this.y, this.width - 20, this.height))
		{
			this.setFocusWithKb(true);
			return;
		}
		this.setFocus(false);
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00089588 File Offset: 0x00087788
	public void setFocus(bool isFocus)
	{
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
			if (TField.kb != null)
			{
				TField.kb.text = TField.currentTField.text;
			}
		}
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x000895F4 File Offset: 0x000877F4
	public void setFocusWithKb(bool isFocus)
	{
		if (this.isFocus != isFocus)
		{
			TField.mode = 0;
		}
		TField.lastKey = -1984;
		TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
		this.isFocus = isFocus;
		if (isFocus)
		{
			TField.currentTField = this;
		}
		else if (TField.currentTField == this)
		{
			TField.currentTField = null;
		}
		if (Thread.CurrentThread.Name == Main.mainThreadName && TField.currentTField != null)
		{
			TouchScreenKeyboard.hideInput = !TField.currentTField.showSubTextField;
			TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
			if (this.inputType == TField.INPUT_TYPE_NUMERIC)
			{
				touchScreenKeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
			}
			bool flag = false;
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				flag = true;
			}
			if (Utils.IsMobile())
			{
				TField.kb = TouchScreenKeyboard.Open(TField.currentTField.text, touchScreenKeyboardType, false, false, flag, false, TField.currentTField.name);
			}
			if (TField.kb != null)
			{
				TField.kb.text = TField.currentTField.text;
			}
			Cout.LogWarning("SHOW KEYBOARD FOR " + TField.currentTField.text);
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0008970A File Offset: 0x0008790A
	public string getText()
	{
		return this.text;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00089712 File Offset: 0x00087912
	public void clearKb()
	{
		if (TField.kb != null)
		{
			TField.kb.text = string.Empty;
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0008972C File Offset: 0x0008792C
	public void setText(string text)
	{
		if (text != null)
		{
			TField.lastKey = -1984;
			this.keyInActiveState = 0;
			this.indexOfActiveChar = 0;
			this.text = text;
			this.paintedText = text;
			if (this.multiline)
			{
				this.paintedText = this.paintedText.Replace('\r', '\t').Replace('\n', '\t');
			}
			if (text == string.Empty && TField.kb != null)
			{
				TField.kb.text = "";
			}
			this.setPasswordTest();
			this.caretPos = text.Length;
			this.setOffset();
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x000897C8 File Offset: 0x000879C8
	public void insertText(string text)
	{
		this.text = this.text.Substring(0, this.caretPos) + text + this.text.Substring(this.caretPos);
		this.setPasswordTest();
		this.caretPos += text.Length;
		this.setOffset();
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00089823 File Offset: 0x00087A23
	public int getMaxTextLenght()
	{
		return this.maxTextLenght;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0008982B File Offset: 0x00087A2B
	public void setMaxTextLenght(int maxTextLenght)
	{
		this.maxTextLenght = maxTextLenght;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00089834 File Offset: 0x00087A34
	public int getIputType()
	{
		return this.inputType;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0008983C File Offset: 0x00087A3C
	public void setIputType(int iputType)
	{
		this.inputType = iputType;
		this.setMaxTextLenght(500);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00089850 File Offset: 0x00087A50
	public void perform(int idAction, object p)
	{
		if (idAction == 1000)
		{
			this.clear();
		}
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00089860 File Offset: 0x00087A60
	internal void HandleInputText()
	{
		if (mSystem.currentTimeMillis() - this.lastTimeCheckVisible > 300L)
		{
			this.isVisible = false;
		}
		if (!this.isVisible)
		{
			return;
		}
		bool flag = false;
		if (Event.current.type == EventType.KeyDown)
		{
			if (this.undoQueue.Count == 0 || this.undoQueue.ElementAt<string>(this.undoIndex) != this.text)
			{
				this.undoQueue.RemoveRange(this.undoIndex + 1, this.undoQueue.Count - this.undoIndex - 1);
				this.undoQueue.Add(this.text);
				this.undoIndex++;
			}
			KeyCode keyCode = Event.current.keyCode;
			if (keyCode <= KeyCode.Tab)
			{
				if (keyCode == KeyCode.Backspace)
				{
					if (this.selectStartIndex < 0)
					{
						if (this.text.Length > 0 && this.caretPos > 0)
						{
							this.text = this.text.Substring(0, this.caretPos - 1) + this.text.Substring(this.caretPos);
							this.caretPos--;
							flag = true;
						}
					}
					else if (this.text.Length > 0)
					{
						int num = Math.Min(this.caretPos, this.selectStartIndex);
						this.text = this.text.Substring(0, Math.Min(this.caretPos, this.selectStartIndex)) + this.text.Substring(Math.Max(this.caretPos, this.selectStartIndex));
						this.caretPos = num;
						flag = true;
					}
					this.selectStartIndex = -1;
					goto IL_09A1;
				}
				if (keyCode == KeyCode.Tab)
				{
					return;
				}
			}
			else
			{
				if (keyCode == KeyCode.Delete)
				{
					if (this.selectStartIndex < 0)
					{
						if (this.text.Length > 0 && this.caretPos < this.text.Length)
						{
							this.text = this.text.Substring(0, this.caretPos) + this.text.Substring(this.caretPos + 1);
							flag = true;
						}
					}
					else if (this.text.Length > 0)
					{
						int num2 = Math.Min(this.caretPos, this.selectStartIndex);
						this.text = this.text.Substring(0, Math.Min(this.caretPos, this.selectStartIndex)) + this.text.Substring(Math.Max(this.caretPos, this.selectStartIndex));
						this.caretPos = num2;
						flag = true;
					}
					this.selectStartIndex = -1;
					goto IL_09A1;
				}
				switch (keyCode)
				{
				case KeyCode.RightArrow:
					if (Event.current.shift)
					{
						if (this.selectStartIndex == -1)
						{
							this.selectStartIndex = this.caretPos;
						}
					}
					else
					{
						this.selectStartIndex = -1;
					}
					if (Event.current.control)
					{
						if (this.caretPos < this.text.Length && this.text[this.caretPos] == ' ')
						{
							this.caretPos++;
							goto IL_09A1;
						}
						while (this.caretPos < this.text.Length)
						{
							if (this.text[this.caretPos] == ' ')
							{
								break;
							}
							this.caretPos++;
						}
						goto IL_09A1;
					}
					else
					{
						if (this.caretPos < this.text.Length)
						{
							this.caretPos++;
							goto IL_09A1;
						}
						goto IL_09A1;
					}
					break;
				case KeyCode.LeftArrow:
					if (Event.current.shift)
					{
						if (this.selectStartIndex == -1)
						{
							this.selectStartIndex = this.caretPos;
						}
					}
					else
					{
						this.selectStartIndex = -1;
					}
					if (Event.current.control)
					{
						if (this.caretPos > 0 && this.text[this.caretPos - 1] == ' ')
						{
							this.caretPos--;
							goto IL_09A1;
						}
						while (this.caretPos > 0)
						{
							if (this.text[this.caretPos - 1] == ' ')
							{
								break;
							}
							this.caretPos--;
						}
						goto IL_09A1;
					}
					else
					{
						if (this.caretPos > 0)
						{
							this.caretPos--;
							goto IL_09A1;
						}
						goto IL_09A1;
					}
					break;
				case KeyCode.Home:
					if (Event.current.shift)
					{
						if (this.selectStartIndex == -1)
						{
							this.selectStartIndex = this.caretPos;
						}
					}
					else
					{
						this.selectStartIndex = -1;
					}
					this.caretPos = 0;
					goto IL_09A1;
				case KeyCode.End:
					if (Event.current.shift)
					{
						if (this.selectStartIndex == -1)
						{
							this.selectStartIndex = this.caretPos;
						}
					}
					else
					{
						this.selectStartIndex = -1;
					}
					this.caretPos = this.text.Length;
					goto IL_09A1;
				}
			}
			if (Event.current.control)
			{
				if (Event.current.keyCode == KeyCode.A)
				{
					this.caretPos = this.text.Length;
					this.selectStartIndex = 0;
					goto IL_09A1;
				}
				if ((Event.current.keyCode == KeyCode.C || Event.current.keyCode == KeyCode.Insert) && this.inputType != TField.INPUT_TYPE_PASSWORD)
				{
					if (this.selectStartIndex < 0)
					{
						GUIUtility.systemCopyBuffer = this.text;
						goto IL_09A1;
					}
					GUIUtility.systemCopyBuffer = this.text.Substring(Math.Min(this.caretPos, this.selectStartIndex), Math.Abs(this.caretPos - this.selectStartIndex));
					goto IL_09A1;
				}
				else
				{
					if (Event.current.keyCode == KeyCode.V)
					{
						if (this.selectStartIndex < 0)
						{
							this.text = this.text.Substring(0, this.caretPos) + GUIUtility.systemCopyBuffer + this.text.Substring(this.caretPos);
							this.caretPos += GUIUtility.systemCopyBuffer.Length;
						}
						else
						{
							int num3 = Math.Min(this.caretPos, this.selectStartIndex);
							this.text = this.text.Substring(0, Math.Min(this.caretPos, this.selectStartIndex)) + GUIUtility.systemCopyBuffer + this.text.Substring(Math.Max(this.caretPos, this.selectStartIndex));
							this.caretPos = num3 + GUIUtility.systemCopyBuffer.Length;
						}
						this.selectStartIndex = -1;
						flag = true;
						goto IL_09A1;
					}
					if (Event.current.keyCode == KeyCode.Z)
					{
						if (this.undoQueue.Count > 0)
						{
							int num4 = this.undoIndex - 1;
							if (num4 < this.undoQueue.Count && num4 >= 0)
							{
								this.text = this.undoQueue.ElementAt<string>(num4);
								this.undoIndex = num4;
							}
							this.caretPos = this.text.Length;
						}
					}
					else if (Event.current.keyCode == KeyCode.Y && this.undoQueue.Count > 0)
					{
						int num5 = this.undoIndex + 1;
						if (num5 < this.undoQueue.Count && num5 >= 0)
						{
							this.text = this.undoQueue.ElementAt<string>(num5);
							this.undoIndex = num5;
						}
						this.caretPos = this.text.Length;
					}
				}
			}
			if (Event.current.shift && Event.current.keyCode == KeyCode.Insert)
			{
				if (this.selectStartIndex < 0)
				{
					this.text = this.text.Substring(0, this.caretPos) + GUIUtility.systemCopyBuffer + this.text.Substring(this.caretPos);
					this.caretPos += GUIUtility.systemCopyBuffer.Length;
				}
				else
				{
					int num6 = Math.Min(this.caretPos, this.selectStartIndex);
					this.text = this.text.Substring(0, Math.Min(this.caretPos, this.selectStartIndex)) + GUIUtility.systemCopyBuffer + this.text.Substring(Math.Max(this.caretPos, this.selectStartIndex));
					this.caretPos = num6 + GUIUtility.systemCopyBuffer.Length;
				}
				this.selectStartIndex = -1;
				flag = true;
			}
			else
			{
				char character = Event.current.character;
				if (character == '\0')
				{
					return;
				}
				if (character == '\n' && !this.multiline && !Event.current.alt)
				{
					return;
				}
				if (this.inputType == TField.INPUT_TYPE_NUMERIC && !char.IsNumber(character) && character != '.' && character != '-' && character != ',')
				{
					return;
				}
				if (this.text.Length >= this.maxTextLenght)
				{
					return;
				}
				Font font = mFont.tahoma_7b_focus.myFont;
				if (!font)
				{
					font = GUI.skin.font;
				}
				if (!font.HasCharacter(character) && character != '\n')
				{
					return;
				}
				if (this.selectStartIndex < 0)
				{
					this.text = this.text.Substring(0, this.caretPos) + character.ToString() + this.text.Substring(this.caretPos);
					this.caretPos++;
				}
				else
				{
					int num7 = Math.Min(this.caretPos, this.selectStartIndex);
					this.text = this.text.Substring(0, Math.Min(this.caretPos, this.selectStartIndex)) + character.ToString() + this.text.Substring(Math.Max(this.caretPos, this.selectStartIndex));
					this.caretPos = num7 + 1;
				}
				this.selectStartIndex = -1;
				string text;
				if (VietnameseInput.ToVietnamese(this.text, out text, ref this.caretPos, this.inputType))
				{
					this.text = text;
				}
				flag = true;
			}
			IL_09A1:
			Event.current.Use();
		}
		else if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && this.isFocus)
		{
			float num8 = Input.mousePosition.x / (float)mGraphics.zoomLevel;
			float num9 = ((float)Screen.height - Input.mousePosition.y) / (float)mGraphics.zoomLevel;
			if (num8 >= (float)this.x && num8 <= (float)(this.x + this.width) && num9 >= (float)this.y && num9 <= (float)(this.y + this.height))
			{
				float num10 = num8 - (float)this.x;
				string text2 = this.paintedText;
				int num11 = mFont.tahoma_8b.getWidth(text2);
				while ((float)num11 > num10)
				{
					num11 = mFont.tahoma_8b.getWidth(text2 = text2.Substring(0, text2.Length - 1));
				}
				if (Event.current.shift || Event.current.type == EventType.MouseDrag)
				{
					if (this.selectStartIndex < 0)
					{
						this.selectStartIndex = this.caretPos;
					}
				}
				else
				{
					this.selectStartIndex = -1;
				}
				this.caretPos = text2.Length;
				Event.current.Use();
			}
		}
		if (flag)
		{
			this.setPasswordTest();
			this.setOffset(0);
		}
	}

	// Token: 0x04001053 RID: 4179
	public bool multiline;

	// Token: 0x04001054 RID: 4180
	private int selectStartIndex = -1;

	// Token: 0x04001055 RID: 4181
	private bool _isFocus;

	// Token: 0x04001056 RID: 4182
	private List<string> undoQueue = new List<string>();

	// Token: 0x04001057 RID: 4183
	private int undoIndex = -1;

	// Token: 0x04001058 RID: 4184
	private bool isVisible;

	// Token: 0x04001059 RID: 4185
	private long lastTimeCheckVisible;

	// Token: 0x0400105A RID: 4186
	public int x;

	// Token: 0x0400105B RID: 4187
	public int y;

	// Token: 0x0400105C RID: 4188
	public int width;

	// Token: 0x0400105D RID: 4189
	public int height;

	// Token: 0x0400105E RID: 4190
	public bool lockArrow;

	// Token: 0x0400105F RID: 4191
	public bool justReturnFromTextBox;

	// Token: 0x04001060 RID: 4192
	public bool paintFocus = true;

	// Token: 0x04001061 RID: 4193
	public const sbyte KEY_LEFT = 14;

	// Token: 0x04001062 RID: 4194
	public const sbyte KEY_RIGHT = 15;

	// Token: 0x04001063 RID: 4195
	public const sbyte KEY_CLEAR = 19;

	// Token: 0x04001064 RID: 4196
	public static int typeXpeed = 2;

	// Token: 0x04001065 RID: 4197
	internal static readonly int[] MAX_TIME_TO_CONFIRM_KEY = new int[] { 30, 14, 11, 9, 6, 4, 2 };

	// Token: 0x04001066 RID: 4198
	internal static int CARET_HEIGHT = 0;

	// Token: 0x04001067 RID: 4199
	internal static readonly int CARET_WIDTH = 1;

	// Token: 0x04001068 RID: 4200
	internal static readonly int CARET_SHOWING_TIME = 5;

	// Token: 0x04001069 RID: 4201
	internal static readonly int TEXT_GAP_X = 4;

	// Token: 0x0400106A RID: 4202
	internal static readonly int MAX_SHOW_CARET_COUNER = 10;

	// Token: 0x0400106B RID: 4203
	public static readonly int INPUT_TYPE_ANY = 0;

	// Token: 0x0400106C RID: 4204
	public static readonly int INPUT_TYPE_NUMERIC = 1;

	// Token: 0x0400106D RID: 4205
	public static readonly int INPUT_TYPE_PASSWORD = 2;

	// Token: 0x0400106E RID: 4206
	public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

	// Token: 0x0400106F RID: 4207
	internal static string[] print = new string[]
	{
		" 0", ".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1", "abc2áàảãạâấầẩẫậăắằẳẵặ2", "def3đéèẻẽẹêếềểễệ3", "ghi4íìỉĩị4", "jkl5", "mno6óòỏõọôốồổỗộơớờởỡợ6", "pqrs7", "tuv8úùủũụưứừửữự8", "wxyz9ýỳỷỹỵ9",
		"*", "#"
	};

	// Token: 0x04001070 RID: 4208
	internal static string[] printA = new string[]
	{
		"0", "1", "abc2", "def3", "ghi4", "jkl5", "mno6", "pqrs7", "tuv8", "wxyz9",
		"0", "0"
	};

	// Token: 0x04001071 RID: 4209
	internal static string[] printBB = new string[]
	{
		" 0", "er1", "ty2", "ui3", "df4", "gh5", "jk6", "cv7", "bn8", "m9",
		"0", "0", "qw!", "as?", "zx", "op.", "l,"
	};

	// Token: 0x04001072 RID: 4210
	internal string text = string.Empty;

	// Token: 0x04001073 RID: 4211
	internal string passwordText = string.Empty;

	// Token: 0x04001074 RID: 4212
	internal string paintedText = string.Empty;

	// Token: 0x04001075 RID: 4213
	internal int caretPos;

	// Token: 0x04001076 RID: 4214
	internal int counter;

	// Token: 0x04001077 RID: 4215
	internal int maxTextLenght = 500;

	// Token: 0x04001078 RID: 4216
	internal int offsetX;

	// Token: 0x04001079 RID: 4217
	internal static int lastKey = -1984;

	// Token: 0x0400107A RID: 4218
	internal int keyInActiveState;

	// Token: 0x0400107B RID: 4219
	internal int indexOfActiveChar;

	// Token: 0x0400107C RID: 4220
	internal int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

	// Token: 0x0400107D RID: 4221
	internal int inputType = TField.INPUT_TYPE_ANY;

	// Token: 0x0400107E RID: 4222
	public static bool isQwerty = true;

	// Token: 0x0400107F RID: 4223
	public static int typingModeAreaWidth;

	// Token: 0x04001080 RID: 4224
	public static int mode = 0;

	// Token: 0x04001081 RID: 4225
	public static long timeChangeMode;

	// Token: 0x04001082 RID: 4226
	public static readonly string[] modeNotify = new string[] { "abc", "Abc", "ABC", "123" };

	// Token: 0x04001083 RID: 4227
	public static readonly int NOKIA = 0;

	// Token: 0x04001084 RID: 4228
	public static readonly int MOTO = 1;

	// Token: 0x04001085 RID: 4229
	public static readonly int ORTHER = 2;

	// Token: 0x04001086 RID: 4230
	public static readonly int BB = 3;

	// Token: 0x04001087 RID: 4231
	public static int changeModeKey = 11;

	// Token: 0x04001088 RID: 4232
	public static readonly sbyte abc = 0;

	// Token: 0x04001089 RID: 4233
	public static readonly sbyte Abc = 1;

	// Token: 0x0400108A RID: 4234
	public static readonly sbyte ABC = 2;

	// Token: 0x0400108B RID: 4235
	public static readonly sbyte number123 = 3;

	// Token: 0x0400108C RID: 4236
	public static TField currentTField;

	// Token: 0x0400108D RID: 4237
	public bool isTfield;

	// Token: 0x0400108E RID: 4238
	public bool isPaintMouse = true;

	// Token: 0x0400108F RID: 4239
	public string name = string.Empty;

	// Token: 0x04001090 RID: 4240
	public string title = string.Empty;

	// Token: 0x04001091 RID: 4241
	public string strInfo;

	// Token: 0x04001092 RID: 4242
	public Command cmdClear;

	// Token: 0x04001093 RID: 4243
	public Command cmdDoneAction;

	// Token: 0x04001094 RID: 4244
	internal mScreen parentScr;

	// Token: 0x04001095 RID: 4245
	internal int timeDelayKyCode;

	// Token: 0x04001096 RID: 4246
	internal int holdCount;

	// Token: 0x04001097 RID: 4247
	public static int changeDau;

	// Token: 0x04001098 RID: 4248
	internal int indexDau = -1;

	// Token: 0x04001099 RID: 4249
	internal int indexTemplate;

	// Token: 0x0400109A RID: 4250
	internal int indexCong;

	// Token: 0x0400109B RID: 4251
	internal long timeDau;

	// Token: 0x0400109C RID: 4252
	internal static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

	// Token: 0x0400109D RID: 4253
	public static Image imgTf;

	// Token: 0x0400109E RID: 4254
	public int timePutKeyClearAll;

	// Token: 0x0400109F RID: 4255
	public int timeClearFirt;

	// Token: 0x040010A0 RID: 4256
	public bool isPaintCarret;

	// Token: 0x040010A1 RID: 4257
	public bool showSubTextField = true;

	// Token: 0x040010A2 RID: 4258
	public static TouchScreenKeyboard kb;

	// Token: 0x040010A3 RID: 4259
	public static int[][] BBKEY = new int[][]
	{
		new int[] { 32, 48 },
		new int[] { 49, 69 },
		new int[] { 50, 84 },
		new int[] { 51, 85 },
		new int[] { 52, 68 },
		new int[] { 53, 71 },
		new int[] { 54, 74 },
		new int[] { 55, 67 },
		new int[] { 56, 66 },
		new int[] { 57, 77 },
		new int[] { 42, 128 },
		new int[] { 35, 137 },
		new int[] { 33, 113 },
		new int[] { 63, 97 },
		new int[] { 64, 121, 122 },
		new int[] { 46, 111 },
		new int[] { 44, 108 }
	};
}

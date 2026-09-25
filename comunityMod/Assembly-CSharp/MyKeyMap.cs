using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class MyKeyMap
{
	// Token: 0x060005BC RID: 1468 RVA: 0x000578F4 File Offset: 0x00055AF4
	public static int map(KeyCode k)
	{
		object obj = MyKeyMap.h[k];
		if (obj == null)
		{
			int num = (int)k;
			if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Event.current.capsLock) && num >= 97 && num <= 122)
			{
				num -= 32;
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				switch (k)
				{
				case KeyCode.Quote:
					num = 34;
					break;
				case KeyCode.LeftParen:
				case KeyCode.RightParen:
				case KeyCode.Asterisk:
				case KeyCode.Plus:
				case KeyCode.Colon:
				case KeyCode.Less:
					break;
				case KeyCode.Comma:
					num = 60;
					break;
				case KeyCode.Minus:
					num = 95;
					break;
				case KeyCode.Period:
					num = 62;
					break;
				case KeyCode.Slash:
					num = 63;
					break;
				case KeyCode.Alpha0:
					num = 41;
					break;
				case KeyCode.Alpha1:
					num = 33;
					break;
				case KeyCode.Alpha2:
					num = 64;
					break;
				case KeyCode.Alpha3:
					num = 35;
					break;
				case KeyCode.Alpha4:
					num = 36;
					break;
				case KeyCode.Alpha5:
					num = 37;
					break;
				case KeyCode.Alpha6:
					num = 94;
					break;
				case KeyCode.Alpha7:
					num = 38;
					break;
				case KeyCode.Alpha8:
					num = 42;
					break;
				case KeyCode.Alpha9:
					num = 40;
					break;
				case KeyCode.Semicolon:
					num = 58;
					break;
				case KeyCode.Equals:
					num = 43;
					break;
				default:
					switch (k)
					{
					case KeyCode.LeftBracket:
						num = 123;
						break;
					case KeyCode.Backslash:
						num = 124;
						break;
					case KeyCode.RightBracket:
						num = 125;
						break;
					case KeyCode.BackQuote:
						num = 126;
						break;
					}
					break;
				}
			}
			return num;
		}
		return (int)obj;
	}

	// Token: 0x04000C05 RID: 3077
	internal static Hashtable h = new Hashtable
	{
		{
			KeyCode.UpArrow,
			-1
		},
		{
			KeyCode.DownArrow,
			-2
		},
		{
			KeyCode.LeftArrow,
			-3
		},
		{
			KeyCode.RightArrow,
			-4
		},
		{
			KeyCode.Return,
			-5
		},
		{
			KeyCode.Backspace,
			-8
		},
		{
			KeyCode.F1,
			-21
		},
		{
			KeyCode.F2,
			-22
		},
		{
			KeyCode.F3,
			-23
		},
		{
			KeyCode.Tab,
			-26
		},
		{
			KeyCode.Escape,
			-30
		},
		{
			KeyCode.F4,
			0
		},
		{
			KeyCode.F5,
			0
		},
		{
			KeyCode.F6,
			0
		},
		{
			KeyCode.F7,
			0
		},
		{
			KeyCode.F8,
			0
		},
		{
			KeyCode.F9,
			0
		},
		{
			KeyCode.F10,
			0
		},
		{
			KeyCode.F11,
			0
		},
		{
			KeyCode.F12,
			0
		},
		{
			KeyCode.F13,
			0
		},
		{
			KeyCode.F14,
			0
		},
		{
			KeyCode.F15,
			0
		},
		{
			KeyCode.LeftShift,
			0
		},
		{
			KeyCode.RightShift,
			0
		},
		{
			KeyCode.LeftAlt,
			0
		},
		{
			KeyCode.RightAlt,
			0
		},
		{
			KeyCode.AltGr,
			0
		},
		{
			KeyCode.LeftControl,
			0
		},
		{
			KeyCode.RightControl,
			0
		},
		{
			KeyCode.LeftMeta,
			0
		},
		{
			KeyCode.RightMeta,
			0
		},
		{
			KeyCode.Numlock,
			0
		},
		{
			KeyCode.PageUp,
			0
		},
		{
			KeyCode.PageDown,
			0
		},
		{
			KeyCode.Insert,
			0
		},
		{
			KeyCode.Delete,
			0
		},
		{
			KeyCode.Pause,
			0
		},
		{
			KeyCode.Break,
			0
		},
		{
			KeyCode.Print,
			0
		},
		{
			KeyCode.SysReq,
			0
		},
		{
			KeyCode.Home,
			0
		},
		{
			KeyCode.End,
			0
		},
		{
			KeyCode.Clear,
			0
		},
		{
			KeyCode.CapsLock,
			0
		},
		{
			KeyCode.Help,
			0
		},
		{
			KeyCode.Menu,
			0
		}
	};
}

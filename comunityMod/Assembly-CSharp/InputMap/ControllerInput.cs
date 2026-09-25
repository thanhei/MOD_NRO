using System;
using Mod;
using Mod.ModMenu;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputMap
{
	// Token: 0x020000CE RID: 206
	internal class ControllerInput : MonoBehaviour
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00093A1D File Offset: 0x00091C1D
		internal static bool IsLeftButtonPressed
		{
			get
			{
				return ControllerInput.leftButton;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00093A24 File Offset: 0x00091C24
		internal static bool IsRightButtonPressed
		{
			get
			{
				return ControllerInput.rightButton;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00093A2B File Offset: 0x00091C2B
		internal static bool IsSelectButtonPressed
		{
			get
			{
				return ControllerInput.selectButton;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00093A32 File Offset: 0x00091C32
		internal static bool IsStartButtonPressed
		{
			get
			{
				return ControllerInput.startButton;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00093A39 File Offset: 0x00091C39
		internal static bool IsWestButtonPressed
		{
			get
			{
				return ControllerInput.westButton;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00093A40 File Offset: 0x00091C40
		internal static bool IsEastButtonPressed
		{
			get
			{
				return ControllerInput.eastButton;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00093A47 File Offset: 0x00091C47
		internal static bool IsNorthButtonPressed
		{
			get
			{
				return ControllerInput.northButton;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00093A4E File Offset: 0x00091C4E
		internal static bool IsSouthButtonPressed
		{
			get
			{
				return ControllerInput.southButton;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00093A55 File Offset: 0x00091C55
		internal static bool IsJoystickButton1Pressed
		{
			get
			{
				return ControllerInput.joystickButton1;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00093A5C File Offset: 0x00091C5C
		internal static bool IsJoystickButton2Pressed
		{
			get
			{
				return ControllerInput.joystickButton2;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00093A63 File Offset: 0x00091C63
		internal static float LeftTriggerValue
		{
			get
			{
				return ControllerInput.leftTrigger;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00093A6A File Offset: 0x00091C6A
		internal static float RightTriggerValue
		{
			get
			{
				return ControllerInput.rightTrigger;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00093A71 File Offset: 0x00091C71
		internal static Vector2 DPadValue
		{
			get
			{
				return ControllerInput.dPad;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00093A78 File Offset: 0x00091C78
		internal static Vector2 Joystick1Value
		{
			get
			{
				return ControllerInput.joystick1;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00093A7F File Offset: 0x00091C7F
		internal static Vector2 Joystick2Value
		{
			get
			{
				return ControllerInput.joystick2;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00093A88 File Offset: 0x00091C88
		private void Awake()
		{
			ControllerInput.controller = new GameController();
			ControllerInput.controller.Input.LeftButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.leftButton = true;
			};
			ControllerInput.controller.Input.LeftButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.leftButton = false;
			};
			ControllerInput.controller.Input.RightButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.rightButton = true;
			};
			ControllerInput.controller.Input.RightButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.rightButton = false;
			};
			ControllerInput.controller.Input.SelectButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.selectButton = true;
			};
			ControllerInput.controller.Input.SelectButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.selectButton = false;
			};
			ControllerInput.controller.Input.StartButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.startButton = true;
			};
			ControllerInput.controller.Input.StartButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.startButton = false;
			};
			ControllerInput.controller.Input.WestButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.westButton = true;
			};
			ControllerInput.controller.Input.WestButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.westButton = false;
			};
			ControllerInput.controller.Input.EastButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.eastButton = true;
			};
			ControllerInput.controller.Input.EastButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.eastButton = false;
			};
			ControllerInput.controller.Input.NorthButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.northButton = true;
			};
			ControllerInput.controller.Input.NorthButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.northButton = false;
			};
			ControllerInput.controller.Input.SouthButton.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.southButton = true;
			};
			ControllerInput.controller.Input.SouthButton.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.southButton = false;
			};
			ControllerInput.controller.Input.JoystickButton1.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystickButton1 = true;
			};
			ControllerInput.controller.Input.JoystickButton1.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystickButton1 = false;
			};
			ControllerInput.controller.Input.JoystickButton2.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystickButton2 = true;
			};
			ControllerInput.controller.Input.JoystickButton2.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystickButton2 = false;
			};
			ControllerInput.controller.Input.LeftTrigger.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.leftTrigger = ctx.ReadValue<float>();
			};
			ControllerInput.controller.Input.LeftTrigger.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.leftTrigger = 0f;
			};
			ControllerInput.controller.Input.RightTrigger.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.rightTrigger = ctx.ReadValue<float>();
			};
			ControllerInput.controller.Input.RightTrigger.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.rightTrigger = 0f;
			};
			ControllerInput.controller.Input.DPad.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.dPad = ctx.ReadValue<Vector2>();
			};
			ControllerInput.controller.Input.DPad.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.dPad = Vector2.zero;
			};
			ControllerInput.controller.Input.Joystick1.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystick1 = ctx.ReadValue<Vector2>();
			};
			ControllerInput.controller.Input.Joystick1.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystick1 = Vector2.zero;
			};
			ControllerInput.controller.Input.Joystick2.performed += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystick2 = ctx.ReadValue<Vector2>();
			};
			ControllerInput.controller.Input.Joystick2.canceled += delegate(InputAction.CallbackContext ctx)
			{
				ControllerInput.joystick2 = Vector2.zero;
			};
			ControllerInput.controller.Enable();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00094100 File Offset: 0x00092300
		private void Update()
		{
			if (ControllerInput.CanControlGameScr())
			{
				ControllerInput.CheckJoystickMoveMyChar();
				ControllerInput.CheckJoystickMoveCamera();
			}
			else
			{
				ControllerInput.CheckDPadButtons();
			}
			if (ControllerInput.IsSouthButtonPressed)
			{
				ControllerInput.ButtonEnter = true;
				ControllerInput.southButton = false;
				return;
			}
			if (ControllerInput.IsEastButtonPressed)
			{
				GameCanvas.keyPressed[13] = true;
				ControllerInput.eastButton = false;
				return;
			}
			if (ControllerInput.IsWestButtonPressed)
			{
				GameScr.gI().doUseHP();
				ControllerInput.westButton = false;
				return;
			}
			if (ControllerInput.IsNorthButtonPressed)
			{
				Utils.useCapsule();
				ControllerInput.northButton = false;
				return;
			}
			if (ControllerInput.IsLeftButtonPressed && GameCanvas.currentScreen is GameScr)
			{
				if (!GameCanvas.panel.isShow)
				{
					GameScr.gI().cmdMenu.performAction();
					return;
				}
			}
			else if (ControllerInput.IsRightButtonPressed && GameCanvas.currentScreen is GameScr)
			{
				if (GameCanvas.panel2 == null || (GameCanvas.panel2 != null && !GameCanvas.panel2.isShow))
				{
					ModMenuMain.ShowPanel();
					return;
				}
			}
			else if (ControllerInput.CanControlGameScr())
			{
				if (ControllerInput.LeftTriggerValue > 0f)
				{
					if (ControllerInput.isLeftTriggerCanceled)
					{
						int num = Array.IndexOf<Skill>(GameScr.keySkill, global::Char.myCharz().myskill);
						int num2 = 0;
						do
						{
							if (--num < 0)
							{
								num = GameScr.keySkill.Length - 1;
							}
							num2++;
						}
						while (GameScr.keySkill[num] == null && num2 < GameScr.keySkill.Length);
						Skill skill = (global::Char.myCharz().myskill = GameScr.keySkill[num]);
						Service.gI().selectSkill((int)skill.template.id);
						GameScr.gI().saveRMSCurrentSkill(skill.template.id);
						GameScr.gI().resetButton();
						ControllerInput.isLeftTriggerCanceled = false;
					}
				}
				else
				{
					ControllerInput.isLeftTriggerCanceled = true;
				}
				if (ControllerInput.RightTriggerValue > 0f)
				{
					if (ControllerInput.isRightTriggerCanceled)
					{
						int num3 = Array.IndexOf<Skill>(GameScr.keySkill, global::Char.myCharz().myskill);
						int num4 = 0;
						do
						{
							if (++num3 == GameScr.keySkill.Length)
							{
								num3 = 0;
							}
							num4++;
						}
						while (GameScr.keySkill[num3] == null && num4 < GameScr.keySkill.Length);
						Skill skill2 = (global::Char.myCharz().myskill = GameScr.keySkill[num3]);
						Service.gI().selectSkill((int)skill2.template.id);
						GameScr.gI().saveRMSCurrentSkill(skill2.template.id);
						GameScr.gI().resetButton();
						ControllerInput.isRightTriggerCanceled = false;
						return;
					}
				}
				else
				{
					ControllerInput.isRightTriggerCanceled = true;
				}
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0009435C File Offset: 0x0009255C
		private static bool CanControlGameScr()
		{
			return GameCanvas.currentScreen is GameScr && (GameCanvas.panel == null || !GameCanvas.panel.isShow) && (GameCanvas.panel2 == null || !GameCanvas.panel2.isShow) && !InfoDlg.isShow && (GameCanvas.currentDialog == null || !(GameCanvas.currentDialog is MsgDlg)) && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu && ChatPopup.serverChatPopUp == null && ChatPopup.currChatPopup == null;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000943EC File Offset: 0x000925EC
		private static void CheckJoystickMoveMyChar()
		{
			if (ControllerInput.Joystick1Value != Vector2.zero)
			{
				ControllerInput.isResetMoveMyChar = true;
				float num = Mathf.Atan2(ControllerInput.Joystick1Value.y, ControllerInput.Joystick1Value.x) * 57.29578f;
				if (num < 0f)
				{
					num += 360f;
				}
				ControllerInput.ResetMoveButtons();
				if (ControllerInput.IsJoystickButton1Pressed)
				{
					if (!ControllerInput.isNextMap)
					{
						if (num >= 45f && num <= 135f)
						{
							Utils.ChangeMapMiddle();
						}
						else if (num >= 135f && num <= 225f)
						{
							Utils.ChangeMapLeft();
						}
						else if (num <= 45f || num >= 315f)
						{
							Utils.ChangeMapRight();
						}
						else if (num >= 225f && num <= 315f)
						{
							Utils.DonTho();
						}
						ControllerInput.isNextMap = true;
						return;
					}
				}
				else if (!ControllerInput.isNextMap)
				{
					if (num >= 45f && num <= 135f)
					{
						ControllerInput.ButtonUpHold = (ControllerInput.ButtonUp = true);
					}
					else if (num >= 225f && num <= 315f)
					{
						ControllerInput.ButtonDownHold = (ControllerInput.ButtonDown = true);
					}
					if (num >= 135f && num <= 225f)
					{
						ControllerInput.ButtonLeftHold = (ControllerInput.ButtonLeft = true);
						return;
					}
					if (num <= 45f || num >= 315f)
					{
						ControllerInput.ButtonRightHold = (ControllerInput.ButtonRight = true);
						return;
					}
				}
			}
			else
			{
				if (ControllerInput.isNextMap)
				{
					ControllerInput.isNextMap = false;
				}
				if (ControllerInput.isResetMoveMyChar)
				{
					ControllerInput.isResetMoveMyChar = false;
					ControllerInput.ResetMoveButtons();
				}
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00094557 File Offset: 0x00092757
		private static void ResetMoveButtons()
		{
			ControllerInput.ButtonUpHold = (ControllerInput.ButtonUp = false);
			ControllerInput.ButtonDownHold = (ControllerInput.ButtonDown = false);
			ControllerInput.ButtonLeftHold = (ControllerInput.ButtonLeft = false);
			ControllerInput.ButtonRightHold = (ControllerInput.ButtonRight = false);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0009458C File Offset: 0x0009278C
		private static void CheckJoystickMoveCamera()
		{
			if (ControllerInput.Joystick2Value != Vector2.zero)
			{
				ControllerInput.isResetCamera = true;
				global::Char.myCharz().cmtoChar = false;
				float num = Mathf.Atan2(ControllerInput.Joystick2Value.y, ControllerInput.Joystick2Value.x) * 57.29578f;
				if (num < 0f)
				{
					num += 360f;
				}
				if (num >= 45f && num <= 135f)
				{
					GameScr.cmy = (GameScr.cmtoY = Mathf.Clamp(GameScr.cmtoY - (int)ControllerInput.Joystick2Value.y * 3, 0, GameScr.cmyLim));
				}
				else if (num >= 225f && num <= 315f)
				{
					GameScr.cmy = (GameScr.cmtoY = Mathf.Clamp(GameScr.cmtoY - (int)ControllerInput.Joystick2Value.y * 3, 0, GameScr.cmyLim));
				}
				if (num >= 135f && num <= 225f)
				{
					GameScr.cmx = (GameScr.cmtoX = Mathf.Clamp(GameScr.cmtoX + (int)ControllerInput.Joystick2Value.x * 3, 24, GameScr.cmxLim));
					return;
				}
				if (num <= 45f || num >= 315f)
				{
					GameScr.cmx = (GameScr.cmtoX = Mathf.Clamp(GameScr.cmtoX + (int)ControllerInput.Joystick2Value.x * 3, 24, GameScr.cmxLim));
					return;
				}
			}
			else
			{
				if (ControllerInput.IsJoystickButton2Pressed)
				{
					global::Char.myCharz().cmtoChar = false;
					return;
				}
				if (ControllerInput.isResetCamera)
				{
					ControllerInput.isResetCamera = false;
					global::Char.myCharz().cmtoChar = true;
				}
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00094704 File Offset: 0x00092904
		private static void CheckDPadButtons()
		{
			if (ControllerInput.DPadValue != Vector2.zero)
			{
				ControllerInput.ResetMoveButtons();
				if (ControllerInput.DPadValue.x == 1f)
				{
					ControllerInput.ButtonRightHold = (ControllerInput.ButtonRight = true);
				}
				else if (ControllerInput.DPadValue.x == -1f)
				{
					ControllerInput.ButtonLeftHold = (ControllerInput.ButtonLeft = true);
				}
				else if (ControllerInput.DPadValue.y == 1f)
				{
					ControllerInput.ButtonUpHold = (ControllerInput.ButtonUp = true);
				}
				else if (ControllerInput.DPadValue.y == -1f)
				{
					ControllerInput.ButtonDownHold = (ControllerInput.ButtonDown = true);
				}
				ControllerInput.dPad = Vector2.zero;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000947AE File Offset: 0x000929AE
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x000947C2 File Offset: 0x000929C2
		private static bool ButtonUp
		{
			get
			{
				return GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21];
			}
			set
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000947D7 File Offset: 0x000929D7
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x000947EB File Offset: 0x000929EB
		private static bool ButtonDown
		{
			get
			{
				return GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22];
			}
			set
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00094800 File Offset: 0x00092A00
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x00094814 File Offset: 0x00092A14
		private static bool ButtonLeft
		{
			get
			{
				return GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23];
			}
			set
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00094829 File Offset: 0x00092A29
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0009483D File Offset: 0x00092A3D
		private static bool ButtonRight
		{
			get
			{
				return GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24];
			}
			set
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00094852 File Offset: 0x00092A52
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00094866 File Offset: 0x00092A66
		private static bool ButtonEnter
		{
			get
			{
				return GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25];
			}
			set
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0009487B File Offset: 0x00092A7B
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0009488F File Offset: 0x00092A8F
		private static bool ButtonUpHold
		{
			get
			{
				return GameCanvas.keyHold[(!Main.isPC) ? 2 : 21];
			}
			set
			{
				GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000948A4 File Offset: 0x00092AA4
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000948B8 File Offset: 0x00092AB8
		private static bool ButtonDownHold
		{
			get
			{
				return GameCanvas.keyHold[(!Main.isPC) ? 8 : 22];
			}
			set
			{
				GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000948CD File Offset: 0x00092ACD
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x000948E1 File Offset: 0x00092AE1
		private static bool ButtonLeftHold
		{
			get
			{
				return GameCanvas.keyHold[(!Main.isPC) ? 4 : 23];
			}
			set
			{
				GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000948F6 File Offset: 0x00092AF6
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0009490A File Offset: 0x00092B0A
		private static bool ButtonRightHold
		{
			get
			{
				return GameCanvas.keyHold[(!Main.isPC) ? 6 : 24];
			}
			set
			{
				GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0009491F File Offset: 0x00092B1F
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00094933 File Offset: 0x00092B33
		private static bool ButtonEnterHold
		{
			get
			{
				return GameCanvas.keyHold[(!Main.isPC) ? 5 : 25];
			}
			set
			{
				GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] = value;
			}
		}

		// Token: 0x04001404 RID: 5124
		private static GameController controller;

		// Token: 0x04001405 RID: 5125
		private static float leftTrigger;

		// Token: 0x04001406 RID: 5126
		private static float rightTrigger;

		// Token: 0x04001407 RID: 5127
		private static bool leftButton;

		// Token: 0x04001408 RID: 5128
		private static bool rightButton;

		// Token: 0x04001409 RID: 5129
		private static bool selectButton;

		// Token: 0x0400140A RID: 5130
		private static bool startButton;

		// Token: 0x0400140B RID: 5131
		private static bool westButton;

		// Token: 0x0400140C RID: 5132
		private static bool eastButton;

		// Token: 0x0400140D RID: 5133
		private static bool northButton;

		// Token: 0x0400140E RID: 5134
		private static bool southButton;

		// Token: 0x0400140F RID: 5135
		private static bool joystickButton1;

		// Token: 0x04001410 RID: 5136
		private static bool joystickButton2;

		// Token: 0x04001411 RID: 5137
		private static Vector2 dPad;

		// Token: 0x04001412 RID: 5138
		private static Vector2 joystick1;

		// Token: 0x04001413 RID: 5139
		private static Vector2 joystick2;

		// Token: 0x04001414 RID: 5140
		private static bool isResetMoveMyChar;

		// Token: 0x04001415 RID: 5141
		private static bool isNextMap;

		// Token: 0x04001416 RID: 5142
		private static bool isResetCamera;

		// Token: 0x04001417 RID: 5143
		private static bool isLeftTriggerCanceled;

		// Token: 0x04001418 RID: 5144
		private static bool isRightTriggerCanceled;
	}
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

namespace InputMap
{
	// Token: 0x020000D3 RID: 211
	internal class InputDeviceDetector : MonoBehaviour
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x00095948 File Offset: 0x00093B48
		private void OnEnable()
		{
			InputSystem.onActionChange += this.DetectCurrentInputDevice;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0009595B File Offset: 0x00093B5B
		private void OnDisable()
		{
			InputSystem.onActionChange -= this.DetectCurrentInputDevice;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0009596E File Offset: 0x00093B6E
		private void DetectCurrentInputDevice(object obj, InputActionChange change)
		{
			if (change == InputActionChange.ActionPerformed)
			{
				InputDeviceDetector.currentDevice = ((InputAction)obj).activeControl.device;
				if (InputDeviceDetector.currentDevice is Keyboard || InputDeviceDetector.currentDevice is Mouse)
				{
					InputDeviceDetector.ShowCursor();
					return;
				}
				InputDeviceDetector.HideCursor();
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000959AC File Offset: 0x00093BAC
		internal static void ShowCursor()
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000959BA File Offset: 0x00093BBA
		internal static void HideCursor()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Confined;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000959C8 File Offset: 0x00093BC8
		internal static bool IsController()
		{
			return InputDeviceDetector.currentDevice is Gamepad;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000959D7 File Offset: 0x00093BD7
		internal static bool IsXboxController()
		{
			return InputDeviceDetector.currentDevice is XInputController;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000959E6 File Offset: 0x00093BE6
		internal static bool IsDualShockController()
		{
			return InputDeviceDetector.currentDevice is DualShockGamepad;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000151BF File Offset: 0x000133BF
		internal static bool IsSwitchController()
		{
			return false;
		}

		// Token: 0x0400144E RID: 5198
		internal static InputDevice currentDevice;
	}
}

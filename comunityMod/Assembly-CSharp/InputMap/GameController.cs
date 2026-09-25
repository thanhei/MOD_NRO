using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace InputMap
{
	// Token: 0x020000D0 RID: 208
	public class GameController : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00094A76 File Offset: 0x00092C76
		public InputActionAsset asset { get; }

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00094A80 File Offset: 0x00092C80
		public GameController()
		{
			this.asset = InputActionAsset.FromJson("{\n    \"name\": \"GameControls\",\n    \"maps\": [\n        {\n            \"name\": \"Input\",\n            \"id\": \"486cd634-e311-46b1-8c42-76b5e8512443\",\n            \"actions\": [\n                {\n                    \"name\": \"LeftTrigger\",\n                    \"type\": \"Value\",\n                    \"id\": \"7be9eef6-cf3a-4c92-ad87-c3d86d8da65d\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RightTrigger\",\n                    \"type\": \"Value\",\n                    \"id\": \"c281b28d-d583-4ac1-893f-6501412e65fb\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"LeftButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"c10bb6a1-18e1-461a-acab-89d784e1dc65\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RightButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"9919a266-cf85-4839-a1e6-0d087cf0b6d1\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SelectButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"8f65c8f2-d22f-4465-a1c1-ea71629aa164\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"StartButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"f4b43fe8-3174-4e9b-ab8e-aa611a201483\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"WestButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"99551ad9-19f2-453f-8815-bfea99dfaf20\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"EastButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"20e06efe-ba9c-4335-8785-f5dd8a8a0bec\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"NorthButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"53e1d2ab-85bc-4459-b765-acdac82f9934\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SouthButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"cc3d0ec4-c940-4bae-aba0-d3bbd0e40054\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"DPad\",\n                    \"type\": \"Value\",\n                    \"id\": \"5d1e0ea0-c55b-412c-8a46-480656a9e8d2\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Joystick1\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"e83c125a-7308-49e5-97f6-2f2cc4e4af7a\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Joystick2\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"cfed8bd0-93c3-49bb-a01b-afdb23259b01\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"JoystickButton1\",\n                    \"type\": \"Button\",\n                    \"id\": \"68993cdb-f776-49ab-b0af-fb8b9719bffa\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"JoystickButton2\",\n                    \"type\": \"Button\",\n                    \"id\": \"446eeb26-abf9-4bfc-bf8f-ccc96f04cc4b\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Keyboard\",\n                    \"type\": \"Button\",\n                    \"id\": \"71b6e43b-1719-41b8-a52c-08ded69a3a0e\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Mouse\",\n                    \"type\": \"Button\",\n                    \"id\": \"3c3dc2af-d78b-42cb-bc24-0344797bac7c\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Pointer\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"8f9b8aed-d3ee-4049-bb68-c83db51099cc\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"5e25293e-e19f-420d-9055-e41c48263140\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"LeftTrigger\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c97d9b5b-be3e-4989-893c-7ee75b782535\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RightTrigger\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d6ec1fed-bc10-4b6c-a875-2ee200afaf6c\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"LeftButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ef9a606b-aa9e-432c-a942-6de5eb49eac6\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RightButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0095fef6-45ea-431c-8253-29d73ce1306c\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SelectButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b489601c-a5d2-4f95-8329-efa4919dbaa5\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"StartButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fe989977-c008-40c9-b453-f508b4d9a226\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"WestButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"83a6536f-dd7c-4750-84aa-402f6a42e091\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"EastButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"76d9c77d-fdac-4abf-96c0-2cb5eb8860c4\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"NorthButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e063af1a-905b-4bf6-aa14-919a2d024013\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SouthButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1597d1d3-71dc-416c-b22c-f9e2421bfd7e\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"DPad\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"87667bc1-ff22-4645-a972-35fc71a474ce\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Joystick1\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"849e84c8-2065-451b-a8a3-13993b985700\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Joystick2\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"00adebb6-8b9a-485c-8737-8b0260d23885\",\n                    \"path\": \"<Gamepad>/leftStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"JoystickButton1\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6f4aea88-ed4b-4228-b040-481c1df3c6f2\",\n                    \"path\": \"<Gamepad>/rightStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"JoystickButton2\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a0912c38-d7b5-43ca-b8a8-2c73a58bfaca\",\n                    \"path\": \"<Keyboard>/anyKey\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Keyboard\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f2d8d49a-ba1e-431e-8028-5d385936c045\",\n                    \"path\": \"<Mouse>/backButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4e6d983a-a505-41ce-8cff-ae077e80a746\",\n                    \"path\": \"<Mouse>/forwardButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"554e22cb-e23e-484a-8af5-4364aebb0177\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8f2ac747-ae36-4436-820f-63c97a49db5e\",\n                    \"path\": \"<Mouse>/middleButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1eafcea2-8944-4c6f-8a37-b13673e47bfb\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6759c82f-c7cf-4256-84a5-0a1438b7d77e\",\n                    \"path\": \"<Pointer>/press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Mouse\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d4b2b9fb-bd57-401f-b00b-a23079772747\",\n                    \"path\": \"<Pointer>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Pointer\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": []\n}");
			this.m_Input = this.asset.FindActionMap("Input", true);
			this.m_Input_LeftTrigger = this.m_Input.FindAction("LeftTrigger", true);
			this.m_Input_RightTrigger = this.m_Input.FindAction("RightTrigger", true);
			this.m_Input_LeftButton = this.m_Input.FindAction("LeftButton", true);
			this.m_Input_RightButton = this.m_Input.FindAction("RightButton", true);
			this.m_Input_SelectButton = this.m_Input.FindAction("SelectButton", true);
			this.m_Input_StartButton = this.m_Input.FindAction("StartButton", true);
			this.m_Input_WestButton = this.m_Input.FindAction("WestButton", true);
			this.m_Input_EastButton = this.m_Input.FindAction("EastButton", true);
			this.m_Input_NorthButton = this.m_Input.FindAction("NorthButton", true);
			this.m_Input_SouthButton = this.m_Input.FindAction("SouthButton", true);
			this.m_Input_DPad = this.m_Input.FindAction("DPad", true);
			this.m_Input_Joystick1 = this.m_Input.FindAction("Joystick1", true);
			this.m_Input_Joystick2 = this.m_Input.FindAction("Joystick2", true);
			this.m_Input_JoystickButton1 = this.m_Input.FindAction("JoystickButton1", true);
			this.m_Input_JoystickButton2 = this.m_Input.FindAction("JoystickButton2", true);
			this.m_Input_Keyboard = this.m_Input.FindAction("Keyboard", true);
			this.m_Input_Mouse = this.m_Input.FindAction("Mouse", true);
			this.m_Input_Pointer = this.m_Input.FindAction("Pointer", true);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00094C63 File Offset: 0x00092E63
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.asset);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00094C70 File Offset: 0x00092E70
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00094C7D File Offset: 0x00092E7D
		public InputBinding? bindingMask
		{
			get
			{
				return this.asset.bindingMask;
			}
			set
			{
				this.asset.bindingMask = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00094C8B File Offset: 0x00092E8B
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00094C98 File Offset: 0x00092E98
		public ReadOnlyArray<InputDevice>? devices
		{
			get
			{
				return this.asset.devices;
			}
			set
			{
				this.asset.devices = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00094CA6 File Offset: 0x00092EA6
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return this.asset.controlSchemes;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00094CB3 File Offset: 0x00092EB3
		public bool Contains(InputAction action)
		{
			return this.asset.Contains(action);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00094CC1 File Offset: 0x00092EC1
		public IEnumerator<InputAction> GetEnumerator()
		{
			return this.asset.GetEnumerator();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00094CCE File Offset: 0x00092ECE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00094CD6 File Offset: 0x00092ED6
		public void Enable()
		{
			this.asset.Enable();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00094CE3 File Offset: 0x00092EE3
		public void Disable()
		{
			this.asset.Disable();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00094CF0 File Offset: 0x00092EF0
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				return this.asset.bindings;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00094CFD File Offset: 0x00092EFD
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			return this.asset.FindAction(actionNameOrId, throwIfNotFound);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00094D0C File Offset: 0x00092F0C
		public int FindBinding(InputBinding bindingMask, out InputAction action)
		{
			return this.asset.FindBinding(bindingMask, out action);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00094D1B File Offset: 0x00092F1B
		public GameController.InputActions Input
		{
			get
			{
				return new GameController.InputActions(this);
			}
		}

		// Token: 0x04001439 RID: 5177
		private readonly InputActionMap m_Input;

		// Token: 0x0400143A RID: 5178
		private List<GameController.IInputActions> m_InputActionsCallbackInterfaces = new List<GameController.IInputActions>();

		// Token: 0x0400143B RID: 5179
		private readonly InputAction m_Input_LeftTrigger;

		// Token: 0x0400143C RID: 5180
		private readonly InputAction m_Input_RightTrigger;

		// Token: 0x0400143D RID: 5181
		private readonly InputAction m_Input_LeftButton;

		// Token: 0x0400143E RID: 5182
		private readonly InputAction m_Input_RightButton;

		// Token: 0x0400143F RID: 5183
		private readonly InputAction m_Input_SelectButton;

		// Token: 0x04001440 RID: 5184
		private readonly InputAction m_Input_StartButton;

		// Token: 0x04001441 RID: 5185
		private readonly InputAction m_Input_WestButton;

		// Token: 0x04001442 RID: 5186
		private readonly InputAction m_Input_EastButton;

		// Token: 0x04001443 RID: 5187
		private readonly InputAction m_Input_NorthButton;

		// Token: 0x04001444 RID: 5188
		private readonly InputAction m_Input_SouthButton;

		// Token: 0x04001445 RID: 5189
		private readonly InputAction m_Input_DPad;

		// Token: 0x04001446 RID: 5190
		private readonly InputAction m_Input_Joystick1;

		// Token: 0x04001447 RID: 5191
		private readonly InputAction m_Input_Joystick2;

		// Token: 0x04001448 RID: 5192
		private readonly InputAction m_Input_JoystickButton1;

		// Token: 0x04001449 RID: 5193
		private readonly InputAction m_Input_JoystickButton2;

		// Token: 0x0400144A RID: 5194
		private readonly InputAction m_Input_Keyboard;

		// Token: 0x0400144B RID: 5195
		private readonly InputAction m_Input_Mouse;

		// Token: 0x0400144C RID: 5196
		private readonly InputAction m_Input_Pointer;

		// Token: 0x020000D1 RID: 209
		public struct InputActions
		{
			// Token: 0x06000B03 RID: 2819 RVA: 0x00094D23 File Offset: 0x00092F23
			public InputActions(GameController wrapper)
			{
				this.m_Wrapper = wrapper;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00094D2C File Offset: 0x00092F2C
			public InputAction LeftTrigger
			{
				get
				{
					return this.m_Wrapper.m_Input_LeftTrigger;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00094D39 File Offset: 0x00092F39
			public InputAction RightTrigger
			{
				get
				{
					return this.m_Wrapper.m_Input_RightTrigger;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00094D46 File Offset: 0x00092F46
			public InputAction LeftButton
			{
				get
				{
					return this.m_Wrapper.m_Input_LeftButton;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00094D53 File Offset: 0x00092F53
			public InputAction RightButton
			{
				get
				{
					return this.m_Wrapper.m_Input_RightButton;
				}
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00094D60 File Offset: 0x00092F60
			public InputAction SelectButton
			{
				get
				{
					return this.m_Wrapper.m_Input_SelectButton;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00094D6D File Offset: 0x00092F6D
			public InputAction StartButton
			{
				get
				{
					return this.m_Wrapper.m_Input_StartButton;
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00094D7A File Offset: 0x00092F7A
			public InputAction WestButton
			{
				get
				{
					return this.m_Wrapper.m_Input_WestButton;
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00094D87 File Offset: 0x00092F87
			public InputAction EastButton
			{
				get
				{
					return this.m_Wrapper.m_Input_EastButton;
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00094D94 File Offset: 0x00092F94
			public InputAction NorthButton
			{
				get
				{
					return this.m_Wrapper.m_Input_NorthButton;
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00094DA1 File Offset: 0x00092FA1
			public InputAction SouthButton
			{
				get
				{
					return this.m_Wrapper.m_Input_SouthButton;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00094DAE File Offset: 0x00092FAE
			public InputAction DPad
			{
				get
				{
					return this.m_Wrapper.m_Input_DPad;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00094DBB File Offset: 0x00092FBB
			public InputAction Joystick1
			{
				get
				{
					return this.m_Wrapper.m_Input_Joystick1;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00094DC8 File Offset: 0x00092FC8
			public InputAction Joystick2
			{
				get
				{
					return this.m_Wrapper.m_Input_Joystick2;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00094DD5 File Offset: 0x00092FD5
			public InputAction JoystickButton1
			{
				get
				{
					return this.m_Wrapper.m_Input_JoystickButton1;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00094DE2 File Offset: 0x00092FE2
			public InputAction JoystickButton2
			{
				get
				{
					return this.m_Wrapper.m_Input_JoystickButton2;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00094DEF File Offset: 0x00092FEF
			public InputAction Keyboard
			{
				get
				{
					return this.m_Wrapper.m_Input_Keyboard;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00094DFC File Offset: 0x00092FFC
			public InputAction Mouse
			{
				get
				{
					return this.m_Wrapper.m_Input_Mouse;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00094E09 File Offset: 0x00093009
			public InputAction Pointer
			{
				get
				{
					return this.m_Wrapper.m_Input_Pointer;
				}
			}

			// Token: 0x06000B16 RID: 2838 RVA: 0x00094E16 File Offset: 0x00093016
			public InputActionMap Get()
			{
				return this.m_Wrapper.m_Input;
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x00094E23 File Offset: 0x00093023
			public void Enable()
			{
				this.Get().Enable();
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x00094E30 File Offset: 0x00093030
			public void Disable()
			{
				this.Get().Disable();
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00094E3D File Offset: 0x0009303D
			public bool enabled
			{
				get
				{
					return this.Get().enabled;
				}
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x00094E4A File Offset: 0x0009304A
			public static implicit operator InputActionMap(GameController.InputActions set)
			{
				return set.Get();
			}

			// Token: 0x06000B1B RID: 2843 RVA: 0x00094E54 File Offset: 0x00093054
			public void AddCallbacks(GameController.IInputActions instance)
			{
				if (instance == null || this.m_Wrapper.m_InputActionsCallbackInterfaces.Contains(instance))
				{
					return;
				}
				this.m_Wrapper.m_InputActionsCallbackInterfaces.Add(instance);
				this.LeftTrigger.started += instance.OnLeftTrigger;
				this.LeftTrigger.performed += instance.OnLeftTrigger;
				this.LeftTrigger.canceled += instance.OnLeftTrigger;
				this.RightTrigger.started += instance.OnRightTrigger;
				this.RightTrigger.performed += instance.OnRightTrigger;
				this.RightTrigger.canceled += instance.OnRightTrigger;
				this.LeftButton.started += instance.OnLeftButton;
				this.LeftButton.performed += instance.OnLeftButton;
				this.LeftButton.canceled += instance.OnLeftButton;
				this.RightButton.started += instance.OnRightButton;
				this.RightButton.performed += instance.OnRightButton;
				this.RightButton.canceled += instance.OnRightButton;
				this.SelectButton.started += instance.OnSelectButton;
				this.SelectButton.performed += instance.OnSelectButton;
				this.SelectButton.canceled += instance.OnSelectButton;
				this.StartButton.started += instance.OnStartButton;
				this.StartButton.performed += instance.OnStartButton;
				this.StartButton.canceled += instance.OnStartButton;
				this.WestButton.started += instance.OnWestButton;
				this.WestButton.performed += instance.OnWestButton;
				this.WestButton.canceled += instance.OnWestButton;
				this.EastButton.started += instance.OnEastButton;
				this.EastButton.performed += instance.OnEastButton;
				this.EastButton.canceled += instance.OnEastButton;
				this.NorthButton.started += instance.OnNorthButton;
				this.NorthButton.performed += instance.OnNorthButton;
				this.NorthButton.canceled += instance.OnNorthButton;
				this.SouthButton.started += instance.OnSouthButton;
				this.SouthButton.performed += instance.OnSouthButton;
				this.SouthButton.canceled += instance.OnSouthButton;
				this.DPad.started += instance.OnDPad;
				this.DPad.performed += instance.OnDPad;
				this.DPad.canceled += instance.OnDPad;
				this.Joystick1.started += instance.OnJoystick1;
				this.Joystick1.performed += instance.OnJoystick1;
				this.Joystick1.canceled += instance.OnJoystick1;
				this.Joystick2.started += instance.OnJoystick2;
				this.Joystick2.performed += instance.OnJoystick2;
				this.Joystick2.canceled += instance.OnJoystick2;
				this.JoystickButton1.started += instance.OnJoystickButton1;
				this.JoystickButton1.performed += instance.OnJoystickButton1;
				this.JoystickButton1.canceled += instance.OnJoystickButton1;
				this.JoystickButton2.started += instance.OnJoystickButton2;
				this.JoystickButton2.performed += instance.OnJoystickButton2;
				this.JoystickButton2.canceled += instance.OnJoystickButton2;
				this.Keyboard.started += instance.OnKeyboard;
				this.Keyboard.performed += instance.OnKeyboard;
				this.Keyboard.canceled += instance.OnKeyboard;
				this.Mouse.started += instance.OnMouse;
				this.Mouse.performed += instance.OnMouse;
				this.Mouse.canceled += instance.OnMouse;
				this.Pointer.started += instance.OnPointer;
				this.Pointer.performed += instance.OnPointer;
				this.Pointer.canceled += instance.OnPointer;
			}

			// Token: 0x06000B1C RID: 2844 RVA: 0x0009539C File Offset: 0x0009359C
			private void UnregisterCallbacks(GameController.IInputActions instance)
			{
				this.LeftTrigger.started -= instance.OnLeftTrigger;
				this.LeftTrigger.performed -= instance.OnLeftTrigger;
				this.LeftTrigger.canceled -= instance.OnLeftTrigger;
				this.RightTrigger.started -= instance.OnRightTrigger;
				this.RightTrigger.performed -= instance.OnRightTrigger;
				this.RightTrigger.canceled -= instance.OnRightTrigger;
				this.LeftButton.started -= instance.OnLeftButton;
				this.LeftButton.performed -= instance.OnLeftButton;
				this.LeftButton.canceled -= instance.OnLeftButton;
				this.RightButton.started -= instance.OnRightButton;
				this.RightButton.performed -= instance.OnRightButton;
				this.RightButton.canceled -= instance.OnRightButton;
				this.SelectButton.started -= instance.OnSelectButton;
				this.SelectButton.performed -= instance.OnSelectButton;
				this.SelectButton.canceled -= instance.OnSelectButton;
				this.StartButton.started -= instance.OnStartButton;
				this.StartButton.performed -= instance.OnStartButton;
				this.StartButton.canceled -= instance.OnStartButton;
				this.WestButton.started -= instance.OnWestButton;
				this.WestButton.performed -= instance.OnWestButton;
				this.WestButton.canceled -= instance.OnWestButton;
				this.EastButton.started -= instance.OnEastButton;
				this.EastButton.performed -= instance.OnEastButton;
				this.EastButton.canceled -= instance.OnEastButton;
				this.NorthButton.started -= instance.OnNorthButton;
				this.NorthButton.performed -= instance.OnNorthButton;
				this.NorthButton.canceled -= instance.OnNorthButton;
				this.SouthButton.started -= instance.OnSouthButton;
				this.SouthButton.performed -= instance.OnSouthButton;
				this.SouthButton.canceled -= instance.OnSouthButton;
				this.DPad.started -= instance.OnDPad;
				this.DPad.performed -= instance.OnDPad;
				this.DPad.canceled -= instance.OnDPad;
				this.Joystick1.started -= instance.OnJoystick1;
				this.Joystick1.performed -= instance.OnJoystick1;
				this.Joystick1.canceled -= instance.OnJoystick1;
				this.Joystick2.started -= instance.OnJoystick2;
				this.Joystick2.performed -= instance.OnJoystick2;
				this.Joystick2.canceled -= instance.OnJoystick2;
				this.JoystickButton1.started -= instance.OnJoystickButton1;
				this.JoystickButton1.performed -= instance.OnJoystickButton1;
				this.JoystickButton1.canceled -= instance.OnJoystickButton1;
				this.JoystickButton2.started -= instance.OnJoystickButton2;
				this.JoystickButton2.performed -= instance.OnJoystickButton2;
				this.JoystickButton2.canceled -= instance.OnJoystickButton2;
				this.Keyboard.started -= instance.OnKeyboard;
				this.Keyboard.performed -= instance.OnKeyboard;
				this.Keyboard.canceled -= instance.OnKeyboard;
				this.Mouse.started -= instance.OnMouse;
				this.Mouse.performed -= instance.OnMouse;
				this.Mouse.canceled -= instance.OnMouse;
				this.Pointer.started -= instance.OnPointer;
				this.Pointer.performed -= instance.OnPointer;
				this.Pointer.canceled -= instance.OnPointer;
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x000958B9 File Offset: 0x00093AB9
			public void RemoveCallbacks(GameController.IInputActions instance)
			{
				if (this.m_Wrapper.m_InputActionsCallbackInterfaces.Remove(instance))
				{
					this.UnregisterCallbacks(instance);
				}
			}

			// Token: 0x06000B1E RID: 2846 RVA: 0x000958D8 File Offset: 0x00093AD8
			public void SetCallbacks(GameController.IInputActions instance)
			{
				foreach (GameController.IInputActions inputActions in this.m_Wrapper.m_InputActionsCallbackInterfaces)
				{
					this.UnregisterCallbacks(inputActions);
				}
				this.m_Wrapper.m_InputActionsCallbackInterfaces.Clear();
				this.AddCallbacks(instance);
			}

			// Token: 0x0400144D RID: 5197
			private GameController m_Wrapper;
		}

		// Token: 0x020000D2 RID: 210
		public interface IInputActions
		{
			// Token: 0x06000B1F RID: 2847
			void OnLeftTrigger(InputAction.CallbackContext context);

			// Token: 0x06000B20 RID: 2848
			void OnRightTrigger(InputAction.CallbackContext context);

			// Token: 0x06000B21 RID: 2849
			void OnLeftButton(InputAction.CallbackContext context);

			// Token: 0x06000B22 RID: 2850
			void OnRightButton(InputAction.CallbackContext context);

			// Token: 0x06000B23 RID: 2851
			void OnSelectButton(InputAction.CallbackContext context);

			// Token: 0x06000B24 RID: 2852
			void OnStartButton(InputAction.CallbackContext context);

			// Token: 0x06000B25 RID: 2853
			void OnWestButton(InputAction.CallbackContext context);

			// Token: 0x06000B26 RID: 2854
			void OnEastButton(InputAction.CallbackContext context);

			// Token: 0x06000B27 RID: 2855
			void OnNorthButton(InputAction.CallbackContext context);

			// Token: 0x06000B28 RID: 2856
			void OnSouthButton(InputAction.CallbackContext context);

			// Token: 0x06000B29 RID: 2857
			void OnDPad(InputAction.CallbackContext context);

			// Token: 0x06000B2A RID: 2858
			void OnJoystick1(InputAction.CallbackContext context);

			// Token: 0x06000B2B RID: 2859
			void OnJoystick2(InputAction.CallbackContext context);

			// Token: 0x06000B2C RID: 2860
			void OnJoystickButton1(InputAction.CallbackContext context);

			// Token: 0x06000B2D RID: 2861
			void OnJoystickButton2(InputAction.CallbackContext context);

			// Token: 0x06000B2E RID: 2862
			void OnKeyboard(InputAction.CallbackContext context);

			// Token: 0x06000B2F RID: 2863
			void OnMouse(InputAction.CallbackContext context);

			// Token: 0x06000B30 RID: 2864
			void OnPointer(InputAction.CallbackContext context);
		}
	}
}

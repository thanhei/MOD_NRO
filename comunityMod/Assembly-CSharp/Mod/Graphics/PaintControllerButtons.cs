using System;
using InputMap;
using InputMap.Icons;
using Mod.ModMenu;
using Mod.R;
using UnityEngine;

namespace Mod.Graphics
{
	// Token: 0x0200015C RID: 348
	internal static class PaintControllerButtons
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x000B4D20 File Offset: 0x000B2F20
		internal static void PaintByImage(Image image, int x, int y, int anchor)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			if (InputDeviceDetector.IsXboxController())
			{
				PaintControllerButtons.GetStartingPoint(image, ref x, ref y, anchor);
				int num = 12 * mGraphics.zoomLevel;
				if (image == GameScr.imgFire0 || image == GameScr.imgFire1)
				{
					Texture2D a = XboxControllerIcons.A;
					int width = PaintControllerButtons.GetWidth(a, num);
					PaintControllerButtons.DrawTexture(x + image.texture.width, y, width, num, a, PaintControllerButtons.LEFT);
				}
				if (image == GameScr.imgFocus || image == GameScr.imgFocus2)
				{
					Texture2D b = XboxControllerIcons.B;
					int width2 = PaintControllerButtons.GetWidth(b, num);
					PaintControllerButtons.DrawTexture(x + image.texture.width, y, width2, num, b, PaintControllerButtons.LEFT);
				}
				if (image == GameScr.imgHP1 || image == GameScr.imgHP2 || image == GameScr.imgHP3 || image == GameScr.imgHP4)
				{
					Texture2D x2 = XboxControllerIcons.X;
					int width3 = PaintControllerButtons.GetWidth(x2, num);
					PaintControllerButtons.DrawTexture(x + image.texture.width, y, width3, num, x2, PaintControllerButtons.LEFT);
				}
				if (image == GameScr.imgMenu)
				{
					Texture2D lb = XboxControllerIcons.LB;
					int width4 = PaintControllerButtons.GetWidth(lb, num);
					PaintControllerButtons.DrawTexture(x, y + image.texture.height, width4, num, lb, 0);
				}
				if (image.texture == ModMenuMain.imgMenu)
				{
					Texture2D rb = XboxControllerIcons.RB;
					int width5 = PaintControllerButtons.GetWidth(rb, num);
					PaintControllerButtons.DrawTexture(x + image.texture.width - width5, y + image.texture.height, width5, num, rb, 0);
				}
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000B4E9C File Offset: 0x000B309C
		internal static void PaintSelectedSkill(GameScr gameScr, mGraphics g)
		{
			if (gameScr.mobCapcha != null)
			{
				return;
			}
			if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || gameScr.isPaintPopup() || GameCanvas.panel.isShow || global::Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
			{
				return;
			}
			if (global::Char.myCharz().statusMe == 14)
			{
				return;
			}
			if (!InputDeviceDetector.IsXboxController())
			{
				return;
			}
			int num = 12 * mGraphics.zoomLevel;
			if (GameScr.isudungCapsun4 || GameScr.isudungCapsun3)
			{
				Texture2D texture2D = XboxControllerIcons.Y;
				Image image = ((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF);
				int num2;
				int num3;
				int num4;
				if (GameScr.gamePad.isSmallGamePad)
				{
					if (GameScr.isAnalog != 1)
					{
						num2 = GameScr.xHP + 5;
						num3 = GameScr.yHP - 6 - 40 + 10;
						num4 = 0;
					}
					else
					{
						num2 = GameScr.xHP + 20 + 5;
						num3 = GameScr.yHP + 20 - 6 - 40 + 10;
						num4 = PaintControllerButtons.HCENTER | PaintControllerButtons.VCENTER;
					}
				}
				else if (GameScr.isAnalog != 1)
				{
					num2 = GameScr.xHP + 20;
					num3 = GameScr.yHP + 20 - 6 - 40;
					num4 = PaintControllerButtons.HCENTER | PaintControllerButtons.VCENTER;
				}
				else
				{
					num2 = GameScr.xHP + 20 + 5;
					num3 = GameScr.yHP + 20 - 6 - 40 + 10;
					num4 = PaintControllerButtons.HCENTER | PaintControllerButtons.VCENTER;
				}
				num2 += g.translateX;
				num3 += g.translateY;
				num2 *= mGraphics.zoomLevel;
				num3 *= mGraphics.zoomLevel;
				int num5 = PaintControllerButtons.GetWidth(texture2D, num);
				PaintControllerButtons.GetStartingPoint(image, ref num2, ref num3, num4);
				PaintControllerButtons.DrawTexture(num2 + image.texture.width, num3, num5, num, texture2D, PaintControllerButtons.LEFT);
			}
			if (GameScr.gamePad.isLargeGamePad)
			{
				Skill[] array;
				if (Main.isPC)
				{
					array = GameScr.keySkill;
				}
				else if (GameCanvas.isTouch)
				{
					array = GameScr.onScreenSkill;
				}
				else
				{
					array = GameScr.keySkill;
				}
				int num6 = int.MaxValue;
				int num7 = int.MinValue;
				int num8 = int.MaxValue;
				int num9 = int.MinValue;
				bool flag = false;
				for (int i = array.Length - 1; i >= 0; i--)
				{
					if (array[i] != null || flag)
					{
						flag = true;
						num6 = Math.Min(num6, GameScr.xS[i]);
						num7 = Math.Max(num7, GameScr.xS[i]);
						num8 = Math.Min(num8, GameScr.yS[i]);
						num9 = Math.Max(num9, GameScr.yS[i]);
					}
				}
				num6 += GameScr.xSkill + g.translateX;
				num7 += GameScr.xSkill + g.translateX + GameScr.imgSkill.getHeight();
				num8 += g.translateY + GameScr.imgSkill.getHeight() / 2;
				num9 += g.translateY + GameScr.imgSkill.getHeight() / 2;
				num6 *= mGraphics.zoomLevel;
				num7 *= mGraphics.zoomLevel;
				num8 *= mGraphics.zoomLevel;
				num9 *= mGraphics.zoomLevel;
				int num10 = (num8 + num9) / 2;
				Texture2D texture2D = XboxControllerIcons.LT;
				int num5 = PaintControllerButtons.GetWidth(texture2D, num);
				PaintControllerButtons.DrawTexture(num6 - num5, num10, num5, num, texture2D, PaintControllerButtons.VCENTER | PaintControllerButtons.HCENTER);
				texture2D = XboxControllerIcons.RT;
				num5 = PaintControllerButtons.GetWidth(texture2D, num);
				PaintControllerButtons.DrawTexture(num7 + num5, num10, num5, num, texture2D, PaintControllerButtons.VCENTER | PaintControllerButtons.HCENTER);
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000B5200 File Offset: 0x000B3400
		internal static void PaintHelp(mGraphics g)
		{
			if (mGraphics.zoomLevel == 1)
			{
				return;
			}
			if (!GameScr.gamePad.isLargeGamePad)
			{
				return;
			}
			if (InputDeviceDetector.IsXboxController() != PaintControllerButtons.lastControllerState)
			{
				PaintControllerButtons.lastTimePaintHelp = mSystem.currentTimeMillis();
			}
			PaintControllerButtons.lastControllerState = InputDeviceDetector.IsXboxController();
			if (!InputDeviceDetector.IsController())
			{
				return;
			}
			if (mSystem.currentTimeMillis() - PaintControllerButtons.lastTimePaintHelp > 5000L)
			{
				return;
			}
			int num;
			if (GameScr.isAnalog == 1)
			{
				num = GameCanvas.h - 30;
			}
			else
			{
				num = GameScr.ySkill - 25;
				Skill[] array;
				if (Main.isPC)
				{
					array = GameScr.keySkill;
				}
				else if (GameCanvas.isTouch)
				{
					array = GameScr.onScreenSkill;
				}
				else
				{
					array = GameScr.keySkill;
				}
				int num2 = GameScr.gI().nSkill;
				if (Main.isPC || !GameCanvas.isTouch)
				{
					num2 = array.Length;
				}
				bool flag = false;
				bool flag2 = false;
				for (int i = num2 - 1; i >= 0; i--)
				{
					if (array[i] != null)
					{
						flag2 = true;
					}
					if (flag2 && GameScr.yS[i] == GameScr.ySkill - 32)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					num -= GameScr.imgSkill.getHeight() + 5;
				}
			}
			int num3 = 10 * mGraphics.zoomLevel;
			g.SetColor(128U);
			g.fillRect(5, num - 48, (mResources.language == 0) ? 100 : 75, 62);
			Texture2D texture2D = XboxControllerIcons.RightStickButton;
			int num4 = PaintControllerButtons.GetWidth(texture2D, num3);
			PaintControllerButtons.DrawTexture(15 * mGraphics.zoomLevel, num * mGraphics.zoomLevel + mFont.tahoma_7b_yellow.getHeight() / 2, num4, num3, texture2D, PaintControllerButtons.HCENTER);
			mFont.tahoma_7_yellow.drawString(g, ": " + Strings.paintControllerButtonsRightStickButtonLockCamera, 10 + num4 / mGraphics.zoomLevel + 5, num, mFont.LEFT, mFont.tahoma_7_grey);
			num -= 12;
			texture2D = XboxControllerIcons.RightStick;
			num4 = PaintControllerButtons.GetWidth(texture2D, num3);
			PaintControllerButtons.DrawTexture(15 * mGraphics.zoomLevel, num * mGraphics.zoomLevel + mFont.tahoma_7b_yellow.getHeight() / 2, num4, num3, texture2D, PaintControllerButtons.HCENTER);
			mFont.tahoma_7_yellow.drawString(g, ": " + Strings.paintControllerButtonsRightStickMoveCamera, 10 + num4 / mGraphics.zoomLevel + 5, num, mFont.LEFT, mFont.tahoma_7_grey);
			num -= 12;
			texture2D = XboxControllerIcons.LeftStickButton;
			num4 = PaintControllerButtons.GetWidth(texture2D, num3);
			PaintControllerButtons.DrawTexture(15 * mGraphics.zoomLevel, num * mGraphics.zoomLevel + mFont.tahoma_7b_yellow.getHeight() / 2, num4, num3, texture2D, PaintControllerButtons.HCENTER);
			mFont.tahoma_7_yellow.drawString(g, ": " + Strings.paintControllerButtonsLeftStickButtonTeleport, 10 + num4 / mGraphics.zoomLevel + 5, num, mFont.LEFT, mFont.tahoma_7_grey);
			num -= 12;
			texture2D = XboxControllerIcons.LeftStick;
			num4 = PaintControllerButtons.GetWidth(texture2D, num3);
			PaintControllerButtons.DrawTexture(15 * mGraphics.zoomLevel, num * mGraphics.zoomLevel + mFont.tahoma_7b_yellow.getHeight() / 2, num4, num3, texture2D, PaintControllerButtons.HCENTER);
			mFont.tahoma_7_yellow.drawString(g, ": " + Strings.paintControllerButtonsLeftStickMove, 10 + num4 / mGraphics.zoomLevel + 5, num, mFont.LEFT, mFont.tahoma_7_grey);
			num -= 12;
			texture2D = XboxControllerIcons.DPad;
			num4 = PaintControllerButtons.GetWidth(texture2D, num3);
			PaintControllerButtons.DrawTexture(15 * mGraphics.zoomLevel, num * mGraphics.zoomLevel + mFont.tahoma_7b_yellow.getHeight() / 2, num4, num3, texture2D, PaintControllerButtons.HCENTER);
			mFont.tahoma_7_yellow.drawString(g, ": " + Strings.paintControllerButtonsDPadArrowKeys, 10 + num4 / mGraphics.zoomLevel + 5, num, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000B5560 File Offset: 0x000B3760
		private static void DrawTexture(int x, int y, int width, int height, Texture2D texture, int anchor)
		{
			if ((anchor & PaintControllerButtons.HCENTER) == PaintControllerButtons.HCENTER)
			{
				x -= width / 2;
			}
			if ((anchor & PaintControllerButtons.LEFT) == PaintControllerButtons.LEFT)
			{
				x -= width;
			}
			if ((anchor & PaintControllerButtons.VCENTER) == PaintControllerButtons.VCENTER)
			{
				y -= height / 2;
			}
			if ((anchor & PaintControllerButtons.TOP) == PaintControllerButtons.TOP)
			{
				y -= height;
			}
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)width, (float)height), texture);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000B55D5 File Offset: 0x000B37D5
		private static int GetWidth(Texture2D texture, int height)
		{
			return height * texture.width / texture.height;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000B55E8 File Offset: 0x000B37E8
		private static void GetStartingPoint(Image image, ref int x, ref int y, int anchor)
		{
			if ((anchor & PaintControllerButtons.HCENTER) == PaintControllerButtons.HCENTER)
			{
				x -= image.texture.width / 2;
			}
			if ((anchor & PaintControllerButtons.LEFT) == PaintControllerButtons.LEFT)
			{
				x -= image.texture.width;
			}
			if ((anchor & PaintControllerButtons.VCENTER) == PaintControllerButtons.VCENTER)
			{
				y -= image.texture.height / 2;
			}
			if ((anchor & PaintControllerButtons.TOP) == PaintControllerButtons.TOP)
			{
				y -= image.texture.height;
			}
		}

		// Token: 0x040017C3 RID: 6083
		private static readonly int HCENTER = 1;

		// Token: 0x040017C4 RID: 6084
		private static readonly int VCENTER = 2;

		// Token: 0x040017C5 RID: 6085
		private static readonly int LEFT = 8;

		// Token: 0x040017C6 RID: 6086
		private static readonly int TOP = 32;

		// Token: 0x040017C7 RID: 6087
		private static long lastTimePaintHelp;

		// Token: 0x040017C8 RID: 6088
		private static bool lastControllerState;
	}
}

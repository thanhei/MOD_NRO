using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mod.Graphics
{
	// Token: 0x02000158 RID: 344
	internal static class CustomGraphics
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x000B28A8 File Offset: 0x000B0AA8
		static CustomGraphics()
		{
			CustomGraphics.Initialize();
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000B295C File Offset: 0x000B0B5C
		internal static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
		{
			float num = pointB.x - pointA.x;
			float num2 = pointB.y - pointA.y;
			float num3 = Mathf.Sqrt(num * num + num2 * num2);
			if (num3 < 0.001f)
			{
				return;
			}
			Texture2D texture2D;
			if (antiAlias)
			{
				width *= 3f;
				texture2D = CustomGraphics.aaLineTex;
			}
			else
			{
				texture2D = CustomGraphics.lineTex;
			}
			float num4 = width * num2 / num3;
			float num5 = width * num / num3;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.m00 = num;
			identity.m01 = -num4;
			identity.m03 = pointA.x + 0.5f * num4;
			identity.m10 = num2;
			identity.m11 = num5;
			identity.m13 = pointA.y - 0.5f * num5;
			GL.PushMatrix();
			GL.MultMatrix(identity);
			GUI.color = color;
			GUI.DrawTexture(CustomGraphics.lineRect, texture2D);
			GL.PopMatrix();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000B2A3C File Offset: 0x000B0C3C
		internal static void DrawCircle(Vector2 center, int radius, Color color, float thichness, bool antiAlias, int segmentsPerQuarter)
		{
			float num = (float)radius * 0.55191505f;
			Vector2 vector = new Vector2(center.x, center.y - (float)radius);
			Vector2 vector2 = new Vector2(center.x - num, center.y - (float)radius);
			Vector2 vector3 = new Vector2(center.x + num, center.y - (float)radius);
			Vector2 vector4 = new Vector2(center.x + (float)radius, center.y);
			Vector2 vector5 = new Vector2(center.x + (float)radius, center.y - num);
			Vector2 vector6 = new Vector2(center.x + (float)radius, center.y + num);
			Vector2 vector7 = new Vector2(center.x, center.y + (float)radius);
			Vector2 vector8 = new Vector2(center.x - num, center.y + (float)radius);
			Vector2 vector9 = new Vector2(center.x + num, center.y + (float)radius);
			Vector2 vector10 = new Vector2(center.x - (float)radius, center.y);
			Vector2 vector11 = new Vector2(center.x - (float)radius, center.y - num);
			Vector2 vector12 = new Vector2(center.x - (float)radius, center.y + num);
			CustomGraphics.DrawBezierLine(vector, vector3, vector4, vector5, color, thichness, antiAlias, segmentsPerQuarter);
			CustomGraphics.DrawBezierLine(vector4, vector6, vector7, vector9, color, thichness, antiAlias, segmentsPerQuarter);
			CustomGraphics.DrawBezierLine(vector7, vector8, vector10, vector12, color, thichness, antiAlias, segmentsPerQuarter);
			CustomGraphics.DrawBezierLine(vector10, vector11, vector, vector2, color, thichness, antiAlias, segmentsPerQuarter);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000B2BB4 File Offset: 0x000B0DB4
		internal static void DrawBezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
		{
			Vector2 vector = CustomGraphics.CubeBezier(start, startTangent, end, endTangent, 0f);
			for (int i = 1; i < segments + 1; i++)
			{
				Vector2 vector2 = CustomGraphics.CubeBezier(start, startTangent, end, endTangent, (float)i / (float)segments);
				CustomGraphics.DrawLine(vector, vector2, color, width, antiAlias);
				vector = vector2;
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000B2C00 File Offset: 0x000B0E00
		private static Vector2 CubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t)
		{
			float num = 1f - t;
			return num * num * num * s + 3f * num * num * t * st + 3f * num * t * t * et + t * t * t * e;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000B2C64 File Offset: 0x000B0E64
		private static void Initialize()
		{
			if (CustomGraphics.lineTex == null)
			{
				CustomGraphics.lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				CustomGraphics.lineTex.SetPixel(0, 1, Color.white);
				CustomGraphics.lineTex.Apply();
			}
			if (CustomGraphics.aaLineTex == null)
			{
				CustomGraphics.aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
				CustomGraphics.aaLineTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
				CustomGraphics.aaLineTex.SetPixel(0, 1, Color.white);
				CustomGraphics.aaLineTex.SetPixel(0, 2, new Color(1f, 1f, 1f, 0f));
				CustomGraphics.aaLineTex.Apply();
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000B2D27 File Offset: 0x000B0F27
		internal static void DrawCircle(Color color, float x, float y, float radius, float thickness)
		{
			Color color2 = GUI.color;
			CustomGraphics.DrawCircle(new Vector2(x, y), Mathf.RoundToInt(radius), color, thickness, true, 10);
			GUI.color = color2;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000B2D4B File Offset: 0x000B0F4B
		internal static void DrawCircle(Color color, IMapObject mapObject, float radius, float thickness)
		{
			CustomGraphics.DrawCircle(color, (float)((mapObject.getX() - GameScr.cmx) * mGraphics.zoomLevel), (float)((mapObject.getY() - GameScr.cmy) * mGraphics.zoomLevel), radius * (float)mGraphics.zoomLevel, thickness * (float)mGraphics.zoomLevel);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000B2D89 File Offset: 0x000B0F89
		internal static void drawRect(mGraphics g, int x, int y, int w, int h, int thickness)
		{
			g.fillRect(x, y, w, thickness);
			g.fillRect(x, y, thickness, h);
			g.fillRect(x + w, y, thickness, h + thickness);
			g.fillRect(x, y + h, w + thickness, thickness);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000B2DC4 File Offset: 0x000B0FC4
		internal static void drawLine(Color color, float x1, float y1, float x2, float y2, int thickness)
		{
			if (x1 == x2 && y1 == y2)
			{
				return;
			}
			string text = string.Format("texture_drawline_{0}_{1}_{2}_{3}_{4}", new object[] { color.r, color.g, color.b, color.a, thickness });
			Texture2D texture2D = CustomGraphics.cachedTextures[text];
			if (texture2D == null)
			{
				texture2D = new Texture2D(thickness, thickness);
				for (int i = 0; i < thickness; i++)
				{
					for (int j = 0; j < thickness; j++)
					{
						texture2D.SetPixel(i, j, color);
					}
				}
				texture2D.Apply();
				CustomGraphics.cachedTextures.Add(text, texture2D);
			}
			Vector2 vector = new Vector2(x1, y1);
			Vector2 vector2 = new Vector2(x2, y2) - vector;
			float num = 57.29578f * Mathf.Atan(vector2.y / vector2.x);
			if (vector2.x < 0f)
			{
				num += 180f;
			}
			GUIUtility.RotateAroundPivot(num, vector);
			Graphics.DrawTexture(new Rect(vector.x, vector.y, vector2.magnitude, (float)thickness), texture2D);
			GUIUtility.RotateAroundPivot(-num, vector);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000B2F08 File Offset: 0x000B1108
		internal static void PaintItemOptions(mGraphics g, Panel instance, Item item, int y)
		{
			int num = instance.X + Panel.WIDTH_PANEL - 2;
			if (instance == GameCanvas.panel2)
			{
				num -= 2;
			}
			if (Utils.HasActivateOption(item))
			{
				mFont.tahoma_7b_red.drawString(g, "$", num, y, mFont.RIGHT);
				num -= mFont.tahoma_7b_red.getWidth("$") + 2;
			}
			uint num2;
			uint num3;
			if (Utils.HasStarOption(item, out num2, out num3))
			{
				CustomGraphics.PaintStarOption(g, num, y, num2, num3);
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000B2F7C File Offset: 0x000B117C
		private static void PaintStarOption(mGraphics g, int x, int y, uint star, uint starE)
		{
			if (star > 0U)
			{
				mFont.tahoma_7b_red.drawString(g, star.ToString(), x - Image.getImageWidth(Panel.imgStar) - mFont.tahoma_7b_red.getWidth(star.ToString()) - 1, y, mFont.LEFT);
				g.drawImage(Panel.imgStar, x - Image.getImageWidth(Panel.imgStar) - 1, y + 1);
			}
			if (starE > 0U)
			{
				if (star == 0U)
				{
					mFont.tahoma_7b_red.drawString(g, starE.ToString(), x - Image.getImageWidth(Panel.imgMaxStar) - mFont.tahoma_7b_red.getWidth(starE.ToString()) - 1, y, mFont.LEFT);
					g.drawImage(Panel.imgMaxStar, x - Image.getImageWidth(Panel.imgMaxStar) - 1, y + 1);
					return;
				}
				mFont.tahoma_7b_red.drawString(g, starE.ToString(), x - mFont.tahoma_7b_red.getWidth(starE.ToString() + star.ToString()) - Image.getImageWidth(Panel.imgMaxStar) * 2 - 2, y, mFont.LEFT);
				g.drawImage(Panel.imgMaxStar, x - mFont.tahoma_7b_red.getWidth(starE.ToString()) - Image.getImageWidth(Panel.imgMaxStar) * 2 - 3, y + 1);
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000B30B8 File Offset: 0x000B12B8
		private static int upgradeEffectX(int tick, int w)
		{
			int num = tick % (4 * w);
			if (0 <= num && num < w)
			{
				return num % w;
			}
			if (w <= num && num < 2 * w)
			{
				return w;
			}
			if (2 * w <= num && num < 3 * w)
			{
				return w - num % w;
			}
			return 0;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000B30F8 File Offset: 0x000B12F8
		private static int upgradeEffectY(int tick, int h)
		{
			int num = tick % (4 * h);
			if (0 <= num && num < h)
			{
				return 0;
			}
			if (h <= num && num < 2 * h)
			{
				return num % h;
			}
			if (2 * h <= num && num < 3 * h)
			{
				return h;
			}
			return h - num % h;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000B3138 File Offset: 0x000B1338
		internal static void PaintItemEffectInPanel(mGraphics g, int x, int y, int w, int h, Item item)
		{
			if (item.itemOption == null)
			{
				return;
			}
			ItemOption bestItemOption = item.GetBestItemOption();
			if (bestItemOption == null)
			{
				return;
			}
			int id = bestItemOption.optionTemplate.id;
			if ((id > 36 || id < 34) && id != 72 && (id < 127 || id > 135) && id != 107)
			{
				return;
			}
			int num = bestItemOption.param;
			if (num > 7 || (id >= 127 && id <= 135))
			{
				num = 7;
			}
			if (id == 107)
			{
				if (num > 1)
				{
					num = (int)Math.Ceiling((double)num / 2.0);
				}
				else if (num == 1)
				{
					return;
				}
			}
			if (num <= 0)
			{
				return;
			}
			if (num >= 4 && num <= 7 && (id > 36 || id < 34))
			{
				g.setColor(CustomGraphics.array2[num]);
				g.fillRect(x - w / 2, y - h / 2, w, h);
			}
			for (int i = 0; i < CustomGraphics.size.Length; i++)
			{
				int num2 = x - w / 2 + 1 + CustomGraphics.upgradeEffectX(GameCanvas.gameTick - i * 4, w - 2);
				int num3 = y - h / 2 + 1 + CustomGraphics.upgradeEffectY(GameCanvas.gameTick - i * 4, h - 2);
				g.setColor(CustomGraphics.colorBorder[0, i]);
				g.fillRect(num2 - CustomGraphics.size[i] / 2, num3 - CustomGraphics.size[i] / 2, CustomGraphics.size[i], CustomGraphics.size[i]);
				if (num > 1)
				{
					if (num > 2)
					{
						g.setColor(CustomGraphics.colorBorder[1, i]);
					}
					num2 = x - w / 2 + 1 + CustomGraphics.upgradeEffectX(GameCanvas.gameTick + 68 - i * 4, w - 2);
					num3 = y - h / 2 + 1 + CustomGraphics.upgradeEffectY(GameCanvas.gameTick + 48 - i * 4, h - 2);
					g.fillRect(num2 - CustomGraphics.size[i] / 2, num3 - CustomGraphics.size[i] / 2, CustomGraphics.size[i], CustomGraphics.size[i]);
					if (num > 3)
					{
						if (num > 4)
						{
							g.setColor(CustomGraphics.colorBorder[2, i]);
						}
						num2 = x - w / 2 + 1 + CustomGraphics.upgradeEffectX(GameCanvas.gameTick + 68 - i * 4, w - 2);
						num3 = y - h / 2 + 1 + CustomGraphics.upgradeEffectY(GameCanvas.gameTick - i * 4, h - 2);
						g.fillRect(num2 - CustomGraphics.size[i] / 2, num3 - CustomGraphics.size[i] / 2, CustomGraphics.size[i], CustomGraphics.size[i]);
						if (num > 5)
						{
							if (num > 6)
							{
								g.setColor(CustomGraphics.colorBorder[3, i]);
							}
							num2 = x - w / 2 + 1 + CustomGraphics.upgradeEffectX(GameCanvas.gameTick - i * 4, w - 2);
							num3 = y - h / 2 + 1 + CustomGraphics.upgradeEffectY(GameCanvas.gameTick + 48 - i * 4, h - 2);
							g.fillRect(num2 - CustomGraphics.size[i] / 2, num3 - CustomGraphics.size[i] / 2, CustomGraphics.size[i], CustomGraphics.size[i]);
						}
					}
				}
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000B3418 File Offset: 0x000B1618
		internal static void DrawAPartOfImage(Image image, float x, float y, float w, float h, int imageX, int imageY, float degAngle, bool scale = true)
		{
			if (scale)
			{
				x *= (float)mGraphics.zoomLevel;
				y *= (float)mGraphics.zoomLevel;
				w *= (float)mGraphics.zoomLevel;
				h *= (float)mGraphics.zoomLevel;
				imageX *= mGraphics.zoomLevel;
				imageY *= mGraphics.zoomLevel;
			}
			int w2 = image.w;
			int h2 = image.h;
			Vector2 vector = new Vector2((float)imageX + w / 2f + x, (float)imageY + h / 2f + y);
			GUIUtility.RotateAroundPivot(degAngle, vector);
			GUI.BeginGroup(new Rect(x - (float)imageX, y - (float)imageY, w, h));
			GUI.DrawTexture(new Rect(0f, 0f, (float)w2, (float)h2), image.texture);
			GUI.EndGroup();
			GUIUtility.RotateAroundPivot(-degAngle, vector);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000B34E4 File Offset: 0x000B16E4
		internal static void DrawImage(Image image, float x, float y, float degAngle = 0f, bool scale = true)
		{
			if (scale)
			{
				x *= (float)mGraphics.zoomLevel;
				y *= (float)mGraphics.zoomLevel;
			}
			int width = image.texture.width;
			int height = image.texture.height;
			Vector2 vector = new Vector2(x + (float)(width / 2), y + (float)(width / 2));
			GUIUtility.RotateAroundPivot(degAngle, vector);
			GUI.BeginGroup(new Rect(x, y, (float)width, (float)height));
			GUI.DrawTexture(new Rect(0f, 0f, (float)width, (float)height), image.texture);
			GUI.EndGroup();
			GUIUtility.RotateAroundPivot(-degAngle, vector);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000B3578 File Offset: 0x000B1778
		internal static void fillRect(int x, int y, int w, int h, Color color, bool scale = true)
		{
			if (scale)
			{
				x *= mGraphics.zoomLevel;
				y *= mGraphics.zoomLevel;
			}
			if (w < 0 || h < 0)
			{
				return;
			}
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, color);
			texture2D.Apply();
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)w, (float)h), texture2D);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000B35D0 File Offset: 0x000B17D0
		internal static Texture2D CropToCircle(Texture2D textureToCrop, int borderThickness = 0, bool isApply = true)
		{
			if (textureToCrop.width != textureToCrop.height)
			{
				throw new ArgumentException("textureToCrop isn't a square texture!");
			}
			int num = textureToCrop.width / 2;
			int num2 = textureToCrop.height / 2;
			for (int i = 0; i < textureToCrop.width * textureToCrop.height; i++)
			{
				int num3 = i % textureToCrop.width;
				int num4 = i / textureToCrop.height;
				double distance = CustomGraphics.getDistance(num, num2, num3, num4);
				if (distance >= (double)textureToCrop.width / 2.0 - 1.0)
				{
					textureToCrop.SetPixel(num3, num4, Color.clear);
				}
				else if (distance >= (double)textureToCrop.width / 2.0 - 1.0 - (double)borderThickness)
				{
					textureToCrop.SetPixel(num3, num4, new Color(0.5f, 0.5f, 0.5f));
				}
			}
			if (isApply)
			{
				textureToCrop.Apply();
			}
			return textureToCrop;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000B36BD File Offset: 0x000B18BD
		private static double getDistance(int x1, int y1, int x2, int y2)
		{
			return Math.Sqrt((double)((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000B36D4 File Offset: 0x000B18D4
		internal static Texture2D RoundCorner(Texture2D texture, int radius)
		{
			int num = texture.width - radius;
			int num2 = texture.height - radius;
			Color color = new Color(0f, 0f, 0f, 0f);
			for (int i = 0; i < radius; i++)
			{
				for (int j = 0; j < radius; j++)
				{
					if (CustomGraphics.getDistance(radius, radius, i, j) > (double)radius)
					{
						texture.SetPixel(i, j, color);
					}
				}
				for (int k = num2; k < texture.height; k++)
				{
					if (CustomGraphics.getDistance(radius, num2, i, k) > (double)radius)
					{
						texture.SetPixel(i, k, color);
					}
				}
			}
			for (int l = num; l < texture.width; l++)
			{
				for (int m = 0; m < radius; m++)
				{
					if (CustomGraphics.getDistance(num, radius, l, m) > (double)radius)
					{
						texture.SetPixel(l, m, color);
					}
				}
				for (int n = num2; n < texture.height; n++)
				{
					if (CustomGraphics.getDistance(num, num2, l, n) > (double)radius)
					{
						texture.SetPixel(l, n, color);
					}
				}
			}
			return texture;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000B37F0 File Offset: 0x000B19F0
		internal static void drawCooldownRect(float x, float y, float w, float h, float value, Color color)
		{
			x *= (float)mGraphics.zoomLevel;
			y *= (float)mGraphics.zoomLevel;
			w *= (float)mGraphics.zoomLevel;
			h *= (float)mGraphics.zoomLevel;
			Matrix4x4 matrix = GUI.matrix;
			UIImage.image.rectTransform.sizeDelta = new Vector2(w, h);
			UIImage.image.fillAmount = value;
			UIImage.image.color = color;
			UIImage.image.material.SetPass(0);
			UIImage.PopulateMesh();
			Graphics.DrawMeshNow(UIImage.mesh, new Vector3(x, y, -1f), Quaternion.identity, 0);
			GUI.matrix = matrix;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000B3890 File Offset: 0x000B1A90
		internal static Texture2D FlipTextureVertically(Texture2D original)
		{
			Color[] pixels = original.GetPixels();
			Color32[] array = new Color32[pixels.Length];
			int width = original.width;
			int height = original.height;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i + j * width] = pixels[i + (height - j - 1) * width];
				}
			}
			Texture2D texture2D = new Texture2D(width, height);
			texture2D.SetPixels32(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000B3914 File Offset: 0x000B1B14
		internal static Texture2D FlipTextureHorizontally(Texture2D original)
		{
			Color[] pixels = original.GetPixels();
			Color32[] array = new Color32[pixels.Length];
			int width = original.width;
			int height = original.height;
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i + j * width] = pixels[width - i - 1 + j * width];
				}
			}
			Texture2D texture2D = new Texture2D(width, height);
			texture2D.SetPixels32(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000B3998 File Offset: 0x000B1B98
		internal static Texture2D Resize(Texture2D texture2D, int width, int height)
		{
			RenderTexture renderTexture = new RenderTexture(width, height, 24);
			RenderTexture.active = renderTexture;
			Graphics.Blit(texture2D, renderTexture);
			Texture2D texture2D2 = new Texture2D(width, height);
			texture2D2.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			texture2D2.Apply();
			return texture2D2;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000B39E4 File Offset: 0x000B1BE4
		internal static void DrawOutline(Rect r, string t, GUIStyle style, int strength, Color outlineColor)
		{
			Color textColor = style.normal.textColor;
			GUIStyleState normal = style.normal;
			style.hover.textColor = outlineColor;
			normal.textColor = outlineColor;
			for (int i = -strength; i <= strength; i++)
			{
				GUI.Label(new Rect(r.x - (float)strength, r.y + (float)i, r.width, r.height), t, style);
				GUI.Label(new Rect(r.x + (float)strength, r.y + (float)i, r.width, r.height), t, style);
			}
			for (int i = -strength + 1; i <= strength - 1; i++)
			{
				GUI.Label(new Rect(r.x + (float)i, r.y - (float)strength, r.width, r.height), t, style);
				GUI.Label(new Rect(r.x + (float)i, r.y + (float)strength, r.width, r.height), t, style);
			}
			style.normal.textColor = (style.hover.textColor = textColor);
			GUI.Label(r, t, style);
		}

		// Token: 0x040017AE RID: 6062
		private static Texture2D aaLineTex = null;

		// Token: 0x040017AF RID: 6063
		private static Texture2D lineTex = null;

		// Token: 0x040017B0 RID: 6064
		private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040017B1 RID: 6065
		private static int[] array2 = new int[] { 0, 0, 0, 0, 600841, 3346944, 3932211, 6684682 };

		// Token: 0x040017B2 RID: 6066
		private static int[] size = new int[] { 2, 1, 1, 1, 1, 1 };

		// Token: 0x040017B3 RID: 6067
		private static int[,] colorBorder = new int[,]
		{
			{ 18687, 16869, 15052, 13235, 11161, 9344 },
			{ 45824, 39168, 32768, 26112, 19712, 13056 },
			{ 16744192, 15037184, 13395456, 11753728, 10046464, 8404992 },
			{ 13500671, 12058853, 10682572, 9371827, 7995545, 6684800 },
			{ 16711705, 15007767, 13369364, 11730962, 10027023, 8388621 }
		};

		// Token: 0x040017B4 RID: 6068
		private static float avgR = 0f;

		// Token: 0x040017B5 RID: 6069
		private static float avgG = 0f;

		// Token: 0x040017B6 RID: 6070
		private static float avgB = 0f;

		// Token: 0x040017B7 RID: 6071
		private static float blurPixelCount = 0f;

		// Token: 0x040017B8 RID: 6072
		private static Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();
	}
}

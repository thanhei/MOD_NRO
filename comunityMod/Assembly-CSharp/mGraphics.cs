using System;
using System.Collections;
using Assets.src.e;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class mGraphics
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000151BF File Offset: 0x000133BF
	// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00004887 File Offset: 0x00002A87
	public static int addYWhenOpenKeyBoard
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0008ED04 File Offset: 0x0008CF04
	internal void cache(string key, Texture value)
	{
		if (mGraphics.cachedTextures.Count > 400)
		{
			mGraphics.cachedTextures.Clear();
		}
		if (value.width * value.height < GameCanvas.w * GameCanvas.h)
		{
			mGraphics.cachedTextures.Add(key, value);
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0008ED54 File Offset: 0x0008CF54
	public void translate(int tx, int ty)
	{
		tx *= mGraphics.zoomLevel;
		ty *= mGraphics.zoomLevel;
		this.translateX += tx;
		this.translateY += ty;
		this.isTranslate = true;
		if (this.translateX == 0 && this.translateY == 0)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0008EDB0 File Offset: 0x0008CFB0
	public void translate(float x, float y)
	{
		this.translateXf += x;
		this.translateYf += y;
		this.isTranslate = true;
		if (this.translateXf == 0f && this.translateYf == 0f)
		{
			this.isTranslate = false;
		}
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0008EE01 File Offset: 0x0008D001
	public int getTranslateX()
	{
		return this.translateX / mGraphics.zoomLevel;
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0008EE0F File Offset: 0x0008D00F
	public int getTranslateY()
	{
		return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0008EE24 File Offset: 0x0008D024
	public void setClip(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		this.clipTX = this.translateX;
		this.clipTY = this.translateY;
		this.clipX = x;
		this.clipY = y;
		this.clipW = w;
		this.clipH = h;
		this.isClip = true;
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0008EE92 File Offset: 0x0008D092
	public int getClipX()
	{
		return GameScr.cmx;
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0008EE99 File Offset: 0x0008D099
	public int getClipY()
	{
		return GameScr.cmy;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0008EEA0 File Offset: 0x0008D0A0
	public int getClipWidth()
	{
		return GameScr.gW;
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0008EEA7 File Offset: 0x0008D0A7
	public int getClipHeight()
	{
		return GameScr.gH;
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0008EEAE File Offset: 0x0008D0AE
	public void fillRect(int x, int y, int w, int h, int color, int alpha)
	{
		this.setColor(color, 0.5f);
		this.fillRect(x, y, w, h);
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0008EEC8 File Offset: 0x0008D0C8
	public void drawLine(int x1, int y1, int x2, int y2)
	{
		x1 *= mGraphics.zoomLevel;
		y1 *= mGraphics.zoomLevel;
		x2 *= mGraphics.zoomLevel;
		y2 *= mGraphics.zoomLevel;
		if (y1 == y2)
		{
			if (x1 > x2)
			{
				int num = x2;
				x2 = x1;
				x1 = num;
			}
			this.fillRect(x1, y1, x2 - x1, 1);
			return;
		}
		if (x1 == x2)
		{
			if (y1 > y2)
			{
				int num2 = y2;
				y2 = y1;
				y1 = num2;
			}
			this.fillRect(x1, y1, 1, y2 - y1);
			return;
		}
		if (this.isTranslate)
		{
			x1 += this.translateX;
			y1 += this.translateY;
			x2 += this.translateX;
			y2 += this.translateY;
		}
		string text = "dl" + this.r.ToString() + this.g.ToString() + this.b.ToString();
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[text];
		if (texture2D == null)
		{
			texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, new Color(this.r, this.g, this.b));
			texture2D.Apply();
			this.cache(text, texture2D);
		}
		Vector2 vector = new Vector2((float)x1, (float)y1);
		Vector2 vector2 = new Vector2((float)x2, (float)y2) - vector;
		float num3 = 57.29578f * Mathf.Atan(vector2.y / vector2.x);
		if (vector2.x < 0f)
		{
			num3 += 180f;
		}
		int num4 = (int)Mathf.Ceil(0f);
		GUIUtility.RotateAroundPivot(num3, vector);
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (this.isClip)
		{
			num5 = this.clipX;
			num6 = this.clipY;
			num7 = this.clipW;
			num8 = this.clipH;
			if (this.isTranslate)
			{
				num5 += this.clipTX;
				num6 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
		}
		Graphics.DrawTexture(new Rect(vector.x - (float)num5, vector.y - (float)num4 - (float)num6, vector2.magnitude, 1f), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
		GUIUtility.RotateAroundPivot(0f - num3, vector);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0008F0FC File Offset: 0x0008D2FC
	public Color setColorMiniMap(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		float num3 = (float)((rgb >> 16) & 255);
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color(num3 / 256f, num5, num4);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0008F148 File Offset: 0x0008D348
	public float[] getRGB(Color cl)
	{
		float num = 256f * cl.r;
		float num2 = 256f * cl.g;
		float num3 = 256f * cl.b;
		return new float[] { num, num2, num3 };
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0008F190 File Offset: 0x0008D390
	public void drawRect(int x, int y, int w, int h)
	{
		int num = 1;
		this.fillRect(x, y, w, num);
		this.fillRect(x, y, num, h);
		this.fillRect(x + w, y, num, h + 1);
		this.fillRect(x, y + h, w + 1, num);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0008F1D4 File Offset: 0x0008D3D4
	public void fillRect(int x, int y, int w, int h)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		if (w < 0 || h < 0)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 1;
		int num2 = 1;
		string text = string.Concat(new string[]
		{
			"fr",
			num.ToString(),
			num2.ToString(),
			this.r.ToString(),
			this.g.ToString(),
			this.b.ToString(),
			this.a.ToString()
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[text];
		if (texture2D == null)
		{
			texture2D = new Texture2D(num, num2);
			texture2D.SetPixel(0, 0, new Color(this.r, this.g, this.b, this.a));
			texture2D.Apply();
			this.cache(text, texture2D);
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (this.isClip)
		{
			num3 = this.clipX;
			num4 = this.clipY;
			num5 = this.clipW;
			num6 = this.clipH;
			if (this.isTranslate)
			{
				num3 += this.clipTX;
				num4 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
		}
		GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0008F37C File Offset: 0x0008D57C
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		int num3 = (rgb >> 16) & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0008F3DB File Offset: 0x0008D5DB
	public void setColor(Color color)
	{
		this.b = color.b;
		this.g = color.g;
		this.r = color.r;
		this.a = color.a;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0008F410 File Offset: 0x0008D610
	public void setBgColor(int rgb)
	{
		if (rgb != this.currentBGColor)
		{
			this.currentBGColor = rgb;
			int num = rgb & 255;
			int num2 = (rgb >> 8) & 255;
			int num3 = (rgb >> 16) & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			Main.main.GetComponent<Camera>().backgroundColor = new Color(this.r, this.g, this.b);
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0008F49C File Offset: 0x0008D69C
	public void drawString(string s, int x, int y, GUIStyle style)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0008F564 File Offset: 0x0008D764
	public void setColor(int rgb, float alpha)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		int num3 = (rgb >> 16) & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = alpha;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0008F5C0 File Offset: 0x0008D7C0
	public void drawString(string s, int x, int y, GUIStyle style, int w)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.Label(new Rect((float)(x - num), (float)(y - num2 - 4), (float)w, 100f), s, style);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0008F688 File Offset: 0x0008D888
	internal void UpdatePos(int anchor)
	{
		Vector2 vector = new Vector2(0f, 0f);
		if (anchor != 3)
		{
			if (anchor != 6)
			{
				if (anchor != 17)
				{
					if (anchor != 20)
					{
						if (anchor != 33)
						{
							if (anchor != 36)
							{
								if (anchor != 10)
								{
									if (anchor != 24)
									{
										if (anchor == 40)
										{
											vector = new Vector2((float)Screen.width, (float)Screen.height);
										}
									}
									else
									{
										vector = new Vector2((float)Screen.width, 0f);
									}
								}
								else
								{
									vector = new Vector2((float)Screen.width, (float)(Screen.height / 2));
								}
							}
							else
							{
								vector = new Vector2(0f, (float)Screen.height);
							}
						}
						else
						{
							vector = new Vector2((float)(Screen.width / 2), (float)Screen.height);
						}
					}
					else
					{
						vector = new Vector2(0f, 0f);
					}
				}
				else
				{
					vector = new Vector2((float)(Screen.width / 2), 0f);
				}
			}
			else
			{
				vector = new Vector2(0f, (float)(Screen.height / 2));
			}
		}
		else
		{
			vector = new Vector2(this.size.x / 2f, this.size.y / 2f);
		}
		this.pos = vector + this.relativePosition;
		this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
		this.pivot = new Vector2(this.rect.xMin + this.rect.width * 0.5f, this.rect.yMin + this.rect.height * 0.5f);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0008F870 File Offset: 0x0008DA70
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
	{
		if (arg0 != null)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			x0 *= mGraphics.zoomLevel;
			y0 *= mGraphics.zoomLevel;
			w0 *= mGraphics.zoomLevel;
			h0 *= mGraphics.zoomLevel;
			this._drawRegion(arg0, (float)x0, (float)y0, w0, h0, arg5, x, y, arg8);
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0008F8D4 File Offset: 0x0008DAD4
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
	{
		if (arg0 != null)
		{
			x *= (float)mGraphics.zoomLevel;
			y *= (float)mGraphics.zoomLevel;
			x0 *= mGraphics.zoomLevel;
			y0 *= mGraphics.zoomLevel;
			w0 *= mGraphics.zoomLevel;
			h0 *= mGraphics.zoomLevel;
			this.__drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0008F938 File Offset: 0x0008DB38
	public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
	{
		this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0008F95C File Offset: 0x0008DB5C
	public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += (float)this.translateX;
			y += (float)this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		int num9 = 1;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += num5;
		y += num6;
		int num10 = 0;
		int num11 = 0;
		if (this.isClip)
		{
			num10 = this.clipX;
			int num12 = this.clipY;
			num11 = this.clipW;
			int num13 = this.clipH;
			if (this.isTranslate)
			{
				num10 += this.clipTX;
				num12 += this.clipTY;
			}
			Rect rect = new Rect(x, y, (float)w, (float)h);
			Rect rect2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
			Rect rect3 = this.intersectRect(rect, rect2);
			if (rect3.width <= 0f || rect3.height <= 0f)
			{
				return;
			}
			num = rect3.width;
			num2 = rect3.height;
			num3 = rect3.x - rect.x;
			num4 = rect3.y - rect.y;
		}
		float num14 = 0f;
		float num15 = 0f;
		if (transform == 2)
		{
			num14 += num;
			num7 = -1f;
			if (this.isClip)
			{
				if ((float)num10 > x)
				{
					num8 = 0f - num3;
				}
				else if ((float)(num10 + num11) < x + (float)w)
				{
					num8 = 0f - ((float)(num10 + num11) - x - (float)w);
				}
			}
		}
		else if (transform == 1)
		{
			num9 = -1;
			num15 += num2;
		}
		else if (transform == 3)
		{
			num9 = -1;
			num15 += num2;
			num7 = -1f;
			num14 += num;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			this.matrixBackup = GUI.matrix;
			this.size = new Vector2((float)w, (float)h);
			this.relativePosition = new Vector2(x, y);
			this.UpdatePos(3);
			if (transform == 6)
			{
				this.UpdatePos(3);
			}
			else if (transform == 5)
			{
				this.size = new Vector2((float)w, (float)h);
				this.UpdatePos(3);
			}
			if (transform == 5)
			{
				GUIUtility.RotateAroundPivot(90f, this.pivot);
			}
			else if (transform == 6)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
			}
			else if (transform == 4)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if ((float)num10 > x)
					{
						num8 = 0f - num3;
					}
					else if ((float)(num10 + num11) < x + (float)w)
					{
						num8 = 0f - ((float)(num10 + num11) - x - (float)w);
					}
				}
			}
			else if (transform == 7)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num9 = -1;
				num15 += num2;
			}
		}
		Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect(((float)x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - ((float)y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = this.matrixBackup;
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0008FD5C File Offset: 0x0008DF5C
	public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		if (image == null)
		{
			return;
		}
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		float num = (float)w;
		float num2 = (float)h;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 1f;
		float num8 = 0f;
		int num9 = 1;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num5 -= num / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num6 -= num2 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num5 -= num;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num6 -= num2;
		}
		x += (int)num5;
		y += (int)num6;
		int num10 = 0;
		int num11 = 0;
		if (this.isClip)
		{
			num10 = this.clipX;
			int num12 = this.clipY;
			num11 = this.clipW;
			int num13 = this.clipH;
			if (this.isTranslate)
			{
				num10 += this.clipTX;
				num12 += this.clipTY;
			}
			Rect rect = new Rect((float)x, (float)y, (float)w, (float)h);
			Rect rect2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
			Rect rect3 = this.intersectRect(rect, rect2);
			if (rect3.width <= 0f || rect3.height <= 0f)
			{
				return;
			}
			num = rect3.width;
			num2 = rect3.height;
			num3 = rect3.x - rect.x;
			num4 = rect3.y - rect.y;
		}
		float num14 = 0f;
		float num15 = 0f;
		if (transform == 2)
		{
			num14 += num;
			num7 = -1f;
			if (this.isClip)
			{
				if (num10 > x)
				{
					num8 = 0f - num3;
				}
				else if (num10 + num11 < x + w)
				{
					num8 = (float)(-(float)(num10 + num11 - x - w));
				}
			}
		}
		else if (transform == 1)
		{
			num9 = -1;
			num15 += num2;
		}
		else if (transform == 3)
		{
			num9 = -1;
			num15 += num2;
			num7 = -1f;
			num14 += num;
		}
		int num16 = 0;
		int num17 = 0;
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			this.matrixBackup = GUI.matrix;
			this.size = new Vector2((float)w, (float)h);
			this.relativePosition = new Vector2((float)x, (float)y);
			this.UpdatePos(3);
			if (transform == 6)
			{
				this.UpdatePos(3);
			}
			else if (transform == 5)
			{
				this.size = new Vector2((float)w, (float)h);
				this.UpdatePos(3);
			}
			if (transform == 5)
			{
				GUIUtility.RotateAroundPivot(90f, this.pivot);
			}
			else if (transform == 6)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
			}
			else if (transform == 4)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if (num10 > x)
					{
						num8 = 0f - num3;
					}
					else if (num10 + num11 < x + w)
					{
						num8 = (float)(-(float)(num10 + num11 - x - w));
					}
				}
			}
			else if (transform == 7)
			{
				GUIUtility.RotateAroundPivot(270f, this.pivot);
				num9 = -1;
				num15 += num2;
			}
		}
		Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
		if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
		{
			GUI.matrix = this.matrixBackup;
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00090150 File Offset: 0x0008E350
	public void drawRegionGui(Image image, float x0, float y0, int w, int h, int transform, float x, float y, int anchor)
	{
		GUI.color = this.setColorMiniMap(807956);
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		x0 *= (float)mGraphics.zoomLevel;
		y0 *= (float)mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x000901AC File Offset: 0x0008E3AC
	public void drawRegion2(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
	{
		GUI.color = image.colorBlend;
		if (this.isTranslate)
		{
			x += this.translateX;
			y += this.translateY;
		}
		string text = string.Concat(new string[]
		{
			"dg",
			x0.ToString(),
			y0.ToString(),
			w.ToString(),
			h.ToString(),
			transform.ToString(),
			image.GetHashCode().ToString()
		});
		Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[text];
		if (texture2D == null)
		{
			texture2D = Image.createImage(image, (int)x0, (int)y0, w, h, transform).texture;
			this.cache(text, texture2D);
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)w;
		float num6 = (float)h;
		float num7 = 0f;
		float num8 = 0f;
		if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
		{
			num7 -= num5 / 2f;
		}
		if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
		{
			num8 -= num6 / 2f;
		}
		if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
		{
			num7 -= num5;
		}
		if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
		{
			num8 -= num6;
		}
		x += (int)num7;
		y += (int)num8;
		if (this.isClip)
		{
			num = this.clipX;
			num2 = this.clipY;
			num3 = this.clipW;
			num4 = this.clipH;
			if (this.isTranslate)
			{
				num += this.clipTX;
				num2 += this.clipTY;
			}
		}
		if (this.isClip)
		{
			GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
		}
		GUI.DrawTexture(new Rect((float)(x - num), (float)(y - num2), (float)w, (float)h), texture2D);
		if (this.isClip)
		{
			GUI.EndGroup();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000903AC File Offset: 0x0008E5AC
	public void drawImagaByDrawTexture(Image image, float x, float y)
	{
		x *= (float)mGraphics.zoomLevel;
		y *= (float)mGraphics.zoomLevel;
		GUI.DrawTexture(new Rect(x + (float)this.translateX, y + (float)this.translateY, (float)image.getRealImageWidth(), (float)image.getRealImageHeight()), image.texture);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00090400 File Offset: 0x0008E600
	public void drawImage(Image image, int x, int y, int anchor)
	{
		if (image != null)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0009042C File Offset: 0x0008E62C
	public void drawImageFog(Image image, int x, int y, int anchor)
	{
		if (image != null)
		{
			this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00090460 File Offset: 0x0008E660
	public void drawImage(Image image, int x, int y)
	{
		if (image != null)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00090494 File Offset: 0x0008E694
	public void drawImage(Image image, float x, float y, int anchor)
	{
		if (image != null)
		{
			this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x000904BE File Offset: 0x0008E6BE
	public void drawRoundRect(int x, int y, int w, int h, int arcWidth, int arcHeight)
	{
		this.drawRect(x, y, w, h);
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000904CB File Offset: 0x0008E6CB
	public void fillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
	{
		this.fillRect(x, y, width, height);
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x000904D8 File Offset: 0x0008E6D8
	public void reset()
	{
		this.isClip = false;
		this.isTranslate = false;
		this.translateX = 0;
		this.translateY = 0;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x000904F8 File Offset: 0x0008E6F8
	public Rect intersectRect(Rect r1, Rect r2)
	{
		float num = r1.x;
		float num2 = r1.y;
		float x = r2.x;
		float y = r2.y;
		float num3 = num + r1.width;
		float num4 = num2 + r1.height;
		float num5 = x + r2.width;
		float num6 = y + r2.height;
		if (num < x)
		{
			num = x;
		}
		if (num2 < y)
		{
			num2 = y;
		}
		if (num3 > num5)
		{
			num3 = num5;
		}
		if (num4 > num6)
		{
			num4 = num6;
		}
		num3 -= num;
		num4 -= num2;
		if (num3 < -30000f)
		{
			num3 = -30000f;
		}
		if (num4 < -30000f)
		{
			num4 = -30000f;
		}
		return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x000905AC File Offset: 0x0008E7AC
	public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
	{
		GUI.color = Color.red;
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		w *= mGraphics.zoomLevel;
		h *= mGraphics.zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)(x + this.translateX), (float)(y + this.translateY), (float)((tranform != 0) ? (-(float)w) : w), (float)h), image.texture);
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0009061D File Offset: 0x0008E81D
	public void drawImageSimple(Image image, int x, int y)
	{
		x *= mGraphics.zoomLevel;
		y *= mGraphics.zoomLevel;
		if (image != null)
		{
			Graphics.DrawTexture(new Rect((float)x, (float)y, (float)image.w, (float)image.h), image.texture);
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0004B171 File Offset: 0x00049371
	public static int getImageWidth(Image image)
	{
		return image.getWidth();
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0004B179 File Offset: 0x00049379
	public static int getImageHeight(Image image)
	{
		return image.getHeight();
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00090656 File Offset: 0x0008E856
	public static bool isNotTranColor(Color color)
	{
		return !(color == Color.clear) && !(color == mGraphics.transParentColor);
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00090678 File Offset: 0x0008E878
	public static Image blend(Image img0, float level, int rgb)
	{
		int num = rgb & 255;
		float num2 = (float)((rgb >> 8) & 255);
		int num3 = (rgb >> 16) & 255;
		float num4 = (float)num / 256f;
		float num5 = num2 / 256f;
		Color color = new Color((float)num3 / 256f, num5, num4);
		Color[] pixels = img0.texture.GetPixels();
		float num6 = color.r;
		float num7 = color.g;
		float num8 = color.b;
		for (int i = 0; i < pixels.Length; i++)
		{
			Color color2 = pixels[i];
			if (mGraphics.isNotTranColor(color2))
			{
				float num9 = (num6 - color2.r) * level + color2.r;
				float num10 = (num7 - color2.g) * level + color2.g;
				float num11 = (num8 - color2.b) * level + color2.b;
				if (num9 > 255f)
				{
					num9 = 255f;
				}
				if (num9 < 0f)
				{
					num9 = 0f;
				}
				if (num10 > 255f)
				{
					num10 = 255f;
				}
				if (num10 < 0f)
				{
					num10 = 0f;
				}
				if (num11 < 0f)
				{
					num11 = 0f;
				}
				if (num11 > 255f)
				{
					num11 = 255f;
				}
				pixels[i].r = num9;
				pixels[i].g = num10;
				pixels[i].b = num11;
			}
		}
		Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
		image.texture.SetPixels(pixels);
		Image.setTextureQuality(image.texture);
		image.texture.Apply();
		Cout.LogError2("BLEND ----------------------------------------------------");
		return image;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00090828 File Offset: 0x0008EA28
	public static Color setColorObj(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		float num3 = (float)((rgb >> 16) & 255);
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color(num3 / 256f, num5, num4);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00090871 File Offset: 0x0008EA71
	public void fillTrans(Image imgTrans, int x, int y, int w, int h)
	{
		this.setColor(0, 0.5f);
		this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x000908A4 File Offset: 0x0008EAA4
	public static int blendColor(float level, int color, int colorBlend)
	{
		Color color2 = mGraphics.setColorObj(colorBlend);
		float num = color2.r * 255f;
		float num2 = color2.g * 255f;
		float num3 = color2.b * 255f;
		Color color3 = mGraphics.setColorObj(color);
		float num4 = (num + color3.r) * level + color3.r;
		float num5 = (num2 + color3.g) * level + color3.g;
		float num6 = (num3 + color3.b) * level + color3.b;
		if (num4 > 255f)
		{
			num4 = 255f;
		}
		if (num4 < 0f)
		{
			num4 = 0f;
		}
		if (num5 > 255f)
		{
			num5 = 255f;
		}
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		if (num6 < 0f)
		{
			num6 = 0f;
		}
		if (num6 > 255f)
		{
			num6 = 255f;
		}
		return (int)num6 & (255 + ((int)num5 << 8)) & (255 + ((int)num4 << 16)) & 255;
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00090998 File Offset: 0x0008EB98
	public static int getIntByColor(Color cl)
	{
		int num = (int)(cl.r * 255f);
		float num2 = cl.b * 255f;
		return ((num & 255) << 16) | (((int)(cl.g * 255f) & 255) << 8) | ((int)num2 & 255);
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0004B1D9 File Offset: 0x000493D9
	public static int getRealImageWidth(Image img)
	{
		return img.w;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0004B1E1 File Offset: 0x000493E1
	public static int getRealImageHeight(Image img)
	{
		return img.h;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x000909E7 File Offset: 0x0008EBE7
	public void fillArg(int i, int j, int k, int l, int m, int n)
	{
		this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00090A0C File Offset: 0x0008EC0C
	public void CreateLineMaterial()
	{
		if (!this.lineMaterial)
		{
			this.lineMaterial = new Material("Shader \"Lines/Colored Blended\" {SubShader { Pass {  Blend SrcAlpha OneMinusSrcAlpha  ZWrite Off Cull Off Fog { Mode Off }  BindChannels { Bind \"vertex\", vertex Bind \"color\", color }} } }");
			this.lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00090A4C File Offset: 0x0008EC4C
	public void drawlineGL(MyVector totalLine)
	{
		this.lineMaterial.SetPass(0);
		GL.PushMatrix();
		GL.Begin(1);
		for (int i = 0; i < totalLine.size(); i++)
		{
			mLine mLine = (mLine)totalLine.elementAt(i);
			GL.Color(new Color(mLine.r, mLine.g, mLine.b, mLine.a));
			int num = mLine.x1 * mGraphics.zoomLevel;
			int num2 = mLine.y1 * mGraphics.zoomLevel;
			int num3 = mLine.x2 * mGraphics.zoomLevel;
			int num4 = mLine.y2 * mGraphics.zoomLevel;
			if (this.isTranslate)
			{
				num += this.translateX;
				num2 += this.translateY;
				num3 += this.translateX;
				num4 += this.translateY;
			}
			for (int j = 0; j < mGraphics.zoomLevel; j++)
			{
				GL.Vertex(new Vector2((float)(num + j), (float)(num2 + j)));
				GL.Vertex(new Vector2((float)(num3 + j), (float)(num4 + j)));
				if (j > 0)
				{
					GL.Vertex(new Vector2((float)(num + j), (float)num2));
					GL.Vertex(new Vector2((float)(num3 + j), (float)num4));
					GL.Vertex(new Vector2((float)num, (float)(num2 + j)));
					GL.Vertex(new Vector2((float)num3, (float)(num4 + j)));
				}
			}
		}
		GL.End();
		GL.PopMatrix();
		totalLine.removeAllElements();
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00090BDC File Offset: 0x0008EDDC
	public void drawLine(mGraphics g, int x, int y, int xTo, int yTo, int nLine, int color)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < nLine; i++)
		{
			myVector.addElement(new mLine(x, y, xTo + i, yTo + i, color));
		}
		g.drawlineGL(myVector);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0002E1CB File Offset: 0x0002C3CB
	internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040011DE RID: 4574
	public static int HCENTER = 1;

	// Token: 0x040011DF RID: 4575
	public static int VCENTER = 2;

	// Token: 0x040011E0 RID: 4576
	public static int LEFT = 4;

	// Token: 0x040011E1 RID: 4577
	public static int RIGHT = 8;

	// Token: 0x040011E2 RID: 4578
	public static int TOP = 16;

	// Token: 0x040011E3 RID: 4579
	public static int BOTTOM = 32;

	// Token: 0x040011E4 RID: 4580
	internal float r;

	// Token: 0x040011E5 RID: 4581
	internal float g;

	// Token: 0x040011E6 RID: 4582
	internal float b;

	// Token: 0x040011E7 RID: 4583
	internal float a;

	// Token: 0x040011E8 RID: 4584
	public int clipX;

	// Token: 0x040011E9 RID: 4585
	public int clipY;

	// Token: 0x040011EA RID: 4586
	public int clipW;

	// Token: 0x040011EB RID: 4587
	public int clipH;

	// Token: 0x040011EC RID: 4588
	internal bool isClip;

	// Token: 0x040011ED RID: 4589
	internal bool isTranslate = true;

	// Token: 0x040011EE RID: 4590
	internal int translateX;

	// Token: 0x040011EF RID: 4591
	internal int translateY;

	// Token: 0x040011F0 RID: 4592
	internal float translateXf;

	// Token: 0x040011F1 RID: 4593
	internal float translateYf;

	// Token: 0x040011F2 RID: 4594
	public static int zoomLevel = 1;

	// Token: 0x040011F3 RID: 4595
	public const int BASELINE = 64;

	// Token: 0x040011F4 RID: 4596
	public const int SOLID = 0;

	// Token: 0x040011F5 RID: 4597
	public const int DOTTED = 1;

	// Token: 0x040011F6 RID: 4598
	public const int TRANS_MIRROR = 2;

	// Token: 0x040011F7 RID: 4599
	public const int TRANS_MIRROR_ROT180 = 1;

	// Token: 0x040011F8 RID: 4600
	public const int TRANS_MIRROR_ROT270 = 4;

	// Token: 0x040011F9 RID: 4601
	public const int TRANS_MIRROR_ROT90 = 7;

	// Token: 0x040011FA RID: 4602
	public const int TRANS_NONE = 0;

	// Token: 0x040011FB RID: 4603
	public const int TRANS_ROT180 = 3;

	// Token: 0x040011FC RID: 4604
	public const int TRANS_ROT270 = 6;

	// Token: 0x040011FD RID: 4605
	public const int TRANS_ROT90 = 5;

	// Token: 0x040011FE RID: 4606
	public static Hashtable cachedTextures = new Hashtable();

	// Token: 0x040011FF RID: 4607
	internal int clipTX;

	// Token: 0x04001200 RID: 4608
	internal int clipTY;

	// Token: 0x04001201 RID: 4609
	internal int currentBGColor;

	// Token: 0x04001202 RID: 4610
	internal Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x04001203 RID: 4611
	internal Rect rect;

	// Token: 0x04001204 RID: 4612
	internal Matrix4x4 matrixBackup;

	// Token: 0x04001205 RID: 4613
	internal Vector2 pivot;

	// Token: 0x04001206 RID: 4614
	public Vector2 size = new Vector2(128f, 128f);

	// Token: 0x04001207 RID: 4615
	public Vector2 relativePosition = new Vector2(0f, 0f);

	// Token: 0x04001208 RID: 4616
	public Color clTrans;

	// Token: 0x04001209 RID: 4617
	public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

	// Token: 0x0400120A RID: 4618
	internal Material lineMaterial;
}

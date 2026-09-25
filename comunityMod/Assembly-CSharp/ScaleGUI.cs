using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class ScaleGUI
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x00077454 File Offset: 0x00075654
	public static void initScaleGUI()
	{
		Cout.println("Init Scale GUI: Screen.w=" + Screen.width.ToString() + " Screen.h=" + Screen.height.ToString());
		ScaleGUI.WIDTH = (float)Screen.width;
		ScaleGUI.HEIGHT = (float)Screen.height;
		ScaleGUI.scaleScreen = false;
		int width = Screen.width;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x000774B8 File Offset: 0x000756B8
	public static void BeginGUI()
	{
		if (ScaleGUI.scaleScreen)
		{
			ScaleGUI.stack.Add(GUI.matrix);
			Matrix4x4 matrix4x = default(Matrix4x4);
			float num = (float)Screen.width / (float)Screen.height;
			Vector3 zero = Vector3.zero;
			float num2 = ((num >= ScaleGUI.WIDTH / ScaleGUI.HEIGHT) ? ((float)Screen.height / ScaleGUI.HEIGHT) : ((float)Screen.width / ScaleGUI.WIDTH));
			matrix4x.SetTRS(zero, Quaternion.identity, Vector3.one * num2);
			GUI.matrix *= matrix4x;
		}
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0007754F File Offset: 0x0007574F
	public static void EndGUI()
	{
		if (ScaleGUI.scaleScreen)
		{
			GUI.matrix = ScaleGUI.stack[ScaleGUI.stack.Count - 1];
			ScaleGUI.stack.RemoveAt(ScaleGUI.stack.Count - 1);
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00077589 File Offset: 0x00075789
	public static float scaleX(float x)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return x;
		}
		x = x * ScaleGUI.WIDTH / (float)Screen.width;
		return x;
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x000775A5 File Offset: 0x000757A5
	public static float scaleY(float y)
	{
		if (!ScaleGUI.scaleScreen)
		{
			return y;
		}
		y = y * ScaleGUI.HEIGHT / (float)Screen.height;
		return y;
	}

	// Token: 0x04000E97 RID: 3735
	public static bool scaleScreen;

	// Token: 0x04000E98 RID: 3736
	public static float WIDTH;

	// Token: 0x04000E99 RID: 3737
	public static float HEIGHT;

	// Token: 0x04000E9A RID: 3738
	internal static List<Matrix4x4> stack = new List<Matrix4x4>();
}

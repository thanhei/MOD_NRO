using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mod.Graphics
{
	// Token: 0x0200015E RID: 350
	internal class UIImage : global::UnityEngine.UI.Image
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x000B617F File Offset: 0x000B437F
		protected override void OnPopulateMesh(Mesh m)
		{
			base.OnPopulateMesh(m);
			UIImage.mesh = m;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000B618E File Offset: 0x000B438E
		internal static void PopulateMesh()
		{
			UIImage.image.OnPopulateMesh(UIImage.mesh);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000B61A0 File Offset: 0x000B43A0
		internal static void OnStart()
		{
			GameObject gameObject = GameObject.Find("Main Camera");
			gameObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
			GameObject gameObject2 = new GameObject("Cooldown Effect");
			UIImage.image = gameObject2.AddComponent<UIImage>();
			UIImage.texture = new Texture2D(1, 1);
			UIImage.texture.SetPixel(0, 0, Color.black);
			UIImage.texture.Apply();
			UIImage.image.sprite = global::UnityEngine.Sprite.Create(UIImage.texture, new Rect(0f, 0f, (float)UIImage.texture.width, (float)UIImage.texture.height), new Vector2(0.5f, 0.5f));
			UIImage.image.type = global::UnityEngine.UI.Image.Type.Filled;
			UIImage.image.fillMethod = global::UnityEngine.UI.Image.FillMethod.Radial360;
			UIImage.image.fillClockwise = true;
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.position = new Vector3(-500f, -500f);
		}

		// Token: 0x040017DA RID: 6106
		internal static Mesh mesh = new Mesh();

		// Token: 0x040017DB RID: 6107
		private static Texture2D texture;

		// Token: 0x040017DC RID: 6108
		internal static UIImage image;
	}
}

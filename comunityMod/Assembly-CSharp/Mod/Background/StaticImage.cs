using System;
using System.IO;
using UnityEngine;

namespace Mod.Background
{
	// Token: 0x02000179 RID: 377
	internal class StaticImage : IBackground
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x000BA04D File Offset: 0x000B824D
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x000BA055 File Offset: 0x000B8255
		public ScaleMode ScaleMode
		{
			get
			{
				return this._scaleMode;
			}
			set
			{
				this._scaleMode = value;
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000BA060 File Offset: 0x000B8260
		internal StaticImage(string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			this.texture.LoadImage(array);
			this.texture.anisoLevel = 0;
			this.texture.filterMode = FilterMode.Point;
			this.texture.mipMapBias = 0f;
			this.texture.wrapMode = TextureWrapMode.Clamp;
			this.texture.Apply();
			this.isLoaded = true;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000BA0F9 File Offset: 0x000B82F9
		public Texture2D[] Textures
		{
			get
			{
				return new Texture2D[] { this.texture };
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000BA10A File Offset: 0x000B830A
		public bool IsLoaded
		{
			get
			{
				return this.isLoaded;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000BA112 File Offset: 0x000B8312
		public void Paint(mGraphics g, int x, int y)
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)Screen.width, (float)Screen.height), this.texture, this._scaleMode);
		}

		// Token: 0x0400188C RID: 6284
		private Texture2D texture = new Texture2D(1, 1);

		// Token: 0x0400188D RID: 6285
		private bool isLoaded;

		// Token: 0x0400188E RID: 6286
		private ScaleMode _scaleMode;
	}
}

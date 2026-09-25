using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MG.GIF;
using Mod.ModHelper;
using UnityEngine;

namespace Mod.Background
{
	// Token: 0x02000174 RID: 372
	internal class GifImage : IBackground
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x000B9DF3 File Offset: 0x000B7FF3
		public Texture2D[] Textures
		{
			get
			{
				return this.frames.ToArray();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x000B9E00 File Offset: 0x000B8000
		public bool IsLoaded
		{
			get
			{
				return this.isLoaded;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x000B9E08 File Offset: 0x000B8008
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x000B9E10 File Offset: 0x000B8010
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

		// Token: 0x0600111A RID: 4378 RVA: 0x000B9E1C File Offset: 0x000B801C
		internal GifImage(string path)
		{
			GifImage <>4__this = this;
			ThreadPool.QueueUserWorkItem(delegate(object _)
			{
				using (Decoder decoder = new Decoder(File.ReadAllBytes(path)))
				{
					MG.GIF.Image img = decoder.NextImage();
					do
					{
						bool completed = false;
						MainThreadDispatcher.Dispatch(delegate
						{
							<>4__this.frames.Add(img.CreateTexture());
							<>4__this.delays.Add((float)img.Delay);
							completed = true;
						});
						while (!completed)
						{
							Thread.Sleep(10);
						}
						img = decoder.NextImage();
					}
					while (img != null);
					<>4__this.isLoaded = true;
				}
			});
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000B9E74 File Offset: 0x000B8074
		public void Paint(mGraphics g, int x, int y)
		{
			if (!this.isLoaded)
			{
				GUI.DrawTexture(new Rect((float)x, (float)y, (float)Screen.width, (float)Screen.height), Texture2D.blackTexture);
				return;
			}
			if (this.paintFrameIndex >= this.frames.Count)
			{
				this.paintFrameIndex = 0;
			}
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)Screen.width, (float)Screen.height), this.frames[this.paintFrameIndex], this._scaleMode);
			if ((float)(mSystem.currentTimeMillis() - this.lastTimePaintAFrame) > this.delays[this.paintFrameIndex] / this.speed)
			{
				this.lastTimePaintAFrame = mSystem.currentTimeMillis();
				this.paintFrameIndex++;
			}
		}

		// Token: 0x0400187F RID: 6271
		internal List<float> delays = new List<float>();

		// Token: 0x04001880 RID: 6272
		internal int paintFrameIndex;

		// Token: 0x04001881 RID: 6273
		private long lastTimePaintAFrame;

		// Token: 0x04001882 RID: 6274
		internal bool isLoaded;

		// Token: 0x04001883 RID: 6275
		internal float speed = 1f;

		// Token: 0x04001884 RID: 6276
		private List<Texture2D> frames = new List<Texture2D>();

		// Token: 0x04001885 RID: 6277
		private ScaleMode _scaleMode;
	}
}

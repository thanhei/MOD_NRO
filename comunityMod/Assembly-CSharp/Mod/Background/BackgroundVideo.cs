using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace Mod.Background
{
	// Token: 0x0200016F RID: 367
	internal class BackgroundVideo : IBackground
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x000B85BA File Offset: 0x000B67BA
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x000B85C2 File Offset: 0x000B67C2
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

		// Token: 0x060010E6 RID: 4326 RVA: 0x000B85CB File Offset: 0x000B67CB
		internal BackgroundVideo(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)) || !File.Exists(path))
			{
				throw new FileNotFoundException();
			}
			this.url = path;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000B85F8 File Offset: 0x000B67F8
		public void Paint(mGraphics g, int x, int y)
		{
			if (mSystem.currentTimeMillis() - this.lastTimeCheckTime > 5000L)
			{
				this.lastTimeCheckTime = mSystem.currentTimeMillis();
				if (BackgroundVideo.videoPlayers[this.videoPlayerIndex].time == this.lastTime)
				{
					BackgroundVideo.videoPlayers[this.videoPlayerIndex].Stop();
					BackgroundVideo.videoPlayers[this.videoPlayerIndex].Play();
					BackgroundVideo.videoPlayers[this.videoPlayerIndex].time = this.lastTime;
				}
			}
			this.lastTime = BackgroundVideo.videoPlayers[this.videoPlayerIndex].time;
			if (BackgroundVideo.videoPlayers[this.videoPlayerIndex].texture != null)
			{
				GUI.DrawTexture(new Rect((float)x, (float)y, (float)Screen.width, (float)Screen.height), BackgroundVideo.videoPlayers[this.videoPlayerIndex].texture, this._scaleMode);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000B86D7 File Offset: 0x000B68D7
		internal void Stop()
		{
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].Stop();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000B86EA File Offset: 0x000B68EA
		internal void Play()
		{
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].Play();
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000B8700 File Offset: 0x000B6900
		internal void Prepare()
		{
			this.isPreparing = true;
			for (int i = 0; i < BackgroundVideo.videoPlayers.Length; i++)
			{
				this.videoPlayerIndex++;
				if (this.videoPlayerIndex == BackgroundVideo.videoPlayers.Length)
				{
					this.videoPlayerIndex = 0;
				}
				if (!BackgroundVideo.videoPlayers[this.videoPlayerIndex].isPlaying)
				{
					break;
				}
			}
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].url = this.url;
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].prepareCompleted += this.BackgroundVideo_prepareCompleted;
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].Prepare();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000B87A2 File Offset: 0x000B69A2
		private void BackgroundVideo_prepareCompleted(VideoPlayer source)
		{
			this.isPreparing = false;
			BackgroundVideo.videoPlayers[this.videoPlayerIndex].prepareCompleted -= this.BackgroundVideo_prepareCompleted;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000B87C8 File Offset: 0x000B69C8
		internal bool isPlaying
		{
			get
			{
				return BackgroundVideo.videoPlayers[this.videoPlayerIndex].isPlaying;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x000B87DB File Offset: 0x000B69DB
		public Texture2D[] Textures
		{
			get
			{
				throw new NotSupportedException("Video does not have an array of texture!");
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000B87E7 File Offset: 0x000B69E7
		public bool IsLoaded
		{
			get
			{
				return BackgroundVideo.videoPlayers[this.videoPlayerIndex].isPrepared;
			}
		}

		// Token: 0x0400185E RID: 6238
		private static VideoPlayer[] videoPlayers = GameObject.Find("Main Camera").GetComponents<VideoPlayer>();

		// Token: 0x0400185F RID: 6239
		internal bool isPreparing;

		// Token: 0x04001860 RID: 6240
		internal string url;

		// Token: 0x04001861 RID: 6241
		private long lastTimeCheckTime;

		// Token: 0x04001862 RID: 6242
		private int videoPlayerIndex;

		// Token: 0x04001863 RID: 6243
		private double lastTime;

		// Token: 0x04001864 RID: 6244
		private ScaleMode _scaleMode;
	}
}

using System;
using UnityEngine;

namespace Mod.Background
{
	// Token: 0x02000178 RID: 376
	public interface IBackground
	{
		// Token: 0x06001121 RID: 4385
		void Paint(mGraphics g, int x, int y);

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06001122 RID: 4386
		Texture2D[] Textures { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06001123 RID: 4387
		bool IsLoaded { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06001124 RID: 4388
		// (set) Token: 0x06001125 RID: 4389
		ScaleMode ScaleMode { get; set; }
	}
}

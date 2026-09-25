using System;

namespace Mod.CustomPanel
{
	// Token: 0x02000164 RID: 356
	internal class CustomPanelMenuConfig
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000B6D01 File Offset: 0x000B4F01
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x000B6D09 File Offset: 0x000B4F09
		internal Action<Panel> SetTabAction { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000B6D12 File Offset: 0x000B4F12
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x000B6D1A File Offset: 0x000B4F1A
		internal Action<Panel> DoFireItemAction { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x000B6D23 File Offset: 0x000B4F23
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x000B6D2B File Offset: 0x000B4F2B
		internal Action<Panel, mGraphics> PaintTabHeaderAction { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000B6D34 File Offset: 0x000B4F34
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x000B6D3C File Offset: 0x000B4F3C
		internal Action<Panel, mGraphics> PaintTopInfoAction { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x000B6D45 File Offset: 0x000B4F45
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x000B6D4D File Offset: 0x000B4F4D
		internal Action<Panel, mGraphics> PaintAction { get; set; }
	}
}

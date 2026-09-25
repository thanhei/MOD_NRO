using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x0200019F RID: 415
	public class DiagonalShortcut : ICalculateHeuristic
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x000C2F50 File Offset: 0x000C1150
		public int Calculate(Position source, Position destination)
		{
			int num = Math.Min(Math.Abs(source.Row - destination.Row), Math.Abs(source.Column - destination.Column));
			int num2 = Math.Abs(source.Row - destination.Row) + Math.Abs(source.Column - destination.Column);
			int num3 = 2;
			return num3 * 2 * num + num3 * (num2 - 2 * num);
		}
	}
}

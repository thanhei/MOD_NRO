using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x0200019E RID: 414
	public class Custom1 : ICalculateHeuristic
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x000C2ED0 File Offset: 0x000C10D0
		public int Calculate(Position source, Position destination)
		{
			int num = 2;
			Position position = new Position(Math.Abs(destination.Row - source.Row), Math.Abs(destination.Column - source.Column));
			int num2 = Math.Abs(position.Row - position.Column);
			int num3 = Math.Abs((position.Row + position.Column - num2) / 2);
			return num * (num3 + num2 + position.Row + position.Column);
		}
	}
}

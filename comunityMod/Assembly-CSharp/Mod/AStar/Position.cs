using System;

namespace Mod.AStar
{
	// Token: 0x02000199 RID: 409
	public readonly struct Position
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000C2CC7 File Offset: 0x000C0EC7
		public int Row { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000C2CCF File Offset: 0x000C0ECF
		public int Column { get; }

		// Token: 0x06001214 RID: 4628 RVA: 0x000C2CD7 File Offset: 0x000C0ED7
		public Position(int row = 0, int column = 0)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000C2CE7 File Offset: 0x000C0EE7
		public bool IsDiagonalTo(Position other)
		{
			return this.Row != other.Row && this.Column != other.Column;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000C2D0C File Offset: 0x000C0F0C
		public static bool operator ==(Position a, Position b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000C2D21 File Offset: 0x000C0F21
		public static bool operator !=(Position a, Position b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000C2D3C File Offset: 0x000C0F3C
		public override bool Equals(object other)
		{
			if (other is Position)
			{
				Position position = (Position)other;
				return this.Row == position.Row && this.Column == position.Column;
			}
			return false;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000C2D7C File Offset: 0x000C0F7C
		public override int GetHashCode()
		{
			return (17 * 23 + this.Row.GetHashCode()) * 23 + this.Column.GetHashCode();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000C2DAF File Offset: 0x000C0FAF
		public override string ToString()
		{
			return string.Format("[{0}.{1}]", this.Row, this.Column);
		}
	}
}

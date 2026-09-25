using System;
using System.Collections.Generic;

namespace Mod.AStar.Collections.MultiDimensional
{
	// Token: 0x020001AD RID: 429
	public class Grid<T> : IModelAGrid<T>
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x000C3550 File Offset: 0x000C1750
		public Grid(int height, int width)
		{
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			this.Height = height;
			this.Width = width;
			this._grid = new T[height * width];
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000C359D File Offset: 0x000C179D
		public int Height { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x000C35A5 File Offset: 0x000C17A5
		public int Width { get; }

		// Token: 0x06001261 RID: 4705 RVA: 0x000C35AD File Offset: 0x000C17AD
		public IEnumerable<Position> GetSuccessorPositions(Position node, bool optionsUseDiagonals = false)
		{
			IEnumerable<ValueTuple<sbyte, sbyte>> offsets = GridOffsets.GetOffsets(optionsUseDiagonals);
			foreach (ValueTuple<sbyte, sbyte> valueTuple in offsets)
			{
				int num = node.Row + (int)valueTuple.Item1;
				if (num >= 0 && num < this.Height)
				{
					int num2 = node.Column + (int)valueTuple.Item2;
					if (num2 >= 0 && num2 < this.Width)
					{
						yield return new Position(num, num2);
					}
				}
			}
			IEnumerator<ValueTuple<sbyte, sbyte>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000118 RID: 280
		public T this[Point point]
		{
			get
			{
				return this[point.ToPosition()];
			}
			set
			{
				this[point.ToPosition()] = value;
			}
		}

		// Token: 0x17000119 RID: 281
		public T this[Position position]
		{
			get
			{
				return this._grid[this.ConvertRowColumnToIndex(position.Row, position.Column)];
			}
			set
			{
				this._grid[this.ConvertRowColumnToIndex(position.Row, position.Column)] = value;
			}
		}

		// Token: 0x1700011A RID: 282
		public T this[int row, int column]
		{
			get
			{
				return this._grid[this.ConvertRowColumnToIndex(row, column)];
			}
			set
			{
				this._grid[this.ConvertRowColumnToIndex(row, column)] = value;
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000C3656 File Offset: 0x000C1856
		private int ConvertRowColumnToIndex(int row, int column)
		{
			return this.Width * row + column;
		}

		// Token: 0x0400198D RID: 6541
		private readonly T[] _grid;
	}
}

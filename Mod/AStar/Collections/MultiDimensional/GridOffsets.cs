using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mod.AStar.Collections.MultiDimensional
{
	// Token: 0x020001AF RID: 431
	public static class GridOffsets
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000C3857 File Offset: 0x000C1A57
		[TupleElementNames(new string[] { "row", "column" })]
		private static IEnumerable<ValueTuple<sbyte, sbyte>> CardinalDirectionOffsets
		{
			[return: TupleElementNames(new string[] { "row", "column" })]
			get
			{
				yield return new ValueTuple<sbyte, sbyte>(0, -1);
				yield return new ValueTuple<sbyte, sbyte>(1, 0);
				yield return new ValueTuple<sbyte, sbyte>(0, 1);
				yield return new ValueTuple<sbyte, sbyte>(-1, 0);
				yield break;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000C3860 File Offset: 0x000C1A60
		[TupleElementNames(new string[] { "row", "column" })]
		private static IEnumerable<ValueTuple<sbyte, sbyte>> DiagonalsOffsets
		{
			[return: TupleElementNames(new string[] { "row", "column" })]
			get
			{
				yield return new ValueTuple<sbyte, sbyte>(1, -1);
				yield return new ValueTuple<sbyte, sbyte>(1, 1);
				yield return new ValueTuple<sbyte, sbyte>(-1, 1);
				yield return new ValueTuple<sbyte, sbyte>(-1, -1);
				yield break;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000C3869 File Offset: 0x000C1A69
		[return: TupleElementNames(new string[] { "row", "column" })]
		public static IEnumerable<ValueTuple<sbyte, sbyte>> GetOffsets(bool withDiagonals = false)
		{
			if (!withDiagonals)
			{
				return GridOffsets.CardinalDirectionOffsets;
			}
			return GridOffsets.CardinalDirectionOffsets.Concat<ValueTuple<sbyte, sbyte>>(GridOffsets.DiagonalsOffsets);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000C3883 File Offset: 0x000C1A83
		public static bool IsCardinalOffset([TupleElementNames(new string[] { "row", "column" })] ValueTuple<sbyte, sbyte> offset)
		{
			return offset.Item1 != 0 && offset.Item2 != 0;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000C3898 File Offset: 0x000C1A98
		public static bool IsDiagonal([TupleElementNames(new string[] { "row", "column" })] ValueTuple<sbyte, sbyte> offset)
		{
			return offset.Item1 != 0 || offset.Item2 != 0;
		}
	}
}

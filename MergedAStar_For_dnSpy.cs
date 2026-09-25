using Mod.AStar.Collections.MultiDimensional;
using Mod.AStar.Collections.PathFinder;
using Mod.AStar.Collections.PriorityQueue;
using Mod.AStar.Heuristics;
using Mod.AStar.Options;
using Mod.AStar;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;


namespace Mod.AStar
{
	// Token: 0x02000195 RID: 405
	public interface IFindAPath
	{
		// Token: 0x06001203 RID: 4611
		Position[] FindPath(Position start, Position end);

		// Token: 0x06001204 RID: 4612
		Point[] FindPath(Point start, Point end);
	}
}


namespace Mod.AStar
{
	// Token: 0x02000199 RID: 409
	public struct Position
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


namespace Mod.AStar
{
	// Token: 0x02000198 RID: 408
	public class Point
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000C2CA1 File Offset: 0x000C0EA1
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x000C2CA9 File Offset: 0x000C0EA9
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000C2CB1 File Offset: 0x000C0EB1
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400196F RID: 6511
		private int x;

		// Token: 0x04001970 RID: 6512
		private int y;
	}
}


namespace Mod.AStar
{
	// Token: 0x0200019B RID: 411
	public class WorldGrid : Grid<short>
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x000C2DF9 File Offset: 0x000C0FF9
		public WorldGrid(int height, int width)
			: base(height, width)
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000C2E04 File Offset: 0x000C1004
		public WorldGrid(short[,] worldArray)
			: base(worldArray.GetLength(0), worldArray.GetLength(1))
		{
			for (int i = 0; i < worldArray.GetLength(0); i++)
			{
				for (int j = 0; j < worldArray.GetLength(1); j++)
				{
					base[i, j] = worldArray[i, j];
				}
			}
		}
	}
}


namespace Mod.AStar
{
	// Token: 0x02000196 RID: 406
	public class PathFinder : IFindAPath
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x000C27A0 File Offset: 0x000C09A0
		public PathFinder(WorldGrid worldGrid, PathFinderOptions pathFinderOptions = null)
		{
			if (worldGrid == null)
			{
				throw new ArgumentNullException("worldGrid");
			}
			this._world = worldGrid;
			this._options = pathFinderOptions ?? new PathFinderOptions();
			this._heuristic = HeuristicFactory.Create(this._options.HeuristicFormula);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000C27F0 File Offset: 0x000C09F0
		public Point[] FindPath(Point start, Point end)
		{
			return (from position in this.FindPath(new Position(start.Y, start.X), new Position(end.Y, end.X))
				select new Point(position.Column, position.Row)).ToArray<Point>();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000C2850 File Offset: 0x000C0A50
		public Position[] FindPath(Position start, Position end)
		{
			int num = 0;
			IModelAGraph<PathFinderNode> modelAGraph = new PathFinderGraph(this._world.Height, this._world.Width, this._options.UseDiagonals);
			PathFinderNode pathFinderNode = new PathFinderNode(start, 0, 2, start);
			modelAGraph.OpenNode(pathFinderNode);
			while (modelAGraph.HasOpenNodes)
			{
				PathFinderNode openNodeWithSmallestF = modelAGraph.GetOpenNodeWithSmallestF();
				if (openNodeWithSmallestF.Position == end)
				{
					return PathFinder.OrderClosedNodesAsArray(modelAGraph, openNodeWithSmallestF);
				}
				if (num > this._options.SearchLimit)
				{
					return new Position[0];
				}
				foreach (PathFinderNode pathFinderNode2 in modelAGraph.GetSuccessors(openNodeWithSmallestF))
				{
					if (this._world[pathFinderNode2.Position] != 0)
					{
						int num2 = openNodeWithSmallestF.G + 1;
						if (this._options.PunishChangeDirection)
						{
							num2 += this.CalculateModifierToG(openNodeWithSmallestF, pathFinderNode2, end);
						}
						int num3 = this._heuristic.Calculate(pathFinderNode2.Position, end);
						switch (this._options.Weighting)
						{
						case Weighting.Positive:
							num3 -= (int)this._world[pathFinderNode2.Position];
							break;
						case Weighting.Negative:
							num3 += (int)this._world[pathFinderNode2.Position];
							break;
						}
						PathFinderNode pathFinderNode3 = new PathFinderNode(pathFinderNode2.Position, num2, num3, openNodeWithSmallestF.Position);
						if (this.BetterPathToSuccessorFound(pathFinderNode3, pathFinderNode2))
						{
							modelAGraph.OpenNode(pathFinderNode3);
						}
					}
				}
				num++;
			}
			return new Position[0];
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000C2A00 File Offset: 0x000C0C00
		private int CalculateModifierToG(PathFinderNode q, PathFinderNode successor, Position end)
		{
			if (q.Position == q.ParentNodePosition)
			{
				return 0;
			}
			int num = global::System.Math.Abs(successor.Position.Row - end.Row) + global::System.Math.Abs(successor.Position.Column - end.Column);
			if (successor.Position.Row - q.Position.Row != 0 && q.Position.Row - q.ParentNodePosition.Row == 0)
			{
				return num;
			}
			if (successor.Position.Row - q.Position.Row != 0 && q.Position.Row - q.ParentNodePosition.Row == 0)
			{
				return num;
			}
			if (this._options.UseDiagonals && successor.Position.Column - successor.Position.Row == q.Position.Column - q.Position.Row && (q.Position.Column - q.Position.Row == q.ParentNodePosition.Column - q.ParentNodePosition.Row && this.IsStraightLine(q.ParentNodePosition, q.Position, successor.Position)))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000C2BA8 File Offset: 0x000C0DA8
		private bool IsStraightLine(Position a, Position b, Position c)
		{
			return (a.Column * (b.Row - c.Row) + b.Column * (c.Row - a.Row) + c.Column * (a.Row - b.Row)) / 2 == 0;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000C2C01 File Offset: 0x000C0E01
		private bool BetterPathToSuccessorFound(PathFinderNode updateSuccessor, PathFinderNode currentSuccessor)
		{
			return !currentSuccessor.HasBeenVisited || (currentSuccessor.HasBeenVisited && updateSuccessor.F < currentSuccessor.F);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000C2C2C File Offset: 0x000C0E2C
		private static Position[] OrderClosedNodesAsArray(IModelAGraph<PathFinderNode> graph, PathFinderNode endNode)
		{
			Stack<Position> stack = new Stack<Position>();
			PathFinderNode pathFinderNode = endNode;
			while (pathFinderNode.Position != pathFinderNode.ParentNodePosition)
			{
				stack.Push(pathFinderNode.Position);
				pathFinderNode = graph.GetParent(pathFinderNode);
			}
			stack.Push(pathFinderNode.Position);
			return stack.ToArray();
		}

		// Token: 0x04001968 RID: 6504
		private const int ClosedValue = 0;

		// Token: 0x04001969 RID: 6505
		private const int DistanceBetweenNodes = 1;

		// Token: 0x0400196A RID: 6506
		private readonly PathFinderOptions _options;

		// Token: 0x0400196B RID: 6507
		private readonly WorldGrid _world;

		// Token: 0x0400196C RID: 6508
		private readonly ICalculateHeuristic _heuristic;
	}
}


namespace Mod.AStar
{
	// Token: 0x0200019A RID: 410
	public static class PositionExtensions
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x000C2DD1 File Offset: 0x000C0FD1
		public static Point ToPoint(this Position position)
		{
			return new Point(position.Column, position.Row);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000C2DE6 File Offset: 0x000C0FE6
		public static Position ToPosition(this Point point)
		{
			return new Position(point.Y, point.X);
		}
	}
}


namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001AC RID: 428
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct PathFinderNode
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000C34F4 File Offset: 0x000C16F4
		public Position Position { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000C34FC File Offset: 0x000C16FC
		public int G { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000C3504 File Offset: 0x000C1704
		public int H { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x000C350C File Offset: 0x000C170C
		public Position ParentNodePosition { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000C3514 File Offset: 0x000C1714
		public int F { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x000C351C File Offset: 0x000C171C
		public bool HasBeenVisited
		{
			get
			{
				return this.F > 0;
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000C3527 File Offset: 0x000C1727
		public PathFinderNode(Position position, int g, int h, Position parentNodePosition)
		{
			this.Position = position;
			this.G = g;
			this.H = h;
			this.ParentNodePosition = parentNodePosition;
			this.F = g + h;
		}
	}
}


namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001AB RID: 427
	internal class PathFinderGraph : IModelAGraph<PathFinderNode>
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000C33CB File Offset: 0x000C15CB
		public bool HasOpenNodes
		{
			get
			{
				return this._open.Count > 0;
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000C33DB File Offset: 0x000C15DB
		public PathFinderGraph(int height, int width, bool allowDiagonalTraversal)
		{
			this._allowDiagonalTraversal = allowDiagonalTraversal;
			this._internalGrid = new Grid<PathFinderNode>(height, width);
			this.Initialise();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000C3410 File Offset: 0x000C1610
		private void Initialise()
		{
			for (int i = 0; i < this._internalGrid.Height; i++)
			{
				for (int j = 0; j < this._internalGrid.Width; j++)
				{
					this._internalGrid[i, j] = new PathFinderNode(new Position(i, j), 0, 0, default(Position));
				}
			}
			this._open.Clear();
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000C3478 File Offset: 0x000C1678
		public IEnumerable<PathFinderNode> GetSuccessors(PathFinderNode node)
		{
			return from successorPosition in this._internalGrid.GetSuccessorPositions(node.Position, this._allowDiagonalTraversal)
				select this._internalGrid[successorPosition];
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000C34A3 File Offset: 0x000C16A3
		public PathFinderNode GetParent(PathFinderNode node)
		{
			return this._internalGrid[node.ParentNodePosition];
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000C34B7 File Offset: 0x000C16B7
		public void OpenNode(PathFinderNode node)
		{
			this._internalGrid[node.Position] = node;
			this._open.Push(node);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000C34D9 File Offset: 0x000C16D9
		public PathFinderNode GetOpenNodeWithSmallestF()
		{
			return this._open.Pop();
		}

		// Token: 0x04001985 RID: 6533
		private readonly bool _allowDiagonalTraversal;

		// Token: 0x04001986 RID: 6534
		private readonly Grid<PathFinderNode> _internalGrid;

		// Token: 0x04001987 RID: 6535
		private readonly SimplePriorityQueue<PathFinderNode> _open = new SimplePriorityQueue<PathFinderNode>(new ComparePathFinderNodeByFValue());
	}
}


namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001A9 RID: 425
	internal class ComparePathFinderNodeByFValue : IComparer<PathFinderNode>
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x000C33A4 File Offset: 0x000C15A4
		public int Compare(PathFinderNode a, PathFinderNode b)
		{
			if (a.F > b.F)
			{
				return 1;
			}
			if (a.F < b.F)
			{
				return -1;
			}
			return 0;
		}
	}
}


namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001AA RID: 426
	internal interface IModelAGraph<T>
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600124A RID: 4682
		bool HasOpenNodes { get; }

		// Token: 0x0600124B RID: 4683
		IEnumerable<T> GetSuccessors(T node);

		// Token: 0x0600124C RID: 4684
		T GetParent(T node);

		// Token: 0x0600124D RID: 4685
		void OpenNode(T node);

		// Token: 0x0600124E RID: 4686
		T GetOpenNodeWithSmallestF();
	}
}


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
			IEnumerable<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> offsets = GridOffsets.GetOffsets(optionsUseDiagonals);
			foreach (System.Collections.Generic.KeyValuePair<sbyte, sbyte> valueTuple in offsets)
			{
				int num = node.Row + (int)valueTuple.Key;
				if (num >= 0 && num < this.Height)
				{
					int num2 = node.Column + (int)valueTuple.Value;
					if (num2 >= 0 && num2 < this.Width)
					{
						yield return new Position(num, num2);
					}
				}
			}
			IEnumerator<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> enumerator = null;
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


namespace Mod.AStar.Collections.MultiDimensional
{
	// Token: 0x020001B2 RID: 434
	public interface IModelAGrid<T>
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06001287 RID: 4743
		int Height { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06001288 RID: 4744
		int Width { get; }

		// Token: 0x17000125 RID: 293
		T this[int row, int column] { get; set; }

		// Token: 0x17000126 RID: 294
		T this[Position position] { get; set; }

		// Token: 0x0600128D RID: 4749
		IEnumerable<Position> GetSuccessorPositions(Position node, bool optionsUseDiagonals = false);
	}
}


namespace Mod.AStar.Collections.MultiDimensional
{
	// Token: 0x020001AF RID: 431
	public static class GridOffsets
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000C3857 File Offset: 0x000C1A57
		
		private static IEnumerable<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> CardinalDirectionOffsets
		{
			
			get
			{
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(0, -1);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(1, 0);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(0, 1);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(-1, 0);
				yield break;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000C3860 File Offset: 0x000C1A60
		
		private static IEnumerable<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> DiagonalsOffsets
		{
			
			get
			{
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(1, -1);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(1, 1);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(-1, 1);
				yield return new System.Collections.Generic.KeyValuePair<sbyte, sbyte>(-1, -1);
				yield break;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000C3869 File Offset: 0x000C1A69
		
		public static IEnumerable<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> GetOffsets(bool withDiagonals = false)
		{
			if (!withDiagonals)
			{
				return GridOffsets.CardinalDirectionOffsets;
			}
			System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<sbyte, sbyte>> list = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<sbyte, sbyte>>(GridOffsets.CardinalDirectionOffsets);
        list.AddRange(GridOffsets.DiagonalsOffsets);
        return list;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000C3883 File Offset: 0x000C1A83
		public static bool IsCardinalOffset( System.Collections.Generic.KeyValuePair<sbyte, sbyte> offset)
		{
			return offset.Key != 0 && offset.Value != 0;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000C3898 File Offset: 0x000C1A98
		public static bool IsDiagonal( System.Collections.Generic.KeyValuePair<sbyte, sbyte> offset)
		{
			return offset.Key != 0 || offset.Value != 0;
		}
	}
}


namespace Mod.AStar.Collections.PriorityQueue
{
	// Token: 0x020001A7 RID: 423
	internal interface IModelAPriorityQueue<T>
	{
		// Token: 0x06001238 RID: 4664
		int Push(T item);

		// Token: 0x06001239 RID: 4665
		T Pop();

		// Token: 0x0600123A RID: 4666
		T Peek();

		// Token: 0x0600123B RID: 4667
		void Clear();

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600123C RID: 4668
		int Count { get; }
	}
}


namespace Mod.AStar.Collections.PriorityQueue
{
	// Token: 0x020001A8 RID: 424
	internal class SimplePriorityQueue<T> : IModelAPriorityQueue<T>
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x000C312A File Offset: 0x000C132A
		public SimplePriorityQueue(IComparer<T> comparer = null)
		{
			this._comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000C3150 File Offset: 0x000C1350
		public T Peek()
		{
			if (this._innerList.Count <= 0)
			{
				return default(T);
			}
			return this._innerList[0];
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000C3181 File Offset: 0x000C1381
		public void Clear()
		{
			this._innerList.Clear();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000C318E File Offset: 0x000C138E
		public int Count
		{
			get
			{
				return this._innerList.Count;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000C319C File Offset: 0x000C139C
		public int Push(T item)
		{
			int num = this._innerList.Count;
			this._innerList.Add(item);
			while (num != 0)
			{
				int num2 = (num - 1) / 2;
				if (this.OnCompare(num, num2) >= 0)
				{
					break;
				}
				this.SwitchElements(num, num2);
				num = num2;
			}
			return num;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000C31E4 File Offset: 0x000C13E4
		public T Pop()
		{
			T t = this._innerList[0];
			int num = 0;
			this._innerList[0] = this._innerList[this._innerList.Count - 1];
			this._innerList.RemoveAt(this._innerList.Count - 1);
			for (;;)
			{
				int num2 = num;
				int num3 = 2 * num + 1;
				int num4 = 2 * num + 2;
				if (this._innerList.Count > num3 && this.OnCompare(num, num3) > 0)
				{
					num = num3;
				}
				if (this._innerList.Count > num4 && this.OnCompare(num, num4) > 0)
				{
					num = num4;
				}
				if (num == num2)
				{
					break;
				}
				this.SwitchElements(num, num2);
			}
			return t;
		}

		// Token: 0x1700010D RID: 269
		public T this[int index]
		{
			get
			{
				return this._innerList[index];
			}
			set
			{
				this._innerList[index] = value;
				this.Update(index);
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000C32B8 File Offset: 0x000C14B8
		private void Update(int i)
		{
			int num;
			int num2;
			for (num = i; num != 0; num = num2)
			{
				num2 = (num - 1) / 2;
				if (this.OnCompare(num, num2) >= 0)
				{
					break;
				}
				this.SwitchElements(num, num2);
			}
			if (num < i)
			{
				return;
			}
			for (;;)
			{
				int num3 = num;
				int num4 = 2 * num + 1;
				num2 = 2 * num + 2;
				if (this._innerList.Count > num4 && this.OnCompare(num, num4) > 0)
				{
					num = num4;
				}
				if (this._innerList.Count > num2 && this.OnCompare(num, num2) > 0)
				{
					num = num2;
				}
				if (num == num3)
				{
					break;
				}
				this.SwitchElements(num, num3);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000C3340 File Offset: 0x000C1540
		private void SwitchElements(int i, int j)
		{
			T t = this._innerList[i];
			this._innerList[i] = this._innerList[j];
			this._innerList[j] = t;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000C337F File Offset: 0x000C157F
		private int OnCompare(int i, int j)
		{
			return this._comparer.Compare(this._innerList[i], this._innerList[j]);
		}

		// Token: 0x04001983 RID: 6531
		private readonly List<T> _innerList = new List<T>();

		// Token: 0x04001984 RID: 6532
		private readonly IComparer<T> _comparer;
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A1 RID: 417
	public class EuclideanNoSQR : ICalculateHeuristic
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x000C3018 File Offset: 0x000C1218
		public int Calculate(Position source, Position destination)
		{
			return (int)((double)2 * (global::System.Math.Pow((double)(source.Row - destination.Row), 2.0) + global::System.Math.Pow((double)(source.Column - destination.Column), 2.0)));
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x0200019F RID: 415
	public class DiagonalShortcut : ICalculateHeuristic
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x000C2F50 File Offset: 0x000C1150
		public int Calculate(Position source, Position destination)
		{
			int num = global::System.Math.Min(global::System.Math.Abs(source.Row - destination.Row), global::System.Math.Abs(source.Column - destination.Column));
			int num2 = global::System.Math.Abs(source.Row - destination.Row) + global::System.Math.Abs(source.Column - destination.Column);
			int num3 = 2;
			return num3 * 2 * num + num3 * (num2 - 2 * num);
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A3 RID: 419
	public enum HeuristicFormula
	{
		// Token: 0x0400197D RID: 6525
		Manhattan = 1,
		// Token: 0x0400197E RID: 6526
		MaxDXDY,
		// Token: 0x0400197F RID: 6527
		DiagonalShortCut,
		// Token: 0x04001980 RID: 6528
		Euclidean,
		// Token: 0x04001981 RID: 6529
		EuclideanNoSQR,
		// Token: 0x04001982 RID: 6530
		Custom1
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A2 RID: 418
	public static class HeuristicFactory
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x000C3068 File Offset: 0x000C1268
		public static ICalculateHeuristic Create(HeuristicFormula heuristicFormula)
		{
			switch (heuristicFormula)
			{
			case HeuristicFormula.Manhattan:
				return new Manhattan();
			case HeuristicFormula.MaxDXDY:
				return new MaxDXDY();
			case HeuristicFormula.DiagonalShortCut:
				return new DiagonalShortcut();
			case HeuristicFormula.Euclidean:
				return new Euclidean();
			case HeuristicFormula.EuclideanNoSQR:
				return new EuclideanNoSQR();
			case HeuristicFormula.Custom1:
				return new Custom1();
			default:
				throw new ArgumentOutOfRangeException("heuristicFormula", heuristicFormula, null);
			}
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A0 RID: 416
	public class Euclidean : ICalculateHeuristic
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x000C2FC4 File Offset: 0x000C11C4
		public int Calculate(Position source, Position destination)
		{
			return (int)((double)2 * global::System.Math.Sqrt(global::System.Math.Pow((double)(source.Row - destination.Row), 2.0) + global::System.Math.Pow((double)(source.Column - destination.Column), 2.0)));
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x0200019E RID: 414
	public class Custom1 : ICalculateHeuristic
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x000C2ED0 File Offset: 0x000C10D0
		public int Calculate(Position source, Position destination)
		{
			int num = 2;
			Position position = new Position(global::System.Math.Abs(destination.Row - source.Row), global::System.Math.Abs(destination.Column - source.Column));
			int num2 = global::System.Math.Abs(position.Row - position.Column);
			int num3 = global::System.Math.Abs((position.Row + position.Column - num2) / 2);
			return num * (num3 + num2 + position.Row + position.Column);
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A5 RID: 421
	public class Manhattan : ICalculateHeuristic
	{
		// Token: 0x06001234 RID: 4660 RVA: 0x000C30CC File Offset: 0x000C12CC
		public int Calculate(Position source, Position destination)
		{
			return 2 * (global::System.Math.Abs(source.Row - destination.Row) + global::System.Math.Abs(source.Column - destination.Column));
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A6 RID: 422
	public class MaxDXDY : ICalculateHeuristic
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x000C30F9 File Offset: 0x000C12F9
		public int Calculate(Position source, Position destination)
		{
			return 2 * global::System.Math.Max(global::System.Math.Abs(source.Row - destination.Row), global::System.Math.Abs(source.Column - destination.Column));
		}
	}
}


namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A4 RID: 420
	public interface ICalculateHeuristic
	{
		// Token: 0x06001233 RID: 4659
		int Calculate(Position source, Position destination);
	}
}


namespace Mod.AStar.Options
{
	// Token: 0x0200019D RID: 413
	public enum Weighting
	{
		// Token: 0x04001979 RID: 6521
		None,
		// Token: 0x0400197A RID: 6522
		Positive,
		// Token: 0x0400197B RID: 6523
		Negative
	}
}


namespace Mod.AStar.Options
{
	// Token: 0x0200019C RID: 412
	public class PathFinderOptions
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x000C2E59 File Offset: 0x000C1059
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x000C2E61 File Offset: 0x000C1061
		public HeuristicFormula HeuristicFormula { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x000C2E6A File Offset: 0x000C106A
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x000C2E72 File Offset: 0x000C1072
		public bool UseDiagonals { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000C2E7B File Offset: 0x000C107B
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x000C2E83 File Offset: 0x000C1083
		public bool PunishChangeDirection { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000C2E8C File Offset: 0x000C108C
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x000C2E94 File Offset: 0x000C1094
		public int SearchLimit { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x000C2E9D File Offset: 0x000C109D
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x000C2EA5 File Offset: 0x000C10A5
		public Weighting Weighting { get; set; }

		// Token: 0x06001229 RID: 4649 RVA: 0x000C2EAE File Offset: 0x000C10AE
		public PathFinderOptions()
		{
			this.HeuristicFormula = HeuristicFormula.Manhattan;
			this.UseDiagonals = true;
			this.SearchLimit = 2000;
		}
	}
}


namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x02000106 RID: 262
	public class Tile
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x000A4224 File Offset: 0x000A2424
		internal Tile(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x04001517 RID: 5399
		internal int x;

		// Token: 0x04001518 RID: 5400
		internal int y;
	}
}


namespace Mod.DungPham.KoiOctiiu957
{
	public class AutoMapAStar
	{
		public static Stack<Tile> FindPath(Tile start, Tile destination)
		{
			PathFinderOptions pathFinderOptions = new PathFinderOptions
			{
				PunishChangeDirection = true,
				UseDiagonals = false
			};
			short[,] array = new short[TileMap.tmh, TileMap.tmw];
			for (int i = 0; i < TileMap.tmh; i++)
			{
				for (int j = 0; j < TileMap.tmw; j++)
				{
					array[i, j] = 1;
					if (TileMap.maps[i * TileMap.tmw + j] != 0 && (TileMap.tileTypeAt(j * (int)TileMap.size, i * (int)TileMap.size, 2) || AutoMapAStar.IsTileMapICantEnter(j * (int)TileMap.size, i * (int)TileMap.size)))
					{
						array[i, j] = 0;
					}
				}
			}
			List<Mod.AStar.Point> list = new PathFinder(new WorldGrid(array), pathFinderOptions).FindPath(new Mod.AStar.Point(start.x, start.y), new Mod.AStar.Point(destination.x, destination.y)).ToList<Mod.AStar.Point>();
			if (list.Count <= 0)
			{
				return new Stack<Tile>();
			}
			for (int k = list.Count - 3; k >= 0; k--)
			{
				if (AutoMapAStar.IsStraightLine(list[k], list[k + 1], list[k + 2]))
				{
					list.RemoveAt(k + 1);
				}
			}
			list.RemoveAt(0);
			return new Stack<Tile>(from p in list.Reverse<Mod.AStar.Point>()
				select new Tile(p.X, p.Y));
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000A3536 File Offset: 0x000A1736
		public static bool IsTileMapICantEnter(int px, int py)
		{
			return TileMap.tileTypeAt(px, py, 4) || TileMap.tileTypeAt(px, py, 8) || TileMap.tileTypeAt(px, py, 8192);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000A355C File Offset: 0x000A175C
		public static bool IsStraightLine(Mod.AStar.Point a, Mod.AStar.Point b, Mod.AStar.Point c)
		{
			return (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2 == 0;
		}
	}
}


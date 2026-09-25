using System;
using System.Collections.Generic;
using System.Linq;
using Mod.AStar.Collections.PathFinder;
using Mod.AStar.Heuristics;
using Mod.AStar.Options;

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
			int num = Math.Abs(successor.Position.Row - end.Row) + Math.Abs(successor.Position.Column - end.Column);
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

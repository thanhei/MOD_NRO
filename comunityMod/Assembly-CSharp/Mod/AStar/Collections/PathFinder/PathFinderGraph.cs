using System;
using System.Collections.Generic;
using System.Linq;
using Mod.AStar.Collections.MultiDimensional;
using Mod.AStar.Collections.PriorityQueue;

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

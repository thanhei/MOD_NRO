using System;
using System.Collections.Generic;
using System.Linq;
using Mod.AStar;
using Mod.AStar.Options;

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

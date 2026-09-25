using System;
using Mod.AStar.Heuristics;

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

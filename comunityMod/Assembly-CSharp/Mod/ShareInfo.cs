using System;
using Mod.ModHelper;

namespace Mod
{
	// Token: 0x020000EC RID: 236
	internal class ShareInfo : ThreadActionUpdate<ShareInfo>
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000A09F4 File Offset: 0x0009EBF4
		internal override int Interval
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000A09FC File Offset: 0x0009EBFC
		protected override void update()
		{
			global::Char @char = global::Char.myCharz();
			global::Char char2 = global::Char.myPetz();
			if (@char.cName == "")
			{
				return;
			}
			ThreadAction<SocketClient>.gI.sendMessage(new
			{
				action = "updateInfo",
				status = Utils.status,
				cName = @char.cName,
				cgender = @char.cgender,
				mapName = TileMap.mapName,
				mapID = TileMap.mapID,
				zoneID = TileMap.zoneID,
				cx = @char.cx,
				cy = @char.cy,
				cHP = @char.cHP,
				cHPFull = @char.cHPFull,
				cMP = @char.cMP,
				cMPFull = @char.cMPFull,
				cStamina = @char.cStamina,
				cPower = @char.cPower,
				cTiemNang = @char.cTiemNang,
				cHPGoc = @char.cHPGoc,
				cMPGoc = @char.cMPGoc,
				cDefGoc = @char.cDefGoc,
				cDamGoc = @char.cDamGoc,
				cCriticalGoc = @char.cCriticalGoc,
				cDamFull = @char.cDamFull,
				cDefull = @char.cDefull,
				cCriticalFull = @char.cCriticalFull,
				cPetHP = char2.cHP,
				cPetHPFull = char2.cHPFull,
				cPetMP = char2.cMP,
				cPetMPFull = char2.cMPFull,
				cPetStamina = char2.cStamina,
				cPetPower = char2.cPower,
				cPetTiemNang = char2.cTiemNang,
				cPetDamFull = char2.cDamFull,
				cPetDefull = char2.cDefull,
				cPetCriticalFull = char2.cCriticalFull,
				xu = @char.xu,
				luong = @char.luong,
				luongKhoa = @char.luongKhoa
			});
		}
	}
}

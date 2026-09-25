using System;
using Mod.Constants;
using UnityEngine;

namespace Mod.Graphics
{
	// Token: 0x02000159 RID: 345
	internal class GraphicsReducer
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000B3B10 File Offset: 0x000B1D10
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000B3B17 File Offset: 0x000B1D17
		internal static ReduceGraphicsLevel Level
		{
			get
			{
				return GraphicsReducer._level;
			}
			set
			{
				GraphicsReducer._level = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000B3B1F File Offset: 0x000B1D1F
		internal static bool IsEnabled
		{
			get
			{
				return GraphicsReducer._level > ReduceGraphicsLevel.Off;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnServerEffectPaint()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000B3B38 File Offset: 0x000B1D38
		internal static bool OnNpcPaint(Npc _this, mGraphics g)
		{
			if (GraphicsReducer._level < ReduceGraphicsLevel.Level2)
			{
				return false;
			}
			if (global::Char.isLoadingMap || _this.isHide || !_this.isPaint() || _this.statusMe == 15)
			{
				return true;
			}
			if (_this.cTypePk != 0)
			{
				return false;
			}
			if (_this.template == null)
			{
				return true;
			}
			if (_this.template.npcTemplateId != 4 && _this.template.npcTemplateId != 51 && _this.template.npcTemplateId != 50)
			{
				g.drawImage(TileMap.bong, _this.cx, _this.cy, 3);
			}
			if (GraphicsReducer._level == ReduceGraphicsLevel.Level2)
			{
				g.setColor(Color.green);
				g.drawRect(_this.cx - 12, _this.cy - _this.ch, 24, _this.ch);
				if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus == _this && ChatPopup.currChatPopup == null)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, _this.cx, _this.cy - _this.ch - 7, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else if (GraphicsReducer._level > ReduceGraphicsLevel.Level2 && global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus == _this && ChatPopup.currChatPopup == null)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, _this.cx, _this.cy - _this.ch - 7, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (_this.indexEffTask < 0 || _this.effTask == null || _this.cTypePk != 0)
			{
				return true;
			}
			SmallImage.drawSmallImage(g, _this.effTask.arrEfInfo[_this.indexEffTask].idImg, _this.cx + _this.effTask.arrEfInfo[_this.indexEffTask].dx, _this.cy + _this.effTask.arrEfInfo[_this.indexEffTask].dy - _this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				_this.indexEffTask++;
				if (_this.indexEffTask >= _this.effTask.arrEfInfo.Length)
				{
					_this.indexEffTask = 0;
				}
			}
			return true;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnTileMapPaintOutTile()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000B3D81 File Offset: 0x000B1F81
		internal static bool OnTileMapPaintTile(mGraphics g)
		{
			if (GraphicsReducer._level > ReduceGraphicsLevel.Level2)
			{
				return true;
			}
			if (GraphicsReducer._level >= ReduceGraphicsLevel.Level1)
			{
				GraphicsReducer.PaintTileMap(g);
				return true;
			}
			return false;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000B3DA0 File Offset: 0x000B1FA0
		internal static bool OnMobPaint(Mob _this, mGraphics g)
		{
			if (GraphicsReducer._level <= ReduceGraphicsLevel.Level1)
			{
				return false;
			}
			if (_this.isHide)
			{
				return true;
			}
			if (_this.isMafuba)
			{
				return true;
			}
			if (_this.isShadown && _this.status != 0)
			{
				_this.paintShadow(g);
			}
			if (!_this.isPaint() || (_this.status == 1 && _this.p3 > 0 && GameCanvas.gameTick % 3 == 0))
			{
				return true;
			}
			if (GraphicsReducer._level >= ReduceGraphicsLevel.Level3)
			{
				return true;
			}
			g.translate(0, GameCanvas.transY);
			g.setColor(Color.yellow);
			if (_this.levelBoss != 0)
			{
				g.setColor(Color.red);
			}
			g.drawRect(Mathf.RoundToInt((float)(_this.x - _this.w / 2)), _this.y - _this.h - 15, _this.w, _this.h);
			g.translate(0, -GameCanvas.transY);
			if (global::Char.myCharz().mobFocus == null || global::Char.myCharz().mobFocus != _this || _this.status == 1 || _this.hp <= 0 || _this.imgHPtem == null)
			{
				return true;
			}
			int imageWidth = mGraphics.getImageWidth(_this.imgHPtem);
			int imageHeight = mGraphics.getImageHeight(_this.imgHPtem);
			int num = imageWidth * _this.per / 100;
			int num2 = num;
			if (_this.per_tem >= _this.per)
			{
				int num3 = imageWidth;
				int per_tem = _this.per_tem;
				int num4;
				if (GameCanvas.gameTick % 6 > 3)
				{
					int offset = _this.offset;
					_this.offset = offset + 1;
					num4 = offset;
				}
				else
				{
					num4 = _this.offset;
				}
				num2 = num3 * (_this.per_tem = per_tem - num4) / 100;
				if (_this.per_tem <= 0)
				{
					_this.per_tem = 0;
				}
				if (_this.per_tem < _this.per)
				{
					_this.per_tem = _this.per;
				}
				if (_this.offset >= 3)
				{
					_this.offset = 3;
				}
			}
			g.drawImage(GameScr.imgHP_tm_xam, _this.x - (imageWidth >> 1), _this.y - _this.h - 5, mGraphics.TOP | mGraphics.LEFT);
			g.setColor(16777215);
			g.fillRect(_this.x - (imageWidth >> 1), _this.y - _this.h - 5, num2, 2);
			g.drawRegion(_this.imgHPtem, 0, 0, num, imageHeight, 0, _this.x - (imageWidth >> 1), _this.y - _this.h - 5, mGraphics.TOP | mGraphics.LEFT);
			return true;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000B3FF4 File Offset: 0x000B21F4
		internal static bool OnMagicTreePaint(MagicTree _this, mGraphics g)
		{
			if (GraphicsReducer._level < ReduceGraphicsLevel.Level2)
			{
				return false;
			}
			if (_this.id == 0)
			{
				return true;
			}
			if (GraphicsReducer._level == ReduceGraphicsLevel.Level2)
			{
				g.setColor(Color.green);
				g.drawRect(_this.cx - 12, _this.cy - SmallImage.smallImg[_this.id][4], 24, SmallImage.smallImg[_this.id][4]);
			}
			if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus == _this)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, _this.cx, _this.cy - SmallImage.smallImg[_this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
				if (_this.name != null)
				{
					mFont.tahoma_7b_white.drawString(g, _this.name, _this.cx, _this.cy - SmallImage.smallImg[_this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else if (_this.name != null)
			{
				mFont.tahoma_7b_white.drawString(g, _this.name, _this.cx, _this.cy - SmallImage.smallImg[_this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
			}
			try
			{
				for (int i = 0; i < _this.currPeas; i++)
				{
					g.setColor(Color.cyan);
					g.drawRect(_this.cx + _this.peaPostionX[i] - SmallImage.smallImg[_this.id][3] / 2, _this.cy + _this.peaPostionY[i] - SmallImage.smallImg[_this.id][4], Image.getImageWidth(MagicTree.pea), Image.getImageHeight(MagicTree.pea));
				}
			}
			catch
			{
			}
			if (_this.indexEffTask < 0 || _this.effTask == null || _this.cTypePk != 0)
			{
				return true;
			}
			SmallImage.drawSmallImage(g, _this.effTask.arrEfInfo[_this.indexEffTask].idImg, _this.cx + _this.effTask.arrEfInfo[_this.indexEffTask].dx, _this.cy - 15 + _this.effTask.arrEfInfo[_this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				_this.indexEffTask++;
				if (_this.indexEffTask >= _this.effTask.arrEfInfo.Length)
				{
					_this.indexEffTask = 0;
				}
			}
			return true;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000B4278 File Offset: 0x000B2478
		internal static bool OnItemMapPaint(ItemMap _this, mGraphics g)
		{
			if (_this.template.type != ItemTemplateType.Satellite)
			{
				return false;
			}
			if (GraphicsReducer._level > ReduceGraphicsLevel.Level2)
			{
				return true;
			}
			if (GraphicsReducer._level > ReduceGraphicsLevel.Level1)
			{
				g.drawImage(TileMap.bong, _this.x + 3, _this.y, mGraphics.VCENTER | mGraphics.HCENTER);
				g.setColor(Color.gray);
				g.drawRect(_this.x - 12, _this.y - Image.getImageHeight(ItemMap.imageAuraItem1), 24, Image.getImageHeight(ItemMap.imageAuraItem1));
				return true;
			}
			return false;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000B430C File Offset: 0x000B250C
		internal static bool OnInfoMePaint(InfoMe _this, mGraphics g)
		{
			if (GraphicsReducer._level <= ReduceGraphicsLevel.Level1)
			{
				return false;
			}
			if (_this.info.info == null || _this.info.info.charInfo != null || _this.charId == null)
			{
				return false;
			}
			if ((_this == GameScr.info2 && GameScr.gI().isVS()) || (_this == GameScr.info2 && GameScr.gI().popUpYesNo != null) || (!GameScr.isPaint || (GameCanvas.currentScreen != GameScr.gI() && GameCanvas.currentScreen != CrackBallScr.gI())) || ChatPopup.serverChatPopUp != null || !_this.isUpdate || global::Char.ischangingMap || (GameCanvas.panel.isShow && _this == GameScr.info2))
			{
				return true;
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			Info info = _this.info;
			if (info != null)
			{
				info.paint(g, _this.cmx, _this.cmy, _this.dir);
			}
			g.setColor(14381226);
			if (global::Char.myCharz().cgender == 0)
			{
				g.setColor(new Color(0.2f, 0.66f, 0.92f));
			}
			if (global::Char.myCharz().cgender == 1)
			{
				g.setColor(4560421);
			}
			g.drawRect(_this.cmtoX - 10, _this.cmtoY - 4 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 15, 15);
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			return true;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnGameScrPaintBgItem()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnGameScrPaintEffect()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnEffectPaint()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000B4494 File Offset: 0x000B2694
		internal static bool OnCharPaintCharBody(global::Char _this, mGraphics g, int cx, int cy, int cdir, bool isPaintBag)
		{
			if (GraphicsReducer._level > ReduceGraphicsLevel.Level2)
			{
				return true;
			}
			if (GraphicsReducer._level <= ReduceGraphicsLevel.Level1)
			{
				return false;
			}
			if (_this.bag >= 0 && _this.statusMe != 14 && _this.isMonkey == 0)
			{
				if (!ClanImage.idImages.containsKey(_this.bag.ToString() + string.Empty))
				{
					ClanImage.idImages.put(_this.bag.ToString() + string.Empty, new ClanImage());
					Service.gI().requestBagImage((sbyte)_this.bag);
				}
				else
				{
					ClanImage clanImage = (ClanImage)ClanImage.idImages.get(_this.bag.ToString() + string.Empty);
					if (clanImage.idImage != null && isPaintBag)
					{
						_this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
					}
				}
			}
			g.setColor(Color.white);
			if (_this.me)
			{
				g.setColor(Color.blue);
			}
			if (_this.IsPet())
			{
				g.setColor(Color.cyan);
			}
			else if (_this.cTypePk == 5)
			{
				g.setColor(Color.red);
				if ((_this.isCharge || _this.isFlyAndCharge || _this.isStandAndCharge) && GameCanvas.gameTick % 8 >= 4)
				{
					g.setColor(Color.white);
				}
			}
			int num = 35;
			int num2 = 12;
			if (_this.IsPet())
			{
				num = 30;
			}
			if (_this.cTypePk == 5)
			{
				num2 = 15;
				num = 40;
			}
			g.drawRect(cx - num2, cy - num, num2 * 2, num);
			if (_this.statusMe == 14)
			{
				if (GameCanvas.gameTick % 4 > 0)
				{
					g.drawImage(ItemMap.imageFlare, cx, cy - _this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				SmallImage.drawSmallImage(g, 79, cx, cy - _this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
			}
			if (_this.protectEff)
			{
				g.setColor(Color.green);
				g.drawRect(cx - 35, cy - 55, 70, 70);
			}
			if (_this.cFlag != 0 && _this.cFlag != -1)
			{
				SmallImage.drawSmallImage(g, _this.flagImage, cx - ((cdir == 1) ? 10 : 0), cy - _this.ch - 30 + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 0, 0);
			}
			return true;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintMapLine()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEffect()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEff_Pet()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEff_LvUp_Front()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEff_LvUp_Behind()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEffFront()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintEffBehind()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintAuraFront()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintAuraBehind()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintSuperEffFront()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintSuperEffBehind()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharPaintMount2()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000B46E4 File Offset: 0x000B28E4
		internal static bool OnCharPaintMount1(global::Char _this, mGraphics g)
		{
			if (GraphicsReducer._level <= ReduceGraphicsLevel.Level1)
			{
				return false;
			}
			if (_this.xMount <= GameScr.cmx || _this.xMount >= GameScr.cmx + GameCanvas.w)
			{
				return true;
			}
			g.setColor(65421);
			g.drawRect(_this.xMount - 20, _this.yMount, 40, 15);
			return true;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000B4742 File Offset: 0x000B2942
		internal static bool OnCharPaint()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level2;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000B3B29 File Offset: 0x000B1D29
		internal static bool OnCharUpdateSuperEff()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Level1;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBgItemPaint()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBackgroundEffectAddEffect()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBackgroundEffectPaintFog()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBackgroundEffectPaintCloud2()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBackgroundEffectUpdateFog()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000B3D74 File Offset: 0x000B1F74
		internal static bool OnBackgroundEffectUpdateCloud2()
		{
			return GraphicsReducer._level > ReduceGraphicsLevel.Off;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000B474F File Offset: 0x000B294F
		internal static bool OnBackgroundEffectInitCloud()
		{
			if (GraphicsReducer._level > ReduceGraphicsLevel.Off)
			{
				BackgroudEffect.imgCloud1 = null;
				BackgroudEffect.imgFog = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000B4768 File Offset: 0x000B2968
		internal static bool ShouldDrawImage(Image image)
		{
			return GraphicsReducer.IsEnabled && image != TileMap.imgLight;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000B4780 File Offset: 0x000B2980
		internal static void InitializeTileMap(bool isFill)
		{
			if (isFill == GraphicsReducer.lastIsFill && GraphicsReducer.mapTile != null)
			{
				return;
			}
			GraphicsReducer.lastIsFill = isFill;
			GraphicsReducer.mapTile.w = (GraphicsReducer.mapTile.h = 25 * mGraphics.zoomLevel);
			GraphicsReducer.mapTile.texture = new Texture2D(25 * mGraphics.zoomLevel, 25 * mGraphics.zoomLevel);
			for (int i = 0; i < GraphicsReducer.mapTile.texture.width; i++)
			{
				for (int j = 0; j < GraphicsReducer.mapTile.texture.height; j++)
				{
					GraphicsReducer.mapTile.texture.SetPixel(i, j, isFill ? GraphicsReducer.colorMap : Color.clear);
				}
			}
			if (!isFill)
			{
				for (int k = 0; k < GraphicsReducer.mapTile.texture.width; k++)
				{
					for (int l = 0; l < mGraphics.zoomLevel; l++)
					{
						GraphicsReducer.mapTile.texture.SetPixel(k, l, GraphicsReducer.colorMap);
						GraphicsReducer.mapTile.texture.SetPixel(l, k, GraphicsReducer.colorMap);
						GraphicsReducer.mapTile.texture.SetPixel(GraphicsReducer.mapTile.texture.width - l, k, GraphicsReducer.colorMap);
						GraphicsReducer.mapTile.texture.SetPixel(k, GraphicsReducer.mapTile.texture.height - l, GraphicsReducer.colorMap);
					}
				}
			}
			GraphicsReducer.mapTile.texture.Apply();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000B4900 File Offset: 0x000B2B00
		internal static void PaintTileMap(mGraphics g)
		{
			GraphicsReducer.InitializeTileMap(GraphicsReducer._level == ReduceGraphicsLevel.Level1);
			for (int i = 2; i < TileMap.tmw - 2; i++)
			{
				for (int j = 0; j < TileMap.tmh - 1; j++)
				{
					if (TileMap.maps[j * TileMap.tmw + i] != 0 && (TileMap.tileTypeAt(i * (int)TileMap.size, j * (int)TileMap.size, 2) || (GraphicsReducer._level < ReduceGraphicsLevel.Level2 && GraphicsReducer.IsTileMapICantEnter(i * (int)TileMap.size, j * (int)TileMap.size))))
					{
						int num = 0;
						int num2 = j + 1;
						while (num2 < TileMap.tmh - 1 && TileMap.maps[num2 * TileMap.tmw + i] != 0)
						{
							if (i + 1 < TileMap.tmw && (TileMap.tileTypeAt((i + 1) * (int)TileMap.size, num2 * (int)TileMap.size, 2) || (GraphicsReducer._level < ReduceGraphicsLevel.Level2 && GraphicsReducer.IsTileMapICantEnter((i + 1) * (int)TileMap.size, num2 * (int)TileMap.size))))
							{
								num = Math.Max(num, num2);
							}
							if (i > 0 && (TileMap.tileTypeAt((i - 1) * (int)TileMap.size, num2 * (int)TileMap.size, 2) || (GraphicsReducer._level < ReduceGraphicsLevel.Level2 && GraphicsReducer.IsTileMapICantEnter((i - 1) * (int)TileMap.size, num2 * (int)TileMap.size))))
							{
								num = Math.Max(num, num2);
							}
							num2++;
						}
						for (int k = j; k < num + 1; k++)
						{
							if (i >= GameScr.gssx && i <= GameScr.gssxe && k >= GameScr.gssy && k <= GameScr.gssye)
							{
								g.drawImage(GraphicsReducer.mapTile, i * (int)TileMap.size, k * (int)TileMap.size + 8);
							}
						}
					}
				}
			}
			for (int l = GameScr.gssx + 1; l < GameScr.gssxe; l++)
			{
				for (int m = GameScr.gssy; m < GameScr.gssye; m++)
				{
					if (TileMap.maps[m * TileMap.tmw + l] != 0 && (TileMap.tileTypeAt(l * (int)TileMap.size, m * (int)TileMap.size, 2) || (GraphicsReducer._level < ReduceGraphicsLevel.Level2 && GraphicsReducer.IsTileMapICantEnter(l * (int)TileMap.size, m * (int)TileMap.size))))
					{
						g.drawImage(GraphicsReducer.mapTile, l * (int)TileMap.size, m * (int)TileMap.size + 8);
					}
				}
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000A3536 File Offset: 0x000A1736
		private static bool IsTileMapICantEnter(int px, int py)
		{
			return TileMap.tileTypeAt(px, py, 4) || TileMap.tileTypeAt(px, py, 8) || TileMap.tileTypeAt(px, py, 8192);
		}

		// Token: 0x040017B9 RID: 6073
		private static bool lastIsFill;

		// Token: 0x040017BA RID: 6074
		private static Image mapTile = new Image();

		// Token: 0x040017BB RID: 6075
		private static Color colorMap = new Color(0f, 0.21f, 0.78f, 1f);

		// Token: 0x040017BC RID: 6076
		private static ReduceGraphicsLevel _level;
	}
}

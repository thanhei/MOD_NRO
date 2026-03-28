using System;

// Token: 0x02000098 RID: 152
public class mResources
{
	// Token: 0x060004CC RID: 1228 RVA: 0x00006BDD File Offset: 0x00004DDD
	public static void loadLanguague()
	{
		mResources.loadLanguague(1);
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x000300E4 File Offset: 0x0002E2E4
	public static void loadLanguague(sbyte newLanguage)
	{
		mResources.language = newLanguage;
		sbyte b = mResources.language;
		if (b != 0)
		{
			if (b != 1)
			{
				if (b == 2)
				{
					LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
					T3.load();
					ServerListScreen.linkweb = "http://dragonball.indonaga.com";
				}
			}
			else
			{
				LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
				T2.load();
				ServerListScreen.linkweb = "http://world.teamobi.com";
			}
		}
		else
		{
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1.png");
			T1.load();
			ServerListScreen.linkweb = "http://ngocrongonline.com";
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00006BE5 File Offset: 0x00004DE5
	public static string replace(string str, string replacement)
	{
		return NinjaUtil.replace(str, "#", replacement);
	}

	// Token: 0x04000861 RID: 2145
	public static string chooseDefaultsv = string.Empty;

	// Token: 0x04000862 RID: 2146
	public static string winLose = string.Empty;

	// Token: 0x04000863 RID: 2147
	public static string learnSkill = string.Empty;

	// Token: 0x04000864 RID: 2148
	public static string updSkill = string.Empty;

	// Token: 0x04000865 RID: 2149
	public static string proficiency = string.Empty;

	// Token: 0x04000866 RID: 2150
	public static string delacc = string.Empty;

	// Token: 0x04000867 RID: 2151
	public static string notiINAPP = string.Empty;

	// Token: 0x04000868 RID: 2152
	public static string notiRuby = string.Empty;

	// Token: 0x04000869 RID: 2153
	public static string equip = string.Empty;

	// Token: 0x0400086A RID: 2154
	public static string unlock = string.Empty;

	// Token: 0x0400086B RID: 2155
	public static string radaCard = string.Empty;

	// Token: 0x0400086C RID: 2156
	public static string not_enough_money_1 = string.Empty;

	// Token: 0x0400086D RID: 2157
	public static string napngoc = string.Empty;

	// Token: 0x0400086E RID: 2158
	public static string functionMaintain1 = string.Empty;

	// Token: 0x0400086F RID: 2159
	public static string tang;

	// Token: 0x04000870 RID: 2160
	public static string kquaVongQuay;

	// Token: 0x04000871 RID: 2161
	public static string useGem;

	// Token: 0x04000872 RID: 2162
	public static string autoFunction;

	// Token: 0x04000873 RID: 2163
	public static string choitiep;

	// Token: 0x04000874 RID: 2164
	public static string attack;

	// Token: 0x04000875 RID: 2165
	public static string defend;

	// Token: 0x04000876 RID: 2166
	public static string follow;

	// Token: 0x04000877 RID: 2167
	public static string status;

	// Token: 0x04000878 RID: 2168
	public static string gohome;

	// Token: 0x04000879 RID: 2169
	public static string pet;

	// Token: 0x0400087A RID: 2170
	public static string maychutathoacmatsong;

	// Token: 0x0400087B RID: 2171
	public static string cauhinhthap;

	// Token: 0x0400087C RID: 2172
	public static string cauhinhcao;

	// Token: 0x0400087D RID: 2173
	public static string combineSpell;

	// Token: 0x0400087E RID: 2174
	public static string combineFail;

	// Token: 0x0400087F RID: 2175
	public static string combineSuccess;

	// Token: 0x04000880 RID: 2176
	public static string turnOnAnalog;

	// Token: 0x04000881 RID: 2177
	public static string turnOffAnalog;

	// Token: 0x04000882 RID: 2178
	public static string analog;

	// Token: 0x04000883 RID: 2179
	public static string inventory_Pass;

	// Token: 0x04000884 RID: 2180
	public static string input_Inventory_Pass;

	// Token: 0x04000885 RID: 2181
	public static string input_Inventory_Pass_wrong = string.Empty;

	// Token: 0x04000886 RID: 2182
	public static string REGISTOPROTECT = string.Empty;

	// Token: 0x04000887 RID: 2183
	public static string turnOnSound = string.Empty;

	// Token: 0x04000888 RID: 2184
	public static string turnOffSound = string.Empty;

	// Token: 0x04000889 RID: 2185
	public static string REGISTERING = string.Empty;

	// Token: 0x0400088A RID: 2186
	public static string SENDINGMSG = string.Empty;

	// Token: 0x0400088B RID: 2187
	public static string SENTMSG = string.Empty;

	// Token: 0x0400088C RID: 2188
	public static string NOSENDMSG = string.Empty;

	// Token: 0x0400088D RID: 2189
	public static string sendMsgSuccess = string.Empty;

	// Token: 0x0400088E RID: 2190
	public static string cannotSendMsg = string.Empty;

	// Token: 0x0400088F RID: 2191
	public static string sendGuessMsgSuccess = string.Empty;

	// Token: 0x04000890 RID: 2192
	public static string sendMsgFail = string.Empty;

	// Token: 0x04000891 RID: 2193
	public static string ALERT_PRIVATE_PASS_1 = string.Empty;

	// Token: 0x04000892 RID: 2194
	public static string ALERT_PRIVATE_PASS_2 = string.Empty;

	// Token: 0x04000893 RID: 2195
	public static string INPUT_PRIVATE_PASS = string.Empty;

	// Token: 0x04000894 RID: 2196
	public static string change_account = string.Empty;

	// Token: 0x04000895 RID: 2197
	public static string alreadyHadAccount1 = string.Empty;

	// Token: 0x04000896 RID: 2198
	public static string alreadyHadAccount2 = string.Empty;

	// Token: 0x04000897 RID: 2199
	public static string userBlank = string.Empty;

	// Token: 0x04000898 RID: 2200
	public static string passwordBlank = string.Empty;

	// Token: 0x04000899 RID: 2201
	public static string accTooShort = string.Empty;

	// Token: 0x0400089A RID: 2202
	public static string phoneInvalid = string.Empty;

	// Token: 0x0400089B RID: 2203
	public static string emailInvalid = string.Empty;

	// Token: 0x0400089C RID: 2204
	public static string registerNewAcc = string.Empty;

	// Token: 0x0400089D RID: 2205
	public static string selectServer = string.Empty;

	// Token: 0x0400089E RID: 2206
	public static string selectServer2 = string.Empty;

	// Token: 0x0400089F RID: 2207
	public static string forgetPass = string.Empty;

	// Token: 0x040008A0 RID: 2208
	public static string password = string.Empty;

	// Token: 0x040008A1 RID: 2209
	public static string[] LOGINLABELS = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040008A2 RID: 2210
	public static string msg = string.Empty;

	// Token: 0x040008A3 RID: 2211
	public static string[] msgg = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040008A4 RID: 2212
	public static string no_msg = string.Empty;

	// Token: 0x040008A5 RID: 2213
	public static string cancelAccountProtection = string.Empty;

	// Token: 0x040008A6 RID: 2214
	public static string plsCheckAcc = string.Empty;

	// Token: 0x040008A7 RID: 2215
	public static string phone = string.Empty;

	// Token: 0x040008A8 RID: 2216
	public static string email = string.Empty;

	// Token: 0x040008A9 RID: 2217
	public static string acc = string.Empty;

	// Token: 0x040008AA RID: 2218
	public static string pwd = string.Empty;

	// Token: 0x040008AB RID: 2219
	public static string goToWebForPassword = string.Empty;

	// Token: 0x040008AC RID: 2220
	public static string dragon_ball = string.Empty;

	// Token: 0x040008AD RID: 2221
	public static string character = string.Empty;

	// Token: 0x040008AE RID: 2222
	public static string account = string.Empty;

	// Token: 0x040008AF RID: 2223
	public static string account_server = string.Empty;

	// Token: 0x040008B0 RID: 2224
	public static string char_name_blank = string.Empty;

	// Token: 0x040008B1 RID: 2225
	public static string char_name_short = string.Empty;

	// Token: 0x040008B2 RID: 2226
	public static string char_name_long = string.Empty;

	// Token: 0x040008B3 RID: 2227
	public static string changeNameChar = string.Empty;

	// Token: 0x040008B4 RID: 2228
	public static string char_name = string.Empty;

	// Token: 0x040008B5 RID: 2229
	public static string login = string.Empty;

	// Token: 0x040008B6 RID: 2230
	public static string login2 = string.Empty;

	// Token: 0x040008B7 RID: 2231
	public static string register = string.Empty;

	// Token: 0x040008B8 RID: 2232
	public static string WAIT = string.Empty;

	// Token: 0x040008B9 RID: 2233
	public static string PLEASEWAIT = string.Empty;

	// Token: 0x040008BA RID: 2234
	public static string CONNECTING = string.Empty;

	// Token: 0x040008BB RID: 2235
	public static string LOGGING = string.Empty;

	// Token: 0x040008BC RID: 2236
	public static string LOADING = string.Empty;

	// Token: 0x040008BD RID: 2237
	public static string downloading_data = string.Empty;

	// Token: 0x040008BE RID: 2238
	public static string select_server = string.Empty;

	// Token: 0x040008BF RID: 2239
	public static string pls_restart_game_error = string.Empty;

	// Token: 0x040008C0 RID: 2240
	public static string pls_restart_game_error2 = string.Empty;

	// Token: 0x040008C1 RID: 2241
	public static string lost_connection = string.Empty;

	// Token: 0x040008C2 RID: 2242
	public static string check_3G = string.Empty;

	// Token: 0x040008C3 RID: 2243
	public static string UPDATE = string.Empty;

	// Token: 0x040008C4 RID: 2244
	public static string change_zone = string.Empty;

	// Token: 0x040008C5 RID: 2245
	public static string select_zone = string.Empty;

	// Token: 0x040008C6 RID: 2246
	public static string website = string.Empty;

	// Token: 0x040008C7 RID: 2247
	public static string server = string.Empty;

	// Token: 0x040008C8 RID: 2248
	public static string planet = string.Empty;

	// Token: 0x040008C9 RID: 2249
	public static string[] MENUME = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008CA RID: 2250
	public static string[] MENUNEWCHAR = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008CB RID: 2251
	public static string[] MENUGENDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008CC RID: 2252
	public static string[] CHAR_ORDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008CD RID: 2253
	public static string[][] mainTab1 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x040008CE RID: 2254
	public static string[][] mainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x040008CF RID: 2255
	public static string[][] petMainTab = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x040008D0 RID: 2256
	public static string[][] petMainTab2 = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x040008D1 RID: 2257
	public static string[] key_skill_qwerty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008D2 RID: 2258
	public static string[] key_skill = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008D3 RID: 2259
	public static string SKILL_FAIL = string.Empty;

	// Token: 0x040008D4 RID: 2260
	public static string HP_EMPTY = string.Empty;

	// Token: 0x040008D5 RID: 2261
	public static string ZONE_HERE = string.Empty;

	// Token: 0x040008D6 RID: 2262
	public static string[] DES_TASK = new string[]
	{
		" ",
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008D7 RID: 2263
	public static string[] DIES = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008D8 RID: 2264
	public static string[] SYNTHESIS = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008D9 RID: 2265
	public static string[] tips = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x040008DA RID: 2266
	public static string TASK_INPUT_CLASS = string.Empty;

	// Token: 0x040008DB RID: 2267
	public static string SERI_NUM = string.Empty;

	// Token: 0x040008DC RID: 2268
	public static string CARD_CODE = string.Empty;

	// Token: 0x040008DD RID: 2269
	public static string pay_card = string.Empty;

	// Token: 0x040008DE RID: 2270
	public static string pay_card2 = string.Empty;

	// Token: 0x040008DF RID: 2271
	public static string serial_blank = string.Empty;

	// Token: 0x040008E0 RID: 2272
	public static string card_code_blank = string.Empty;

	// Token: 0x040008E1 RID: 2273
	public static string billion = string.Empty;

	// Token: 0x040008E2 RID: 2274
	public static string million = string.Empty;

	// Token: 0x040008E3 RID: 2275
	public static string MENU = string.Empty;

	// Token: 0x040008E4 RID: 2276
	public static string CLOSE = string.Empty;

	// Token: 0x040008E5 RID: 2277
	public static string ON = string.Empty;

	// Token: 0x040008E6 RID: 2278
	public static string OFF = string.Empty;

	// Token: 0x040008E7 RID: 2279
	public static string ENABLE = string.Empty;

	// Token: 0x040008E8 RID: 2280
	public static string DELETE = string.Empty;

	// Token: 0x040008E9 RID: 2281
	public static string VIEW = string.Empty;

	// Token: 0x040008EA RID: 2282
	public static string CONTINUE = string.Empty;

	// Token: 0x040008EB RID: 2283
	public static string NEXTSTEP = string.Empty;

	// Token: 0x040008EC RID: 2284
	public static string USE = string.Empty;

	// Token: 0x040008ED RID: 2285
	public static string SORT = string.Empty;

	// Token: 0x040008EE RID: 2286
	public static string YES = string.Empty;

	// Token: 0x040008EF RID: 2287
	public static string NO = string.Empty;

	// Token: 0x040008F0 RID: 2288
	public static string EXIT = string.Empty;

	// Token: 0x040008F1 RID: 2289
	public static string CHAT = string.Empty;

	// Token: 0x040008F2 RID: 2290
	public static string REVENGE = string.Empty;

	// Token: 0x040008F3 RID: 2291
	public static string OK = string.Empty;

	// Token: 0x040008F4 RID: 2292
	public static string retry = string.Empty;

	// Token: 0x040008F5 RID: 2293
	public static string uncheck = string.Empty;

	// Token: 0x040008F6 RID: 2294
	public static string remember = string.Empty;

	// Token: 0x040008F7 RID: 2295
	public static string ACCEPT = string.Empty;

	// Token: 0x040008F8 RID: 2296
	public static string CANCEL = string.Empty;

	// Token: 0x040008F9 RID: 2297
	public static string SELECT = string.Empty;

	// Token: 0x040008FA RID: 2298
	public static string enter = string.Empty;

	// Token: 0x040008FB RID: 2299
	public static string open_link = string.Empty;

	// Token: 0x040008FC RID: 2300
	public static string DOYOUWANTEXIT = string.Empty;

	// Token: 0x040008FD RID: 2301
	public static string NEWCHAR = string.Empty;

	// Token: 0x040008FE RID: 2302
	public static string BACK = string.Empty;

	// Token: 0x040008FF RID: 2303
	public static string LOCKED = string.Empty;

	// Token: 0x04000900 RID: 2304
	public static string KILL = string.Empty;

	// Token: 0x04000901 RID: 2305
	public static string KILLBOSS = string.Empty;

	// Token: 0x04000902 RID: 2306
	public static string NOLOCK = string.Empty;

	// Token: 0x04000903 RID: 2307
	public static string XU = string.Empty;

	// Token: 0x04000904 RID: 2308
	public static string LUONG = string.Empty;

	// Token: 0x04000905 RID: 2309
	public static string RUBY = string.Empty;

	// Token: 0x04000906 RID: 2310
	public static string PK_NOW = string.Empty;

	// Token: 0x04000907 RID: 2311
	public static string CUU_SAT = string.Empty;

	// Token: 0x04000908 RID: 2312
	public static string NOT_ENOUGH_MP = string.Empty;

	// Token: 0x04000909 RID: 2313
	public static string you_receive = string.Empty;

	// Token: 0x0400090A RID: 2314
	public static string MONTH = string.Empty;

	// Token: 0x0400090B RID: 2315
	public static string WEEK = string.Empty;

	// Token: 0x0400090C RID: 2316
	public static string DAY = string.Empty;

	// Token: 0x0400090D RID: 2317
	public static string HOUR = string.Empty;

	// Token: 0x0400090E RID: 2318
	public static string SECOND = string.Empty;

	// Token: 0x0400090F RID: 2319
	public static string MINUTE = string.Empty;

	// Token: 0x04000910 RID: 2320
	public static string LEARN_SKILL = string.Empty;

	// Token: 0x04000911 RID: 2321
	public static string rank = string.Empty;

	// Token: 0x04000912 RID: 2322
	public static string active_point = string.Empty;

	// Token: 0x04000913 RID: 2323
	public static string friend = string.Empty;

	// Token: 0x04000914 RID: 2324
	public static string enemy = string.Empty;

	// Token: 0x04000915 RID: 2325
	public static string no_friend = string.Empty;

	// Token: 0x04000916 RID: 2326
	public static string chat_world = string.Empty;

	// Token: 0x04000917 RID: 2327
	public static string change_flag = string.Empty;

	// Token: 0x04000918 RID: 2328
	public static string gameInfo = string.Empty;

	// Token: 0x04000919 RID: 2329
	public static string quayso = string.Empty;

	// Token: 0x0400091A RID: 2330
	public static string option = string.Empty;

	// Token: 0x0400091B RID: 2331
	public static string high = string.Empty;

	// Token: 0x0400091C RID: 2332
	public static string medium = string.Empty;

	// Token: 0x0400091D RID: 2333
	public static string low = string.Empty;

	// Token: 0x0400091E RID: 2334
	public static string increase_vga = string.Empty;

	// Token: 0x0400091F RID: 2335
	public static string decrease_vga = string.Empty;

	// Token: 0x04000920 RID: 2336
	public static string serverchat_off = string.Empty;

	// Token: 0x04000921 RID: 2337
	public static string serverchat_on = string.Empty;

	// Token: 0x04000922 RID: 2338
	public static string x2Screen = string.Empty;

	// Token: 0x04000923 RID: 2339
	public static string x1Screen = string.Empty;

	// Token: 0x04000924 RID: 2340
	public static string changeSizeScreen = string.Empty;

	// Token: 0x04000925 RID: 2341
	public static string aura_off = string.Empty;

	// Token: 0x04000926 RID: 2342
	public static string aura_on = string.Empty;

	// Token: 0x04000927 RID: 2343
	public static string aura_off_2 = string.Empty;

	// Token: 0x04000928 RID: 2344
	public static string aura_on_2 = string.Empty;

	// Token: 0x04000929 RID: 2345
	public static string hat_off = string.Empty;

	// Token: 0x0400092A RID: 2346
	public static string hat_on = string.Empty;

	// Token: 0x0400092B RID: 2347
	public static string chest = string.Empty;

	// Token: 0x0400092C RID: 2348
	public static string[] chestt = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400092D RID: 2349
	public static string[] inventory = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400092E RID: 2350
	public static string[] combine = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400092F RID: 2351
	public static string[] mapp = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000930 RID: 2352
	public static string[] item_give = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000931 RID: 2353
	public static string[] item_receive = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000932 RID: 2354
	public static string[] zonee = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000933 RID: 2355
	public static string zone = string.Empty;

	// Token: 0x04000934 RID: 2356
	public static string map = string.Empty;

	// Token: 0x04000935 RID: 2357
	public static string item_receive2 = string.Empty;

	// Token: 0x04000936 RID: 2358
	public static string item = string.Empty;

	// Token: 0x04000937 RID: 2359
	public static string give_upper = string.Empty;

	// Token: 0x04000938 RID: 2360
	public static string receive_upper = string.Empty;

	// Token: 0x04000939 RID: 2361
	public static string receive_all = string.Empty;

	// Token: 0x0400093A RID: 2362
	public static string no_map = string.Empty;

	// Token: 0x0400093B RID: 2363
	public static string go_to_quest = string.Empty;

	// Token: 0x0400093C RID: 2364
	public static string from_earth = string.Empty;

	// Token: 0x0400093D RID: 2365
	public static string from_namec = string.Empty;

	// Token: 0x0400093E RID: 2366
	public static string from_sayda = string.Empty;

	// Token: 0x0400093F RID: 2367
	public static string expire = string.Empty;

	// Token: 0x04000940 RID: 2368
	public static string pow_request = string.Empty;

	// Token: 0x04000941 RID: 2369
	public static string your_pow = string.Empty;

	// Token: 0x04000942 RID: 2370
	public static string used = string.Empty;

	// Token: 0x04000943 RID: 2371
	public static string place = string.Empty;

	// Token: 0x04000944 RID: 2372
	public static string FOREVER = string.Empty;

	// Token: 0x04000945 RID: 2373
	public static string NOUPGRADE = string.Empty;

	// Token: 0x04000946 RID: 2374
	public static string NOTUPGRADE = string.Empty;

	// Token: 0x04000947 RID: 2375
	public static string UPGRADE = string.Empty;

	// Token: 0x04000948 RID: 2376
	public static string UPGRADING = string.Empty;

	// Token: 0x04000949 RID: 2377
	public static string make_shortcut = string.Empty;

	// Token: 0x0400094A RID: 2378
	public static string into_place = string.Empty;

	// Token: 0x0400094B RID: 2379
	public static string move_to_chest = string.Empty;

	// Token: 0x0400094C RID: 2380
	public static string move_to_chest2 = string.Empty;

	// Token: 0x0400094D RID: 2381
	public static string press_chat_querty = string.Empty;

	// Token: 0x0400094E RID: 2382
	public static string press_chat = string.Empty;

	// Token: 0x0400094F RID: 2383
	public static string saying = string.Empty;

	// Token: 0x04000950 RID: 2384
	public static string miss = string.Empty;

	// Token: 0x04000951 RID: 2385
	public static string donate = string.Empty;

	// Token: 0x04000952 RID: 2386
	public static string receive = string.Empty;

	// Token: 0x04000953 RID: 2387
	public static string press_twice = string.Empty;

	// Token: 0x04000954 RID: 2388
	public static string can_harvest = string.Empty;

	// Token: 0x04000955 RID: 2389
	public static string do_accept_qwerty = string.Empty;

	// Token: 0x04000956 RID: 2390
	public static string do_accept = string.Empty;

	// Token: 0x04000957 RID: 2391
	public static string plsRestartGame = string.Empty;

	// Token: 0x04000958 RID: 2392
	public static string is_online = string.Empty;

	// Token: 0x04000959 RID: 2393
	public static string is_offline = string.Empty;

	// Token: 0x0400095A RID: 2394
	public static string make_friend = string.Empty;

	// Token: 0x0400095B RID: 2395
	public static string chat_player = string.Empty;

	// Token: 0x0400095C RID: 2396
	public static string chat_with = string.Empty;

	// Token: 0x0400095D RID: 2397
	public static string clan_capsuledonate = string.Empty;

	// Token: 0x0400095E RID: 2398
	public static string clan_capsuleself = string.Empty;

	// Token: 0x0400095F RID: 2399
	public static string clan_point = string.Empty;

	// Token: 0x04000960 RID: 2400
	public static string give_pea = string.Empty;

	// Token: 0x04000961 RID: 2401
	public static string receive_pea = string.Empty;

	// Token: 0x04000962 RID: 2402
	public static string request_pea = string.Empty;

	// Token: 0x04000963 RID: 2403
	public static string time = string.Empty;

	// Token: 0x04000964 RID: 2404
	public static string received = string.Empty;

	// Token: 0x04000965 RID: 2405
	public static string power = string.Empty;

	// Token: 0x04000966 RID: 2406
	public static string join_date = string.Empty;

	// Token: 0x04000967 RID: 2407
	public static string clan_leader = string.Empty;

	// Token: 0x04000968 RID: 2408
	public static string clan_coleader = string.Empty;

	// Token: 0x04000969 RID: 2409
	public static string power_point = string.Empty;

	// Token: 0x0400096A RID: 2410
	public static string member = string.Empty;

	// Token: 0x0400096B RID: 2411
	public static string[] memberr = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400096C RID: 2412
	public static string[] chatClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400096D RID: 2413
	public static string[] leaveClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400096E RID: 2414
	public static string[] createClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400096F RID: 2415
	public static string[] findClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000970 RID: 2416
	public static string[] khau_hieuu = new string[]
	{
		string.Empty
	};

	// Token: 0x04000971 RID: 2417
	public static string[] bieu_tuongg = new string[]
	{
		string.Empty
	};

	// Token: 0x04000972 RID: 2418
	public static string[] request_pea2 = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04000973 RID: 2419
	public static string level = string.Empty;

	// Token: 0x04000974 RID: 2420
	public static string clan_birthday = string.Empty;

	// Token: 0x04000975 RID: 2421
	public static string clan_list = string.Empty;

	// Token: 0x04000976 RID: 2422
	public static string create = string.Empty;

	// Token: 0x04000977 RID: 2423
	public static string find = string.Empty;

	// Token: 0x04000978 RID: 2424
	public static string leave = string.Empty;

	// Token: 0x04000979 RID: 2425
	public static string not_join_clan = string.Empty;

	// Token: 0x0400097A RID: 2426
	public static string[] clanEmpty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400097B RID: 2427
	public static string input_clan_name = string.Empty;

	// Token: 0x0400097C RID: 2428
	public static string clan_name = string.Empty;

	// Token: 0x0400097D RID: 2429
	public static string chat_clan = string.Empty;

	// Token: 0x0400097E RID: 2430
	public static string input_clan_name_to_create = string.Empty;

	// Token: 0x0400097F RID: 2431
	public static string input_clan_slogan = string.Empty;

	// Token: 0x04000980 RID: 2432
	public static string do_u_want_join_clan = string.Empty;

	// Token: 0x04000981 RID: 2433
	public static string select_clan_icon = string.Empty;

	// Token: 0x04000982 RID: 2434
	public static string request_join_clan = string.Empty;

	// Token: 0x04000983 RID: 2435
	public static string view_clan_member = string.Empty;

	// Token: 0x04000984 RID: 2436
	public static string create_clan_co_leader = string.Empty;

	// Token: 0x04000985 RID: 2437
	public static string create_clan_leader = string.Empty;

	// Token: 0x04000986 RID: 2438
	public static string disable_clan_mastership = string.Empty;

	// Token: 0x04000987 RID: 2439
	public static string kick_clan_mem = string.Empty;

	// Token: 0x04000988 RID: 2440
	public static string clan_name_blank = string.Empty;

	// Token: 0x04000989 RID: 2441
	public static string clan_slogan_blank = string.Empty;

	// Token: 0x0400098A RID: 2442
	public static string cannot_find_clan = string.Empty;

	// Token: 0x0400098B RID: 2443
	public static string ago = string.Empty;

	// Token: 0x0400098C RID: 2444
	public static string findingClan = string.Empty;

	// Token: 0x0400098D RID: 2445
	public static string trade = string.Empty;

	// Token: 0x0400098E RID: 2446
	public static string not_lock_trade = string.Empty;

	// Token: 0x0400098F RID: 2447
	public static string not_lock_trade_upper = string.Empty;

	// Token: 0x04000990 RID: 2448
	public static string locked_trade = string.Empty;

	// Token: 0x04000991 RID: 2449
	public static string locked_trade_upper = string.Empty;

	// Token: 0x04000992 RID: 2450
	public static string lock_trade = string.Empty;

	// Token: 0x04000993 RID: 2451
	public static string wait_opp_lock_trade = string.Empty;

	// Token: 0x04000994 RID: 2452
	public static string press_done = string.Empty;

	// Token: 0x04000995 RID: 2453
	public static string THROW = string.Empty;

	// Token: 0x04000996 RID: 2454
	public static string SPLIT = string.Empty;

	// Token: 0x04000997 RID: 2455
	public static string done = string.Empty;

	// Token: 0x04000998 RID: 2456
	public static string opponent = string.Empty;

	// Token: 0x04000999 RID: 2457
	public static string you = string.Empty;

	// Token: 0x0400099A RID: 2458
	public static string mlock = string.Empty;

	// Token: 0x0400099B RID: 2459
	public static string money_trade = string.Empty;

	// Token: 0x0400099C RID: 2460
	public static string GETOUT = string.Empty;

	// Token: 0x0400099D RID: 2461
	public static string MOVEOUT = string.Empty;

	// Token: 0x0400099E RID: 2462
	public static string MOVEFORPET = string.Empty;

	// Token: 0x0400099F RID: 2463
	public static string GETOUTMONEY = string.Empty;

	// Token: 0x040009A0 RID: 2464
	public static string GETINMONEY = string.Empty;

	// Token: 0x040009A1 RID: 2465
	public static string SENDMONEY = string.Empty;

	// Token: 0x040009A2 RID: 2466
	public static string GETIN = string.Empty;

	// Token: 0x040009A3 RID: 2467
	public static string SALE = string.Empty;

	// Token: 0x040009A4 RID: 2468
	public static string SALES = string.Empty;

	// Token: 0x040009A5 RID: 2469
	public static string SALEALL = string.Empty;

	// Token: 0x040009A6 RID: 2470
	public static string BUY = string.Empty;

	// Token: 0x040009A7 RID: 2471
	public static string BUYS = string.Empty;

	// Token: 0x040009A8 RID: 2472
	public static string input_money_to_trade = string.Empty;

	// Token: 0x040009A9 RID: 2473
	public static string input_money = string.Empty;

	// Token: 0x040009AA RID: 2474
	public static string input_money_wrong = string.Empty;

	// Token: 0x040009AB RID: 2475
	public static string not_enough_money = string.Empty;

	// Token: 0x040009AC RID: 2476
	public static string input_quantity_to_trade = string.Empty;

	// Token: 0x040009AD RID: 2477
	public static string input_quantity = string.Empty;

	// Token: 0x040009AE RID: 2478
	public static string input_quantity_wrong = string.Empty;

	// Token: 0x040009AF RID: 2479
	public static string already_has_item = string.Empty;

	// Token: 0x040009B0 RID: 2480
	public static string unlock_item_to_trade = string.Empty;

	// Token: 0x040009B1 RID: 2481
	public static string root = string.Empty;

	// Token: 0x040009B2 RID: 2482
	public static string need = string.Empty;

	// Token: 0x040009B3 RID: 2483
	public static string need_upper = string.Empty;

	// Token: 0x040009B4 RID: 2484
	public static string free = string.Empty;

	// Token: 0x040009B5 RID: 2485
	public static string free1 = string.Empty;

	// Token: 0x040009B6 RID: 2486
	public static string free2 = string.Empty;

	// Token: 0x040009B7 RID: 2487
	public static string select_item = string.Empty;

	// Token: 0x040009B8 RID: 2488
	public static string random = string.Empty;

	// Token: 0x040009B9 RID: 2489
	public static string say_hello = string.Empty;

	// Token: 0x040009BA RID: 2490
	public static string say_wat_do_u_want_to_buy = string.Empty;

	// Token: 0x040009BB RID: 2491
	public static string say_wat_do_u_want_to_buy2 = string.Empty;

	// Token: 0x040009BC RID: 2492
	public static string do_u_sure_to_trade = string.Empty;

	// Token: 0x040009BD RID: 2493
	public static string learn_with = string.Empty;

	// Token: 0x040009BE RID: 2494
	public static string buy_with = string.Empty;

	// Token: 0x040009BF RID: 2495
	public static string can_not_do_when_die = string.Empty;

	// Token: 0x040009C0 RID: 2496
	public static string use_for_combine = string.Empty;

	// Token: 0x040009C1 RID: 2497
	public static string use_for_trade = string.Empty;

	// Token: 0x040009C2 RID: 2498
	public static string not_enough_luong_world_channel = string.Empty;

	// Token: 0x040009C3 RID: 2499
	public static string world_channel_5_luong = string.Empty;

	// Token: 0x040009C4 RID: 2500
	public static string want_to_trade = string.Empty;

	// Token: 0x040009C5 RID: 2501
	public static string hasJustUpgrade1 = string.Empty;

	// Token: 0x040009C6 RID: 2502
	public static string hasJustUpgrade2 = string.Empty;

	// Token: 0x040009C7 RID: 2503
	public static string potential_to_learn = string.Empty;

	// Token: 0x040009C8 RID: 2504
	public static string potential_point = string.Empty;

	// Token: 0x040009C9 RID: 2505
	public static string achievement_point = string.Empty;

	// Token: 0x040009CA RID: 2506
	public static string increase = string.Empty;

	// Token: 0x040009CB RID: 2507
	public static string increase_upper = string.Empty;

	// Token: 0x040009CC RID: 2508
	public static string not_enough_potential_point1 = string.Empty;

	// Token: 0x040009CD RID: 2509
	public static string not_enough_potential_point2 = string.Empty;

	// Token: 0x040009CE RID: 2510
	public static string use_potential_point_for1 = string.Empty;

	// Token: 0x040009CF RID: 2511
	public static string use_potential_point_for2 = string.Empty;

	// Token: 0x040009D0 RID: 2512
	public static string for_HP = string.Empty;

	// Token: 0x040009D1 RID: 2513
	public static string for_KI = string.Empty;

	// Token: 0x040009D2 RID: 2514
	public static string for_hit_point = string.Empty;

	// Token: 0x040009D3 RID: 2515
	public static string for_armor = string.Empty;

	// Token: 0x040009D4 RID: 2516
	public static string for_crit = string.Empty;

	// Token: 0x040009D5 RID: 2517
	public static string can_buy_from_Uron1 = string.Empty;

	// Token: 0x040009D6 RID: 2518
	public static string can_buy_from_Uron2 = string.Empty;

	// Token: 0x040009D7 RID: 2519
	public static string can_buy_from_Uron3 = string.Empty;

	// Token: 0x040009D8 RID: 2520
	public static string critdame = string.Empty;

	// Token: 0x040009D9 RID: 2521
	public static string giamsatthuong = string.Empty;

	// Token: 0x040009DA RID: 2522
	public static string HP = string.Empty;

	// Token: 0x040009DB RID: 2523
	public static string KI = string.Empty;

	// Token: 0x040009DC RID: 2524
	public static string hit_point = string.Empty;

	// Token: 0x040009DD RID: 2525
	public static string armor = string.Empty;

	// Token: 0x040009DE RID: 2526
	public static string vitality = string.Empty;

	// Token: 0x040009DF RID: 2527
	public static string critical = string.Empty;

	// Token: 0x040009E0 RID: 2528
	public static string cap_do = string.Empty;

	// Token: 0x040009E1 RID: 2529
	public static string KI_consume = string.Empty;

	// Token: 0x040009E2 RID: 2530
	public static string cooldown = string.Empty;

	// Token: 0x040009E3 RID: 2531
	public static string milisecond = string.Empty;

	// Token: 0x040009E4 RID: 2532
	public static string max_level_reach = string.Empty;

	// Token: 0x040009E5 RID: 2533
	public static string next_level_require = string.Empty;

	// Token: 0x040009E6 RID: 2534
	public static string potential = string.Empty;

	// Token: 0x040009E7 RID: 2535
	public static string not_learn = string.Empty;

	// Token: 0x040009E8 RID: 2536
	public static string learn_require = string.Empty;

	// Token: 0x040009E9 RID: 2537
	public static string learn = string.Empty;

	// Token: 0x040009EA RID: 2538
	public static string to_gain_20hp = string.Empty;

	// Token: 0x040009EB RID: 2539
	public static string to_gain_20mp = string.Empty;

	// Token: 0x040009EC RID: 2540
	public static string to_gain_1pow = string.Empty;

	// Token: 0x040009ED RID: 2541
	public static string[][] hairStyleName = new string[][]
	{
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		},
		new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		}
	};

	// Token: 0x040009EE RID: 2542
	public static string hp_ki_full = string.Empty;

	// Token: 0x040009EF RID: 2543
	public static string quest_place = string.Empty;

	// Token: 0x040009F0 RID: 2544
	public static string no_mission = string.Empty;

	// Token: 0x040009F1 RID: 2545
	public static string reward_mission = string.Empty;

	// Token: 0x040009F2 RID: 2546
	public static string achievement_mission = string.Empty;

	// Token: 0x040009F3 RID: 2547
	public static string trangbi = string.Empty;

	// Token: 0x040009F4 RID: 2548
	public static string wat_do_u_want = string.Empty;

	// Token: 0x040009F5 RID: 2549
	public static string off = string.Empty;

	// Token: 0x040009F6 RID: 2550
	public static string on = string.Empty;

	// Token: 0x040009F7 RID: 2551
	public static string select_map = string.Empty;

	// Token: 0x040009F8 RID: 2552
	public static string offPlease = string.Empty;

	// Token: 0x040009F9 RID: 2553
	public static string onPlease = string.Empty;

	// Token: 0x040009FA RID: 2554
	public static sbyte language;

	// Token: 0x040009FB RID: 2555
	public const sbyte VIETNAM = 0;

	// Token: 0x040009FC RID: 2556
	public const sbyte ENGLISH = 1;

	// Token: 0x040009FD RID: 2557
	public const sbyte INDONESIA = 2;

	// Token: 0x040009FE RID: 2558
	public static string choigame;

	// Token: 0x040009FF RID: 2559
	public static string no_enemy = string.Empty;

	// Token: 0x04000A00 RID: 2560
	public static string kigui;

	// Token: 0x04000A01 RID: 2561
	public static string kiguiXu;

	// Token: 0x04000A02 RID: 2562
	public static string kiguiLuong;

	// Token: 0x04000A03 RID: 2563
	public static string kiguiXuchat;

	// Token: 0x04000A04 RID: 2564
	public static string kiguiLuongchat;

	// Token: 0x04000A05 RID: 2565
	public static string huykigui;

	// Token: 0x04000A06 RID: 2566
	public static string nhantien;

	// Token: 0x04000A07 RID: 2567
	public static string dangban;

	// Token: 0x04000A08 RID: 2568
	public static string daban;

	// Token: 0x04000A09 RID: 2569
	public static string num;

	// Token: 0x04000A0A RID: 2570
	public static string upTop;

	// Token: 0x04000A0B RID: 2571
	public static string page;

	// Token: 0x04000A0C RID: 2572
	public static string getDown;

	// Token: 0x04000A0D RID: 2573
	public static string getUp;

	// Token: 0x04000A0E RID: 2574
	public static string notYetSell;

	// Token: 0x04000A0F RID: 2575
	public static string charger;

	// Token: 0x04000A10 RID: 2576
	public static string finishBomong;

	// Token: 0x04000A11 RID: 2577
	public static string note;

	// Token: 0x04000A12 RID: 2578
	public static string regNote;

	// Token: 0x04000A13 RID: 2579
	public static string remain;

	// Token: 0x04000A14 RID: 2580
	public static string faster;

	// Token: 0x04000A15 RID: 2581
	public static string fasterQuestion;

	// Token: 0x04000A16 RID: 2582
	public static string chuacotaikhoan;

	// Token: 0x04000A17 RID: 2583
	public static string taidulieudechoi;

	// Token: 0x04000A18 RID: 2584
	public static string huy;

	// Token: 0x04000A19 RID: 2585
	public static string taidulieu;

	// Token: 0x04000A1A RID: 2586
	public static string xoadulieu;

	// Token: 0x04000A1B RID: 2587
	public static string deletaDataNote;

	// Token: 0x04000A1C RID: 2588
	public static string playNew;

	// Token: 0x04000A1D RID: 2589
	public static string playAcc;

	// Token: 0x04000A1E RID: 2590
	public static string vuilongnhapduthongtin;

	// Token: 0x04000A1F RID: 2591
	public static string not_register_yet = string.Empty;

	// Token: 0x04000A20 RID: 2592
	public static string nhanngoc;

	// Token: 0x04000A21 RID: 2593
	public static string fusion;

	// Token: 0x04000A22 RID: 2594
	public static string sure_fusion;

	// Token: 0x04000A23 RID: 2595
	public static string fusionForever;

	// Token: 0x04000A24 RID: 2596
	public static string xinchucmung;

	// Token: 0x04000A25 RID: 2597
	public static string den;

	// Token: 0x04000A26 RID: 2598
	public static string nhatvatpham;

	// Token: 0x04000A27 RID: 2599
	public static string confirmChangeServer;

	// Token: 0x04000A28 RID: 2600
	public static string cauhinhthuong;

	// Token: 0x04000A29 RID: 2601
	public static string countDown_waitingroom;

	// Token: 0x04000A2A RID: 2602
	public static string potential_to_learn_tuyetKi = string.Empty;
}

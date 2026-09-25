using System;

// Token: 0x020000C5 RID: 197
public class mResources
{
	// Token: 0x06000A34 RID: 2612 RVA: 0x00090D67 File Offset: 0x0008EF67
	public static void loadLanguague()
	{
		mResources.loadLanguague(1);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00090D70 File Offset: 0x0008EF70
	public static void loadLanguague(sbyte newLanguage)
	{
		mResources.language = newLanguage;
		sbyte b = mResources.language;
		if (b != 0)
		{
			if (b == 1)
			{
				LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
				T2.load();
				ServerListScreen.linkweb = "http://world.teamobi.com";
				return;
			}
			if (b == 2)
			{
				LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1E.png");
				T3.load();
				ServerListScreen.linkweb = "http://dragonball.indonaga.com";
				return;
			}
		}
		else
		{
			LoginScr.imgTitle = GameCanvas.loadImage("/mainImage/logo1.png");
			T1.load();
			ServerListScreen.linkweb = "http://ngocrongonline.com";
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00090DF0 File Offset: 0x0008EFF0
	public static string replace(string str, string replacement)
	{
		return NinjaUtil.replace(str, "#", replacement);
	}

	// Token: 0x04001213 RID: 4627
	public static string chooseDefaultsv = string.Empty;

	// Token: 0x04001214 RID: 4628
	public static string winLose = string.Empty;

	// Token: 0x04001215 RID: 4629
	public static string learnSkill = string.Empty;

	// Token: 0x04001216 RID: 4630
	public static string updSkill = string.Empty;

	// Token: 0x04001217 RID: 4631
	public static string proficiency = string.Empty;

	// Token: 0x04001218 RID: 4632
	public static string delacc = string.Empty;

	// Token: 0x04001219 RID: 4633
	public static string notiINAPP = string.Empty;

	// Token: 0x0400121A RID: 4634
	public static string notiRuby = string.Empty;

	// Token: 0x0400121B RID: 4635
	public static string equip = string.Empty;

	// Token: 0x0400121C RID: 4636
	public static string unlock = string.Empty;

	// Token: 0x0400121D RID: 4637
	public static string radaCard = string.Empty;

	// Token: 0x0400121E RID: 4638
	public static string not_enough_money_1 = string.Empty;

	// Token: 0x0400121F RID: 4639
	public static string napngoc = string.Empty;

	// Token: 0x04001220 RID: 4640
	public static string functionMaintain1 = string.Empty;

	// Token: 0x04001221 RID: 4641
	public static string tang;

	// Token: 0x04001222 RID: 4642
	public static string kquaVongQuay;

	// Token: 0x04001223 RID: 4643
	public static string useGem;

	// Token: 0x04001224 RID: 4644
	public static string autoFunction;

	// Token: 0x04001225 RID: 4645
	public static string choitiep;

	// Token: 0x04001226 RID: 4646
	public static string attack;

	// Token: 0x04001227 RID: 4647
	public static string defend;

	// Token: 0x04001228 RID: 4648
	public static string follow;

	// Token: 0x04001229 RID: 4649
	public static string status;

	// Token: 0x0400122A RID: 4650
	public static string gohome;

	// Token: 0x0400122B RID: 4651
	public static string pet;

	// Token: 0x0400122C RID: 4652
	public static string maychutathoacmatsong;

	// Token: 0x0400122D RID: 4653
	public static string cauhinhthap;

	// Token: 0x0400122E RID: 4654
	public static string cauhinhcao;

	// Token: 0x0400122F RID: 4655
	public static string combineSpell;

	// Token: 0x04001230 RID: 4656
	public static string combineFail;

	// Token: 0x04001231 RID: 4657
	public static string combineSuccess;

	// Token: 0x04001232 RID: 4658
	public static string turnOnAnalog;

	// Token: 0x04001233 RID: 4659
	public static string turnOffAnalog;

	// Token: 0x04001234 RID: 4660
	public static string analog;

	// Token: 0x04001235 RID: 4661
	public static string inventory_Pass;

	// Token: 0x04001236 RID: 4662
	public static string input_Inventory_Pass;

	// Token: 0x04001237 RID: 4663
	public static string input_Inventory_Pass_wrong = string.Empty;

	// Token: 0x04001238 RID: 4664
	public static string REGISTOPROTECT = string.Empty;

	// Token: 0x04001239 RID: 4665
	public static string turnOnSound = string.Empty;

	// Token: 0x0400123A RID: 4666
	public static string turnOffSound = string.Empty;

	// Token: 0x0400123B RID: 4667
	public static string REGISTERING = string.Empty;

	// Token: 0x0400123C RID: 4668
	public static string SENDINGMSG = string.Empty;

	// Token: 0x0400123D RID: 4669
	public static string SENTMSG = string.Empty;

	// Token: 0x0400123E RID: 4670
	public static string NOSENDMSG = string.Empty;

	// Token: 0x0400123F RID: 4671
	public static string sendMsgSuccess = string.Empty;

	// Token: 0x04001240 RID: 4672
	public static string cannotSendMsg = string.Empty;

	// Token: 0x04001241 RID: 4673
	public static string sendGuessMsgSuccess = string.Empty;

	// Token: 0x04001242 RID: 4674
	public static string sendMsgFail = string.Empty;

	// Token: 0x04001243 RID: 4675
	public static string ALERT_PRIVATE_PASS_1 = string.Empty;

	// Token: 0x04001244 RID: 4676
	public static string ALERT_PRIVATE_PASS_2 = string.Empty;

	// Token: 0x04001245 RID: 4677
	public static string INPUT_PRIVATE_PASS = string.Empty;

	// Token: 0x04001246 RID: 4678
	public static string change_account = string.Empty;

	// Token: 0x04001247 RID: 4679
	public static string alreadyHadAccount1 = string.Empty;

	// Token: 0x04001248 RID: 4680
	public static string alreadyHadAccount2 = string.Empty;

	// Token: 0x04001249 RID: 4681
	public static string userBlank = string.Empty;

	// Token: 0x0400124A RID: 4682
	public static string passwordBlank = string.Empty;

	// Token: 0x0400124B RID: 4683
	public static string accTooShort = string.Empty;

	// Token: 0x0400124C RID: 4684
	public static string phoneInvalid = string.Empty;

	// Token: 0x0400124D RID: 4685
	public static string emailInvalid = string.Empty;

	// Token: 0x0400124E RID: 4686
	public static string registerNewAcc = string.Empty;

	// Token: 0x0400124F RID: 4687
	public static string selectServer = string.Empty;

	// Token: 0x04001250 RID: 4688
	public static string selectServer2 = string.Empty;

	// Token: 0x04001251 RID: 4689
	public static string forgetPass = string.Empty;

	// Token: 0x04001252 RID: 4690
	public static string password = string.Empty;

	// Token: 0x04001253 RID: 4691
	public static string[] LOGINLABELS = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001254 RID: 4692
	public static string msg = string.Empty;

	// Token: 0x04001255 RID: 4693
	public static string[] msgg = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001256 RID: 4694
	public static string no_msg = string.Empty;

	// Token: 0x04001257 RID: 4695
	public static string cancelAccountProtection = string.Empty;

	// Token: 0x04001258 RID: 4696
	public static string plsCheckAcc = string.Empty;

	// Token: 0x04001259 RID: 4697
	public static string phone = string.Empty;

	// Token: 0x0400125A RID: 4698
	public static string email = string.Empty;

	// Token: 0x0400125B RID: 4699
	public static string acc = string.Empty;

	// Token: 0x0400125C RID: 4700
	public static string pwd = string.Empty;

	// Token: 0x0400125D RID: 4701
	public static string goToWebForPassword = string.Empty;

	// Token: 0x0400125E RID: 4702
	public static string dragon_ball = string.Empty;

	// Token: 0x0400125F RID: 4703
	public static string character = string.Empty;

	// Token: 0x04001260 RID: 4704
	public static string account = string.Empty;

	// Token: 0x04001261 RID: 4705
	public static string account_server = string.Empty;

	// Token: 0x04001262 RID: 4706
	public static string char_name_blank = string.Empty;

	// Token: 0x04001263 RID: 4707
	public static string char_name_short = string.Empty;

	// Token: 0x04001264 RID: 4708
	public static string char_name_long = string.Empty;

	// Token: 0x04001265 RID: 4709
	public static string changeNameChar = string.Empty;

	// Token: 0x04001266 RID: 4710
	public static string char_name = string.Empty;

	// Token: 0x04001267 RID: 4711
	public static string login = string.Empty;

	// Token: 0x04001268 RID: 4712
	public static string login2 = string.Empty;

	// Token: 0x04001269 RID: 4713
	public static string register = string.Empty;

	// Token: 0x0400126A RID: 4714
	public static string WAIT = string.Empty;

	// Token: 0x0400126B RID: 4715
	public static string PLEASEWAIT = string.Empty;

	// Token: 0x0400126C RID: 4716
	public static string CONNECTING = string.Empty;

	// Token: 0x0400126D RID: 4717
	public static string LOGGING = string.Empty;

	// Token: 0x0400126E RID: 4718
	public static string LOADING = string.Empty;

	// Token: 0x0400126F RID: 4719
	public static string downloading_data = string.Empty;

	// Token: 0x04001270 RID: 4720
	public static string select_server = string.Empty;

	// Token: 0x04001271 RID: 4721
	public static string pls_restart_game_error = string.Empty;

	// Token: 0x04001272 RID: 4722
	public static string pls_restart_game_error2 = string.Empty;

	// Token: 0x04001273 RID: 4723
	public static string lost_connection = string.Empty;

	// Token: 0x04001274 RID: 4724
	public static string check_3G = string.Empty;

	// Token: 0x04001275 RID: 4725
	public static string UPDATE = string.Empty;

	// Token: 0x04001276 RID: 4726
	public static string change_zone = string.Empty;

	// Token: 0x04001277 RID: 4727
	public static string select_zone = string.Empty;

	// Token: 0x04001278 RID: 4728
	public static string website = string.Empty;

	// Token: 0x04001279 RID: 4729
	public static string server = string.Empty;

	// Token: 0x0400127A RID: 4730
	public static string planet = string.Empty;

	// Token: 0x0400127B RID: 4731
	public static string[] MENUME = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400127C RID: 4732
	public static string[] MENUNEWCHAR = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400127D RID: 4733
	public static string[] MENUGENDER = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400127E RID: 4734
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

	// Token: 0x0400127F RID: 4735
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

	// Token: 0x04001280 RID: 4736
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

	// Token: 0x04001281 RID: 4737
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

	// Token: 0x04001282 RID: 4738
	public static string[][] petMainTab2 = new string[][] { new string[]
	{
		string.Empty,
		string.Empty
	} };

	// Token: 0x04001283 RID: 4739
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

	// Token: 0x04001284 RID: 4740
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

	// Token: 0x04001285 RID: 4741
	public static string SKILL_FAIL = string.Empty;

	// Token: 0x04001286 RID: 4742
	public static string HP_EMPTY = string.Empty;

	// Token: 0x04001287 RID: 4743
	public static string ZONE_HERE = string.Empty;

	// Token: 0x04001288 RID: 4744
	public static string[] DES_TASK = new string[]
	{
		" ",
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x04001289 RID: 4745
	public static string[] DIES = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400128A RID: 4746
	public static string[] SYNTHESIS = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400128B RID: 4747
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

	// Token: 0x0400128C RID: 4748
	public static string TASK_INPUT_CLASS = string.Empty;

	// Token: 0x0400128D RID: 4749
	public static string SERI_NUM = string.Empty;

	// Token: 0x0400128E RID: 4750
	public static string CARD_CODE = string.Empty;

	// Token: 0x0400128F RID: 4751
	public static string pay_card = string.Empty;

	// Token: 0x04001290 RID: 4752
	public static string pay_card2 = string.Empty;

	// Token: 0x04001291 RID: 4753
	public static string serial_blank = string.Empty;

	// Token: 0x04001292 RID: 4754
	public static string card_code_blank = string.Empty;

	// Token: 0x04001293 RID: 4755
	public static string billion = string.Empty;

	// Token: 0x04001294 RID: 4756
	public static string million = string.Empty;

	// Token: 0x04001295 RID: 4757
	public static string MENU = string.Empty;

	// Token: 0x04001296 RID: 4758
	public static string CLOSE = string.Empty;

	// Token: 0x04001297 RID: 4759
	public static string ON = string.Empty;

	// Token: 0x04001298 RID: 4760
	public static string OFF = string.Empty;

	// Token: 0x04001299 RID: 4761
	public static string ENABLE = string.Empty;

	// Token: 0x0400129A RID: 4762
	public static string DELETE = string.Empty;

	// Token: 0x0400129B RID: 4763
	public static string VIEW = string.Empty;

	// Token: 0x0400129C RID: 4764
	public static string CONTINUE = string.Empty;

	// Token: 0x0400129D RID: 4765
	public static string NEXTSTEP = string.Empty;

	// Token: 0x0400129E RID: 4766
	public static string USE = string.Empty;

	// Token: 0x0400129F RID: 4767
	public static string SORT = string.Empty;

	// Token: 0x040012A0 RID: 4768
	public static string YES = string.Empty;

	// Token: 0x040012A1 RID: 4769
	public static string NO = string.Empty;

	// Token: 0x040012A2 RID: 4770
	public static string EXIT = string.Empty;

	// Token: 0x040012A3 RID: 4771
	public static string CHAT = string.Empty;

	// Token: 0x040012A4 RID: 4772
	public static string REVENGE = string.Empty;

	// Token: 0x040012A5 RID: 4773
	public static string OK = string.Empty;

	// Token: 0x040012A6 RID: 4774
	public static string retry = string.Empty;

	// Token: 0x040012A7 RID: 4775
	public static string uncheck = string.Empty;

	// Token: 0x040012A8 RID: 4776
	public static string remember = string.Empty;

	// Token: 0x040012A9 RID: 4777
	public static string ACCEPT = string.Empty;

	// Token: 0x040012AA RID: 4778
	public static string CANCEL = string.Empty;

	// Token: 0x040012AB RID: 4779
	public static string SELECT = string.Empty;

	// Token: 0x040012AC RID: 4780
	public static string enter = string.Empty;

	// Token: 0x040012AD RID: 4781
	public static string open_link = string.Empty;

	// Token: 0x040012AE RID: 4782
	public static string DOYOUWANTEXIT = string.Empty;

	// Token: 0x040012AF RID: 4783
	public static string NEWCHAR = string.Empty;

	// Token: 0x040012B0 RID: 4784
	public static string BACK = string.Empty;

	// Token: 0x040012B1 RID: 4785
	public static string LOCKED = string.Empty;

	// Token: 0x040012B2 RID: 4786
	public static string KILL = string.Empty;

	// Token: 0x040012B3 RID: 4787
	public static string KILLBOSS = string.Empty;

	// Token: 0x040012B4 RID: 4788
	public static string NOLOCK = string.Empty;

	// Token: 0x040012B5 RID: 4789
	public static string XU = string.Empty;

	// Token: 0x040012B6 RID: 4790
	public static string LUONG = string.Empty;

	// Token: 0x040012B7 RID: 4791
	public static string RUBY = string.Empty;

	// Token: 0x040012B8 RID: 4792
	public static string PK_NOW = string.Empty;

	// Token: 0x040012B9 RID: 4793
	public static string CUU_SAT = string.Empty;

	// Token: 0x040012BA RID: 4794
	public static string NOT_ENOUGH_MP = string.Empty;

	// Token: 0x040012BB RID: 4795
	public static string you_receive = string.Empty;

	// Token: 0x040012BC RID: 4796
	public static string MONTH = string.Empty;

	// Token: 0x040012BD RID: 4797
	public static string WEEK = string.Empty;

	// Token: 0x040012BE RID: 4798
	public static string DAY = string.Empty;

	// Token: 0x040012BF RID: 4799
	public static string HOUR = string.Empty;

	// Token: 0x040012C0 RID: 4800
	public static string SECOND = string.Empty;

	// Token: 0x040012C1 RID: 4801
	public static string MINUTE = string.Empty;

	// Token: 0x040012C2 RID: 4802
	public static string LEARN_SKILL = string.Empty;

	// Token: 0x040012C3 RID: 4803
	public static string rank = string.Empty;

	// Token: 0x040012C4 RID: 4804
	public static string active_point = string.Empty;

	// Token: 0x040012C5 RID: 4805
	public static string friend = string.Empty;

	// Token: 0x040012C6 RID: 4806
	public static string enemy = string.Empty;

	// Token: 0x040012C7 RID: 4807
	public static string no_friend = string.Empty;

	// Token: 0x040012C8 RID: 4808
	public static string chat_world = string.Empty;

	// Token: 0x040012C9 RID: 4809
	public static string change_flag = string.Empty;

	// Token: 0x040012CA RID: 4810
	public static string gameInfo = string.Empty;

	// Token: 0x040012CB RID: 4811
	public static string quayso = string.Empty;

	// Token: 0x040012CC RID: 4812
	public static string option = string.Empty;

	// Token: 0x040012CD RID: 4813
	public static string high = string.Empty;

	// Token: 0x040012CE RID: 4814
	public static string medium = string.Empty;

	// Token: 0x040012CF RID: 4815
	public static string low = string.Empty;

	// Token: 0x040012D0 RID: 4816
	public static string increase_vga = string.Empty;

	// Token: 0x040012D1 RID: 4817
	public static string decrease_vga = string.Empty;

	// Token: 0x040012D2 RID: 4818
	public static string serverchat_off = string.Empty;

	// Token: 0x040012D3 RID: 4819
	public static string serverchat_on = string.Empty;

	// Token: 0x040012D4 RID: 4820
	public static string x2Screen = string.Empty;

	// Token: 0x040012D5 RID: 4821
	public static string x1Screen = string.Empty;

	// Token: 0x040012D6 RID: 4822
	public static string changeSizeScreen = string.Empty;

	// Token: 0x040012D7 RID: 4823
	public static string aura_off = string.Empty;

	// Token: 0x040012D8 RID: 4824
	public static string aura_on = string.Empty;

	// Token: 0x040012D9 RID: 4825
	public static string aura_off_2 = string.Empty;

	// Token: 0x040012DA RID: 4826
	public static string aura_on_2 = string.Empty;

	// Token: 0x040012DB RID: 4827
	public static string hat_off = string.Empty;

	// Token: 0x040012DC RID: 4828
	public static string hat_on = string.Empty;

	// Token: 0x040012DD RID: 4829
	public static string chest = string.Empty;

	// Token: 0x040012DE RID: 4830
	public static string[] chestt = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012DF RID: 4831
	public static string[] inventory = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E0 RID: 4832
	public static string[] combine = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E1 RID: 4833
	public static string[] mapp = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E2 RID: 4834
	public static string[] item_give = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E3 RID: 4835
	public static string[] item_receive = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E4 RID: 4836
	public static string[] zonee = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x040012E5 RID: 4837
	public static string zone = string.Empty;

	// Token: 0x040012E6 RID: 4838
	public static string map = string.Empty;

	// Token: 0x040012E7 RID: 4839
	public static string item_receive2 = string.Empty;

	// Token: 0x040012E8 RID: 4840
	public static string item = string.Empty;

	// Token: 0x040012E9 RID: 4841
	public static string give_upper = string.Empty;

	// Token: 0x040012EA RID: 4842
	public static string receive_upper = string.Empty;

	// Token: 0x040012EB RID: 4843
	public static string receive_all = string.Empty;

	// Token: 0x040012EC RID: 4844
	public static string no_map = string.Empty;

	// Token: 0x040012ED RID: 4845
	public static string go_to_quest = string.Empty;

	// Token: 0x040012EE RID: 4846
	public static string from_earth = string.Empty;

	// Token: 0x040012EF RID: 4847
	public static string from_namec = string.Empty;

	// Token: 0x040012F0 RID: 4848
	public static string from_sayda = string.Empty;

	// Token: 0x040012F1 RID: 4849
	public static string expire = string.Empty;

	// Token: 0x040012F2 RID: 4850
	public static string pow_request = string.Empty;

	// Token: 0x040012F3 RID: 4851
	public static string your_pow = string.Empty;

	// Token: 0x040012F4 RID: 4852
	public static string used = string.Empty;

	// Token: 0x040012F5 RID: 4853
	public static string place = string.Empty;

	// Token: 0x040012F6 RID: 4854
	public static string FOREVER = string.Empty;

	// Token: 0x040012F7 RID: 4855
	public static string NOUPGRADE = string.Empty;

	// Token: 0x040012F8 RID: 4856
	public static string NOTUPGRADE = string.Empty;

	// Token: 0x040012F9 RID: 4857
	public static string UPGRADE = string.Empty;

	// Token: 0x040012FA RID: 4858
	public static string UPGRADING = string.Empty;

	// Token: 0x040012FB RID: 4859
	public static string make_shortcut = string.Empty;

	// Token: 0x040012FC RID: 4860
	public static string into_place = string.Empty;

	// Token: 0x040012FD RID: 4861
	public static string move_to_chest = string.Empty;

	// Token: 0x040012FE RID: 4862
	public static string move_to_chest2 = string.Empty;

	// Token: 0x040012FF RID: 4863
	public static string press_chat_querty = string.Empty;

	// Token: 0x04001300 RID: 4864
	public static string press_chat = string.Empty;

	// Token: 0x04001301 RID: 4865
	public static string saying = string.Empty;

	// Token: 0x04001302 RID: 4866
	public static string miss = string.Empty;

	// Token: 0x04001303 RID: 4867
	public static string donate = string.Empty;

	// Token: 0x04001304 RID: 4868
	public static string receive = string.Empty;

	// Token: 0x04001305 RID: 4869
	public static string press_twice = string.Empty;

	// Token: 0x04001306 RID: 4870
	public static string can_harvest = string.Empty;

	// Token: 0x04001307 RID: 4871
	public static string do_accept_qwerty = string.Empty;

	// Token: 0x04001308 RID: 4872
	public static string do_accept = string.Empty;

	// Token: 0x04001309 RID: 4873
	public static string plsRestartGame = string.Empty;

	// Token: 0x0400130A RID: 4874
	public static string is_online = string.Empty;

	// Token: 0x0400130B RID: 4875
	public static string is_offline = string.Empty;

	// Token: 0x0400130C RID: 4876
	public static string make_friend = string.Empty;

	// Token: 0x0400130D RID: 4877
	public static string chat_player = string.Empty;

	// Token: 0x0400130E RID: 4878
	public static string chat_with = string.Empty;

	// Token: 0x0400130F RID: 4879
	public static string clan_capsuledonate = string.Empty;

	// Token: 0x04001310 RID: 4880
	public static string clan_capsuleself = string.Empty;

	// Token: 0x04001311 RID: 4881
	public static string clan_point = string.Empty;

	// Token: 0x04001312 RID: 4882
	public static string give_pea = string.Empty;

	// Token: 0x04001313 RID: 4883
	public static string receive_pea = string.Empty;

	// Token: 0x04001314 RID: 4884
	public static string request_pea = string.Empty;

	// Token: 0x04001315 RID: 4885
	public static string time = string.Empty;

	// Token: 0x04001316 RID: 4886
	public static string received = string.Empty;

	// Token: 0x04001317 RID: 4887
	public static string power = string.Empty;

	// Token: 0x04001318 RID: 4888
	public static string join_date = string.Empty;

	// Token: 0x04001319 RID: 4889
	public static string clan_leader = string.Empty;

	// Token: 0x0400131A RID: 4890
	public static string clan_coleader = string.Empty;

	// Token: 0x0400131B RID: 4891
	public static string power_point = string.Empty;

	// Token: 0x0400131C RID: 4892
	public static string member = string.Empty;

	// Token: 0x0400131D RID: 4893
	public static string[] memberr = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400131E RID: 4894
	public static string[] chatClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x0400131F RID: 4895
	public static string[] leaveClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001320 RID: 4896
	public static string[] createClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001321 RID: 4897
	public static string[] findClan = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001322 RID: 4898
	public static string[] khau_hieuu = new string[] { string.Empty };

	// Token: 0x04001323 RID: 4899
	public static string[] bieu_tuongg = new string[] { string.Empty };

	// Token: 0x04001324 RID: 4900
	public static string[] request_pea2 = new string[]
	{
		string.Empty,
		string.Empty
	};

	// Token: 0x04001325 RID: 4901
	public static string level = string.Empty;

	// Token: 0x04001326 RID: 4902
	public static string clan_birthday = string.Empty;

	// Token: 0x04001327 RID: 4903
	public static string clan_list = string.Empty;

	// Token: 0x04001328 RID: 4904
	public static string create = string.Empty;

	// Token: 0x04001329 RID: 4905
	public static string find = string.Empty;

	// Token: 0x0400132A RID: 4906
	public static string leave = string.Empty;

	// Token: 0x0400132B RID: 4907
	public static string not_join_clan = string.Empty;

	// Token: 0x0400132C RID: 4908
	public static string[] clanEmpty = new string[]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	// Token: 0x0400132D RID: 4909
	public static string input_clan_name = string.Empty;

	// Token: 0x0400132E RID: 4910
	public static string clan_name = string.Empty;

	// Token: 0x0400132F RID: 4911
	public static string chat_clan = string.Empty;

	// Token: 0x04001330 RID: 4912
	public static string input_clan_name_to_create = string.Empty;

	// Token: 0x04001331 RID: 4913
	public static string input_clan_slogan = string.Empty;

	// Token: 0x04001332 RID: 4914
	public static string do_u_want_join_clan = string.Empty;

	// Token: 0x04001333 RID: 4915
	public static string select_clan_icon = string.Empty;

	// Token: 0x04001334 RID: 4916
	public static string request_join_clan = string.Empty;

	// Token: 0x04001335 RID: 4917
	public static string view_clan_member = string.Empty;

	// Token: 0x04001336 RID: 4918
	public static string create_clan_co_leader = string.Empty;

	// Token: 0x04001337 RID: 4919
	public static string create_clan_leader = string.Empty;

	// Token: 0x04001338 RID: 4920
	public static string disable_clan_mastership = string.Empty;

	// Token: 0x04001339 RID: 4921
	public static string kick_clan_mem = string.Empty;

	// Token: 0x0400133A RID: 4922
	public static string clan_name_blank = string.Empty;

	// Token: 0x0400133B RID: 4923
	public static string clan_slogan_blank = string.Empty;

	// Token: 0x0400133C RID: 4924
	public static string cannot_find_clan = string.Empty;

	// Token: 0x0400133D RID: 4925
	public static string ago = string.Empty;

	// Token: 0x0400133E RID: 4926
	public static string findingClan = string.Empty;

	// Token: 0x0400133F RID: 4927
	public static string trade = string.Empty;

	// Token: 0x04001340 RID: 4928
	public static string not_lock_trade = string.Empty;

	// Token: 0x04001341 RID: 4929
	public static string not_lock_trade_upper = string.Empty;

	// Token: 0x04001342 RID: 4930
	public static string locked_trade = string.Empty;

	// Token: 0x04001343 RID: 4931
	public static string locked_trade_upper = string.Empty;

	// Token: 0x04001344 RID: 4932
	public static string lock_trade = string.Empty;

	// Token: 0x04001345 RID: 4933
	public static string wait_opp_lock_trade = string.Empty;

	// Token: 0x04001346 RID: 4934
	public static string press_done = string.Empty;

	// Token: 0x04001347 RID: 4935
	public static string THROW = string.Empty;

	// Token: 0x04001348 RID: 4936
	public static string SPLIT = string.Empty;

	// Token: 0x04001349 RID: 4937
	public static string done = string.Empty;

	// Token: 0x0400134A RID: 4938
	public static string opponent = string.Empty;

	// Token: 0x0400134B RID: 4939
	public static string you = string.Empty;

	// Token: 0x0400134C RID: 4940
	public static string mlock = string.Empty;

	// Token: 0x0400134D RID: 4941
	public static string money_trade = string.Empty;

	// Token: 0x0400134E RID: 4942
	public static string GETOUT = string.Empty;

	// Token: 0x0400134F RID: 4943
	public static string MOVEOUT = string.Empty;

	// Token: 0x04001350 RID: 4944
	public static string MOVEFORPET = string.Empty;

	// Token: 0x04001351 RID: 4945
	public static string GETOUTMONEY = string.Empty;

	// Token: 0x04001352 RID: 4946
	public static string GETINMONEY = string.Empty;

	// Token: 0x04001353 RID: 4947
	public static string SENDMONEY = string.Empty;

	// Token: 0x04001354 RID: 4948
	public static string GETIN = string.Empty;

	// Token: 0x04001355 RID: 4949
	public static string SALE = string.Empty;

	// Token: 0x04001356 RID: 4950
	public static string SALES = string.Empty;

	// Token: 0x04001357 RID: 4951
	public static string SALEALL = string.Empty;

	// Token: 0x04001358 RID: 4952
	public static string BUY = string.Empty;

	// Token: 0x04001359 RID: 4953
	public static string BUYS = string.Empty;

	// Token: 0x0400135A RID: 4954
	public static string input_money_to_trade = string.Empty;

	// Token: 0x0400135B RID: 4955
	public static string input_money = string.Empty;

	// Token: 0x0400135C RID: 4956
	public static string input_money_wrong = string.Empty;

	// Token: 0x0400135D RID: 4957
	public static string not_enough_money = string.Empty;

	// Token: 0x0400135E RID: 4958
	public static string input_quantity_to_trade = string.Empty;

	// Token: 0x0400135F RID: 4959
	public static string input_quantity = string.Empty;

	// Token: 0x04001360 RID: 4960
	public static string input_quantity_wrong = string.Empty;

	// Token: 0x04001361 RID: 4961
	public static string already_has_item = string.Empty;

	// Token: 0x04001362 RID: 4962
	public static string unlock_item_to_trade = string.Empty;

	// Token: 0x04001363 RID: 4963
	public static string root = string.Empty;

	// Token: 0x04001364 RID: 4964
	public static string need = string.Empty;

	// Token: 0x04001365 RID: 4965
	public static string need_upper = string.Empty;

	// Token: 0x04001366 RID: 4966
	public static string free = string.Empty;

	// Token: 0x04001367 RID: 4967
	public static string free1 = string.Empty;

	// Token: 0x04001368 RID: 4968
	public static string free2 = string.Empty;

	// Token: 0x04001369 RID: 4969
	public static string select_item = string.Empty;

	// Token: 0x0400136A RID: 4970
	public static string random = string.Empty;

	// Token: 0x0400136B RID: 4971
	public static string say_hello = string.Empty;

	// Token: 0x0400136C RID: 4972
	public static string say_wat_do_u_want_to_buy = string.Empty;

	// Token: 0x0400136D RID: 4973
	public static string say_wat_do_u_want_to_buy2 = string.Empty;

	// Token: 0x0400136E RID: 4974
	public static string do_u_sure_to_trade = string.Empty;

	// Token: 0x0400136F RID: 4975
	public static string learn_with = string.Empty;

	// Token: 0x04001370 RID: 4976
	public static string buy_with = string.Empty;

	// Token: 0x04001371 RID: 4977
	public static string can_not_do_when_die = string.Empty;

	// Token: 0x04001372 RID: 4978
	public static string use_for_combine = string.Empty;

	// Token: 0x04001373 RID: 4979
	public static string use_for_trade = string.Empty;

	// Token: 0x04001374 RID: 4980
	public static string not_enough_luong_world_channel = string.Empty;

	// Token: 0x04001375 RID: 4981
	public static string world_channel_5_luong = string.Empty;

	// Token: 0x04001376 RID: 4982
	public static string want_to_trade = string.Empty;

	// Token: 0x04001377 RID: 4983
	public static string hasJustUpgrade1 = string.Empty;

	// Token: 0x04001378 RID: 4984
	public static string hasJustUpgrade2 = string.Empty;

	// Token: 0x04001379 RID: 4985
	public static string potential_to_learn = string.Empty;

	// Token: 0x0400137A RID: 4986
	public static string potential_point = string.Empty;

	// Token: 0x0400137B RID: 4987
	public static string achievement_point = string.Empty;

	// Token: 0x0400137C RID: 4988
	public static string increase = string.Empty;

	// Token: 0x0400137D RID: 4989
	public static string increase_upper = string.Empty;

	// Token: 0x0400137E RID: 4990
	public static string not_enough_potential_point1 = string.Empty;

	// Token: 0x0400137F RID: 4991
	public static string not_enough_potential_point2 = string.Empty;

	// Token: 0x04001380 RID: 4992
	public static string use_potential_point_for1 = string.Empty;

	// Token: 0x04001381 RID: 4993
	public static string use_potential_point_for2 = string.Empty;

	// Token: 0x04001382 RID: 4994
	public static string for_HP = string.Empty;

	// Token: 0x04001383 RID: 4995
	public static string for_KI = string.Empty;

	// Token: 0x04001384 RID: 4996
	public static string for_hit_point = string.Empty;

	// Token: 0x04001385 RID: 4997
	public static string for_armor = string.Empty;

	// Token: 0x04001386 RID: 4998
	public static string for_crit = string.Empty;

	// Token: 0x04001387 RID: 4999
	public static string can_buy_from_Uron1 = string.Empty;

	// Token: 0x04001388 RID: 5000
	public static string can_buy_from_Uron2 = string.Empty;

	// Token: 0x04001389 RID: 5001
	public static string can_buy_from_Uron3 = string.Empty;

	// Token: 0x0400138A RID: 5002
	public static string HP = string.Empty;

	// Token: 0x0400138B RID: 5003
	public static string KI = string.Empty;

	// Token: 0x0400138C RID: 5004
	public static string hit_point = string.Empty;

	// Token: 0x0400138D RID: 5005
	public static string armor = string.Empty;

	// Token: 0x0400138E RID: 5006
	public static string vitality = string.Empty;

	// Token: 0x0400138F RID: 5007
	public static string critical = string.Empty;

	// Token: 0x04001390 RID: 5008
	public static string cap_do = string.Empty;

	// Token: 0x04001391 RID: 5009
	public static string KI_consume = string.Empty;

	// Token: 0x04001392 RID: 5010
	public static string cooldown = string.Empty;

	// Token: 0x04001393 RID: 5011
	public static string milisecond = string.Empty;

	// Token: 0x04001394 RID: 5012
	public static string max_level_reach = string.Empty;

	// Token: 0x04001395 RID: 5013
	public static string next_level_require = string.Empty;

	// Token: 0x04001396 RID: 5014
	public static string potential = string.Empty;

	// Token: 0x04001397 RID: 5015
	public static string not_learn = string.Empty;

	// Token: 0x04001398 RID: 5016
	public static string learn_require = string.Empty;

	// Token: 0x04001399 RID: 5017
	public static string learn = string.Empty;

	// Token: 0x0400139A RID: 5018
	public static string to_gain_20hp = string.Empty;

	// Token: 0x0400139B RID: 5019
	public static string to_gain_20mp = string.Empty;

	// Token: 0x0400139C RID: 5020
	public static string to_gain_1pow = string.Empty;

	// Token: 0x0400139D RID: 5021
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

	// Token: 0x0400139E RID: 5022
	public static string hp_ki_full = string.Empty;

	// Token: 0x0400139F RID: 5023
	public static string quest_place = string.Empty;

	// Token: 0x040013A0 RID: 5024
	public static string no_mission = string.Empty;

	// Token: 0x040013A1 RID: 5025
	public static string reward_mission = string.Empty;

	// Token: 0x040013A2 RID: 5026
	public static string achievement_mission = string.Empty;

	// Token: 0x040013A3 RID: 5027
	public static string trangbi = string.Empty;

	// Token: 0x040013A4 RID: 5028
	public static string wat_do_u_want = string.Empty;

	// Token: 0x040013A5 RID: 5029
	public static string off = string.Empty;

	// Token: 0x040013A6 RID: 5030
	public static string on = string.Empty;

	// Token: 0x040013A7 RID: 5031
	public static string select_map = string.Empty;

	// Token: 0x040013A8 RID: 5032
	public static string offPlease = string.Empty;

	// Token: 0x040013A9 RID: 5033
	public static string onPlease = string.Empty;

	// Token: 0x040013AA RID: 5034
	public static sbyte language;

	// Token: 0x040013AB RID: 5035
	public const sbyte VIETNAM = 0;

	// Token: 0x040013AC RID: 5036
	public const sbyte ENGLISH = 1;

	// Token: 0x040013AD RID: 5037
	public const sbyte INDONESIA = 2;

	// Token: 0x040013AE RID: 5038
	public static string choigame;

	// Token: 0x040013AF RID: 5039
	public static string no_enemy = string.Empty;

	// Token: 0x040013B0 RID: 5040
	public static string kigui;

	// Token: 0x040013B1 RID: 5041
	public static string kiguiXu;

	// Token: 0x040013B2 RID: 5042
	public static string kiguiLuong;

	// Token: 0x040013B3 RID: 5043
	public static string kiguiXuchat;

	// Token: 0x040013B4 RID: 5044
	public static string kiguiLuongchat;

	// Token: 0x040013B5 RID: 5045
	public static string huykigui;

	// Token: 0x040013B6 RID: 5046
	public static string nhantien;

	// Token: 0x040013B7 RID: 5047
	public static string dangban;

	// Token: 0x040013B8 RID: 5048
	public static string daban;

	// Token: 0x040013B9 RID: 5049
	public static string num;

	// Token: 0x040013BA RID: 5050
	public static string upTop;

	// Token: 0x040013BB RID: 5051
	public static string page;

	// Token: 0x040013BC RID: 5052
	public static string getDown;

	// Token: 0x040013BD RID: 5053
	public static string getUp;

	// Token: 0x040013BE RID: 5054
	public static string notYetSell;

	// Token: 0x040013BF RID: 5055
	public static string charger;

	// Token: 0x040013C0 RID: 5056
	public static string finishBomong;

	// Token: 0x040013C1 RID: 5057
	public static string note;

	// Token: 0x040013C2 RID: 5058
	public static string regNote;

	// Token: 0x040013C3 RID: 5059
	public static string remain;

	// Token: 0x040013C4 RID: 5060
	public static string faster;

	// Token: 0x040013C5 RID: 5061
	public static string fasterQuestion;

	// Token: 0x040013C6 RID: 5062
	public static string chuacotaikhoan;

	// Token: 0x040013C7 RID: 5063
	public static string taidulieudechoi;

	// Token: 0x040013C8 RID: 5064
	public static string huy;

	// Token: 0x040013C9 RID: 5065
	public static string taidulieu;

	// Token: 0x040013CA RID: 5066
	public static string xoadulieu;

	// Token: 0x040013CB RID: 5067
	public static string deletaDataNote;

	// Token: 0x040013CC RID: 5068
	public static string playNew;

	// Token: 0x040013CD RID: 5069
	public static string playAcc;

	// Token: 0x040013CE RID: 5070
	public static string vuilongnhapduthongtin;

	// Token: 0x040013CF RID: 5071
	public static string not_register_yet = string.Empty;

	// Token: 0x040013D0 RID: 5072
	public static string nhanngoc;

	// Token: 0x040013D1 RID: 5073
	public static string fusion;

	// Token: 0x040013D2 RID: 5074
	public static string sure_fusion;

	// Token: 0x040013D3 RID: 5075
	public static string fusionForever;

	// Token: 0x040013D4 RID: 5076
	public static string xinchucmung;

	// Token: 0x040013D5 RID: 5077
	public static string den;

	// Token: 0x040013D6 RID: 5078
	public static string nhatvatpham;

	// Token: 0x040013D7 RID: 5079
	public static string confirmChangeServer;

	// Token: 0x040013D8 RID: 5080
	public static string cauhinhthuong;

	// Token: 0x040013D9 RID: 5081
	public static string countDown_waitingroom;

	// Token: 0x040013DA RID: 5082
	public static string potential_to_learn_tuyetKi = string.Empty;
}

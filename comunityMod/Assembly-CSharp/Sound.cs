using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class Sound
{
	// Token: 0x060008F4 RID: 2292 RVA: 0x00004887 File Offset: 0x00002A87
	public static void setActivity(SoundMn.AssetManager ac)
	{
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0008217C File Offset: 0x0008037C
	public static void stop()
	{
		for (int i = 0; i < Sound.player.Length; i++)
		{
			if (Sound.player[i] != null)
			{
				Sound.player[i].GetComponent<AudioSource>().Pause();
			}
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000151BF File Offset: 0x000133BF
	public static bool isPlaying()
	{
		return false;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x000821BB File Offset: 0x000803BB
	public static void init()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Audio Player";
		gameObject.transform.position = Vector3.zero;
		gameObject.AddComponent<AudioListener>();
		Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x000821F0 File Offset: 0x000803F0
	public static void init(int[] musicID, int[] sID)
	{
		if (Sound.player == null && Sound.music == null)
		{
			Sound.init();
			Sound.l1 = musicID.Length;
			Sound.player = new GameObject[musicID.Length + sID.Length];
			Sound.music = new AudioClip[musicID.Length + sID.Length];
			for (int i = 0; i < Sound.player.Length; i++)
			{
				Sound.getAssetSoundFile((i >= Sound.l1) ? ("/sound/" + (i - Sound.l1).ToString()) : ("/music/" + i.ToString()), i);
			}
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00082289 File Offset: 0x00080489
	public static void playSound(int id, float volume)
	{
		Sound.play(id + Sound.l1, volume);
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00082298 File Offset: 0x00080498
	public static void playSound1(int id, float volume)
	{
		Sound.play(id, volume);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x000822A1 File Offset: 0x000804A1
	public static void getAssetSoundFile(string fileName, int pos)
	{
		Sound.stop(pos);
		string empty = string.Empty;
		Sound.load(Main.res + fileName, pos);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x000822C0 File Offset: 0x000804C0
	public static void stopAllz()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		for (int j = 0; j < Sound.l1; j++)
		{
			Sound.sTopSoundBG(j);
		}
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x000822FC File Offset: 0x000804FC
	public static void stopAllBg()
	{
		for (int i = 0; i < Sound.music.Length; i++)
		{
			Sound.stop(i);
		}
		Sound.sTopSoundBG(0);
		Sound.sTopSoundRun();
		Sound.stopSoundNatural(0);
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00004887 File Offset: 0x00002A87
	public static void update()
	{
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00082332 File Offset: 0x00080532
	public static void stopMusic(int x)
	{
		if (GameCanvas.isPlaySound)
		{
			Sound.stop(x);
		}
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00082341 File Offset: 0x00080541
	public static void play(int id, float volume)
	{
		if (!Sound.isNotPlay && GameCanvas.isPlaySound)
		{
			Sound.start(volume, id);
		}
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00082358 File Offset: 0x00080558
	public static void playSoundRun(int id, float volume)
	{
		if (GameCanvas.isPlaySound && !(Sound.SoundRun == null))
		{
			Sound.SoundRun.GetComponent<AudioSource>().loop = true;
			Sound.SoundRun.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundRun.GetComponent<AudioSource>().volume = volume;
			Sound.SoundRun.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x000823BE File Offset: 0x000805BE
	public static void sTopSoundRun()
	{
		Sound.SoundRun.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x000823CF File Offset: 0x000805CF
	public static bool isPlayingSound()
	{
		return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x000823F0 File Offset: 0x000805F0
	public static void playSoundNatural(int id, float volume, bool isLoop)
	{
		if (GameCanvas.isPlaySound && !(Sound.SoundBGLoop == null))
		{
			Sound.SoundWater.GetComponent<AudioSource>().loop = isLoop;
			Sound.SoundWater.GetComponent<AudioSource>().clip = Sound.music[id];
			Sound.SoundWater.GetComponent<AudioSource>().volume = volume;
			Sound.SoundWater.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00082456 File Offset: 0x00080656
	public static void stopSoundNatural(int id)
	{
		Sound.SoundWater.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00082467 File Offset: 0x00080667
	public static bool isPlayingSoundatural(int id)
	{
		return !(Sound.SoundWater == null) && Sound.SoundWater.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00082487 File Offset: 0x00080687
	public static void playMus(int type, float vl, bool loop)
	{
		if (!Sound.isNotPlay)
		{
			vl -= 0.3f;
			if (vl <= 0f)
			{
				vl = 0.01f;
			}
			Sound.playSoundBGLoop(type, vl);
		}
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000824B0 File Offset: 0x000806B0
	public static void playSoundBGLoop(int id, float volume)
	{
		if (GameCanvas.isPlaySound)
		{
			if (id == SoundMn.AIR_SHIP)
			{
				Sound.playSound1(id, volume + 0.2f);
				return;
			}
			if (!(Sound.SoundBGLoop == null) && !Sound.isPlayingSoundBG(id))
			{
				Sound.SoundBGLoop.GetComponent<AudioSource>().loop = true;
				Sound.SoundBGLoop.GetComponent<AudioSource>().clip = Sound.music[id];
				Sound.SoundBGLoop.GetComponent<AudioSource>().volume = volume;
				Sound.SoundBGLoop.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00082534 File Offset: 0x00080734
	public static void sTopSoundBG(int id)
	{
		Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x00082545 File Offset: 0x00080745
	public static bool isPlayingSoundBG(int id)
	{
		return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x00082565 File Offset: 0x00080765
	public static void load(string filename, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__load(filename, pos);
			return;
		}
		Sound._load(filename, pos);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0008258C File Offset: 0x0008078C
	internal static void _load(string filename, int pos)
	{
		if (Sound.status != 0)
		{
			Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Sound.filenametemp);
			return;
		}
		Sound.filenametemp = filename;
		Sound.postem = pos;
		Sound.status = 2;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
			return;
		}
		Cout.Log(string.Concat(new string[]
		{
			"Load Audio ",
			filename,
			" done in ",
			(i * 5).ToString(),
			"ms"
		}));
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00082637 File Offset: 0x00080837
	internal static void __load(string filename, int pos)
	{
		Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
		GameObject.Find("Main Camera").AddComponent<AudioSource>();
		Sound.player[pos] = GameObject.Find("Main Camera");
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00082676 File Offset: 0x00080876
	public static void start(float volume, int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__start(volume, pos);
			return;
		}
		Sound._start(volume, pos);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000826A0 File Offset: 0x000808A0
	public static void _start(float volume, int pos)
	{
		if (Sound.status != 0)
		{
			Debug.LogError("CANNOT START AUDIO WHEN STARTING");
			return;
		}
		Sound.volumetem = volume;
		Sound.postem = pos;
		Sound.status = 3;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR START AUDIO");
			return;
		}
		Debug.Log("Start Audio done in " + (i * 5).ToString() + "ms");
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0008271A File Offset: 0x0008091A
	public static void __start(float volume, int pos)
	{
		if (!(Sound.player[pos] == null))
		{
			Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00082744 File Offset: 0x00080944
	public static void stop(int pos)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Sound.__stop(pos);
			return;
		}
		Sound._stop(pos);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0008276C File Offset: 0x0008096C
	public static void _stop(int pos)
	{
		if (Sound.status != 0)
		{
			Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
			return;
		}
		Sound.postem = pos;
		Sound.status = 4;
		int i;
		for (i = 0; i < 100; i++)
		{
			Thread.Sleep(5);
			if (Sound.status == 0)
			{
				break;
			}
		}
		if (i == 100)
		{
			Debug.LogError("TOO LONG FOR STOP AUDIO");
			return;
		}
		Debug.Log("Stop Audio done in " + (i * 5).ToString() + "ms");
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x000827E0 File Offset: 0x000809E0
	public static void __stop(int pos)
	{
		if (Sound.player[pos] != null)
		{
			Sound.player[pos].GetComponent<AudioSource>().Stop();
		}
	}

	// Token: 0x04000FB6 RID: 4022
	internal const int INTERVAL = 5;

	// Token: 0x04000FB7 RID: 4023
	internal const int MAXTIME = 100;

	// Token: 0x04000FB8 RID: 4024
	public static int status;

	// Token: 0x04000FB9 RID: 4025
	public static int postem;

	// Token: 0x04000FBA RID: 4026
	public static int timestart;

	// Token: 0x04000FBB RID: 4027
	internal static string filenametemp;

	// Token: 0x04000FBC RID: 4028
	internal static float volumetem;

	// Token: 0x04000FBD RID: 4029
	public static bool isSound = true;

	// Token: 0x04000FBE RID: 4030
	public static bool isNotPlay;

	// Token: 0x04000FBF RID: 4031
	public static bool stopAll;

	// Token: 0x04000FC0 RID: 4032
	public static AudioSource SoundWater;

	// Token: 0x04000FC1 RID: 4033
	public static AudioSource SoundRun;

	// Token: 0x04000FC2 RID: 4034
	public static AudioSource SoundBGLoop;

	// Token: 0x04000FC3 RID: 4035
	public static AudioClip[] music;

	// Token: 0x04000FC4 RID: 4036
	public static GameObject[] player;

	// Token: 0x04000FC5 RID: 4037
	public static sbyte MLogin;

	// Token: 0x04000FC6 RID: 4038
	public static sbyte MBClick = 1;

	// Token: 0x04000FC7 RID: 4039
	public static sbyte MTone = 2;

	// Token: 0x04000FC8 RID: 4040
	public static sbyte MSanzu = 3;

	// Token: 0x04000FC9 RID: 4041
	public static sbyte MChakumi = 4;

	// Token: 0x04000FCA RID: 4042
	public static sbyte MChai = 5;

	// Token: 0x04000FCB RID: 4043
	public static sbyte MOshin = 6;

	// Token: 0x04000FCC RID: 4044
	public static sbyte MEchigo = 7;

	// Token: 0x04000FCD RID: 4045
	public static sbyte MKojin = 8;

	// Token: 0x04000FCE RID: 4046
	public static sbyte MHaruna = 9;

	// Token: 0x04000FCF RID: 4047
	public static sbyte MHirosaki = 10;

	// Token: 0x04000FD0 RID: 4048
	public static sbyte MOokaza = 11;

	// Token: 0x04000FD1 RID: 4049
	public static sbyte MGiotuyet = 12;

	// Token: 0x04000FD2 RID: 4050
	public static sbyte MHangdong = 13;

	// Token: 0x04000FD3 RID: 4051
	public static sbyte MDeKeu = 14;

	// Token: 0x04000FD4 RID: 4052
	public static sbyte MChimKeu = 15;

	// Token: 0x04000FD5 RID: 4053
	public static sbyte MBuocChan = 16;

	// Token: 0x04000FD6 RID: 4054
	public static sbyte MNuocChay = 17;

	// Token: 0x04000FD7 RID: 4055
	public static sbyte MBomMau = 18;

	// Token: 0x04000FD8 RID: 4056
	public static sbyte MKiemGo = 19;

	// Token: 0x04000FD9 RID: 4057
	public static sbyte MKiem = 20;

	// Token: 0x04000FDA RID: 4058
	public static sbyte MTieu = 21;

	// Token: 0x04000FDB RID: 4059
	public static sbyte MKunai = 22;

	// Token: 0x04000FDC RID: 4060
	public static sbyte MCung = 23;

	// Token: 0x04000FDD RID: 4061
	public static sbyte MDao = 24;

	// Token: 0x04000FDE RID: 4062
	public static sbyte MQuat = 25;

	// Token: 0x04000FDF RID: 4063
	public static sbyte MCung2 = 26;

	// Token: 0x04000FE0 RID: 4064
	public static sbyte MTieu2 = 27;

	// Token: 0x04000FE1 RID: 4065
	public static sbyte MTieu3 = 28;

	// Token: 0x04000FE2 RID: 4066
	public static sbyte MKiem2 = 29;

	// Token: 0x04000FE3 RID: 4067
	public static sbyte MKiem3 = 30;

	// Token: 0x04000FE4 RID: 4068
	public static sbyte MDao2 = 31;

	// Token: 0x04000FE5 RID: 4069
	public static sbyte MDao3 = 32;

	// Token: 0x04000FE6 RID: 4070
	public static sbyte MCung3 = 33;

	// Token: 0x04000FE7 RID: 4071
	public static int l1;
}

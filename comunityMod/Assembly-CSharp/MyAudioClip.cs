using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class MyAudioClip
{
	// Token: 0x060005AF RID: 1455 RVA: 0x0005740C File Offset: 0x0005560C
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0005742C File Offset: 0x0005562C
	public void Play()
	{
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = mSystem.currentTimeMillis();
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x000151BF File Offset: 0x000133BF
	public bool isPlaying()
	{
		return false;
	}

	// Token: 0x04000C01 RID: 3073
	public string name;

	// Token: 0x04000C02 RID: 3074
	public AudioClip clip;

	// Token: 0x04000C03 RID: 3075
	public long timeStart;
}

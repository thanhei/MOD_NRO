using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class MyAudioClip
{
	// Token: 0x0600005B RID: 91 RVA: 0x0000433F File Offset: 0x0000253F
	public MyAudioClip(string filename)
	{
		this.clip = (AudioClip)Resources.Load(filename);
		this.name = filename;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000435F File Offset: 0x0000255F
	public void Play()
	{
		Main.main.GetComponent<AudioSource>().PlayOneShot(this.clip);
		this.timeStart = mSystem.currentTimeMillis();
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004381 File Offset: 0x00002581
	public bool isPlaying()
	{
		return false;
	}

	// Token: 0x04000021 RID: 33
	public string name;

	// Token: 0x04000022 RID: 34
	public AudioClip clip;

	// Token: 0x04000023 RID: 35
	public long timeStart;
}

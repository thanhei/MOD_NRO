using System;
using Mod;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class Main2 : MonoBehaviour
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x00093984 File Offset: 0x00091B84
	private void Start()
	{
		GameEvents.OnMainStart();
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0009398B File Offset: 0x00091B8B
	private void Update()
	{
		GameEvents.OnUpdateMain();
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00093992 File Offset: 0x00091B92
	private void FixedUpdate()
	{
		GameEvents.OnFixedUpdateMain();
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00093999 File Offset: 0x00091B99
	private void OnApplicationPause(bool pause)
	{
		GameEvents.OnGamePause(pause);
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000939A1 File Offset: 0x00091BA1
	private void OnApplicationQuit()
	{
		GameEvents.OnGameClosing();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatManager : Singleton<ThreatManager>
{
    private IThreat[] interfaceThreats;

	private void Start()
	{
		interfaceThreats = gameObject.GetComponentsInChildren<IThreat>();
	}

	public void InitializeThreat(int index)
	{
		interfaceThreats[index].Init();
	}

	public void DeactivateThreat(int index)
	{
		interfaceThreats[index].Deactivate();
	}
}

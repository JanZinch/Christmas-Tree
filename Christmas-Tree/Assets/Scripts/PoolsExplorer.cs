using System;
using UnityEngine;

[AddComponentMenu("Pool/PoolsExplorer")]
public class PoolsExplorer : MonoBehaviour
{
	[SerializeField] private PoolsManager.PoolPart[] _pools = null;
	private static PoolsExplorer _instance = null;

	void OnValidate()
	{
		for (int i = 0; i < _pools.Length; i++)
		{
			_pools[i].prefabId = i;
		}
	}

	void Awake()
	{
		Initialize();
	}

	void Initialize()
	{
		if (_instance != null)
		{
			throw new Exception("Pool Explorer already exists! Only one Pool Explorer may be created.");
		}
		else {

			_instance = this;
			PoolsManager.Initialize(_pools);
		}		
	}

}
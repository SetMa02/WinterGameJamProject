using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class FireHeatTransfer : MonoBehaviour
{
	[SerializeField]private SphereCollider _sphereCollider;
	private float _heatPerSecond;
	private PlayerStatus _playerStatus;
	[SerializeField]private FireManager _fireManager;

	public void SetHeatPerSecond(float heatPerSecond)
	{
		_heatPerSecond = heatPerSecond;
		Debug.Log("setted");
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			playerStatus.HeatUp(_fireManager.currentHeat, false);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			playerStatus.HeatUp(0,false);
		}
	}
}

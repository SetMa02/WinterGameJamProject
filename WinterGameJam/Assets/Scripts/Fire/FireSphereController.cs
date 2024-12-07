using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireSphereController : MonoBehaviour
{
	public FireManager _fireManager;

	private void Start()
	{
		_fireManager = GetComponentInParent<FireManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			_fireManager.SphereEnter(playerStatus);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			_fireManager.SphereExit(playerStatus);
		}
	}
}

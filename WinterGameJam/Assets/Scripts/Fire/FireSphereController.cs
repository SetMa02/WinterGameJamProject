using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireSphereController : MonoBehaviour
{
	public FireManager _fireManager;

	private SphereCollider _sphereCollider;

	private void Start()
	{
		_fireManager = GetComponentInParent<FireManager>();
		_sphereCollider = GetComponent<SphereCollider>();
	}

	private void FixedUpdate()
	{
		_sphereCollider.radius = 15f + _fireManager.CurrentStage.FireSize;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			_fireManager.SphereStay(playerStatus);
		}
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

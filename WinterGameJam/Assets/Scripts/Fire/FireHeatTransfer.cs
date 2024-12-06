using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireHeatTransfer : MonoBehaviour
{
	private SphereCollider _sphereCollider;
	private FireManager _fireManager;
	private float _heatIncrease;

	private void Start()
	{
		_sphereCollider = GetComponent<SphereCollider>();
		_sphereCollider.isTrigger = true;

		_fireManager = FindObjectOfType<FireManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			playerStatus.SetNearFire(true, _fireManager);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
		{
			playerStatus.SetNearFire(false);
		}
	}

	public void SetTemperatureIncreaseRate(float heatPerSecond)
	{
		_heatIncrease = heatPerSecond;
	}
}
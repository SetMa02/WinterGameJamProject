using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireHeatTransfer : MonoBehaviour
{
	private SphereCollider _sphereCollider;
	private FireManager _fireManager;

	private void Start()
	{
		_sphereCollider = GetComponent<SphereCollider>();
		_sphereCollider.isTrigger = true;

		_fireManager = FindObjectOfType<FireManager>();
		if (_fireManager == null)
		{
			Debug.LogError("FireManager not found in the scene.");
		}
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
		// Этот метод можно использовать для передачи информации о текущем тепле костра
		// Например, для изменения скорости увеличения температуры игрока
	}
}

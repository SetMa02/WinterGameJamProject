using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] private float _temperature = 100f;
	[SerializeField] private float _health = 100f;
	[SerializeField] private float _temperatureDecreaseRate = 0.1f;
	[SerializeField] private float _defaultTemperatureIncreaseRate = 0.5f;
	private float _currentTemperatureIncreaseRate;
	private float _coldEffectMultiplier = 1f;

	public bool NearFire { get; private set; } = false;
	private FireManager _currentFireManager;

	public float Temperature
	{
		get { return _temperature; }
		set { _temperature = Mathf.Clamp(value, 0f, 100f); }
	}

	public float Health
	{
		get { return _health; }
		set { _health = Mathf.Clamp(value, 0f, 100f); }
	}

	void Start()
	{
		_currentTemperatureIncreaseRate = _defaultTemperatureIncreaseRate;
	}

	void Update()
	{
		if (!NearFire)
		{
			Temperature -= _temperatureDecreaseRate * Time.deltaTime * _coldEffectMultiplier;
		}
		else
		{
			Temperature += _currentTemperatureIncreaseRate * Time.deltaTime;
		}

		if (Temperature <= 10f)
		{
			Health -= 0.05f * Time.deltaTime;
			if (Health <= 0f)
			{
				Die();
			}
		}
	}

	public float GetPlayersTemperature()
	{
		return _temperature;
	}

	public void SetNearFire(bool isNear, FireManager fireManager = null)
	{
		NearFire = isNear;
		if (isNear && fireManager != null)
		{
			_currentFireManager = fireManager;
			SetTemperatureIncreaseRate(fireManager.currentHeat);
			ApplyBuffs();
		}
		else
		{
			_currentFireManager = null;
			SetTemperatureIncreaseRate(_defaultTemperatureIncreaseRate);
			RemoveBuffs();
		}
		Debug.Log("Near fire: " + NearFire + ", FireManager: " + fireManager);
	}

	public void SetTemperatureIncreaseRate(float newRate)
	{
		_currentTemperatureIncreaseRate = newRate;
	}

	public float GetSpeedMultiplier()
	{
		if (_temperature < 50f)
			return 0.8f;
		return 1f;
	}

	public FireManager CurrentFireManager => _currentFireManager;

	private void Die()
	{
		Debug.Log("Игрок погиб!");
		// Логика окончания игры или перезагрузки сцены
	}

	private void ApplyBuffs()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		if (playerMovement != null)
		{
			playerMovement.ApplySpeedMultiplier(1.2f);
		}
	}

	private void RemoveBuffs()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		if (playerMovement != null)
		{
			playerMovement.ApplySpeedMultiplier(1f);
		}
	}

	public void IncreaseColdEffect(float multiplier)
	{
		_coldEffectMultiplier = multiplier;
		Debug.Log("Cold effect multiplier set to: " + _coldEffectMultiplier);
	}
}

using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] private float _temperature = 100f;
	[SerializeField] private float _health = 100f;
	[SerializeField] private float _temperatureDecreaseRate = 0.1f;
	[SerializeField] private float _defaultTemperatureIncreaseRate = 0.5f;
	private float _currentTemperatureIncreaseRate;

	public bool NearFire { get; private set; } = false;

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
			Temperature -= _temperatureDecreaseRate * Time.deltaTime;
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
				// ААААААА смерт
			}
		}
	}

	public void SetNearFire(bool isNear)
	{
		NearFire = isNear;
		Debug.Log("Near fire");
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
}

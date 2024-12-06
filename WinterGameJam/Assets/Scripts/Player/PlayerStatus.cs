using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] private float _temperature = 100f;
	[SerializeField] private float _health = 100f;
	[SerializeField] private float _temperatureDecreaseRate = 0.1f;
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

	public float GetSpeedMultiplier()
	{
		if (_temperature < 50f)
			return 0.8f;
		return 1f;
	}

	void Update()
	{
		if (!NearFire)
		{
			Temperature -= _temperatureDecreaseRate * Time.deltaTime;
		}
		else
		{
			Temperature += 0.5f * Time.deltaTime;
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
	}
}

using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	public float temperature = 100f;
	public float health = 100f;
	public float temperatureDecreaseRate = 0.1f;
	public bool nearFire = false;

	public float GetSpeedMultiplier()
	{
		if (temperature < 50f)
			return 0.8f;
		else
			return 1f;
	}

	void Update()
	{
		if (!nearFire)
		{
			temperature -= temperatureDecreaseRate * Time.deltaTime;
			if (temperature < 0) temperature = 0;
		}
		else
		{
			temperature += 0.5f * Time.deltaTime;
			if (temperature > 100f) temperature = 100f;
		}

		if (temperature <= 10f)
		{
			health -= 0.05f * Time.deltaTime;

			if (health <= 0f)
			{
				// ААААААА смерт
			}
		}
	}
}

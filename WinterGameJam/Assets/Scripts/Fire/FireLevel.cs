using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevel : MonoBehaviour
{
	public float HeatPerSecond;
	public float FireSize;
	private FireHeatTransfer _fireHeatTransfer;

	private void Start()
	{
		_fireHeatTransfer = GetComponentInChildren<FireHeatTransfer>();
		_fireHeatTransfer.SetTemperatureIncreaseRate(HeatPerSecond);
	}

	public void FireLevelUp(FireStage level)
	{
		HeatPerSecond = level.HeatPerSecond;
		transform.localScale = new Vector3(
			transform.localScale.x * level.FireSize,
			transform.localScale.y * level.FireSize,
			transform.localScale.z * level.FireSize
		);

		_fireHeatTransfer.SetTemperatureIncreaseRate(HeatPerSecond);
	}
}

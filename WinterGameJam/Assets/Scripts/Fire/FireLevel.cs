using UnityEngine;

public class FireLevel : MonoBehaviour
{

	public float HeatPerSecond;
	public float FireSize;
	private FireHeatTransfer _fireHeatTransfer;

	public void FireLevelUp(FireStage level)
	{
		if (level == null)
		{
			Debug.LogError("FireStage level is null in FireLevelUp.");
			return;
		}

		HeatPerSecond = level.HeatPerSecond;
		FireSize = level.FireSize;

		transform.localScale = new Vector3(
			transform.localScale.x * level.FireSize,
			transform.localScale.y * level.FireSize,
			transform.localScale.z * level.FireSize
		);

		if (_fireHeatTransfer != null)
		{
			_fireHeatTransfer.SetHeatPerSecond(HeatPerSecond);
			Debug.Log($"FireLevelUp called. New HeatPerSecond: {HeatPerSecond}, New FireSize: {FireSize}");
		}
		else
		{
			Debug.LogError("FireHeatTransfer is null in FireLevel.");
		}
	}
}

	

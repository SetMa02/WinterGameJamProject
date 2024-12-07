using UnityEngine;

[CreateAssetMenu(fileName = "NewFireStage", menuName = "FireStage")]
public class FireStage : ScriptableObject
{
	public float Radius;
	public float HeatPerSecond;
	public float FireSize;
}

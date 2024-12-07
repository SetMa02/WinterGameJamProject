using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewFireStage", menuName = "FireStage")]
public class FireStage : ScriptableObject
{
	public float Radius;
	public float HeatPerSecond;
	public float FireSize;
	[FormerlySerializedAs("MaxFierHeat")] public float MaxFireHeat;
}

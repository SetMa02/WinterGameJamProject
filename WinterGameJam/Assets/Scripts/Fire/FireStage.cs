using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewFireStage", menuName = "FireStage")]
public class FireStage : ScriptableObject
{
	public float Radius;
	public float HeatPerSecond;
	public float FireSize;
	public float MaxFireHeat;
	public Sprite LogsSprite;
}

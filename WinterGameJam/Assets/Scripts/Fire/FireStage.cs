using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFireLevel", menuName = "Level")]
public class FireStage : ScriptableObject
{
    public float Radius;
    public float HetPerSecond;
    public float FireSize;
}

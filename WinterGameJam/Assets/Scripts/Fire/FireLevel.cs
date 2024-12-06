using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevel : MonoBehaviour
{
    public float HeatPerSecond;
    public float FireSize;
    

    public void FireLevelUp(float newHeatPerSecond, float newFireSize)
    {
        HeatPerSecond = newHeatPerSecond;
        FireSize = newFireSize;
    }

    public void UpdateSpriteSize(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.size = new Vector2(FireSize, FireSize);
    }
}

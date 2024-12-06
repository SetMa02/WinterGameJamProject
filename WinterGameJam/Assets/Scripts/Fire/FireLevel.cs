using System;
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
        HeatPerSecond = 1f;
        _fireHeatTransfer.SetTemperatureIncreaseRate(HeatPerSecond);
    }

    public void FireLevelUp(float newHeatPerSecond, float newFireSize)
    {
        HeatPerSecond = newHeatPerSecond;
        FireSize = newFireSize;
        _fireHeatTransfer.SetTemperatureIncreaseRate(HeatPerSecond);
    }

    public void UpdateSpriteSize(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.size = new Vector2(FireSize, FireSize);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireLEvelIndicator : MonoBehaviour
{
    [SerializeField] private FireManager _fireManager;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();

        _slider.maxValue = _fireManager.maxHeat;
    }

    private void Update()
    {
        _slider.value = _fireManager.currentHeat;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Animator), typeof(FireHeatBuff))]
[RequireComponent(typeof(FireLevel), typeof(FireAnimationManager))]
public class FireManager : MonoBehaviour
{
    public float maxHeat;
    public float currentHeat;
    private FireHeatBuff _fireHeatBuff;
    private FireLevel _fireLevel;
    private FireAnimationManager _fireAnimationManager;
    private BoxCollider2D _collider;
    private Animator _animator;

    private void Start()
    {
        _fireHeatBuff = GetComponent<FireHeatBuff>();
        _fireAnimationManager = GetComponent<FireAnimationManager>();
        _fireLevel = GetComponent<FireLevel>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void HeatLossPerSecond()
    {
        
    }

    public void AddFuel(ItemType ItemName)
    {
        
    }
    
    public void AddStone()
    {
        
    }

}
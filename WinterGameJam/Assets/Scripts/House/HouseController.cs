using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private GameObject _roof;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _flue;
    private BoxCollider _boxCollider;

    private void Start()
    {
        
    }
}

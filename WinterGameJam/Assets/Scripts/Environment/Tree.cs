using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour
{
    private Animator _animator;
    private Item _axe;
    private float _chopTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent(out InventoryManager inventoryManager))
        {
            if (inventoryManager.HasItem(_axe) && Input.GetKey(KeyCode.E))
            {
                inventoryManager.StartChopping();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireHeatTransfer : MonoBehaviour
{
    private SphereCollider _sphereCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
        {
            playerStatus.SetNearFire(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerStatus>(out PlayerStatus playerStatus))
        {
            playerStatus.SetNearFire(false);
        }
    }
}

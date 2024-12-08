using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour
{
	private Animator _animator;
	public Item _axe;
	private float _chopTime;
	private readonly string _chopAnimation = "Chopping";
	private readonly string _falled = "Falled";
	[SerializeField] private GameObject _trunk;
	[SerializeField] private GameObject _log;
	private BoxCollider _collider;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider>();
	}

	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.TryGetComponent(out InventoryManager inventoryManager))
		{
			if (inventoryManager.HasItem(_axe) && Input.GetKeyUp(KeyCode.E))
			{
				inventoryManager.StartChopping();
				_animator.SetTrigger(_chopAnimation);
				StartCoroutine(Chop());
			}
		}
	}

	private void FallSoundPlay()
	{
		SoundManager.Instance.PlaySound("ДеревоПадает", transform.position, 2f);
	}

	private void TrunkDissapear()
	{
		_log.transform.SetParent(null);
		_trunk.SetActive(false);
		_log.SetActive(true);
		_log.transform.position = _trunk.transform.position;
		_collider.isTrigger = true;
	}

	private IEnumerator Chop()
	{
		yield return new WaitForSeconds(3f);

		_animator.SetTrigger(_falled);
		yield return null;
	}
}

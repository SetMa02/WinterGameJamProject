using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _baseSpeed = 3f;
	private Rigidbody _rb;
	private PlayerStatus _playerStatus;

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_playerStatus = GetComponent<PlayerStatus>();
	}
	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
		float currentSpeed = _baseSpeed * _playerStatus.GetSpeedMultiplier();
		_rb.velocity = direction * currentSpeed;
	}
}

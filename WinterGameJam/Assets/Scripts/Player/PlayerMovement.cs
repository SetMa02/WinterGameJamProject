using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _baseSpeed = 3f;
	private Rigidbody2D _rb;
	private PlayerStatus _playerStatus;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_playerStatus = GetComponent<PlayerStatus>();
	}

	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector2 direction = new Vector2(horizontal, vertical).normalized;
		float currentSpeed = _baseSpeed * _playerStatus.GetSpeedMultiplier();
		_rb.velocity = direction * currentSpeed;
	}
}

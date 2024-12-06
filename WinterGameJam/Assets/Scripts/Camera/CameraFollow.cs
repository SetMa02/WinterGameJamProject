using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;

	void LateUpdate()
	{
		if (_target == null) return;
		Vector3 desiredPosition = _target.position + _offset;
		Vector3 smoothed = Vector3.Lerp(this.transform.position, desiredPosition, _smoothSpeed);
		this.transform.position = smoothed;
	}
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 0.125f;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _zoomSpeed = 2f;
	[SerializeField] private float _minZoom = 5f;
	[SerializeField] private float _maxZoom = 15f;

	private void LateUpdate()
	{
		if (_target == null) return;

		float scrollInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollInput != 0)
		{
			_offset.y = Mathf.Clamp(_offset.y - scrollInput * _zoomSpeed, _minZoom, _maxZoom);
		}

		Vector3 desiredPosition = _target.position + _offset;
		Vector3 smoothed = Vector3.Lerp(this.transform.position, desiredPosition, _smoothSpeed);
		this.transform.position = smoothed;
	}
}

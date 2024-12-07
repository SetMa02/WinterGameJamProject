using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationDelay = 0.2f;

    private readonly string IsUpward = "IsUpward";
    private readonly string IsInfront = "IsInfront";
    private readonly string IsSideways = "IsSideways";
    private readonly string Speed = "Speed";

    private Rigidbody _rigidbody; 
    private bool _canSwitch = true;
    private float _speed; 

    private void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _speed = _rigidbody.velocity.magnitude;
        _animator.SetFloat(Speed, _speed);

        if (_canSwitch)
        {
            TrackCurrentState();
        }
    }

    private void TrackCurrentState()
    {
        // Обработчик нажатий клавиш для изменения состояний анимации
        if (Input.GetKeyDown(KeyCode.W))
        {
            SwitchAnimation(IsUpward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchAnimation(IsInfront);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            SwitchAnimation(IsSideways);
        }
    }

    private void SwitchAnimation(string state)
    {
        if (!_canSwitch) return;

        _canSwitch = false;
        OffAllStates();
        _animator.SetBool(state, true);
        
        StartCoroutine(EnableSwitchAfterDelay());
    }

    private IEnumerator EnableSwitchAfterDelay()
    {
        yield return new WaitForSeconds(_animationDelay);
        _canSwitch = true;
    }

    private void OffAllStates()
    {
        _animator.SetBool(IsSideways, false);
        _animator.SetBool(IsInfront, false);
        _animator.SetBool(IsUpward, false);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovementHero : MonoBehaviour
{
    private const string AnimationMovement = "Speed";
    private const string AnimationJamp = "Jamp";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Animator _animator;

    private float _horizontalMove = 0f;
    private bool _isGroundet = false;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FindPosition();

        Vector2 targetVelocity = new Vector2(_horizontalMove * _speed, _rigidbody.velocity.y);
        _rigidbody.velocity = targetVelocity;

        if (Input.GetButton("Jump") && _isGroundet)
        {
            _rigidbody.AddForce(transform.up * _jumpPower);
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }

    private void Update()
    {
        float turn = 180f;

        _horizontalMove = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat(AnimationMovement, Mathf.Abs(_horizontalMove));

        if (_isGroundet == false)
        {
            _animator.SetBool(AnimationJamp, true);
        }
        else
        {
            _animator.SetBool(AnimationJamp, false);
        }

        if (_horizontalMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, turn, 0);
        }
        else if (_horizontalMove < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void FindPosition()
    {
        float radius = 0.3f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Ground"));

        _isGroundet = colliders.Length > 0;
    }
}
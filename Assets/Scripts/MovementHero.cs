using UnityEngine;

public class MovementHero : MonoBehaviour
{
    //private const string _movement = "Horizontal";
    //private const string _jamp = "Jamp";
    //private const string _animationMovement = "Speed";
    //private const string _animationJamp = "Jamp";

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

        if (Input.GetButton("Jamp") && _isGroundet)
        {
            _rigidbody.AddForce(transform.up * _jumpPower);

        }
    }

    private void Update()
    {
        float turn = 180f;

        _horizontalMove = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (_isGroundet == false)
        {
            _animator.SetBool("Jamp", true);
        }
        else
        {
            _animator.SetBool("Jamp", false);
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

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class MovementHero : MonoBehaviour
{
    private const string AnimationMovement = "Speed";
    private const string AnimationJump = "Jump";
    private const string MovementByClicking = "Horizontal";
    private const string ClickJump = "Jump";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _playerModel;

    private BoxCollider2D _boxCollider;
    private float _horizontalMove = 0f;
    private bool _isGroundet = false;
    private Rigidbody2D _rigidbody;
    private float _turn = 180f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        FindPosition();

        Vector2 targetVelocity = new Vector2(_horizontalMove * _speed, _rigidbody.velocity.y);
        _rigidbody.velocity = targetVelocity;

        if (Input.GetButton(ClickJump) && _isGroundet)
        {
            _rigidbody.AddForce(transform.up * _jumpPower);
            _rigidbody.velocity = new Vector2(0, 0);
        }
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(MovementByClicking);

        _animator.SetFloat(AnimationMovement, Mathf.Abs(_horizontalMove));

        if (_isGroundet == false)
        {
            _animator.SetBool(AnimationJump, true);
        }
        else
        {
            _animator.SetBool(AnimationJump, false);
        }

        if (_horizontalMove > 0)
        {
            _playerModel.rotation = Quaternion.Euler(0, _turn, 0);
            _boxCollider.transform.rotation = Quaternion.Euler(0, _turn, 0);
        }
        else if (_horizontalMove < 0)
        {
            _playerModel.rotation = Quaternion.Euler(Vector3.zero);
            _boxCollider.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void FindPosition()
    {
        float radius = 0.3f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, _layerMask);

        _isGroundet = colliders.Length > 0;
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Transform _transform;
    [SerializeField] private Enemy _enemy;

    private int _turn = 180;
    private int _speed = 4;
    private Transform _playerPosition;

    private void Update()
    {
        if (_playerPosition != null)
        {
            Run(_playerPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            _playerPosition = hero.transform;

            _enemyMovement.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            _playerPosition = null;

            _enemyMovement.enabled = true;
        }
    }

    private void Run(Transform purpose)
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, _rayLength, _layerMask);

        if (hit.collider != null && _enemy.IsFrozen == false)
        {
            var direction = purpose.position - _transform.position;

            if (direction.x > 0)
            {
                _transform.rotation = Quaternion.Euler(0, _turn, 0);
            }
            else
            {
                _transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            _transform.position = Vector3.MoveTowards(_transform.position, new Vector2(purpose.position.x, _transform.position.y), _speed * Time.deltaTime);
        }
    }
}

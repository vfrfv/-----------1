using UnityEngine;

[RequireComponent (typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _way;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _directionPoint;

    private Enemy _enemy;
    private Transform[] _points;
    private int _currentPoint;
    private float _turn = 180f;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _points = new Transform[_way.childCount];

        for (int i = 0; i < _way.childCount; i++)
        {
            _points[i] = _way.GetChild(i);
        }
    }

    private void Update()
    {      
        if(_enemy.IsFrozen == false)
        {
            Transform target = _points[_currentPoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            var direction = target.position - transform.position;

            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, _turn, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            if (transform.position == target.position)
            {
                _currentPoint++;

                if (_currentPoint == _points.Length)
                {
                    _currentPoint = 0;
                }
            }
        }     
    }
}

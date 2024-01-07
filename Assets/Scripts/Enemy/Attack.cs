using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float _rayLength;
    [SerializeField] private LayerMask _layerMask;

    private int _turn = 180;
    private int _speed = 4;
    private EnemyMovement _enemyMovement;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(Run(hero.transform));

            _enemyMovement.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _enemyMovement.enabled = true;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, Vector2.down * _rayLength);
    //}

    private IEnumerator Run(Transform purpose)
    {
        bool isRun = true;

        while (isRun)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayLength, _layerMask);

            if (hit.collider != null)
            {
                var direction = purpose.position - transform.position;

                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, _turn, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                }

                transform.position = Vector3.MoveTowards(transform.position, new Vector2(purpose.position.x, transform.position.y), _speed * Time.deltaTime);

                yield return null;
            }
            else
            {
                isRun = false;
            }
        }
    }
}

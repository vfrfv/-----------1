using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private int _health = 4;
    private int _damage = 1;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private MovementHero _movementHero;
    private RaisePharmacy _raisePharmacy;
    //private Pharmacy _pharmacy;  

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _movementHero = GetComponent<MovementHero>();
        _raisePharmacy = GetComponent<RaisePharmacy>();
        //_pharmacy = GetComponent<Pharmacy>();
    }

    private void OnEnable()
    {
        _raisePharmacy.Colected += AddLives;
    }

    private void OnDisable()
    {
        _raisePharmacy.Colected -= AddLives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float forceRepulsion = 666;

        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            Vector2 knockbackDirection = (transform.position - enemy.transform.position).normalized;

            _rigidbody.AddForce(knockbackDirection * forceRepulsion);
            enemy.ApplyDamage(_damage);
            StartCoroutine(ChangeColorForDuration());
        }

        StopCoroutine(ChangeColorForDuration());
    }

    private IEnumerator ChangeColorForDuration()
    {
        float repaintingTime = 0.20f;
        Color defaultColor = _spriteRenderer.color;

        _movementHero.enabled = false;
        _spriteRenderer.material.color = Color.red;

        yield return new WaitForSeconds(repaintingTime);

        _spriteRenderer.material.color = defaultColor;
        _movementHero.enabled = true;
    }

    private void AddLives()
    {
        int maximumNumberLives = 4;

        if (_health < maximumNumberLives)
        {
            _health++;
        }
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

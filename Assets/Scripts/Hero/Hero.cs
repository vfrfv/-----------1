using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hero : MonoBehaviour, IHelth
{
    private int _damage = 1;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private MovementHero _movementHero;
    private RaisePharmacy _raisePharmacy;

    public int CurrentNumberLives { get; private set; } = 6;
    public int MaximumNumberLives { get; private set; } = 6;

    public event Action<float> Changed;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _movementHero = GetComponent<MovementHero>();
        _raisePharmacy = GetComponent<RaisePharmacy>();
    }

    private void Start()
    {
        Changed?.Invoke(CurrentNumberLives);
    }

    private void OnEnable()
    {
        _raisePharmacy.Colected += Treated;
    }

    private void OnDisable()
    {
        _raisePharmacy.Colected -= Treated;
    }

    public void AddLife(int life)
    {
        if (CurrentNumberLives < MaximumNumberLives)
        {
            CurrentNumberLives += life;
            Changed?.Invoke(CurrentNumberLives);
        }
    }

    public void ApplyDamage(int damage)
    {
        CurrentNumberLives -= damage;
        Changed?.Invoke(CurrentNumberLives);

        if (CurrentNumberLives <= 0)
            Die();
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

    private void Treated()
    {
        if (CurrentNumberLives < MaximumNumberLives)
        {
            CurrentNumberLives++;
            Changed?.Invoke(CurrentNumberLives);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

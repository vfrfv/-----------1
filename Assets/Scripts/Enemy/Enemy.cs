using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHelth
{
    public event Action<float> Changed;
    private int _damage = 1;

    public int Health { get; private set; } = 6;

    private void Start()
    {
        Changed?.Invoke(Health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Hero hero))
        {
            hero.ApplyDamage(_damage);
        }
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        Changed?.Invoke(Health);

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

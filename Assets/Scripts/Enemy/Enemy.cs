using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHelth
{
    private int _damage = 1;

    public int Value { get; private set; } = 15;
    public bool IsFrozen { get; private set; } = false;

    public event Action<float> Changed;

    private void Start()
    {
        Changed?.Invoke(Value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Hero hero))
        {
            hero.ApplyDamage(_damage);
        }
    }

    public void Freeze()
    {
        IsFrozen = true;
    }

    public void Unfreeze()
    {
        IsFrozen = false;
    }

    public void ApplyDamage(int damage)
    {
        Value -= damage;
        Changed?.Invoke(Value);

        if (Value <= 0)
            Die();
    }

    public int GiveLife()
    {
        int lefe = 1;

        if (Value > 0)
        {
            Value -= lefe;
            Changed?.Invoke(Value);
        }

        return lefe;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

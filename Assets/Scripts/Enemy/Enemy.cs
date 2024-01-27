using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHelth
{
    private int _damage = 1;
    private StateMashine _state;

    public int CurrentNumberLives { get; private set; } = 15;
    public bool IsFrozen { get; private set; } = false;

    public event Action<float> Changed;

    private void Start()
    {
        _state = new StateMashine();
        _state.Initialize(new Walking());
        Changed?.Invoke(CurrentNumberLives);
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
        CurrentNumberLives -= damage;
        Changed?.Invoke(CurrentNumberLives);

        if (CurrentNumberLives <= 0)
            Die();
    }

    public int GiveLife()
    {
        int lefe = 1;

        if (CurrentNumberLives > 0)
        {
            CurrentNumberLives -= lefe;
            Changed?.Invoke(CurrentNumberLives);
        }

        return lefe;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

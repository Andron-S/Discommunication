using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IAttackable
{
    [SerializeField] protected float CurrentHealth;

    protected float Health;

    public abstract void TakeDamage(float damage);
    public abstract void Die();
    public abstract void Attack();
    public abstract IEnumerator CalculatingAttackDelay();
}
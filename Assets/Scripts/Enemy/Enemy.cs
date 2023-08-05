using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IAttackable
{
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float AttackDelay;
    [SerializeField] protected float Health;
    [SerializeField] protected float Armor;
    [SerializeField] protected float Speed;

    protected bool _isAttackCooldowned;

    public static event Action<float> OnHealthPointsChanged;

    protected void Start()
    {
        _isAttackCooldowned = true;
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();
    public abstract void Attack();
    public abstract IEnumerator CalculatingAttackDelay();
}
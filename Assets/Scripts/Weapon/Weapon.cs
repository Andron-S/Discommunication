using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IAttackable
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _durable;

    public abstract void Attack();
    public abstract IEnumerator CalculatingAttackDelay();
}

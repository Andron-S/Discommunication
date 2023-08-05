using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IAttackable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _durable;
    [SerializeField] public abstract void Attack();
    public abstract IEnumerator CalculatingAttackDelay();
}

using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private float _healthPoints;

    public static event Action<float> OnHealthPointsChanged;

    private PlayerMovement _playerMovement;
    private Weapon _weapon;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void EatAbility()
    {
        // написать логику
    }

    public void Attack()
    {
        _weapon.Attack();
    }

    public IEnumerator CalculatingAttackDelay()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {


        OnHealthPointsChanged?.Invoke(_healthPoints);
    }

    public void Die()
    {
        throw new NotImplementedException();
    }

    private void GetWeapon()
    {

    }
}
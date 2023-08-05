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
    //Weapon

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void GetDamage(float damage)
    {

        
        OnHealthPointsChanged?.Invoke(_healthPoints);
    }

    //public int Attack()
    //{
        
    //}

    public void EatAbility()
    {

    }

    private void GetWeapon()
    {

    }

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public IEnumerator CalculatingAttackDelay()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}
using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private float _healthPoints;
    [SerializeField] private float _attackDelay;

    private bool _isAttackCooldowned;

    public static event Action<float> OnHealthPointsChanged;

    private void Start()
    {
        _healthPoints = 50;
        _attackDelay = 2;

        _isAttackCooldowned = true;
    }

    public void EatAbility()
    {
        if(_isAttackCooldowned == false)
        {
            return;
        }

        Debug.Log("Активация способности кусать");

        _isAttackCooldowned = false;
        var attackDelayCorutine = StartCoroutine(CalculatingAttackDelay());
    }

    public void Attack()
    {
        Debug.Log("Атака оружием");
        // вызвать метод атаки у класса Оружия
    }

    public IEnumerator CalculatingAttackDelay()
    { 
        yield return new WaitForSeconds(_attackDelay);

        _isAttackCooldowned = true;
    }

    public void TakeDamage(float damage)
    {

        OnHealthPointsChanged?.Invoke(_healthPoints);

        if(_healthPoints <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        GameEventManager.ReloadCurrentScene();
    }

    private void GetWeapon()
    {

    }
}
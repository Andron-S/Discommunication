using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private float _healthPoints;

    public static event Action<float> OnHealthPointsChanged;

    private void Start()
    {
        
    }

    public void EatAbility()
    {
        Debug.Log("��������� ����������� ������");
    }

    public void Attack()
    {
        Debug.Log("����� �������");
        // ������� ����� ����� � ������ ������
    }

    public IEnumerator CalculatingAttackDelay()
    {
        throw new NotImplementedException();
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
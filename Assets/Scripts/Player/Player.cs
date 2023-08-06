using Assets;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private float _healthPoints;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Image _healthBar;

    private bool _isAttackCooldowned;
    private float _maxHealth = 100;

    public static event Action<float> OnHealthPointsChanged;

    private void Start()
    {
        _healthPoints = 50;
        _attackDelay = 2;

        _isAttackCooldowned = true;
    }

    public void EatAbility()
    {
        if (_isAttackCooldowned == false)
        {
            return;
        }

        Debug.Log("Player is attacking with ability");

        _isAttackCooldowned = false;
        var attackDelayCorutine = StartCoroutine(CalculatingAttackDelay());
    }

    public void AttackWeapon()
    {
        Debug.Log("Player is attacking with weapon");

        if (_weapon == null)
        {
            Debug.Log("Player has't Weapon");
            return;
        }

        _weapon.Attack();
    }

    public void AttackMelee()
    {
        // Maybe Player need private personal Weapon with only Melee attack?

        Debug.Log("Player is attacking Melee");

    }

    public IEnumerator CalculatingAttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        _isAttackCooldowned = true;
    }

    public void TakeDamage(float damage)
    {

        OnHealthPointsChanged?.Invoke(_healthPoints);
        _healthBar.fillAmount = _healthPoints / _maxHealth;
        if (_healthPoints <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        //TODO: вызвать меню
        GameEventManager.ReloadCurrentScene();
    }

    private void GetWeapon(Weapon weapon)
    {
        // Get Weapon from died Enemy
    }
}
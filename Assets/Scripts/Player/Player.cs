using Assets;

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IAttackable, IDamageable
{
    [SerializeField] private float _healthPoints;
    [SerializeField] private float _reducingHealthPointsDelay;
    [SerializeField] private float _reducingHealthPointsDamage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Image _healthBar;
    [SerializeField] private EatAbility _eatAbility;

    private bool _isAttackCooldowned;
    private float _maxHealth = 100;
    private Coroutine reducingHealthPointsCorutine;
    private WeaponHand _weaponHand; 

    public static event Action<float> OnHealthPointsChanged;

    private void Start()
    {
        _healthPoints = 50;
        _attackDelay = 2;

        _isAttackCooldowned = true;

        _reducingHealthPointsDelay = 1;
        _reducingHealthPointsDamage = 1;

        reducingHealthPointsCorutine = StartCoroutine(ReducingHealthPoints());

        _weaponHand = GetComponentInChildren<WeaponHand>();

        CreateWeapon();
    }

    public void EatAbility()
    {
        _healthPoints += _eatAbility.TryEat();
    }

    public void AttackWeapon()
    {
        if (_weapon == null)
        {
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
        _healthPoints -= damage;

        OnHealthPointsChanged?.Invoke(_healthPoints);

        // Need to link healthBar with Scene
        //_healthBar.fillAmount = _healthPoints / _maxHealth;

        if (_healthPoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //TODO: вызвать меню
        //TODO: call death animation/effect
        Destroy(gameObject);
        GameEventManager.ReloadCurrentScene();
    }

    public void DropWeapon()
    {
        if (_weapon == null)
        {
            return;
        }

        GameObject weapon = GetComponentInChildren<Weapon>().gameObject;
        Destroy(weapon);
    }

    public void GetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.transform.position = _weaponHand.transform.position;
        _weapon.transform.SetParent(_weaponHand.transform);
        // Get Weapon from died Enemy
    }

    private IEnumerator ReducingHealthPoints()
    {
        while (_healthPoints > 0)
        {
            _healthPoints -= _reducingHealthPointsDamage;
            yield return new WaitForSeconds(_reducingHealthPointsDelay);
        }

        Die();
    }

    private void CreateWeapon()
    {
        Weapon weapon = Instantiate(_weapon, _weaponHand.transform.position, Quaternion.identity, gameObject.transform);
       // weapon.transform.right = _weaponHand.transform.up;
        _weapon = weapon;
    }
}
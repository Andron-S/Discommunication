using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon: MonoBehaviour
{
    [SerializeField] private float _meleeDamage = 40;
    [SerializeField] private float _durable = 5;
    //[SerializeField] private float _beginRotation;
    [SerializeField] private float _meleeAttackRadius = 5; // Если делать ближний удар по радиусу, то в 2-3 раза больше размера оружия
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private bool _isMeleeMode = false;

    private BulletBeginer _bulletBeginer;

    public void Start()
    {
        _bulletBeginer = GetComponentInChildren<BulletBeginer>();
    }

    public void Attack()
    {
        if (_durable > 0)
        {
            if (_isMeleeMode)
            {
                MeleeAttack();
            }
            else
            {
                DistantAttack();
            }

            _durable--;
        }

        if (_durable == 0)
        {
            Destroy(gameObject);
        }
    }

    private bool TryGetMeleeAttackedTargets(out List<IDamageable> targets)
    {
        // Логика ближнего боя

        targets = null;
        return false;
    }

    private void MeleeAttack()
    {
        if (TryGetMeleeAttackedTargets(out List<IDamageable> targets))
        {
            foreach (IDamageable liver in targets)
            {
                liver.TakeDamage(_meleeDamage);
            }
        }
    }

    private void DistantAttack()
    {
        Bullet createdBullet = Instantiate(_bulletTemplate, _bulletBeginer.transform.position, _bulletBeginer.transform.rotation);
        //createdBullet.FlyLive();
    }
}

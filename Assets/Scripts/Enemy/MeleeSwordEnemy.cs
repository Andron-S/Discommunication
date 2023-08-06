using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwordEnemy : Enemy
{
    [SerializeField] private Weapon _weapon;

    protected void Start()
    {
        base.Start();
    }

    public override void AttackWeapon()
    {
        // weapon duration of enemy has a limit? 
        // Because now weapon has _durable = 5 and will destroy
        if (_weapon == null)
        {
            return;
        }

        _weapon.Attack();
    }

    public override IEnumerator CalculatingAttackDelay()
    {
        yield return new WaitForSeconds(AttackDelay);

        _isAttackCooldowned = true;
    }

    public override void Die()
    {
        // Add animation of death

        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        if(Armor > 0)
        {
            Armor -= damage;
        }
        else
        {
            Health -= damage;
        }

        Debug.Log("Damage is gotten by " + gameObject.name);

        if(Health <=0)
        {
            Die();
        }
    }
}

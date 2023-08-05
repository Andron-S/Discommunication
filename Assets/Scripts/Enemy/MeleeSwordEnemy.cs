using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwordEnemy : Enemy
{
    protected void Start()
    {
        base.Start();

        Health = 100;
        Armor = 50;
        Speed = 5;
    }

    public override void Attack()
    {
        // ����� ������
    }

    public override IEnumerator CalculatingAttackDelay()
    {
        yield return new WaitForSeconds(AttackDelay);

        _isAttackCooldowned = true;
    }

    public override void Die()
    {
        // ����� �������� ������

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

        if(Health <=0)
        {
            Die();
        }
    }
}

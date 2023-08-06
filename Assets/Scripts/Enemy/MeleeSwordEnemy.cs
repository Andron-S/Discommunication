using System.Collections;

using UnityEngine;

public class MeleeSwordEnemy : Enemy
{
    protected void Start()
    {
        base.Start();
    }

    public override void AttackWeapon()
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

        Debug.Log("Damage is gotten by " + gameObject.name);

        if(Health <=0)
        {
            Die();
        }
    }
}

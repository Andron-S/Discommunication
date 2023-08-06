using System.Collections;

using UnityEngine;

public class MeleeSwordEnemy : Enemy
{
    private Weapon _weapon;

    private bool _isOnCooldown = false;

    protected void Start()
    {
        base.Start();

        _weapon = GetComponentInChildren<Weapon>();
       // _weapon.SetDurable(1000);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Attack");

        if (collision.collider.TryGetComponent(out Player player))
        {
            TryAttack();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (_isOnCooldown)
            return;

        _weapon.Attack();
        StartCoroutine(Cooldown());
    } 

    IEnumerator Cooldown()
    {
        _isOnCooldown = true;

        yield return new WaitForSeconds(AttackDelay);
        _isOnCooldown = false;
    }

}

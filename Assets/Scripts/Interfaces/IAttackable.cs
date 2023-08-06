using System.Collections;

public interface IAttackable 
{
    public void AttackWeapon();
    public IEnumerator CalculatingAttackDelay();
}

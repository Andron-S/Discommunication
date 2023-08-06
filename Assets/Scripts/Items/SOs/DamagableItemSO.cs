using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO/Items", menuName = "SO/Create new DamagableItemSO")]
public class DamagableItemSO : EffectingItemSO<Enemy>
{
    [SerializeField] private float _damage;

    protected override void _AddEffect(Enemy component)
    {
        component.TakeDamage(_damage);
    }

    protected override void _RemoveEffect(Enemy component)
    {

    }
}

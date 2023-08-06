using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO/Items", menuName = "SO/Create new DamagablePlayerItemSO")]
public class DamagablePlayerItemSO : EffectingItemSO<Player>
{
    [SerializeField] private float _damage;

    protected override void _AddEffect(Player component)
    {
        component.TakeDamage(_damage);
    }

    protected override void _RemoveEffect(Player component)
    {

    }
}

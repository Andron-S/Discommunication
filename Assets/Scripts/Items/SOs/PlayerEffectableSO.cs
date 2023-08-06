using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO/Items", menuName = "SO/Create new PlayerAffectableItem")]
public class PlayerEffectableSO : EffectingItemSO<Player>
{
    [SerializeField] private int hpChange = 1;

    protected override void _AddEffect(Player player)
    {
        Debug.Log($"{this}>>>effecting {player}");
        //player.TakeDamage(-hpChange);
        player.GetComponent<HPBarSimple>().UpdateHP(hpChange);
        ///
        ///
        ///ну и прочее
        ///
        ///

    }

    protected override void _RemoveEffect(Player player)
    {
        Debug.Log($"{this}>>>remove effect from {player}");
        //player.TakeDamage(hpChange);
        player.GetComponent<HPBarSimple>().UpdateHP(-hpChange);
        ///
        ///
        ///ну и прочее
        ///
        ///

    }
}

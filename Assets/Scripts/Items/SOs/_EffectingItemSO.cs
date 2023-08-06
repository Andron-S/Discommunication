using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectingItemSO<EffectingComponent> : ItemSO where EffectingComponent : MonoBehaviour
{
    public override void OnCollected(IItemCollector collector)
    {
        Debug.Log($"{this}>>>Was collected by {collector}");
        EffectingComponent effectingComponent;
        if (collector.GetCollectorObject().TryGetComponent(out effectingComponent))
        {
            _AddEffect(effectingComponent);
        }
    }

    public override void OnDropped(IItemCollector collector)
    {
        Debug.Log($"{this}>>>Was dropped by {collector}");
        EffectingComponent effectingComponent;
        if (collector.GetCollectorObject().TryGetComponent(out effectingComponent))
        {
            if (ItemType == ItemType.Collectable)
            {
                _RemoveEffect(effectingComponent);
            }
        }
    }


    protected abstract void _AddEffect(EffectingComponent component);
    protected abstract void _RemoveEffect(EffectingComponent component);
}

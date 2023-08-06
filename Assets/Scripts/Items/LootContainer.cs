using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [SerializeField] private List<LootData> _lootData;

    public void DropAllItems()
    {
        var dropList = new List<ItemSO>();
        foreach(var lootData in _lootData)
        {
            var newDrop = lootData.GetLoot();
            if(newDrop != null)
                dropList.Add(newDrop);
        }

        foreach(var drop in dropList)
        {
            var item = drop.SpawnItemObjectAt(transform.position);
            item.OnDropped();
        }
    }
}


[System.Serializable]
public class LootData{
    [SerializeField] private ItemSO _item = null;
    [SerializeField] private ItemSO _itemInstead = null;
    [SerializeField] [Range(0.0f, 1f)] private float _chance = 1f;

    public ItemSO GetLoot()
    {
        var luck = Random.Range(0f, 1f);
        if(luck <= _chance)
        {
            return _item;
        }
        else
        {
            return _itemInstead;
        }
    }
}

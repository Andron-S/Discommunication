using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemContainer : ItemContainer, IItemCollector
{

    public void DropFirstItem()
    {
        var itemSO = _itemInventory.RemoveItem(0);
        if (itemSO != null)
        {
            _DropItem(itemSO);
        }
    }

    public GameObject GetCollectorObject()
    {
        return gameObject;
    }

    public bool TryCollectItem(ItemSO itemSO)
    {
        if (_itemInventory.TryAddItem(itemSO))
        {
            itemSO.OnCollected(this);
            return true;
        }

        return false;
    }

    protected override void _OnInventotyChanged()
    {
        Debug.Log("Inventory Changed");
        //
        //
        // добавлем что хотим
        //
        //
    }

    private void _DropItem(ItemSO droppedItemSO)
    {
        var item = droppedItemSO.SpawnItemObjectAt(transform.position);
        droppedItemSO.OnDropped(this);
        item.OnDropped();
    }
}

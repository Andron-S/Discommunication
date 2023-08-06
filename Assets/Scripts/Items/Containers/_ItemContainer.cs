using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour
{
    
    [SerializeField] protected Inventory _itemInventory;


    private void OnEnable()
    {
        _itemInventory.AddOnInventoryChangedListener(_OnInventotyChanged);
    }


    private void OnDisable()
    {
        _itemInventory.RemoveOnInventoryChangedListener(_OnInventotyChanged);
    }

    protected void _DropItem(ItemSO itemSO)
    {
        var droppingItemSO = _itemInventory.RemoveItem(itemSO);
        if(droppingItemSO != null)
        {
            var droppedItem = droppingItemSO.SpawnItemObjectAt(transform.position);
            droppedItem.transform.position = transform.position;
            droppedItem.OnDropped();
        }
    }

    protected abstract void _OnInventotyChanged();
}


[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemSO> _itemSOs;
    [SerializeField] private int _maxInventorySize = -1;

    private event Action _onInventoryChanged = null;


    public void AddOnInventoryChangedListener(Action listeningAction)
    {
        _onInventoryChanged += listeningAction;
    }


    public void RemoveOnInventoryChangedListener(Action listeningAction)
    {
        _onInventoryChanged -= listeningAction;
    }


    public bool TryAddItem(ItemSO itemSO)
    {
        if(itemSO.ItemType == ItemType.Consuming)
        {
            return true;
        }
        if(_maxInventorySize < 0 || _itemSOs.Count < _maxInventorySize)
        {
            _itemSOs.Add(itemSO);
            _onInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }

    public ItemSO RemoveItem(int index)
    {
        if(index < _itemSOs.Count)
        {
            var removingItem = _itemSOs[0];
            _itemSOs.Remove(removingItem);
            _onInventoryChanged?.Invoke();
            return removingItem;
        }
        return null;
    }

    public ItemSO RemoveItem(ItemSO itemSO)
    {
        var matchingItems = _itemSOs.FindAll(x => x.ItemID == itemSO.ItemID);
        if (matchingItems.Count > 0)
        {
            var droppingItemData = matchingItems[0];
            _itemSOs.Remove(droppingItemData);
            _onInventoryChanged?.Invoke();
            return droppingItemData;
        }
        return null;
    }
}
